namespace SpaceInvaders;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public partial class Form1 : Form
{
    private Timer gameTimer = new Timer();
    private int fps = 60;

    private Player player = new Player(100, 500);
    private Controller controller = new Controller();

    private BulletManager bulletManager;
    private EnemyManager enemyManager;

    private BlockOfProtection block = new BlockOfProtection(300, 450);

    public Form1()
    {
        InitializeComponent();

        this.DoubleBuffered = true;
        this.Width = 800;
        this.Height = 600;
        this.Text = "Space Invaders";
        this.BackColor = Color.Black;

        bulletManager = new BulletManager(this.ClientSize.Height);
        enemyManager = new EnemyManager(this.ClientSize.Width, bulletManager);

        gameTimer.Interval = 1000 / fps;
        gameTimer.Tick += GameTimer_Tick;
        gameTimer.Start();

        this.Paint += Form1_Paint;
    }

    private void GameTimer_Tick(object sender, EventArgs e)
    {
        if (controller.MoveLeft)
            player.MoveLeft();
        if (controller.MoveRight)
            player.MoveRight(this.ClientSize.Width);

        if (controller.ShootRequested)
        {
            Shoot();
            controller.ResetShoot();
        }

        enemyManager.Update();
        bulletManager.Update();

        CheckCollisions();

        this.Invalidate();
    }

    private void Shoot()
    {
        if (!bulletManager.GetBullets().Any(b => b.IsPlayerBullet))
        {
            Point center = player.GetCenter();
            bulletManager.AddBullet(center.X, center.Y - 10, true, true);
        }
    }

    private void CheckCollisions()
    {
        var bullets = bulletManager.GetBullets();
        var enemies = enemyManager.GetEnemies();

        for (int i = bullets.Count - 1; i >= 0; i--)
        {
            var bullet = bullets[i];
            var bulletRect = bullet.GetRect();

            if (bullet.IsPlayerBullet)
            {
                for (int j = enemies.Count - 1; j >= 0; j--)
                {
                    if (enemies[j].GetRect().IntersectsWith(bulletRect))
                    {
                        enemyManager.RemoveEnemy(enemies[j]);
                        bulletManager.Remove(bullet);
                        break;
                    }
                }
            }
            else
            {
                if (!block.IsDestroyed && bulletRect.IntersectsWith(block.GetRect()))
                {
                    block.TakeDamage();
                    bulletManager.Remove(bullet);
                }
                
            } 
            
        }
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        controller.KeyDown(keyData);
        return base.ProcessCmdKey(ref msg, keyData);
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
        controller.KeyUp(e.KeyCode);
        base.OnKeyUp(e);
    }

    private void Form1_Paint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;

        // Player
        g.FillRectangle(Brushes.Blue, player.GetRect());

        // Inimigos
        foreach (var enemy in enemyManager.GetEnemies())
        {
            g.FillRectangle(Brushes.Red, enemy.GetRect());
        }

        // Tiros
        foreach (var bullet in bulletManager.GetBullets())
        {
            g.FillRectangle(Brushes.Yellow, bullet.GetRect());
        }

        // Escudo
        block.Draw(g);

        // HUD
        g.DrawString("←/→ Mover | Espaço: Atirar", this.Font, Brushes.White, new PointF(10, 10));
        g.DrawString($"Inimigos: {enemyManager.GetEnemies().Count}", this.Font, Brushes.White, new PointF(10, 30));
        g.DrawString($"Tiros: {bulletManager.GetBullets().Count}", this.Font, Brushes.White, new PointF(10, 50));
    }
}

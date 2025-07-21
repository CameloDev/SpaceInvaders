namespace SpaceInvaders;
using System.Windows.Forms;
public partial class Form1 : Form
{
    private Timer gameTimer = new Timer();
    private int fps = 60;
    private Player player = new Player { X = 100, Y = 500 };
    private Controller controller = new Controller();

    private EnemyManager enemyManager;
    private BulletManager bulletManager;

    public Form1()
    {
        InitializeComponent();

        this.DoubleBuffered = true;
        this.Width = 800;
        this.Height = 600;
        this.Text = "Space Invaders";

        enemyManager = new EnemyManager(this.ClientSize.Width);
        enemyManager.InitializeEnemies();

        bulletManager = new BulletManager(this.ClientSize.Height);

        gameTimer.Interval = 1000 / fps;
        gameTimer.Tick += GameTimer_Tick;
        gameTimer.Start();

        this.Paint += Form1_Paint;
    }

    private void GameTimer_Tick(object sender, EventArgs e)
    {
        if (controller.MoveLeft && player.X > 0)
            player.X -= player.Speed;
        if (controller.MoveRight && player.X < this.ClientSize.Width - player.Width)
            player.X += player.Speed;

        if (controller.ShootRequested)
        {
            Shoot();
            Thread.Sleep(100);
            controller.ResetShoot();
        }

        if (controller.MoveLeft) player.MoveLeft();
        if (controller.MoveRight) player.MoveRight();
        enemyManager.Update();
        bulletManager.Update();
        CheckCollisions();

        this.Invalidate();
    }

    private void Shoot()
    {
        bulletManager.AddBullet(new Bullet
        {
            X = player.X + player.Width / 2 - 2,
            Y = player.Y - 10,
            IsPlayerBullet = true
        });
    }

    private void CheckCollisions()
    {
        var bullets = bulletManager.GetBullets();
        var enemies = enemyManager.GetEnemies();

        for (int i = bullets.Count - 1; i >= 0; i--)
        {
            var bullet = bullets[i];
            if (!bullet.IsPlayerBullet)
                continue;

            Rectangle bulletRect = bullet.GetRect();

            for (int j = enemies.Count - 1; j >= 0; j--)
            {
                var enemy = enemies[j];

                if (bulletRect.IntersectsWith(enemy.GetRect()))
                {
                    enemyManager.RemoveEnemy(enemy);
                    bulletManager.RemoveBullet(bullet);
                    break;
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

        g.FillRectangle(Brushes.Blue, player.GetRect());

        foreach (var enemy in enemyManager.GetEnemies())
        {
            g.FillRectangle(Brushes.Red, enemy.GetRect());
        }

        foreach (var bullet in bulletManager.GetBullets())
        {
            g.FillRectangle(Brushes.Yellow, bullet.GetRect());
        }

        g.DrawString("Use Left/Right arrows to move, Space to shoot", 
                     this.Font, Brushes.White, new PointF(10, 10));
    }
}


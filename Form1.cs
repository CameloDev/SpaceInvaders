namespace SpaceInvaders;
using System.Windows.Forms;
public partial class Form1 : Form
{
    private Timer gameTimer = new Timer();
    private int fps = 60;
    private Player player = new Player { X = 100, Y = 500 };
    private List<Enemy> enemies = new List<Enemy>();
    private List<Bullet> bullets = new List<Bullet>();
    private Controller controller = new Controller();
    
    private int enemyDirection = 1; // 1 = direita, -1 = esquerda
    private int enemySpeed = 5; // pixels por frame
    private int enemyMoveDownAmount = 20;
    private int enemyMoveCounter = 0; // contador para controlar movimento mais lento
    private int enemyMoveInterval = 10; // mover a cada 10 ticks
    public Form1()
    {
        InitializeComponent();
        InitializeEnemies();
        this.DoubleBuffered = true;
        this.Width = 800;
        this.Height = 600;
        this.Text = "Space Invaders";


        gameTimer.Tick += GameTimer_Tick;
        gameTimer.Interval = 1000 / fps;
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
            controller.ResetShoot();
        }

        UpdateEnemies();
        UpdateBullets();
        CheckCollisions();

        this.Invalidate();
    }
    private void Shoot()
    {
        bullets.Add(new Bullet
        {
            X = player.X + player.Width / 2 - 2,
            Y = player.Y - 10,
            IsPlayerBullet = true
        });
    }
    private void InitializeEnemies()
    {
        int rows = 3;
        int cols = 8;
        int padding = 10;
        int startX = 50;
        int startY = 50;

        enemies.Clear();

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                enemies.Add(new Enemy
                {
                    X = startX + c * (40 + padding),
                    Y = startY + r * (30 + padding)
                });
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
    private void UpdateEnemies()
    {
        enemyMoveCounter++;

        if (enemyMoveCounter >= enemyMoveInterval)
        {
            bool hitEdge = false;

            // Move todos os inimigos na direção atual
            foreach (var enemy in enemies)
            {
                enemy.Move(enemyDirection * enemySpeed, 0);
            }

            // Verifica se algum inimigo bateu na borda da tela
            if (enemies.Any(e => e.X <= 0) || enemies.Any(e => e.X + e.Width >= this.ClientSize.Width))
            {
                hitEdge = true;
            }

            if (hitEdge)
            {
                // Muda direção e desce os inimigos
                enemyDirection *= -1;
                foreach (var enemy in enemies)
                {
                    enemy.Y += enemyMoveDownAmount;
                }
            }

            enemyMoveCounter = 0;
        }
    }
    private void UpdateBullets()
    {
        for (int i = bullets.Count - 1; i >= 0; i--)
        {
            var bullet = bullets[i];
            bullet.Y += bullet.IsPlayerBullet ? -bullet.Speed : bullet.Speed;

            // Remove bala fora da tela
            if (bullet.Y < 0 || bullet.Y > this.ClientSize.Height)
            {
                bullets.RemoveAt(i);
            }
        }
    }
    private void CheckCollisions()
    {
        for (int i = bullets.Count - 1; i >= 0; i--)
        {
            var bullet = bullets[i];

            if (!bullet.IsPlayerBullet)
                continue; // Ignora balas inimigas (se existirem)

            Rectangle bulletRect = bullet.GetRect();

            for (int j = enemies.Count - 1; j >= 0; j--)
            {
                var enemy = enemies[j];

                if (bulletRect.IntersectsWith(enemy.GetRect()))
                {
                    // Remove inimigo e bala
                    enemies.RemoveAt(j);
                    bullets.RemoveAt(i);
                    break;
                }
            }
        }
    }

    private void Form1_Paint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;


        g.FillRectangle(Brushes.Blue, player.GetRect());

        foreach (var enemy in enemies)
        {
            g.FillRectangle(Brushes.Red, enemy.GetRect());
        }

        foreach (var bullet in bullets)
        {
            g.FillRectangle(Brushes.Yellow, bullet.GetRect());
        }
    }

}

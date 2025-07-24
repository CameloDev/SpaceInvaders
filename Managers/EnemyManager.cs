namespace SpaceInvaders
{
public class EnemyManager
    {
        private List<Enemy> enemies;
        private int direction = 1; // 1 = direita, -1 = esquerda
        private int moveDownStep = 10;
        private int screenWidth;
        private BulletManager bulletManager;
        private int shootCooldown = 0;

        public EnemyManager(int screenWidth, BulletManager bulletManager)
        {
            this.screenWidth = screenWidth;
            this.bulletManager = bulletManager;
            enemies = new List<Enemy>();
            InitializeEnemies();
        }

        private void InitializeEnemies()
        {
            int startX = 100;
            int startY = 50;
            int spacingX = 40;
            int spacingY = 40;

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    int x = startX + col * spacingX;
                    int y = startY + row * spacingY;
                    enemies.Add(new Enemy(x, y));
                }
            }
        }

        public void Update()
        {
            MoveEnemies();
            TryShoot();
        }

        private void MoveEnemies()
        {
            bool shouldMoveDown = false;

            foreach (var enemy in enemies)
            {
                enemy.Move(direction);
                if (enemy.ReachedScreenEdge(screenWidth))
                {
                    shouldMoveDown = true;
                }
            }

            if (shouldMoveDown)
            {
                direction *= -1;
                foreach (var enemy in enemies)
                {
                    enemy.MoveDown(moveDownStep);
                }
            }
        }

        private void TryShoot()
        {
            if (shootCooldown > 0)
            {
                shootCooldown--;
                return;
            }

            // Inimigo aleatório atira
            if (enemies.Count > 0)
            {
                var random = new System.Random();
                int index = random.Next(enemies.Count);
                Enemy shooter = enemies[index];
                bulletManager.AddBullet(shooter.X + 15, shooter.Y + 20, false, false);
                shootCooldown = 60; // tempo entre tiros
            }
        }

        public void Draw(Graphics g)
        {
            foreach (var enemy in enemies)
            {
                enemy.Draw(g);
            }
        }

        public List<Enemy> GetEnemies()
        {
            return enemies;
        }

        public void RemoveEnemy(Enemy enemy)
        {
            enemies.Remove(enemy);
        }
    }
}
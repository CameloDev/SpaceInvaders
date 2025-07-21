namespace SpaceInvaders
{
    public class EnemyManager
    {
        private List<Enemy> enemies = new List<Enemy>();
        private int enemyDirection = 1;
        private int enemySpeed = 5;
        private int enemyMoveDownAmount = 20;
        private int enemyMoveCounter = 0;
        private int enemyMoveInterval = 10;
        private int gameWidth;

        public EnemyManager(int gameWidth)
        {
            this.gameWidth = gameWidth;
        }

        public void InitializeEnemies()
        {
            enemies.Clear();
            int rows = 3;
            int cols = 8;
            int padding = 10;
            int startX = 50;
            int startY = 50;

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

        public void Update()
        {
            enemyMoveCounter++;

            if (enemyMoveCounter >= enemyMoveInterval)
            {
                bool hitEdge = false;

                foreach (var enemy in enemies)
                {
                    enemy.Move(enemyDirection * enemySpeed, 0);
                }

                if (enemies.Any(e => e.X <= 0) || enemies.Any(e => e.X + e.Width >= gameWidth))
                {
                    hitEdge = true;
                }

                if (hitEdge)
                {
                    enemyDirection *= -1;
                    foreach (var enemy in enemies)
                    {
                        enemy.Move(0, enemyMoveDownAmount);
                    }
                }

                enemyMoveCounter = 0;
            }
        }

        public void RemoveEnemy(Enemy enemy)
        {
            enemies.Remove(enemy);
        }

        public List<Enemy> GetEnemies()
        {
            return enemies;
        }
    }
}
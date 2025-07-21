namespace SpaceInvaders
{
    public class BulletManager
    {
        private List<Bullet> bullets = new List<Bullet>();
        private int gameHeight;

        public BulletManager(int gameHeight)
        {
            this.gameHeight = gameHeight;
        }

        public void AddBullet(Bullet bullet)
        {
            bullets.Add(bullet);
        }

        public void Update()
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                bullets[i].Move();

                if (bullets[i].Y < 0 || bullets[i].Y > gameHeight)
                {
                    bullets.RemoveAt(i);
                }
            }
        }

        public List<Bullet> GetBullets()
        {
            return bullets;
        }

        public void RemoveBullet(Bullet bullet)
        {
            bullets.Remove(bullet);
        }
    }
}
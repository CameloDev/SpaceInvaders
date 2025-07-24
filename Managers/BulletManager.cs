namespace SpaceInvaders
{
    public class BulletManager
    {
        private List<Bullet> bullets = new List<Bullet>();
        private int bulletSpeed = 10;
         private int screenHeight;
        public BulletManager(int screenHeight)
        {
            this.screenHeight = screenHeight;
        }
        public void AddBullet(int x, int y, bool goingUp, bool isPlayerBullet)
        {
            bullets.Add(new Bullet(x, y, bulletSpeed, goingUp, isPlayerBullet, screenHeight));
        }


        public void Update()
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                bullets[i].Move();

                if (bullets[i].IsOffScreen())
                {
                    bullets.RemoveAt(i);
                }
            }
        }

        public List<Bullet> GetBullets()
        {
            return bullets;
        }

        public void Clear()
        {
            bullets.Clear();
        }
        public void Remove(Bullet bullet)
        {
            bullets.Remove(bullet);
        }
    }
}
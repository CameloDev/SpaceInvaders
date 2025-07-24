namespace SpaceInvaders
{
    public class Bullet
    {
        private int x;
        private int y;
        private int speed;
        private bool goingUp;
        private readonly int width = 5;
        private readonly int height = 10;

        private int screenHeight;

        public Bullet(int x, int y, int speed, bool goingUp, bool isPlayerBullet, int screenHeight)
        {
            this.x = x;
            this.y = y;
            this.speed = speed;
            this.goingUp = goingUp;
            this.IsPlayerBullet = isPlayerBullet;
            this.screenHeight = screenHeight;
        }

        public void Move()
        {
            y += goingUp ? -speed : speed;
        }

        public bool IsOffScreen()
        {
            return y < 0 || y > screenHeight;
        }

        public Rectangle GetRect()
        {
            return new Rectangle(x, y, width, height);
        }

        public bool IsGoingUp()
        {
            return goingUp;
        }
        public bool IsPlayerBullet { get; set; }
    }
}
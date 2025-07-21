namespace SpaceInvaders
{
    public class Bullet
    {
        public int X, Y;
        public int Width = 5;
        public int Height = 10;
        public int Speed = 15;
        public bool IsPlayerBullet;

        public Rectangle GetRect() => new Rectangle(X, Y, Width, Height);

        public void Move()
        {
            Y += IsPlayerBullet ? -Speed : Speed;
        }
    }
}
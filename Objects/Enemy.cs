namespace SpaceInvaders
{
    public class Enemy
    {
        public int X, Y;
        public int Width = 40, Height = 20;
        public bool IsAlive = true;

        public Rectangle GetRect() => new Rectangle(X, Y, Width, Height);

        public void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }
    }
}
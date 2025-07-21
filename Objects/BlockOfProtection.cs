namespace SpaceInvaders
{
    public class BlockOfProtection
    {
        public int X, Y;
        public int Width = 60, Height = 20;
        public bool IsDestroyed = false;

        public Rectangle GetRect() => new Rectangle(X, Y, Width, Height);

        public void Destroy()
        {
            IsDestroyed = true;
        }
    }
}
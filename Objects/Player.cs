namespace SpaceInvaders
{
    public class Player
    {
        public int X, Y;
        public int Width = 50, Height = 20;
        public int Speed = 10;
        
        public Rectangle GetRect() => new Rectangle(X, Y, Width, Height);
    }
}
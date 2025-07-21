namespace SpaceInvaders
{
    public class Player
    {
        public int X, Y;
        public int Width = 50, Height = 20;
        public int Speed = 10;
        public int Lives = 3;

        public void CountLives()
        {
            Lives--;
            if (Lives < 0) Lives = 0;
        }

        public Rectangle GetRect() => new Rectangle(X, Y, Width, Height);
    }
}
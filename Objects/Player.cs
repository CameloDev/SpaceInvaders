namespace SpaceInvaders
{
    public class Player
    {
        private int x;
        private int y;
        private int width = 50;
        private int height = 20;
        private int speed = 10;

        public Player(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void MoveLeft()
        {
            x -= speed;
            if (x < 0) x = 0;
        }

        public void MoveRight(int formWidth)
        {
            x += speed;
            if (x + width > formWidth)
                x = formWidth - width;
        }

        public Rectangle GetRect()
        {
            return new Rectangle(x, y, width, height);
        }

        public Point GetCenter()
        {
            return new Point(x + width / 2, y);
        }
    }
}
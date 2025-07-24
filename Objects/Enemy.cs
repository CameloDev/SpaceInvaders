namespace SpaceInvaders
{
  public class Enemy
    {
        private int x;
        private int y;
        private int width = 30;
        private int height = 20;
          private int speed = 5;
        private bool movingRight = true;

        public Enemy(int startX, int startY)
        {
            x = startX;
            y = startY;
        }

        public void Update(int formWidth)
        {
            if (movingRight)
            {
                x += speed;
                if (x + width >= formWidth)
                {
                    movingRight = false;
                    y += height; 
                }
            }
            else
            {
                x -= speed;
                if (x <= 0)
                {
                    movingRight = true;
                    y += height; 
                }
            }
        }

        public Rectangle GetRect()
        {
            return new Rectangle(x, y, width, height);
        }

        public bool CollidesWith(Rectangle bullet)
        {
            return GetRect().IntersectsWith(bullet);
        }
        public void Move(int direction)
        {
            x += speed * direction;
        }
        public bool ReachedScreenEdge(int screenWidth)
        {
            return x <= 0 || x + width >= screenWidth;
        }
        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Red, GetRect());
        }
        public void MoveDown(int step)
        {
            y += step;
        }
        public bool IsOffScreen(int screenHeight)
        {
            return y > screenHeight;
        }
        public bool IsAlive => y < 600; 

        public int X => x;
        public int Y => y;
        public int Width => width;
        public int Height => height;
    }
}
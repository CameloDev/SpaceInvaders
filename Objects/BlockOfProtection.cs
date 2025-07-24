namespace SpaceInvaders
{
  public class BlockOfProtection
    {
        private int x;
        private int y;
        private int width;
        private int height;
        private int health;

        public BlockOfProtection(int x, int y, int width = 40, int height = 30, int health = 3)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.health = health;
        }

        public Rectangle GetRect()
        {
            return new Rectangle(x, y, width, height);
        }

        public void TakeDamage()
        {
            if (health > 0)
                health--;
        }

        public bool IsDestroyed => health <= 0;

        public void Draw(Graphics g)
        {
            if (!IsDestroyed)
            {
                Brush brush = health switch
                {
                    3 => Brushes.Green,
                    2 => Brushes.Yellow,
                    1 => Brushes.Red,
                    _ => Brushes.Transparent
                };
                g.FillRectangle(brush, x, y, width, height);
            }
        }
    }
}
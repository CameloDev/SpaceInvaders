namespace SpaceInvaders
{
    public class Controller
    {
        public bool MoveLeft { get; private set; }
        public bool MoveRight { get; private set; }
        public bool ShootRequested { get; private set; }

        public void KeyDown(Keys key)
        {
            if (key == Keys.Left)
                MoveLeft = true;
            else if (key == Keys.Right)
                MoveRight = true;
            else if (key == Keys.Space)
                ShootRequested = true;
        }

        public void KeyUp(Keys key)
        {
            if (key == Keys.Left)
                MoveLeft = false;
            else if (key == Keys.Right)
                MoveRight = false;
            else if (key == Keys.Space)
                ShootRequested = false;
        }

        // Depois que o Form tratar o tiro, resetar a flag pra evitar tiro infinito
        public void ResetShoot()
        {
            ShootRequested = false;
        }
    }

}
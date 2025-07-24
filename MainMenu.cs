using System;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            this.Text = "Space Invaders - Menu";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.ClientSize = new Size(300, 200);
            this.BackColor = Color.Black;
            Button startButton = new Button
            {
                Text = "Iniciar Jogo",
                Location = new Point(90, 40),
                Size = new Size(120, 40),
                BackColor = Color.DarkGray
            };
            startButton.Click += StartButton_Click;
            this.Controls.Add(startButton);

            Button exitButton = new Button
            {
                Text = "Sair",
                Location = new Point(90, 100),
                Size = new Size(120, 40),
                BackColor = Color.DarkGray
            };
            exitButton.Click += (s, e) => Application.Exit();
            this.Controls.Add(exitButton);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 game = new Form1();
            game.FormClosed += (s, args) => this.Close();
            game.Show();
        }
    }
}

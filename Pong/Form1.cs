using System;
using System.Windows.Forms;

namespace Pong
{
    public partial class Pong : Form
    {
        // Location Variables
        int cpuDirection = 5;
        int ballXCoordinate = 5;
        int ballYCoordinate = 5;
        // Score Variables
        int playerScore = 0;
        int cpuScore = 0;
        // Size Variables
        int bottomBoundary;
        int centerPoint;
        int xMidpoint;
        int yMidpoint;
        // Detection Variables
        bool playerDetectedUp;
        bool playerDetectedDown;
        bool gameOver = false;
        // Special Keys
        int spaceBarClicked = 0;


        public Pong()
        {
            InitializeComponent();
            bottomBoundary = ClientSize.Height - Player1.Height;
            xMidpoint = ClientSize.Width / 2;
            yMidpoint = ClientSize.Height / 2;
        }

        private void Pong_Load(object sender, EventArgs e)
        {

        }

        private void PongTimer_Tick(object sender, EventArgs e)
        {
            Random newBallSpot = new Random();
            int newSpot = newBallSpot.Next(100, ClientSize.Height - 100);
            // Adjust where the ball is
            Ball.Top -= ballYCoordinate;
            Ball.Left -= ballXCoordinate;
            // Make the CPU move
            Player2.Top += cpuDirection;
            // Check if CPU has reached the top or the bottom
            if (Player2.Top < 0 || Player2.Top > bottomBoundary)
            {
                cpuDirection = -cpuDirection;
            }
            // Check if ball has exited the left side of the screen
            if (Ball.Left < 0)
            {
                Ball.Left = xMidpoint;
                Ball.Top = newSpot;
                ballXCoordinate = -ballXCoordinate;
                cpuScore++;
                Player2Score.Text = cpuScore.ToString();
            }

            // Check if ball has exited the right side of the screen
            if (Ball.Left + Ball.Width > ClientSize.Width)
            {
                Ball.Left = xMidpoint;
                Ball.Top = newSpot;
                ballXCoordinate = -ballXCoordinate;
                playerScore++;
                Player1Score.Text = playerScore.ToString();
            }

            // Ensure the ball is within the boundaries of the screen
            if (Ball.Top < 0 || Ball.Top + Ball.Height > ClientSize.Height)
            {
                ballYCoordinate = -ballYCoordinate;
            }

            // Check if the ball hits the paddles
            if (Ball.Bounds.IntersectsWith(Player1.Bounds) || Ball.Bounds.IntersectsWith(Player2.Bounds))
            {
                ballXCoordinate = -ballXCoordinate;
            }

            // Move Player up
            if (playerDetectedUp == true && Player1.Top > 0)
            {
                Player1.Top -= 10;
            }
            // Move Player down
            if (playerDetectedDown == true && Player1.Top < bottomBoundary)
            {
                Player1.Top += 10;
            }
            // Check for winner
            if (playerScore >= 3 || cpuScore >=3) 
            { 
                PongTimer.Stop();
                gameOver = true;
            }
        }

        private void Pong_KeyDown(object sender, KeyEventArgs e)
        {
            // If player presses the up arrow, move paddle upwards
            if (e.KeyCode == Keys.Up) { playerDetectedUp = true; }
            // If player presses the down arrow, move paddle downwards
            if (e.KeyCode == Keys.Down) { playerDetectedDown = true; }

            // If player presses space bar, pause the game
            if (e.KeyCode == Keys.Space)
            {
                if (spaceBarClicked % 2 == 0 && gameOver == false)
                {
                    PongTimer.Stop();
                }
                else if(gameOver == true)
                {
                    Player1Score.Text = "0";
                    Player2Score.Text = "0";
                    playerScore = cpuScore = 0;
                    PongTimer.Start();
                }
                else
                {
                    PongTimer.Start();
                }
            }
            spaceBarClicked++;
        }

        private void Pong_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) { playerDetectedUp = false; }
            if (e.KeyCode == Keys.Down) { playerDetectedDown = false; }
        }
    }
}

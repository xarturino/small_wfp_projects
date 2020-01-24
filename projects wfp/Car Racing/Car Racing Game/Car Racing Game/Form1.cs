using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Racing_Game
{
    public partial class Form1 : Form
    {
        int carSpeed = 5;
        int roadSpeed = 5;
        bool carLeft;
        bool carRight;
        int trafficSpeed = 5;
        int Score = 0;
        Random rnd = new Random();


        private void Reset()
        {
            trophy.Visible = false;
            button1.Enabled = false;
            explosion.Visible = false;
            trafficSpeed = 5;
            roadSpeed = 5;
            Score = 0;

            player.Left = 161;
            player.Top = 286;

            carLeft = false;
            carRight = false;

            AI1.Left = 66;
            AI1.Top = -120;

            AI2.Left = 294;
            AI2.Top = -185;

            roadTrack2.Left = -3;
            roadTrack2.Top = -222;
            roadTrack1.Left = -2;
            roadTrack1.Top = -638;

            timer1.Start();
        }
        public Form1()
        {
            InitializeComponent();
            Reset();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Score++;
            distance.Text = "" + Score;

            roadTrack1.Top += roadSpeed;
            roadTrack2.Top += roadSpeed;

            if(roadTrack1.Top > 630)
            {
                roadTrack1.Top = -630;
            }
            if (roadTrack2.Top > 630)
            {
                roadTrack2.Top = -630;
            }


            if (carLeft) { player.Left -= carSpeed; }
            if (carRight) { player.Left += carSpeed; }

            if (player.Left < 1)
            {
                carLeft = false;
            }
            else if (player.Left + player.Width > 380)
            {
                carRight = false;
            }

            AI1.Top += trafficSpeed;
            AI2.Top += trafficSpeed;

            if (AI1.Top > panel1.Height)
            {
                changeAI1();
                AI1.Left = rnd.Next(2, 160);
                AI1.Top = rnd.Next(100, 200) * -1;
            }

            if (AI2.Top > panel1.Height)
            {
                changeAI2();
                AI2.Left = rnd.Next(185, 327);
                AI2.Top = rnd.Next(100, 200) * -1;
            }

            if(player.Bounds.IntersectsWith(AI1.Bounds)|| player.Bounds.IntersectsWith(AI2.Bounds))
            {
                gameOver();
            }

            if(Score > 100 && Score < 500)
            {
                trafficSpeed = 6;
                roadSpeed = 7;
            }
            else if(Score>500 && Score < 1000)
            {
                trafficSpeed = 7;
                roadSpeed = 8;
            }
            else if (Score > 1200)
            {
                trafficSpeed = 9;
                roadSpeed = 10;
            }
        }

        private void moveCar(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && player.Left > 0)
            {
                carLeft = true;
            }
            if (e.KeyCode == Keys.Right && player.Left + player.Width < panel1.Width)
            {
                carRight = true;
            }
        }

        private void stopCar(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                carLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                carRight = false;
            }
        }

        private void changeAI1()
        {
            int num = rnd.Next(1, 8);
            switch (num)
            {
                case 1:
                    AI1.Image = Properties.Resources.carGreen;
                    break;
                case 2:
                    AI1.Image = Properties.Resources.carGrey;
                    break;
                case 3:
                    AI1.Image = Properties.Resources.carOrange;
                    break;
                case 4:
                    AI1.Image = Properties.Resources.carPink;
                    break;
                case 5:
                    AI1.Image = Properties.Resources.CarRed;
                    break;
                case 6:
                    AI1.Image = Properties.Resources.TruckBlue;
                    break;
                case 7:
                    AI1.Image = Properties.Resources.TruckWhite;
                    break;
                case 8:
                    AI1.Image = Properties.Resources.ambulance;
                    break;
                default:
                    break;
            }
        }

        private void changeAI2()
        {
            int num = rnd.Next(1, 8);
            switch (num)
            {
                case 1:
                    AI2.Image = Properties.Resources.carGreen;
                    break;
                case 2:
                    AI2.Image = Properties.Resources.carGrey;
                    break;
                case 3:
                    AI2.Image = Properties.Resources.carOrange;
                    break;
                case 4:
                    AI2.Image = Properties.Resources.carPink;
                    break;
                case 5:
                    AI2.Image = Properties.Resources.CarRed;
                    break;
                case 6:
                    AI2.Image = Properties.Resources.TruckBlue;
                    break;
                case 7:
                    AI2.Image = Properties.Resources.TruckWhite;
                    break;
                case 8:
                    AI2.Image = Properties.Resources.ambulance;
                    break;
                default:
                    break;
            }
        }

        private void gameOver()
        {
            trophy.Visible = true;
            timer1.Stop();
            button1.Enabled = true;

            explosion.Visible = true;
            player.Controls.Add(explosion);
            explosion.Location = new Point(-8, 5);
            explosion.BackColor = Color.Transparent;
            explosion.BringToFront();

            if (Score < 1000)
            {
                trophy.Image = Properties.Resources.bronze;
            }
            if (Score > 2000)
            {
                trophy.Image = Properties.Resources.silver;
            }
            if (Score > 3500)
            {
                trophy.Image = Properties.Resources.gold;
            }

            playSound();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void playSound()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"hit.wav");
            player.Play();
        }


    }
}

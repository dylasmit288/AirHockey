/*
 * Air Hockey Summative, By: Dylan Smith, 1/7/21
 * ICS3U
*/
 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;


namespace AirHockey
{
    public partial class Form1 : Form
    {
        //Global Variables
        SoundPlayer goal = new SoundPlayer(Properties.Resources.DJ_Airhorn_Sound_Effect);

        int stick1X = 420;
        int stick1Y = 110;
        int player1Score = 0;

        int stick2X = 20;
        int stick2Y = 110;
        int player2Score = 0;

        int stickWidth = 40;
        int stickHeight = 40;
        int stickSpeed = 2;

        int p1NetX = 0;
        int p1NetY = 133;

        int p2NetX = 465;
        int p2NetY = 133;

        int netHeight = 90;
        int netWidth = 15;

        int puckX = 233;
        int puckY = 130;
        int puckXSpeed = 1;
        int puckYSpeed = -1;
        int puckWidth = 15;
        int puckHeight = 15;

        bool leftDown = false;
        bool rightDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;

        bool wDown = false;
        bool aDown = false;
        bool sDown = false;
        bool dDown = false;

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush redBrush = new SolidBrush(Color.DarkRed);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        Font screenFont = new Font("Consolas", 12);

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Left:
                    leftDown = true;
                    break;
                case Keys.Right:
                    rightDown = true;
                    break;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Left:
                    leftDown = false;
                    break;
                case Keys.Right:
                    rightDown = false;
                    break;
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(blackBrush, puckX, puckY, puckWidth, puckHeight);

            e.Graphics.FillRectangle(blueBrush, stick1X, stick1Y, stickWidth, stickHeight);
            e.Graphics.FillRectangle(redBrush, stick2X, stick2Y, stickWidth, stickHeight);


            e.Graphics.DrawString($"{player1Score}", screenFont, blackBrush, 210, 10);
            e.Graphics.DrawString($"{player2Score}", screenFont, blackBrush, 250, 10);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gameTimer.Enabled = true; 
        }

        private void gameTimer_Tick_1(object sender, EventArgs e)
        {
            //move puck
            puckX += puckXSpeed;
            puckY += puckYSpeed;

            //move player 1
            if (wDown == true && stick1Y > 0)
            {
                stick1Y -= stickSpeed;
            }

            if (sDown == true && stick1Y < this.Height - stickHeight)
            {
                stick1Y += stickSpeed;
            }

            if (aDown == true && stick1X > 0)
            {
                stick1X -= stickSpeed;
            }

            if (dDown == true && stick1X < this.Width - stickWidth)
            {
                stick1X += stickSpeed;
            }

            //move player 2
            if (upArrowDown == true && stick2Y > 0)
            {
                stick2Y -= stickSpeed;
            }

            if (downArrowDown == true && stick2Y < this.Height - stickHeight)
            {
                stick2Y += stickSpeed;
            }

            if (leftDown == true && stick2X > 0)
            {
                stick2X -= stickSpeed;
            }

            if (rightDown == true && stick2X < this.Width - stickWidth)
            {
                stick2X += stickSpeed;
            }
            //top and bottom wall collision
            if (puckY < 0 || puckY > this.Height - puckHeight)
            {
                puckYSpeed *= -1;  // or: puckYSpeed = -puckYSpeed;
            }

            //create Rectangles of objects on screen to be used for collision detection
            Rectangle player1Rec = new Rectangle(stick1X, stick1Y, stickWidth, stickHeight);
            Rectangle player2Rec = new Rectangle(stick2X, stick2Y, stickWidth, stickHeight);
            Rectangle puckRec = new Rectangle(puckX, puckY, puckWidth, puckHeight);
            Rectangle p1Net = new Rectangle(p1NetX, p1NetY, netWidth, netHeight);
            Rectangle p2Net = new Rectangle(p2NetX, p2NetY, netWidth, netHeight);

            //check if puck hits either stick. If it does change the direction
            //and place the puck in front of the stick hit
            if (player1Rec.IntersectsWith(puckRec))
            {
                puckXSpeed *= -1;
            }

            else if (player2Rec.IntersectsWith(puckRec))
            {
                puckXSpeed *= -1;
            }

            if (puckRec.IntersectsWith(p1Net))
            {
                goal.Play();
                player2Score++;
                puckX = 295;
                puckY = 195;

                stick1Y = 170;
                stick2Y = 170;
            }

            if (puckRec.IntersectsWith(p2Net))
            {
                goal.Play();
                player1Score++;
                puckX = 295;
                puckY = 195;

                stick1Y = 170;
                stick2Y = 170;
            }

            else if (puckX > 480)
            {
                puckXSpeed *= -1;
            }

            else if (puckX < 0)
            {
                puckXSpeed *= -1;
            }


            if (player1Score == 3 || player2Score == 3)
            {
                gameTimer.Enabled = false;
            }
            Refresh();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanki5
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            tankGame.addTank(Pens.DarkGreen, 50, 50);
            tankGame.addTank(Pens.Red, 200, 300);
        }

        public void ResetGame()
        {
            InitializeComponent();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A: tankGame.startTurningLeft(0);
                    break;
                case Keys.D: tankGame.startTurningRight(0);
                    break;
                case Keys.W: tankGame.startForward(0);
                    break;
                case Keys.S: tankGame.startBackwards(0);
                    break;
                case Keys.Q: tankGame.startFiring(0, Pens.DarkGreen);
                    break;

                case Keys.J: tankGame.startTurningLeft(1);
                    break;
                case Keys.L: tankGame.startTurningRight(1);
                    break;
                case Keys.I: tankGame.startForward(1);
                    break;
                case Keys.K: tankGame.startBackwards(1);
                    break;
                case Keys.U: tankGame.startFiring(1, Pens.Red);
                    break;
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A: tankGame.stopTurningLeft(0);
                    break;
                case Keys.D: tankGame.stopTurningRight(0);
                    break;
                /*case Keys.W: tankGame.stopForward(0);
                    break;
                case Keys.S: tankGame.stopBackwards(0);*/
                    break;

                case Keys.J: tankGame.stopTurningLeft(1);
                    break;
                case Keys.L: tankGame.stopTurningRight(1);
                    break;
                /*case Keys.I: tankGame.stopForward(1);
                    break;
                case Keys.K: tankGame.stopBackwards(1);*/
                    break;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!tankGame.isGameFinished())
            {
                tankGame.timePassed(1f);
            }
        }
    }
}

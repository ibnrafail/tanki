using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanki5
{
    public partial class TankiGame : UserControl
    {
        private Tank[] tanks = new Tank[2];
        private Border border;
        private int tanksCount;
        private bool gameFinished = false;

        private List<Bullet> bullets;

        public TankiGame(int width, int height)
        {
            InitializeComponent();
            border = new Border(width, height);
            bullets = new List<Bullet>();
        }

        public bool isGameFinished()
        {
            return gameFinished;
        }

        private void TankiGame_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Добавить танк.
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void addTank(Pen pen, float x, float y)
        {
            tanks[tanksCount] = new Tank(pen, x, y);
            tanksCount++;
        }

        /// <summary>
        /// Начать поворот танка влево.
        /// </summary>
        /// <param name="tankNum"></param>
        public void startTurningLeft(int tankNum)
        {
            tanks[tankNum].setAngularVelocity(-10f);
        }

        /// <summary>
        /// Начать поворот танка вправо.
        /// </summary>
        /// <param name="tankNum"></param>
        public void startTurningRight(int tankNum)
        {
            tanks[tankNum].setAngularVelocity(10f);
        }
        
        /// <summary>
        /// Начать движение прямо.
        /// </summary>
        /// <param name="tankNum"></param>
        public void startForward(int tankNum)
        {
            tanks[tankNum].setVelocity(5);
            tanks[tankNum].IsForward = true;
        }

        /// <summary>
        /// Начать движение назад.
        /// </summary>
        /// <param name="tankNum"></param>
        public void startBackwards(int tankNum)
        {
            tanks[tankNum].setVelocity(5);
            tanks[tankNum].IsForward = false;
        }

        /// <summary>
        /// Начать стрельбу.
        /// </summary>
        /// <param name="tankNum"></param>
        public void startFiring(int tankNum, Pen pen)
        {
            bullets.Add(new Bullet(tanks[tankNum].getBarrelEndCoordinate(), tankNum, pen));
        }

        /// <summary>
        /// Завершить поворот танка влево.
        /// </summary>
        /// <param name="tankNum"></param>
        public void stopTurningLeft(int tankNum)
        {
            tanks[tankNum].setAngularVelocity(0f);
        }

        /// <summary>
        /// Начать поворот танка вправо.
        /// </summary>
        /// <param name="tankNum"></param>
        public void stopTurningRight(int tankNum)
        {
            tanks[tankNum].setAngularVelocity(0f);
        }

        /// <summary>
        /// Завершить движение прямо.
        /// </summary>
        /// <param name="tankNum"></param>
        public void stopForward(int tankNum)
        {
            tanks[tankNum].setVelocity(0);
        }

        /// <summary>
        /// Начать движение назад.
        /// </summary>
        /// <param name="tankNum"></param>
        public void stopBackwards(int tankNum)
        {
            tanks[tankNum].setVelocity(0);
        }

        public void timePassed(float time)
        {
            if (!this.gameFinished)
            {
                string message = "";
                tanks[0].move(time);
                List<Segment> tank1Segments = tanks[0].getTempSegmentsList();

                tanks[1].move(time);
                List<Segment> tank2Segments = tanks[1].getTempSegmentsList();

                if (AreIntersect(tank1Segments, tank2Segments) != true)
                {
                    if (border.intersectsWith(tank1Segments) != true)
                        tanks[0].applyCoordinates();
                    if (border.intersectsWith(tank2Segments) != true)
                        tanks[1].applyCoordinates();
                }

                List<Bullet> bulletsToRemove = new List<Bullet>();

                foreach (var i in bullets)
                {
                    i.move(time);
                    List<Segment> tankSegments = i.getBulletOwnerIdx() == 0 ? tank2Segments : tank1Segments;
                    if (!border.intersectsWith(i.getBarrelSegment()))
                    {
                        i.applyCoordinates();
                    }
                    else
                    {
                        bulletsToRemove.Add(i);
                    }

                    if (!AreIntersect(i.getBarrelSegment(), tankSegments))
                    {
                        i.applyCoordinates();
                    }
                    else
                    {
                        bulletsToRemove.Add(i);
                        int tankidx = i.getBulletOwnerIdx() == 0 ? 1 : 0;
                        if (tanks[tankidx].life > 0)
                        {
                            tanks[tankidx].life--;
                        }
                        else
                        {
                            string tankColor = i.getBulletOwnerIdx() == 0 ? "GREEN " : "RED ";
                            message = tankColor + " won";
                            gameFinished = true;
                            break;
                        }
                    }
                }

                foreach (var i in bulletsToRemove)
                {
                    bullets.Remove(i);
                }

                if (gameFinished)
                {
                    MessageBox.Show(message, "");
                }
                Refresh();
            }
        }

        /// <summary>
        /// Проверяет пересекаются ли стороны одного танка со сторонами другого.
        /// </summary>
        /// <param name="tank1Segments"></param>
        /// <param name="tank2Segments"></param>
        /// <returns><c>true</c> если отрезки пересекаются</returns>
        /// <returns><c>false</c> если отрезки не пересекаются</returns>
        public bool AreIntersect(List<Segment> tank1Segments, List<Segment> tank2Segments)
        {
            foreach (var i in tank1Segments)
            {
                foreach (var j in tank2Segments)
                {
                    if (Segment.intersect(i, j))
                        return true;
                }
            }

            return false;
        }

        private void TankiGame_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            this.border.draw(g);

            for (int i = 0; i < tanksCount; i++)
            {
                tanks[i].draw(g);
            }

            foreach (var i in bullets)
            {
                i.draw(g);
            }
        }

    }
}

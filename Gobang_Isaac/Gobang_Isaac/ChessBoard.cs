using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gobang_Isaac
{
    public partial class ChessBoard : UserControl
    {
        Bitmap mybmp;  //棋盘背景
        ChessMatrix chessMx = new ChessMatrix();
        public delegate void DelClickPos(int X, int Y);
        public event DelClickPos ClickPos;

        object _lock = new object();

        ChessColor chessState = ChessColor.Black;
        public ChessBoard()
        {
            InitializeComponent();
        }


        public void init()
        {
            mybmp = new Bitmap(800, 800);
            chessState = ChessColor.Black;
            chessMx.init();
            using (Graphics gobj = Graphics.FromImage(mybmp))
            {
                // gobj.FillRectangle(Brushes.Yellow, 0, 0, 800, 800);
                for (int i = 0; i < 15; i++)
                {
                    Point x = new Point(60 + i * 40, 60);
                    Point y = new Point(60 + i * 40, 620);
                    gobj.DrawLine(Pens.Black, x, y);
                }

                for (int i = 0; i < 15; i++)
                {
                    Point x = new Point(60, 60 + i * 40);
                    Point y = new Point(620, 60 + i * 40);
                    gobj.DrawLine(Pens.Black, x, y);
                }

                gobj.FillEllipse(Brushes.Black, 335, 335, 10, 10);
                gobj.FillEllipse(Brushes.Black, 175, 175, 10, 10);
                gobj.FillEllipse(Brushes.Black, 495, 175, 10, 10);
                gobj.FillEllipse(Brushes.Black, 175, 495, 10, 10);
                gobj.FillEllipse(Brushes.Black, 495, 495, 10, 10);

                string[] tempstring = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O" };

                Font f = new Font("微软雅黑", 16, FontStyle.Regular);
                for (int i = 0; i < tempstring.Length; i++)
                {
                    gobj.DrawString(tempstring[i], f, Brushes.Black, new PointF(50 + i * 40, 630));
                }

                string[] tempstring2 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" };
                for (int i = 0; i < tempstring2.Length; i++)
                {
                    if (i < 9)
                    {
                        gobj.DrawString(tempstring2[i], f, Brushes.Black, new PointF(25, 600 - i * 40));
                    }
                    else
                    {
                        gobj.DrawString(tempstring2[i], f, Brushes.Black, new PointF(15, 605 - i * 40));
                    }
                }

                f.Dispose();
                gobj.Dispose();
            }
            //this.CreateGraphics().DrawImage(mybmp, 40, 0);

            //Graphics g1 = this.CreateGraphics();
            //g1.DrawImage(mybmp, 40, 0);
            Invalidate();
        }
   

        private void ChessBoard_Paint(object sender, PaintEventArgs e)
        {
            Graphics gobjqqq = e.Graphics;
            if (mybmp != null)
            {
                lock(_lock)
                {
                    gobjqqq.DrawImage(mybmp, 0, 0);
                }

            }
        }

        private void ChessBoard_Load(object sender, EventArgs e)
        {
            init();
           
        }
       
        private void ChessBoard_MouseDown(object sender, MouseEventArgs e)
        {
            if (chessState == ChessColor.Black)
            {
                if (e.X > 40 && e.Y > 40 && e.X < 630 && e.Y < 630)
                {
                    int XX = (e.X - 40) / 40;
                    int YY = (e.Y - 40) / 40;
                    ClickPos(XX, YY);
                    DrawChessMan(ChessColor.Black, XX, YY);                    
                }
            }
        }

        public void DrawChessMan(ChessColor p,int XX,int YY)
        {
            if (chessMx.isSpace(XX, YY))
            {
                chessMx.ChessDown(p, XX, YY);
                lock(_lock)
                {
                    Graphics g = Graphics.FromImage(mybmp);
                    {
                        ChessMan.DrawChessMan(g, p, XX * 40 + 60, YY * 40 + 60);
                    }
                    g.Dispose();
                }
             
                Invalidate();
                ChessColor WinnerColor = chessMx.win(XX, YY);
                if (WinnerColor != ChessColor.Space)
                {
                    MessageBox.Show(WinnerColor.ToString());
                }
                if(p==ChessColor.Black)
                {
                    chessState = ChessColor.White;
                }
                else
                {
                    chessState = ChessColor.Black;
                }
            }
        }

    }
}

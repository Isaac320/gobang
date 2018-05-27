using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gobang_Isaac
{
    public partial class MainForm : Form
    {
        ChessBoard chess = new ChessBoard();
        IChessEngine myChessEngine = new ChessEngine();
        bool flag = true;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Controls.Add(chess);
            myChessEngine.GetBestMove += MyChessEngine_GetBestMove;
            chess.ClickPos += Chess_ClickPos;
        }

        private void Chess_ClickPos(int X, int Y)
        {
            myChessEngine.PMove(X, Y);
        }

        private void MyChessEngine_GetBestMove(int X, int Y)
        {
            chess.DrawChessMan(ChessColor.White, X, Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chess.init();
            myChessEngine.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ((ChessEngine)myChessEngine).SetTurn_Time(1000);
        }
    }
}

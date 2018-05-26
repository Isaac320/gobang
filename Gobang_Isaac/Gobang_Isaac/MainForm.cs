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
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Controls.Add(chess);
        }
    }
}

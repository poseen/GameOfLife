using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        BoardOfLife board;

        public Form1()
        {
            InitializeComponent();
            this.board = new BoardOfLife(483 / 4, 250 / 4);
            this.board.FillLife(0.4);
            this.pictureBox1.Image = board.ToImage();
        }

        private void RefreshImage()
        {
            this.board.Step();
            this.pictureBox1.Image = this.board.ToImage();
            this.pictureBox1.Refresh();
            this.label1.Text = $"Generation: [{this.board.Generation}] | Number of alive cells: [{this.board.NumberOfLivingCells}]";
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = !this.timer1.Enabled;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.RefreshImage();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = !this.timer1.Enabled;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.board.FillLife(this.trackBar1.Value / 100.0d);
            this.Refresh();
        }
    }
}
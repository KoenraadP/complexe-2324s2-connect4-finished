using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectFour
{
    public enum State
    {
        Empty,
        Red,
        Yellow
    }

    public partial class frmConnect4 : Form
    {
        State[,] grid = new State[6,7];

        public frmConnect4()
        {
            InitializeComponent();
        }

        private void ShowTokens()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Token t = new Token();
                    Controls.Add(t);
                    t.BackColor = Color.LightGray;
                    t.Location = new Point(j*60+50,i*60+50);
                }
            }
        }

        private void GenerateButtons()
        {
            /*Button btn = new Button();
            btn.Size = new Size(50, 25);
            btn.Name = "btnCol" + 1;
            btn.Location = new Point(0 + 50, 10);
            Controls.Add(btn);*/

            for(int i = 0;i < grid.GetLength(1);i++)
            {
                Button btn = new Button();
                btn.Size = new Size(50, 25);
                btn.Name = "btnCol" + i;
                btn.Location = new Point(i * 60 + 50, 10);
                Controls.Add(btn);
            }
        }

        private void FrmConnect4_Load(object sender, EventArgs e)
        {
            /*Token t = new Token();
            Controls.Add(t);
            t.Location = new Point(ClientSize.Width-t.Width, ClientSize.Height-t.Height);

            Debug.WriteLine(grid[0, 0]);*/

            ShowTokens();
            GenerateButtons();
        }
    }
}

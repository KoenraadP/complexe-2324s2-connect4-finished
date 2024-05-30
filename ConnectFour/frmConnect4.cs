using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        State player = State.Red;

        public frmConnect4()
        {
            InitializeComponent();
        }

        private void ShowTokens()
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    Token t = new Token();

                    // ieder vakje van het grid controleren op empty, red of yellow
                    // kleur aanpassen indien nodig
                    switch (grid[row,col])
                    {
                        case State.Red:
                            t.BackColor = Color.Red;
                            break;
                        case State.Yellow:
                            t.BackColor = Color.Yellow;
                            break;
                        default:
                            t.BackColor = Color.LightGray;
                            break;
                    }
                    t.Location = new Point(col * 60 + 50, row * 60 + 50);
                    Controls.Add(t);
                    t.BringToFront();
                    //t.BackColor = Color.LightGray;                    
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
                btn.Click += Btn_Click;
            }
        }

        private void ChangePlayer()
        {
            if (player == State.Red)
            {
                player = State.Yellow;
            }
            else
            {
                player = State.Red;
            }
        }

        private bool CheckWinner()
        {
            // vertical v1

            // enkel de eerste drie rijen zijn hier van tel om naar onder te controleren
            for (int row = 0; row <= 2; row ++)
            {
                // wel iedere kolom controleren
                for (int col = 0; col < grid.GetLength (1); col ++)
                {
                    if (grid[row,col] == player &&
                        grid[row + 1, col] == player &&
                        grid[row + 2, col] == player &&
                        grid[row + 3, col] == player)
                    {
                        DialogResult result = MessageBox.Show("Winner!", "Gewonnen!", MessageBoxButtons.RetryCancel);
                        if (result == DialogResult.Retry)
                        {
                            Restart();
                            return true;
                        }

                    }
                }
            }

            //  horizontal v1

            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1) - 3; col++)
                {
                    if (grid[row, col] == player && 
                        grid[row, col + 1] == player &&
                        grid[row, col + 2] == player && 
                        grid[row, col + 3] == player)
                    {
                        DialogResult result = MessageBox.Show("Winner!", "Gewonnen!", MessageBoxButtons.RetryCancel);
                        if (result == DialogResult.Retry)
                        {
                            Restart();
                            return true;
                        }
                    }
                }
            }

            // diagonal up
            for (int row = 3; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1) - 3; col++)
                {
                    if (grid[row, col] == player &&
                        grid[row - 1, col + 1] == player &&
                        grid[row - 2, col + 2] == player &&
                        grid[row - 3, col + 3] == player)
                    {
                        DialogResult result = MessageBox.Show("Winner!", "Gewonnen!", MessageBoxButtons.RetryCancel);
                        if (result == DialogResult.Retry)
                        {
                            Restart();
                            return true;
                        }
                    }
                }
            }

            // diagonal down
            for (int row = 3; row < grid.GetLength(0); row++)
            {
                for (int col = 3; col < grid.GetLength(1); col++)
                {
                    if (grid[row, col] == player &&
                        grid[row - 1, col - 1] == player &&
                        grid[row - 2, col - 2] == player &&
                        grid[row - 3, col - 3] == player)
                    {
                        DialogResult result = MessageBox.Show("Winner!", "Gewonnen!", MessageBoxButtons.RetryCancel);
                        if (result == DialogResult.Retry)
                        {
                            Restart();
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        // checkwinner v2
        // vereist wel een andere manier om te gebruiken met parameters

        /*private void CheckWinner(int row, int col)
        {
            int counter = 1;

            // verticaal

            // up
            for (int i = row - 1; i >= 0; i--)
            {
                if (grid[i, col] == player)
                {
                    counter++;
                }
                else
                {
                    break;
                }
            }

            // down
            for (int i = row + 1; i < grid.GetLength(0); i++)
            {
                if (grid[i, col] == player)
                {
                    counter++;
                }
                else
                {
                    break;
                }
            }

            if (counter >= 4)
            {
                Winner();
            }

            // horizontaal

            counter = 1;

            // links
            for (int i = col - 1; i >= 0; i--)
            {
                if (grid[row, i] == player)
                {
                    counter++;
                }
                else
                {
                    break;
                }
            }

            // rechts
            for (int i = col + 1; i < grid.GetLength(1); i++)
            {
                if (grid[row, i] == player)
                {
                    counter++;
                }
                else
                {
                    break;
                }
            }

            if (counter >= 4)
            {
                Winner();
            }

            counter = 1;


            // diagonal 1 --> boven links naar beneden rechts

            int j = col - 1;

            for (int i = row - 1; i >= 0 && j >= 0; i--, j--)
            {
                if (grid[i, j] == player)
                {
                    counter++;
                }
                else
                {
                    break;
                }
            }

            j = col + 1;

            for (int i = row + 1; i < grid.GetLength(0) && j < grid.GetLength(1); i++, j++)
            {
                if (grid[i, j] == player)
                {
                    counter++;
                }
                else
                {
                    break;
                }

            }

            if (counter >= 4)
            {
                Winner();
            }

            counter = 1;

            // diagonal 2 --> boven rechts naar beneden links

            j = col - 1;

            for (int i = row + 1; i < grid.GetLength(0) && j >= 0; i++, j--)
            {

                if (grid[i, j] == player)
                {
                    counter++;
                }
                else
                {
                    break;
                }

            }

            j = col + 1;

            for (int i = row - 1; i >= 0 && j < grid.GetLength(1); i--, j++)
            {

                if (grid[i, j] == player)
                {
                    counter++;
                }
                else
                {
                    break;
                }

            }

            if (counter >= 4)
            {
                Winner();
            }

        }*/


        private void Restart()
        {
            // nieuw leeg speelveld
            grid = new State[6, 7];
            ShowTokens();

            //player opnieuwstarten op rood
            player = State.Red;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            // koppel de aangeklikte knop aan een variabele
            Button b = (Button)sender;
            // huidige kolom instellen via laatste karakter van button naam
            int col = Convert.ToInt32(b.Name.Last().ToString());

            // onderaan beginnen controleren in huidige kolom
            // zodra een 'leeg' vakje gevonden wordt, State vervangen door Red
            for (int row = grid.GetLength(0) -1; row >= 0; row--)
            {
                if (grid[row,col] == State.Empty)
                {
                    grid[row, col] = player;
                    break;
                }
            }

            ShowTokens();

            if (!CheckWinner())
            {
                ChangePlayer();
            }        
            
            // maak een nieuwe token aan
            //Token t = new Token();
            // plaats de token op de laatste plaats van de eerste kolom (onderaan links)
            // 0 * 60 + 50, 5 * 60 + 50
            //t.Location = new Point(50, 350);
            //Controls.Add(t);
            // zet de token VOOR de grijze token die er al staat
            //t.BringToFront();
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

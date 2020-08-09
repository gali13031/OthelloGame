using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B19_Ex05_Aviv_315529131_Gal_205431141
{
    public partial class FormGameBoard : Form
    {
        private const string k_RedsMove = "Othello - Red's Turn";
        private const string k_YellowsMove = "Othello - Yellow's Turn";
        private const int k_ButtonSize = 40;
        private const int k_SpaceBetweenButtons = 5;
        private readonly int r_BoardSize;
        private PictureButton[,] m_BoardButtonMatrix;
        private UserInterface m_UserInterface;

        public FormGameBoard(int i_BoardSize, UserInterface i_UserInterface)
        {
            r_BoardSize = i_BoardSize;
            m_UserInterface = i_UserInterface;
            initializeComponent();
            buildBoard();
        }

        public string RedsMoveText
        {
            get
            {
                return k_RedsMove;
            }
        }

        public string WhitesMoveText
        {
            get
            {
                return k_YellowsMove;
            }
        }

        public PictureBox[,] BoardButtonMatrix
        {
            get
            {
                return m_BoardButtonMatrix;
            }
        }

        private void initializeComponent()
        {
            this.Text = k_RedsMove;
            int formSize = ((k_ButtonSize + k_SpaceBetweenButtons) * r_BoardSize) + k_SpaceBetweenButtons;
            this.ClientSize = new Size(formSize, formSize);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void buildBoard()
        {
            m_BoardButtonMatrix = new PictureButton[r_BoardSize, r_BoardSize];

            for (int i = 0; i < r_BoardSize; i++)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    m_BoardButtonMatrix[i, j] = new PictureButton();
                    m_BoardButtonMatrix[i, j].X = i;
                    m_BoardButtonMatrix[i, j].Y = j;
                    m_BoardButtonMatrix[i, j].Width = m_BoardButtonMatrix[i, j].Height = k_ButtonSize;
                    m_BoardButtonMatrix[i, j].Location = new System.Drawing.Point((i * (k_ButtonSize + k_SpaceBetweenButtons)) + k_SpaceBetweenButtons, (j * (k_SpaceBetweenButtons + k_ButtonSize)) + k_SpaceBetweenButtons);
                    m_BoardButtonMatrix[i, j].Click += m_ButtonBoard_Click;
                    m_BoardButtonMatrix[i, j].TabIndex = ((i + 1) * r_BoardSize) + (j + 1);
                    m_BoardButtonMatrix[i, j].Enabled = false;
                    m_BoardButtonMatrix[i, j].Visible = true;
                    m_BoardButtonMatrix[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    m_BoardButtonMatrix[i, j].BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

                    this.Controls.Add(m_BoardButtonMatrix[i, j]);
                }
            }
        }

        private void m_ButtonBoard_Click(object sender, EventArgs e)
        {
            PictureButton button = sender as PictureButton;
            int x = button.X;
            int y = button.Y;
            m_UserInterface.PlayerTurn(x, y);
        }

        public void InitializeNewGameMatrix()
        {
            int center = r_BoardSize / 2;

            m_BoardButtonMatrix[center - 1, center - 1].Image = Properties.Resources.CoinYellow;
            m_BoardButtonMatrix[center - 1, center - 1].Visible = true;

            m_BoardButtonMatrix[center, center].Image = Properties.Resources.CoinYellow;
            m_BoardButtonMatrix[center, center].Visible = true;

            m_BoardButtonMatrix[center - 1, center].Image = Properties.Resources.CoinRed;
            m_BoardButtonMatrix[center - 1, center].Visible = true;

            m_BoardButtonMatrix[center, center - 1].Image = Properties.Resources.CoinRed;
            m_BoardButtonMatrix[center, center - 1].Visible = true;
        }

        public void ChangeColorValidMoves(List<Point> i_ValidMoves)
        {
            foreach (Point point in i_ValidMoves)
            {
                m_BoardButtonMatrix[point.m_X, point.m_Y].Image = null;
                m_BoardButtonMatrix[point.m_X, point.m_Y].BackColor = Color.SpringGreen;
                m_BoardButtonMatrix[point.m_X, point.m_Y].Enabled = true;
                m_BoardButtonMatrix[point.m_X, point.m_Y].Visible = true;
            }
        }

        public void ChangeBackToOriginal(List<Point> i_ValidMoves)
        {
            foreach (Point point in i_ValidMoves)
            {
                if (m_BoardButtonMatrix[point.m_X, point.m_Y].BackColor == Color.SpringGreen)
                {
                    m_BoardButtonMatrix[point.m_X, point.m_Y].Visible = true;
                    m_BoardButtonMatrix[point.m_X, point.m_Y].Enabled = false;
                    m_BoardButtonMatrix[point.m_X, point.m_Y].BackColor = Color.Empty;
                }
            }
        }

        public void RestartBoard()
        {
            for (int i = 0; i < r_BoardSize; i++)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    m_BoardButtonMatrix[i, j].BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                    m_BoardButtonMatrix[i, j].Image = null;
                    m_BoardButtonMatrix[i, j].Visible = true;
                    m_BoardButtonMatrix[i, j].BackColor = Color.Empty;
                }
            }
        }
    }
}

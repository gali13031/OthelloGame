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
    public partial class FormGameSettings : Form
    {
        private const int k_MinBoardSize = 6;
        private const int k_MaxBoardSize = 12;
        private readonly Button r_ButtonChooseBoardSize = new Button();
        private readonly Button r_ButtonAgaintComputer = new Button();
        private readonly Button r_ButtonAgaintPlayer = new Button();
        private int m_BoardSize = k_MinBoardSize;
    
        private bool m_AgainstComputer = false;

        public FormGameSettings()
        {
            this.Text = "Othello - Game Settings";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ClientSize = new System.Drawing.Size(360, 200);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            startForm();
        }

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }
        }

        public bool AgainstComputer
        {
            get
            {
                return m_AgainstComputer;
            }
        }

        private void startForm()
        {
            r_ButtonChooseBoardSize.Text = "Board Size: 6x6 (click to increase)";
            r_ButtonChooseBoardSize.Size = new Size(this.ClientSize.Width - 40, 50);
            r_ButtonChooseBoardSize.Location = new System.Drawing.Point((this.ClientSize.Width / 2) - (r_ButtonChooseBoardSize.Size.Width / 2), 20);

            r_ButtonAgaintComputer.Text = "Play against the computer";
            r_ButtonAgaintComputer.Size = new Size((r_ButtonChooseBoardSize.Size.Width / 2) - 20, 40);
            int topLeft = r_ButtonChooseBoardSize.Top + (this.ClientSize.Height / 2);
            r_ButtonAgaintComputer.Location = new System.Drawing.Point(r_ButtonChooseBoardSize.Location.X, topLeft);

            r_ButtonAgaintPlayer.Text = "Play against your friend";
            r_ButtonAgaintPlayer.Size = r_ButtonAgaintComputer.Size;
            r_ButtonAgaintPlayer.Location = new System.Drawing.Point(r_ButtonAgaintComputer.Width + 60, topLeft);

            this.Controls.Add(r_ButtonChooseBoardSize);
            this.Controls.Add(r_ButtonAgaintComputer);
            this.Controls.Add(r_ButtonAgaintPlayer);

            this.r_ButtonChooseBoardSize.Click += new EventHandler(m_ButtonChooseBoardSize_Click);
            this.r_ButtonAgaintComputer.Click += new EventHandler(m_ButtonComputer_Click);
            this.r_ButtonAgaintPlayer.Click += new EventHandler(m_ButtonPlayer_Click);
        }

        private void m_ButtonChooseBoardSize_Click(object i_Sender, EventArgs i_EventArgs)
        {
            m_BoardSize += 2;

            if (m_BoardSize > k_MaxBoardSize)
            {
                m_BoardSize = k_MinBoardSize;
            }

            this.r_ButtonChooseBoardSize.Text = string.Format("Board Size: {0}x{0} (click to increase)", m_BoardSize);
        }

        private void m_ButtonComputer_Click(object i_Sender, EventArgs i_EventArgs)
        {
            m_AgainstComputer = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void m_ButtonPlayer_Click(object i_Sender, EventArgs i_EventArgs)
        {
            m_AgainstComputer = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

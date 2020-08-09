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
    public class Player
    {
        private Image m_Player;
        private bool m_IsComputer;
        private int m_Score = 0;

        public Player(Image i_Player)
        {
            m_Player = i_Player;
        }

        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        public bool IsComputer
        {
            get
            {
                return m_IsComputer;
            }

            set
            {
                m_IsComputer = value;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace B19_Ex05_Aviv_315529131_Gal_205431141
{
    // $G$ CSS-999 (-3) Class name should start with PictureBox
    public class PictureButton : PictureBox
    {
        private int m_X;
        private int m_Y;

        public int X
        {
            get
            {
                return m_X;
            }

            set
            {
                m_X = value;
            }
        }

        public int Y
        {
            get
            {
                return m_Y;
            }

            set
            {
                m_Y = value;
            }
        }
    }
}

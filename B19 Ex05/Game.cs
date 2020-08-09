using System;
using System.Collections.Generic;
using System.Text;

namespace B19_Ex05_Aviv_315529131_Gal_205431141
{
    public class Game
    {
        private readonly int m_BoardSize;
        private char m_CurrentPlayer = 'X';
        private char m_Opponent = 'O';
        private char[,] m_Board;
        private List<Point> m_ValidMoves = new List<Point>();
        private Point m_ScoreCount;

        // $G$ NTT-999 (-5) You should have used constants here
        public Game(int i_BoardSize, Player i_P1, Player i_P2)
        {
            m_BoardSize = i_BoardSize;
            m_Board = new char[i_BoardSize, i_BoardSize];

            for (int i = 0; i < i_BoardSize; ++i)
            {
                for (int j = 0; j < i_BoardSize; ++j)
                {
                    m_Board[i, j] = ' ';
                }
            }

            int center = m_BoardSize / 2;

            m_Board[center - 1, center - 1] = 'O';
            m_Board[center, center] = 'O';
            m_Board[center - 1, center] = 'X';
            m_Board[center, center - 1] = 'X';

            updateValidMoves();
        }

        public Point ScoreCount
        {
            get
            {
                return m_ScoreCount;
            }
        }

        public char[,] Board
        {
            get
            {
                return m_Board;
            }

            set
            {
                m_Board = value;
            }
        }

        public List<Point> ValidMoves
        {
            get
            {
                return m_ValidMoves;
            }
        }

        public void SwapTurns()
        {
            if (m_CurrentPlayer == 'X')
            {
                m_CurrentPlayer = 'O';
                m_Opponent = 'X';
            }
            else
            {
                m_CurrentPlayer = 'X';
                m_Opponent = 'O';
            }

            updateValidMoves();
        }

        private void updateValidMoves()
        {
            m_ValidMoves.Clear();
            for (int i = 0; i < m_Board.GetLength(0); i++)
            {
                for (int j = 0; j < m_Board.GetLength(1); j++)
                {
                    if (m_Board[i, j] == ' ')
                    {
                        if (isValidPath(new Point(-1, 0), i, j) ||
                            isValidPath(new Point(-1, 1), i, j) ||
                            isValidPath(new Point(0, 1), i, j) ||
                            isValidPath(new Point(1, 1), i, j) ||
                            isValidPath(new Point(1, 0), i, j) ||
                            isValidPath(new Point(1, -1), i, j) ||
                            isValidPath(new Point(0, -1), i, j) ||
                            isValidPath(new Point(-1, -1), i, j))
                        {
                            m_ValidMoves.Add(new Point(i, j));
                        }
                    }
                }
            }
        }

        public bool TryToInsertMove(int i_Row, int i_Col)
        {
            bool isValid = false;

            if (m_ValidMoves.Contains(new Point(i_Row, i_Col)))
            {
                isValid = true;
                if (isValidPath(new Point(-1, 0), i_Row, i_Col))
                {
                    flipSlots(new Point(-1, 0), i_Row, i_Col);
                }

                if (isValidPath(new Point(-1, 1), i_Row, i_Col))
                {
                    flipSlots(new Point(-1, 1), i_Row, i_Col);
                }

                if (isValidPath(new Point(0, 1), i_Row, i_Col))
                {
                    flipSlots(new Point(0, 1), i_Row, i_Col);
                }

                if (isValidPath(new Point(1, 1), i_Row, i_Col))
                {
                    flipSlots(new Point(1, 1), i_Row, i_Col);
                }

                if (isValidPath(new Point(1, 0), i_Row, i_Col))
                {
                    flipSlots(new Point(1, 0), i_Row, i_Col);
                }

                if (isValidPath(new Point(1, -1), i_Row, i_Col))
                {
                    flipSlots(new Point(1, -1), i_Row, i_Col);
                }

                if (isValidPath(new Point(0, -1), i_Row, i_Col))
                {
                    flipSlots(new Point(0, -1), i_Row, i_Col);
                }

                if (isValidPath(new Point(-1, -1), i_Row, i_Col))
                {
                    flipSlots(new Point(-1, -1), i_Row, i_Col);
                }
            }

            return isValid;
        }

        private bool isValidPath(Point i_Direction, int i_StartPointX, int i_StartPointY)
        {
            bool foundPath = false;
            i_StartPointX += i_Direction.m_X;
            i_StartPointY += i_Direction.m_Y;

            if ((!isPassLimit(i_StartPointX, i_StartPointY)) && (m_Board[i_StartPointX, i_StartPointY] == m_Opponent))
            {
                while ((!isPassLimit(i_StartPointX, i_StartPointY)) && (m_Board[i_StartPointX, i_StartPointY] == m_Opponent))
                {
                    i_StartPointX += i_Direction.m_X;
                    i_StartPointY += i_Direction.m_Y;
                }

                if (!isPassLimit(i_StartPointX, i_StartPointY) && m_Board[i_StartPointX, i_StartPointY] == m_CurrentPlayer)
                {
                    foundPath = true;
                }
            }

            return foundPath;
        }

        private bool isPassLimit(int i_StartPointX, int i_StartPointY)
        {
            bool passLimit = false;
            if ((m_Board.GetLength(0) <= i_StartPointX) || (i_StartPointX < 0) || ((m_Board.GetLength(1) <= i_StartPointY) || (i_StartPointY < 0)))
            {
                passLimit = true;
            }

            return passLimit;
        }

        private void flipSlots(Point i_Direction, int i_StartPointX, int i_StartPointY)
        {
            m_Board[i_StartPointX, i_StartPointY] = m_CurrentPlayer;
            i_StartPointX += i_Direction.m_X;
            i_StartPointY += i_Direction.m_Y;

            while (m_Board[i_StartPointX, i_StartPointY] != m_CurrentPlayer)
            {
                m_Board[i_StartPointX, i_StartPointY] = m_CurrentPlayer;
                i_StartPointX += i_Direction.m_X;
                i_StartPointY += i_Direction.m_Y;
                if ((i_StartPointX <= 0) || (i_StartPointX > (m_Board.GetLength(0) - 1)))
                {
                    break;
                }

                if ((i_StartPointY <= 0) || (i_StartPointY > (m_Board.GetLength(1) - 1)))
                {
                    break;
                }
            }
        }

        public void ScoreRanking()
        {
            m_ScoreCount.m_X = 0;
            m_ScoreCount.m_Y = 0;
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (Board[i, j] == 'X')
                    {
                        m_ScoreCount.m_X++;
                    }
                    else
                    {
                        m_ScoreCount.m_Y++;
                    }
                }
            }
        }
    }
}

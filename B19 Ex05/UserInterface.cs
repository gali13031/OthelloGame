using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace B19_Ex05_Aviv_315529131_Gal_205431141
{
    public class UserInterface
    {
        private bool m_IsEnd;
        private Player m_RedPlayer;
        private Player m_YellowPlayer;
        private Player m_CurrentPlayer;
        private int m_RedsScoreTotalGames;
        private int m_YellowsScoreTotalGames;
        private Game m_CurrentGame;
        private FormGameBoard m_GameGraphic;
        private FormGameSettings m_FormSettings;

        public void StartGame()
        {
            m_RedPlayer = new Player(Properties.Resources.CoinRed);
            m_YellowPlayer = new Player(Properties.Resources.CoinYellow);
            m_CurrentPlayer = m_RedPlayer;
            m_RedsScoreTotalGames = m_YellowsScoreTotalGames = 0;

            m_FormSettings = new FormGameSettings();
            m_FormSettings.ShowDialog();
            if (m_FormSettings.DialogResult == DialogResult.OK)
            {
                if (m_FormSettings.AgainstComputer)
                {
                    m_YellowPlayer.IsComputer = true;
                }

                m_CurrentGame = new Game(m_FormSettings.BoardSize, m_RedPlayer, m_YellowPlayer);
                m_GameGraphic = new FormGameBoard(m_FormSettings.BoardSize, this);
                m_GameGraphic.InitializeNewGameMatrix();

                List<Point> validMoves = m_CurrentGame.ValidMoves;
                m_GameGraphic.ChangeColorValidMoves(validMoves);
                m_GameGraphic.ShowDialog();
            }
        }

        public void PlayerTurn(int i_X, int i_Y)
        {
            Random rnd = new Random();
            bool checkMove = false;
            m_IsEnd = false;

            if (!m_CurrentPlayer.IsComputer)
            {
                checkMove = m_CurrentGame.TryToInsertMove(i_X, i_Y);
                moveTurn(checkMove);
            }

            while (m_CurrentPlayer.IsComputer)
            {
                Thread.Sleep(300);
                Point nextMove = m_CurrentGame.ValidMoves[rnd.Next(m_CurrentGame.ValidMoves.Count)];
                m_CurrentGame.TryToInsertMove(nextMove.m_X, nextMove.m_Y);
                moveTurn(checkMove);

                if (m_IsEnd)
                {
                    break;
                }
            }
        }

        private void moveTurn(bool i_CheckMove)
        {
            if (i_CheckMove)
            {
                m_GameGraphic.ChangeColorValidMoves(m_CurrentGame.ValidMoves);
                changeColorByTurn();
                m_GameGraphic.ChangeBackToOriginal(m_CurrentGame.ValidMoves);
                m_CurrentGame.SwapTurns();
                switchPlayer(m_CurrentPlayer);
                m_GameGraphic.ChangeColorValidMoves(m_CurrentGame.ValidMoves);

                if (m_CurrentGame.ValidMoves.Count == 0)
                {
                    m_CurrentGame.SwapTurns();
                    switchPlayer(m_CurrentPlayer);
                    m_GameGraphic.ChangeColorValidMoves(m_CurrentGame.ValidMoves);
                    if (m_CurrentGame.ValidMoves.Count == 0)
                    {
                        m_IsEnd = true;
                        showEndOfGameMessage();
                    }
                }
            }
        }

        private void switchPlayer(Player i_Player)
        {
            if (i_Player == m_RedPlayer)
            {
                m_CurrentPlayer = m_YellowPlayer;
                m_GameGraphic.Text = m_GameGraphic.WhitesMoveText;
            }
            else
            {
                m_CurrentPlayer = m_RedPlayer;
                m_GameGraphic.Text = m_GameGraphic.RedsMoveText;
            }
        }

        // $G$ CSS-999 (-3) instead of using strings in a if statement use constants 
        private void changeColorByTurn()
        {
            for (int i = 0; i < m_FormSettings.BoardSize; i++)
            {
                for (int j = 0; j < m_FormSettings.BoardSize; j++)
                {
                    if (m_CurrentGame.Board[i, j] == 'X')
                    {
                        m_GameGraphic.BoardButtonMatrix[i, j].Image = Properties.Resources.CoinRed;
                        m_GameGraphic.BoardButtonMatrix[i, j].Visible = true;
                        m_GameGraphic.BoardButtonMatrix[i, j].Enabled = false;
                    }

                    if (m_CurrentGame.Board[i, j] == 'O')
                    {
                        m_GameGraphic.BoardButtonMatrix[i, j].Image = Properties.Resources.CoinYellow;
                        m_GameGraphic.BoardButtonMatrix[i, j].Visible = true;
                        m_GameGraphic.BoardButtonMatrix[i, j].Enabled = false;
                    }
                }
            }
        }

        private void anotherRound()
        {
            m_GameGraphic.RestartBoard();
            m_GameGraphic.InitializeNewGameMatrix();
            m_CurrentPlayer = m_RedPlayer;
            m_CurrentGame = new Game(m_FormSettings.BoardSize, m_RedPlayer, m_YellowPlayer);
            List<Point> validMoves = m_CurrentGame.ValidMoves;
            m_GameGraphic.ChangeColorValidMoves(validMoves);
        }

        private void showEndOfGameMessage()
        {
            string endMessage = null;
            m_CurrentGame.ScoreRanking();
            m_RedPlayer.Score = m_CurrentGame.ScoreCount.m_X;
            m_YellowPlayer.Score = m_CurrentGame.ScoreCount.m_Y;

            if (m_RedPlayer.Score > m_YellowPlayer.Score)
            {
                m_RedsScoreTotalGames++;
                endMessage = string.Format(
@"Red Won!! ({0}/{1}) ({2}/{3})
Would you like another round?",
m_RedPlayer.Score,
m_YellowPlayer.Score,
m_RedsScoreTotalGames,
m_YellowsScoreTotalGames);
            }
            else if (m_YellowPlayer.Score > m_RedPlayer.Score)
            {
                m_YellowsScoreTotalGames++;
                endMessage = string.Format(
@"Yellow Won!! ({1}/{0}) ({3}/{2})
Would you like another round?",
m_RedPlayer.Score,
m_YellowPlayer.Score,
m_RedsScoreTotalGames,
m_YellowsScoreTotalGames);
            }
            else
            {
                endMessage = string.Format(
@"It's a tie!! ({1}/{0}) ({3}/{2})
Would you like another round?",
m_RedPlayer.Score,
m_YellowPlayer.Score,
m_RedsScoreTotalGames,
m_YellowsScoreTotalGames);
            }

            DialogResult dialogResult = MessageBox.Show(endMessage, "Othello", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                anotherRound();
            }
            else
            {
                m_GameGraphic.Close();
            }
        }
    }
}
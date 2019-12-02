using System;
using System.Collections.Generic;
using Windows.Foundation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class TetrisGame
    {
        public int boardWidth = 10;
        public int boardHeight = 24;
        public string[][][] pieces;
        private bool[,] gameGrid = new bool[24, 10];
        //todo piece 1
        public bool getGridValue(int row, int col)
        {
            return gameGrid[row, col];
        }

        public bool setGridValue(int row, int col, bool fill)
        {
            if((row>=0 && col>=0) && (row<24 && col < 10))
            {
                gameGrid[row, col] = fill;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void settlePiece(Point coordinate, string[] pieceFrame)
        {
            //sees if piece can go in grid
            //if yes, put it in
            //if no, gameOver

            if (coordinate.Y > 0 && coordinate.X > 0)
            {

                //cleaning up previous upper active grid spaces
                //activeGrid[(int)coordinate.Y - 1, (int)coordinate.X] = false;
                //activeGrid[(int)coordinate.Y - 1, (int)coordinate.X+1] = false;
                //activeGrid[(int)coordinate.Y - 1, (int)coordinate.X+2] = false;
            }
            for (int i = 3; i >= 0; i--)
            {
                for (int j = 3; j >= 0; j--)
                {
                    if (pieceFrame[i][j] == 'f')
                    {
                        setGridValue((int)coordinate.Y + i, (int)coordinate.X + j, true);
                        //if piece intersecting with bottom piece
                    }

                }
            }
        }


        public void NewGame()
        {
            for (int row = 0; row < boardHeight; row++)
            {
                for (int col = 0; col < boardWidth; col++)
                {
                    gameGrid[row, col] = false;
                }
            }
            GamePiece gamePiece = new GamePiece();
            if (gamePiece.pieces != null)
            {
                pieces = gamePiece.pieces;
            }
        }
    }
}

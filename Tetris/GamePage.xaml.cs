using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Tetris
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        VirtualKey global_direction = VirtualKey.Right;
        private TetrisGame tetrisGame;
        bool gameOver = false;
        bool[,] activeGrid = new bool[32,18];
        public Point coordinate = new Point(5, 5);
        int piece = 0;
        int phase = 0;
        int score = 0;
        public GamePage()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;

            tetrisGame = new TetrisGame();
            tetrisGame.NewGame();
            CreateGrid();
            renderPiece(tetrisGame.pieces[piece][phase],ref coordinate);
            DrawGrid();
            int x = 0;

            
        }

        private void scootinDown()
        {
            Thread.Sleep(1000);
            unrenderPieces();
            coordinate.Y++;
            renderPiece(tetrisGame.pieces[piece][phase], ref coordinate);
            DrawGrid();
        }

        private void CreateGrid()
        {

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            // Remove all previously-existing rectangles
            gameCanvas.Children.Clear();

            //int rectSize = (int)gameCanvas.Width / tetrisGame.boardWidth;
            int rectSize = 20;
            SolidColorBrush black = new SolidColorBrush(Windows.UI.Colors.Black);
            SolidColorBrush white = new SolidColorBrush(Windows.UI.Colors.White);

            // Turn entire grid on and create rectangles to represent it
            for (int r = 0; r < tetrisGame.boardHeight; r++)
            {
                for (int c = 0; c < tetrisGame.boardWidth; c++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Fill = white;
                    rect.Width = rectSize + 1;
                    rect.Height = rect.Width + 1;
                    rect.Stroke = black;

                    // Store each row and col as a Point
                    rect.Tag = new Point(r, c);

                    int x = c * rectSize;
                    int y = r * rectSize;

                    Canvas.SetTop(rect, y);
                    Canvas.SetLeft(rect, x);

                    activeGrid[r, c] = false;

                    // Add the new rectangle to the canvas' children
                    gameCanvas.Children.Add(rect);
                }
            }
        }

        private void DrawGrid()
        {
            int index = 0;

            SolidColorBrush black = new SolidColorBrush(Windows.UI.Colors.Black);
            SolidColorBrush white = new SolidColorBrush(Windows.UI.Colors.White);

            // Set colors of each rectangle based on grid values
            for (int r = 0; r < tetrisGame.boardHeight; r++)
            {
                for (int c = 0; c < tetrisGame.boardWidth; c++)
                {
                    Rectangle rect = gameCanvas.Children[index] as Rectangle;
                    index++;

                    if (tetrisGame.getGridValue(r, c) || activeGrid[r+4,c+4])
                    {
                        // On
                        rect.Fill = black;
                        rect.Stroke = white;
                    }
                    else if (!tetrisGame.getGridValue(r,c) || !activeGrid[r+4, c+4])
                    {
                        // Off
                        rect.Fill = white;
                        rect.Stroke = black;
                    }
                }
            }
        }

        public int checkRowCompletion()
        {

            label.Text = "CheckingRowCompletion";
     
            int rowCompletionCount = 0;
            for(int i = 0; i < 24; i++)
            {
                bool rowIsComplete = true;
                for (int j = 0; j < 10 && rowIsComplete; j++)
                {
                    if(!tetrisGame.getGridValue(i, j))
                    {
                        rowIsComplete = false;
                    }
                }
                if (rowIsComplete)
                {
                    label.Text = "row is complete";
                    for(int j = 0; j < 10; j++)
                    {
                        tetrisGame.setGridValue(i, j, false);
                    }
                    removeRowAndScootEverythingDown(i);
                    rowCompletionCount++;
                    unrenderPieces();
                    DrawGrid();
                }
            }
            if (rowCompletionCount > 0)
            {

            }
            return rowCompletionCount;
        }

        public void removeRowAndScootEverythingDown(int row)
        {
            for(int i = row; i>0; i--)
            {
                for(int j = 0; j<10; j++)
                {
                    tetrisGame.setGridValue(i, j, tetrisGame.getGridValue(i - 1, j));
                }
            }
        }

        public bool unrenderPieces()
        {
            //sees if piece can go in grid
            //if yes, put it in
            //if no, gameOver

            if (coordinate.Y > 0 && coordinate.X > 0)
            {

                //cleaning up previous upper active grid spaces
            }

            for (int i = 0; i <32; i++)
            {
                for (int j = 0; j<18; j++)
                {
                    activeGrid[i, j] = false;    
                }
            }



            return true;
        }
        public bool renderPiece(string[] pieceFrame, ref Point coordinate)
        {
            //sees if piece can go in grid
            //if yes, put it in
            //if no, gameOver

            if (coordinate.Y > 0 && coordinate.X > 0)
            {

                //cleaning up previous upper active grid spaces
            }
            bool pieceAlreadySettled = false;
            for (int i = 3; i>=0; i--)
            {
                for (int j = 3; j>=0; j--)
                {
                    if (pieceFrame[i][j] == 'f')
                    {
                        activeGrid[(int)coordinate.Y + i, (int)coordinate.X + j] = true;
                        //if piece intersecting with bottom piece
                        label.Text = ((int)coordinate.Y + i, (int)coordinate.X + j).ToString();
                        if (((coordinate.Y + i >= 28) || tetrisGame.getGridValue((int)coordinate.Y + i - 4, (int)coordinate.X + j - 4)) && !pieceAlreadySettled)
                        {
                            //bottom-right most block of the piece has been told to intersect with 
                            //a settled piece in the grid. Or it hit below bottom row

                            //Therefore, current piece needs to be settled one space above where it tried to place itself
                            coordinate.Y -= 5;
                            coordinate.X -= 4;
                            tetrisGame.settlePiece(coordinate, pieceFrame);
                            unrenderPieces();
                            pieceAlreadySettled = true;
                            coordinate.Y = 5;
                            coordinate.X = 5;
                        }
                        }
                    else
                    {
                        activeGrid[(int)coordinate.Y + i, (int)coordinate.X + j] = false;
                    }
                }
            }



            return true;
        }



        void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            DrawGrid();
            //https://stackoverflow.com/questions/48963052/uwp-page-not-firing-keydown-event-at-all
            label.Text = args.VirtualKey.ToString() as string;
            global_direction = args.VirtualKey;


            if (global_direction == VirtualKey.Up)
            {
                if (coordinate.Y > 0)
                {
                    unrenderPieces();
                    coordinate.Y--;
                    renderPiece(tetrisGame.pieces[piece][phase],ref coordinate);
                    DrawGrid();
                }
            }
            if (global_direction == VirtualKey.Down)
            {
                if (coordinate.Y <27)
                {
                    unrenderPieces();
                    coordinate.Y++;
                    renderPiece(tetrisGame.pieces[piece][phase], ref coordinate);
                    DrawGrid();
                }

            }
            if (global_direction == VirtualKey.Left)
            {
                if (coordinate.X > 0)
                {
                    unrenderPieces();
                    coordinate.X--;
                    renderPiece(tetrisGame.pieces[piece][phase], ref coordinate);
                    DrawGrid();
                }

            }
            if (global_direction == VirtualKey.Right)
            {
                if (coordinate.X <12)
                {
                    unrenderPieces();
                    coordinate.X++;
                    renderPiece(tetrisGame.pieces[piece][phase], ref coordinate);
                    DrawGrid();
                }
                
            }
            if (global_direction == VirtualKey.Space)
            {
                piece += 1;
                phase = 0;
            }
            if (global_direction == VirtualKey.C)
            {
                if (phase < 3)
                {
                    phase++;
                    renderPiece(tetrisGame.pieces[piece][phase], ref coordinate);
                    DrawGrid();

                }
            }
            if (global_direction == VirtualKey.V)
            {
                if (phase > 0)
                {
                    phase--;
                    renderPiece(tetrisGame.pieces[piece][phase], ref coordinate);
                    DrawGrid();
                }
            }
            score += checkRowCompletion();
            scoreBox.Text = score.ToString();
            DrawGrid();
        }
    }
}

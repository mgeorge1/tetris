using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class GamePiece
    {
        public string[][][] pieces;
        public GamePiece()
        {
            string[][] piece0 = new string[][]
            {
                piece0frame0, piece0frame1, piece0frame2, piece0frame3
            };
            string[][] piece1 = new string[][]
            {
                piece1frame0, piece1frame1, piece1frame2, piece1frame3
            };
            string[][] piece2 = new string[][]
            {
                piece2frame0, piece2frame1, piece2frame2, piece2frame3
            };

            pieces = new string[][][]
            {
                piece0, piece1, piece2
            };
        }

        string[] piece0frame0 = new string[]
                {"  f ",
                 " ff ",
                 "  f ",
                 "    "};
        string[] piece0frame1 = new string[]
                {"    ",
                 " fff",
                 "  f ",
                 "    "};
        string[] piece0frame2 = new string[]
                {"  f ",
                 "  ff",
                 "  f ",
                 "    "};
        string[] piece0frame3 = new string[]
                {"  f ",
                 " fff",
                 "    ",
                 "    "};


        //================================================

        string[] piece1frame0 = new string[]
                {"    ",
                 " ff ",
                 " f  ",
                 " f  "};
        string[] piece1frame1 = new string[]
                {"    ",
                 " f  ",
                 " fff",
                 "    "};
        string[] piece1frame2 = new string[]
                {"  f ",
                 "  f ",
                 " ff ",
                 "    "};
        string[] piece1frame3 = new string[]{"    ",
                 "fff ",
                 "  f ",
                 "    "};

        //================================================

        string[] piece2frame0 = new string[]
                {"  f ",
                 "  f ",
                 "  f ",
                 "  f " };

        string[] piece2frame1 = new string[]
                {"    ",
                 "ffff",
                 "    ",
                 "    " };
        string[] piece2frame2 = new string[]
                {"  f ",
                 "  f ",
                 "  f ",
                 "  f " };
        string[] piece2frame3 = new string[]
                {"    ",
                 "ffff",
                 "    ",
                 "    " };


        //================================================
        //    },
        //    {
        //        {"    ",
        //         "    ",
        //         "    ",
        //         "    " },

        //        {"    ",
        //         "    ",
        //         "    ",
        //         "    " },

        //        {"    ",
        //         "    ",
        //         "    ",
        //         "    " },

        //        {"    ",
        //         "    ",
        //         "    ",
        //         "    " }
        //    },
        //    {
        //        {"    ",
        //         "    ",
        //         "    ",
        //         "    " },

        //        {"    ",
        //         "    ",
        //         "    ",
        //         "    " },

        //        {"    ",
        //         "    ",
        //         "    ",
        //         "    " },

        //        {"    ",
        //         "    ",
        //         "    ",
        //         "    " }
        //    },
        //    {
        //        {"    ",
        //         "    ",
        //         "    ",
        //         "    " },

        //        {"    ",
        //         "    ",
        //         "    ",
        //         "    " },

        //        {"    ",
        //         "    ",
        //         "    ",
        //         "    " },

        //        {"    ",
        //         "    ",
        //         "    ",
        //         "    " }
        //    },
        //    {
        //        {"    ",
        //         "    ",
        //         "    ",
        //         "    " },

        //        {"    ",
        //         "    ",
        //         "    ",
        //         "    " },

        //        {"    ",
        //         "    ",
        //         "    ",
        //         "    " },

        //        {"    ",
        //         "    ",
        //         "    ",
        //         "    " }
        //    }

        //public string[][][] piece = new string[7][4][4]
        //{

        //};
    }
}

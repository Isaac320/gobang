using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gobang_Isaac
{
    enum ChessColor
    {
        Black,
        White,
        Space
    }
    class ChessMatrix
    {
        ChessColor[,] ChessMat = new ChessColor[15, 15];   //棋盘矩阵
        ChessColor[,] ChessMatBig = new ChessColor[23, 23];   //棋盘大矩阵 为了更好判断是否胜利。

        public void init()
        {
            for(int i=0;i<15;i++)
            {
                for(int j=0;j<15;j++)
                {
                    ChessMat[i, j] = ChessColor.Space;
                }
            }


            for (int i = 0; i < 23; i++)
            {
                for (int j = 0; j < 23; j++)
                {
                    ChessMatBig[i, j] = ChessColor.Space;
                }
            }
        }

        public bool isSpace(int PosX,int PosY)
        {
            return ChessMat[PosX, PosY] == ChessColor.Space;
        }

        public void ChessDown(ChessColor chessA,int PosX,int PosY)
        {
            ChessMat[PosX, PosY] = chessA;
            ChessMatBig[PosX + 4, PosY + 4] = chessA;
        }

        public ChessColor win(int PosX,int PosY)
        {
            int x = PosX + 4;
            int y = PosY + 4;
            ChessColor p = ChessMatBig[x, y];
            for(int i=0;i<5;i++)          
            {
                bool f1 = true;
                bool f2 = true;
                bool f3 = true;
                bool f4 = true;
                for(int j=0;j<5;j++)
                {
                    f1 = f1 && (ChessMatBig[x - 4 + i + j,y] == p);        //四个方向判断是否五子连珠
                    f2 = f2 && (ChessMatBig[x, y - 4 + i + j] == p);       
                    f3 = f3 && (ChessMatBig[x - 4 + i + j, y - 4 + i + j] == p);  
                    f4 = f4 && (ChessMatBig[x - 4 + i + j, y + 4 - i - j] == p);  
                }
                if(f1||f2||f3||f4)
                {
                    return p;
                }                             
            }
            return ChessColor.Space;
        }
    }

  

   
}

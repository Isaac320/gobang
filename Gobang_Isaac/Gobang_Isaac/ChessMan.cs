﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Gobang_Isaac
{
    class ChessMan
    {
        static Image ChessManPic = global::Gobang_Isaac.Properties.Resources.a;
       public static void DrawChessMan(Graphics g, ChessColor color,int PosX,int PosY)
        {
            Bitmap mybmp = new Bitmap(40, 40);
            using (Graphics gobj = Graphics.FromImage(mybmp))
            {
                if (color == ChessColor.Black)
                {
                    gobj.DrawImage(ChessManPic, new Rectangle(0, 0, 40, 40), new Rectangle(23, 23, 70, 70), GraphicsUnit.Pixel);
                }
                else
                {
                    gobj.DrawImage(ChessManPic, new Rectangle(0, 0, 40, 40), new Rectangle(95, 23, 70, 70), GraphicsUnit.Pixel);
                }
            }
           
            g.DrawImage(mybmp, PosX-20, PosY-20);
        }
    }
}

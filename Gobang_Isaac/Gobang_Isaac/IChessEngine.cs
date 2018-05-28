using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gobang_Isaac
{
    public delegate void DelGetBestMove(int X, int Y);
    interface IChessEngine
    {
           
        /// <summary>
        /// 人类上一步走法
        /// </summary>
        /// <param name="PosX"></param>
        /// <param name="PosY"></param>
        void PMove(int PosX, int PosY);

        void Start();

        void Restart();

        //引擎找到最佳走法事件
        event DelGetBestMove GetBestMove;
    }
}

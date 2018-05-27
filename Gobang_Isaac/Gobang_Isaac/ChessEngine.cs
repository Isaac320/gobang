using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Gobang_Isaac
{
    
    class ChessEngine:IChessEngine
    {
        public delegate void DelReadStdOutput(string result);
        public event DelReadStdOutput ReadStdOutput;
        public event DelGetBestMove GetBestMove;
        Process CmdProcess = new Process();
        private void init()
        {
            DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent;
            RealAction(dir.FullName + @"\Resources\brain.exe");
            ReadStdOutput += ChessEngine_ReadStdOutput;
        }

        public ChessEngine()
        {
            init();
        }

        public void Start()
        {
            CmdProcess.StandardInput.WriteLine("start 15");
        }

        public void SetTurn_Time(int millisecond)
        {
            CmdProcess.StandardInput.WriteLine("info timeout_turn " + millisecond.ToString());
        }

        public void Restart()
        {
            CmdProcess.StandardInput.WriteLine("restart");
        }

        private void ChessEngine_ReadStdOutput(string result)
        {
            int len = result.Length;
            if (len < 6 && result.Contains(","))
            {
                string[] tempS=result.Split(',');
                int X = int.Parse(tempS[0]);
                int Y = int.Parse(tempS[1]);
                GetBestMove(X, Y);
            }
        }

        public void PMove(int PosX,int PosY)
        {
            CmdProcess.StandardInput.WriteLine("turn " + PosX.ToString() + "," + PosY.ToString());
        }

        private void RealAction(string StartFileName)
        {

            CmdProcess.StartInfo.FileName = StartFileName;      // 命令  
            CmdProcess.StartInfo.CreateNoWindow = true;         // 不创建新窗口  
            CmdProcess.StartInfo.UseShellExecute = false;
            CmdProcess.StartInfo.RedirectStandardInput = true;  // 重定向输入  
            CmdProcess.StartInfo.RedirectStandardOutput = true; // 重定向标准输出  
            CmdProcess.StartInfo.RedirectStandardError = true;  // 重定向错误输出  
            CmdProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;  
            CmdProcess.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
         //   CmdProcess.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);

            CmdProcess.EnableRaisingEvents = true;                      // 启用Exited事件  
         //   CmdProcess.Exited += new EventHandler(CmdProcess_Exited);   // 注册进程结束事件  
            CmdProcess.Start();
            CmdProcess.BeginOutputReadLine();
            CmdProcess.BeginErrorReadLine();

            // 如果打开注释，则以同步方式执行命令，此例子中用Exited事件异步执行。  
            // CmdProcess.WaitForExit();       
        }

        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                // 4. 异步调用，需要invoke  
                //  this.Invoke(ReadStdOutput, new object[] { e.Data });
                ReadStdOutput(e.Data);
            }
        }

       
    }
}

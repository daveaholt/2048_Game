using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace Philotechnia.interfaces
{
    public interface IGame
    {
        int CurrentScore { get; }
        static int HighScore { get; }
        void Start();
        void End(Process p);
        void Draw(Process p);
        void ProcessInput(ConsoleKeyInfo cki);
        bool IsOver();
        bool IsWon();
    }
}

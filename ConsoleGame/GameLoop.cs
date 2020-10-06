using Philotechnia.interfaces;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace Philotechnia
{
    public class GameLoop : IGameLoop
    {
        public void Run(IGame game)
        {
            var p = Process.GetCurrentProcess();            
            game.Start();
            game.Draw(p);

            bool isOver = false;
            bool isWon = false;
            do
            {
                if (Console.KeyAvailable)
                {
                    game.ProcessInput(Console.ReadKey(true));
                    Console.Clear();
                    game.Draw(p);

                    isOver = game.IsOver();
                    isWon = game.IsWon();

                    if (isOver || isWon)
                    {
                        game.End(p);
                        Thread.Sleep(2000);
                        break;
                    }
                }
            } while (!isOver && !isWon);
        }
    }
}

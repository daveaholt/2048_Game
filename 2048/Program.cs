using Philotechnia;
using Philotechnia.interfaces;
using System;

namespace _2048
{
    class Program
    {
        static void Main(string[] args)
        {
            IMainUserInterface userInterface = new MainUserInterface();
            IGameLoop gameLoop = new GameLoop();
            IGame game = new Game_2048();
            userInterface.StartGame(gameLoop, game);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Philotechnia.interfaces
{
    public interface IMainUserInterface
    {
        void StartGame(IGameLoop gameLoop, IGame game);
    }
}

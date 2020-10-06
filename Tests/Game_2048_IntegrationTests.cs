using _2048;
using NUnit.Framework;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Tests
{
    class Game_2048_IntegrationTests
    {
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldDrawCells()
        {
            var psi = new ProcessStartInfo
            {
                FileName = "CMD.EXE",
                Arguments = "/K",
                UseShellExecute = false
            };
            var p = Process.Start(psi); 

            // var c1 = new Cell(2, 0, 0, 75, 75);
            // var c2 = new Cell(4, 0, 75, 75, 75);
            // var c3 = new Cell(8, 0, 150, 75, 75);
            // var c4 = new Cell(16, 0, 225, 75, 75);
            // var c5 = new Cell(32, 0, 300, 75, 75);
            // var c6 = new Cell(64, 0, 375, 75, 75);
            // var c7 = new Cell(128, 75, 0, 75, 75);
            // var c8 = new Cell(256, 75, 75, 75, 75);
            // var c9 = new Cell(512, 75, 150, 75, 75);
            // var c10 = new Cell(1024, 75, 225, 75, 75);
            // var c11 = new Cell(0, 75, 300, 75, 75);
            // c1.Display(p);
            // c2.Display(p);
            // c3.Display(p);
            // c4.Display(p);
            // c5.Display(p);
            // c6.Display(p);
            // c7.Display(p);
            // c8.Display(p);
            // c9.Display(p);
            // c10.Display(p);
            // c11.Display(p);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Reflection;
using Projet;

namespace Projet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Disable vertical scrolling
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            SpaceShip spaceShip = new SpaceShip();

        }
    }
}

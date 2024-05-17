using System;
using System.Collections.Generic;
using Projet_Space_invaders;
using System.Windows.Input;
using System.Runtime.InteropServices;

namespace Projet_Space_invaders
{ 
    class Program
    {

    [STAThread]
    static void Main(string[] args)
    {
        // Disable vertical scrolling
        Console.CursorVisible = false;
        Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
        SpaceShip spaceShip = new SpaceShip();

    }

    }
}


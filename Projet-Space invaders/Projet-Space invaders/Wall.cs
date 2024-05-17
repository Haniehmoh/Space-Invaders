using System;
using System.Collections.Generic;

namespace Projet_Space_invaders
{
    internal class Wall
    {
        public int XPosition { get;  set; }
        public int YPosition { get;  set; }
        public string Symbol { get;  set; }

        public Wall(int x, int y, string symbol)
        {
            XPosition = x;
            YPosition = y;
            Symbol = symbol;
        }

        public void Draw()
        {
            Console.SetCursorPosition(XPosition, YPosition);
            Console.Write(Symbol);
        }
}   }


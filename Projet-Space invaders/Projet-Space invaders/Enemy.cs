using System;
using System.Collections.Generic;
using System.Windows.Input;
using Projet_Space_invaders;
using System.Windows.Media.Media3D;
using System.Linq;

namespace Projet_Space_invaders
{
    internal class Enemy
    {
        public int XPosition { get; set; }
        public  int YPosition { get; set; }
        public string Symbol { get; set; }
      
        public Enemy(int xEnemy, int yEnemy, string symbol)
        {
            XPosition = xEnemy;
            YPosition = yEnemy;
            Symbol = symbol;
        }

        public void DrawEnemies()
        {
            if (YPosition >= 0 && XPosition >= 0 && YPosition < Console.WindowHeight && XPosition < Console.WindowWidth)
            {
                Console.SetCursorPosition(XPosition, YPosition);
                Console.Write(Symbol);
            }
        }
    }
}

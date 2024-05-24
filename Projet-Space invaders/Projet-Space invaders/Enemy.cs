///ETML 
///Hanieh Mohajerani 
///date:18.01.2024
///Description:creer les enemies
using System;

namespace Projet_Space_invaders
{
    /// <summary>
    /// classe enemi
    /// </summary>
    class Enemy
    {
        /// <summary>
        /// Position initiale du enemi en x
        /// </summary>
        public int XPosition { get; set; }

        /// <summary>
        /// Position initiale du enemi en y
        /// </summary>
        public int YPosition { get; set; }

        /// <summary>
        /// creer symbol de enemi
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Constructeur de la classe Enemy.
        /// </summary>
        /// <param name="xEnemy"></param>
        /// <param name="yEnemy"></param>
        /// <param name="symbol"></param>
        public Enemy(int xEnemy, int yEnemy, string symbol)
        {
            XPosition = xEnemy;
            YPosition = yEnemy;
            Symbol = symbol;
        }

        /// <summary>
        /// creer les enemies
        /// </summary>
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

/* dIAGRAMME UML
 * -----------------------------------
|             Enemy               |
-----------------------------------
| - XPosition: int                |
| - YPosition: int                |
| - Symbol: string                |
-----------------------------------
| + Enemy(int, int, string)       |
| + DrawEnemies(): void           |
-----------------------------------

 * */
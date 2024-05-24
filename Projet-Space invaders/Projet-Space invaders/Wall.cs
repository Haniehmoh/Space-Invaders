///ETML 
///Hanieh Mohajerani 
///date:18.01.2024
///Description:Définit les murs que le vaisseau doit protéger des missiles ennemis.
using System;
using System.Collections.Generic;
using System.IO;

namespace Projet_Space_invaders
{
    /// <summary>
    /// classe wall
    /// </summary>
    class Wall
    {
        /// <summary>
        /// Position initiale du mure en X
        /// </summary>
        public int XPosition { get;  set; }

        /// <summary>
        /// Position initiale du mure en Y
        /// </summary>
        public int YPosition { get;  set; }

        /// <summary>
        /// symbol de mure
        /// </summary>
        public string Symbol { get;  set; }


        /// <summary>
        /// Constructeur de la classe Wall.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="symbol"></param>
        public Wall(int x, int y, string symbol)
        {
            XPosition = x;
            YPosition = y;
            Symbol = symbol;
        }

        /// <summary>
        /// creer et afficher les mures
        /// </summary>
        public void Draw()
        {
            Console.SetCursorPosition(XPosition, YPosition);
            Console.Write(Symbol);
        }
       
    }
}
/*Diagramme UML
 * -----------------------------------
|             Wall                |
-----------------------------------
| - XPosition: int                |
| - YPosition: int                |
| - Symbol: string                |
-----------------------------------
| + Wall(int, int, string)        |
| + Draw(): void                  |
-----------------------------------

 * 
 * */


///ETML 
///Hanieh Mohajerani 
///date:18.01.2024
///Description:classe creer les position des enemies et ship
using System;
using System.Collections.Generic;


namespace Projet_Space_invaders
{
    /// <summary>
    /// classe Position heritage de classe Missile
    /// </summary>
     class Position : Missile
    {
        /// <summary>
        ///  Référence à l'objet Spaceship
        /// </summary>
        private SpaceShip _spaceShip;

        /// <summary>
        ///  Constructeur de la classe Position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="speed"></param>
        /// <param name="symbol"></param>
        /// <param name="spaceShip"></param>
        public Position(int x, int y, int speed, char symbol, SpaceShip spaceShip) : base(x, y, speed, symbol)
        {
            this._spaceShip = spaceShip;
        }

        public SpaceShip SpaceShip { get; internal set; }
        public char Symbol { get; internal set; }
        public int Speed { get; internal set; }
    }
}

/*Diagramme UML
 * -----------------------------------
|            Position             |
-----------------------------------
| - _spaceShip: SpaceShip         |
-----------------------------------
| + Position(int, int, int, char, |
|            SpaceShip)           |
-----------------------------------

 * */

using System;
using System.Collections.Generic;

namespace Projet_Space_invaders
{
    internal class Position : Missile
    {
        private SpaceShip _spaceShip;

        public Position(int x, int y, int speed, char symbol, SpaceShip spaceShip) : base(x, y, speed, symbol)
        {
            _spaceShip = spaceShip;
        }

    }
}

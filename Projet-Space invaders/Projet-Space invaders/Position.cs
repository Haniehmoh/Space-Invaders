using System;
using System.Collections.Generic;
using Projet_Space_invaders;
using System.Windows.Input;

namespace Projet_Space_invaders
{
    internal class Position : Missile
    {
        private SpaceShip _spaceShip;

        public Position(int x, int y, int speed, char symbol, SpaceShip spaceShip) : base(x, y, speed, symbol)
        {
            _spaceShip = spaceShip;
        }

        public void MoveAndCheckCollision()
        {
            Move();

            // Vérifier la collision avec le vaisseau
            if (_positionX >= _spaceShip._shipPositionX &&
                _positionX < _spaceShip._shipPositionX + _spaceShip._shipSymbol.Length &&
                _positionY == _spaceShip._shipPositionY)
            {
                // Effacer complètement le vaisseau de la console
                for (int i = 0; i < _spaceShip._shipSymbol.Length; i++)
                {
                    Console.SetCursorPosition(_spaceShip._shipPositionX + i, _spaceShip._shipPositionY);
                    Console.Write(' '); // Effacer le symbole du vaisseau
                }

                // Supprimer le missile de la liste
                _spaceShip.RemoveEnemyMissile(this);

                // Réinitialiser la position du vaisseau
                _spaceShip.ResetShipPosition();
            }
        }

    }
}



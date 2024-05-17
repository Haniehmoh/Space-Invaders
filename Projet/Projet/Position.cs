using System;
using System.Collections.Generic;
using Projet;

namespace Projet
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
                // _spaceShip.RemoveEnemyMissile(this);

                // Réinitialiser la position du vaisseau
                _spaceShip.ResetShipPosition();
            }
        }

        // Mettre à jour les positions des ennemis
        public void UpdateEnemyPositions(int enemyPositionX, int enemyPositionY, bool isRight,
                                         List<int[]> enemyPositions, List<Missile> enemyMissiles,
                                         char enemySymbol)
        {
            int numLines = 4; // Nombre de lignes d'ennemis
            int numEnemiesPerLine = 5; // Nombre d'ennemis par ligne
            int spacingX = 5; // Espacement horizontal entre les ennemis
            int spacingY = 2; // Espacement vertical entre les lignes d'ennemis

            Random random = new Random();

            // Calculer la nouvelle position X des ennemis
            int newEnemyPositionX = enemyPositionX + (isRight ? 1 : -1);

            // Vérifier les limites de l'écran pour inverser la direction si nécessaire
            if (newEnemyPositionX >= Console.WindowWidth - numEnemiesPerLine * spacingX)
            {
                isRight = false;
                enemyPositionY++;
            }
            else if (newEnemyPositionX <= 0) // Modification pour traiter le bord gauche
            {
                isRight = true;
                enemyPositionY++;
            }
            else
            {
                enemyPositionX = newEnemyPositionX;
            }

            // Effacer la liste des positions des ennemis
            enemyPositions.Clear();

            // Mettre à jour les positions des ennemis et tirer des missiles
            for (int line = 0; line < numLines; line++)
            {
                for (int i = 0; i < numEnemiesPerLine; i++)
                {
                    int x = enemyPositionX + i * spacingX;
                    int y = enemyPositionY + line * spacingY;
                    enemyPositions.Add(new int[] { x, y });

                    // Tirer aléatoirement si l'ennemi est sur la dernière ligne de la console
                    if (line == numLines - 1 && random.Next(0, 100) < 5)
                    {
                        // Créer un missile et l'ajouter à la liste des missiles ennemis
                        enemyMissiles.Add(new Position(x + 2, y - 1, 1, '|', _spaceShip));
                    }
                }
            }
        }
    }
}

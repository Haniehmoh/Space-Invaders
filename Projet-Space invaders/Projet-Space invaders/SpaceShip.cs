using System;
using System.Collections.Generic;
using test_jeu;
using System.Windows.Input;
using Projet_Space_invaders;
using System.Windows.Media.Media3D;

namespace Projet_Hanieh_Mohajerani
{
    internal class SpaceShip
    {
        private int _shipPositionX = 20; // Position initiale du vaisseau
        private int _shipPositionY = 25; // Position initiale du vaisseau
        private string _shipSymbol = "<-->";
        private List<Missile> _shipMissiles = new List<Missile>();
        private int _enemyPositionX = 1; // Position initiale de l'ennemi
        private int _enemyPositionY = 1; // Position initiale de l'ennemi
        private string _enemySymbol = "<*>";
        private List<Missile> _enemyMissiles = new List<Missile>();
        private bool isright = true;
        private List<int[]> _enemyPositions = new List<int[]>();
        private List<Missile> _missile =new List<Missile>();
       
        public SpaceShip()
        {
            Draw();

            while (true)
            {
                HandleShipInput();
                HandleEnemyInput();
                MoveShipMissiles();
                ShootFromEnemy(1,1);
                Draw();
                System.Threading.Thread.Sleep(20); // Petit délai pour ne pas surcharger le CPU
            }
        }

        private void HandleShipInput()
        {
            if (Console.KeyAvailable)
            {
                if (Keyboard.IsKeyDown(Key.Left) && _shipPositionX > 0)
                    _shipPositionX--;
                if (Keyboard.IsKeyDown(Key.Right) && _shipPositionX + _shipSymbol.Length < Console.WindowWidth)
                    _shipPositionX++;
                if (Keyboard.IsKeyDown(Key.Space)) // Tirer vers le haut
                    ShootFromShip();
            }
        }


        /* private void HandleEnemyInput()
         {
            
             if (random.Next(0, 100) < 5) // Modifier ce nombre pour ajuster la probabilité de tir
             {
                 foreach (var enemyPosition in _enemyPositions)
                 {
                     // Tirer seulement si l'ennemi est sur la dernière ligne de la console
                     if (enemyPosition[1] == Console.WindowHeight - 1)
                     {
                        // ShootFromEnemy(enemyPosition[0], enemyPosition[1]);
                     }
                 }
             }

             if (isright)
             {
                 _enemyPositionX++;
                 if (_enemyPositionX >= Console.WindowWidth - _enemySymbol.Length)
                 {
                     isright = false;
                     _enemyPositionY++;
                 }
             }
             else
             {
                 _enemyPositionX--;
                 if (_enemyPositionX <= 0)
                 {
                     isright = true;
                     _enemyPositionY++;
                 }
             }
         }*/



        private void HandleEnemyInput()
        {
            int numLines = 4; // Nombre de lignes d'ennemis
            int numEnemiesPerLine = 5; // Nombre d'ennemis par ligne
            int spacingX = 5; // Espacement horizontal entre les ennemis
            int spacingY = 2; // Espacement vertical entre les lignes d'ennemis

            Random random = new Random();

            // Calculer la nouvelle position X des ennemis
            int newEnemyPositionX = _enemyPositionX + (isright ? 1 : -1);

            // Vérifier les limites de l'écran pour inverser la direction si nécessaire
            if (newEnemyPositionX >= Console.WindowWidth - _enemySymbol.Length || newEnemyPositionX <= 0)
            {
                isright = !isright;
                _enemyPositionY++;
            }
            else
            {
                _enemyPositionX = newEnemyPositionX;
            }

            // Effacer la liste des positions des ennemis
            _enemyPositions.Clear();

            // Mettre à jour les positions des ennemis et tirer des missiles
            for (int line = 0; line < numLines; line++)
            {
                for (int i = 0; i < numEnemiesPerLine; i++)
                {
                    int x = _enemyPositionX + i * spacingX;
                    int y = _enemyPositionY + line * spacingY;
                    _enemyPositions.Add(new int[] { x, y });

                    // Tirer aléatoirement si l'ennemi est sur la dernière ligne de la console
                    if (line == numLines - 1 && random.Next(0, 100) < 5)
                    {
                        // Créer un missile et l'ajouter à la liste des missiles ennemis
                        _enemyMissiles.Add(new Missile(x + _enemySymbol.Length / 2, y + 1, -1, '|')); // Déplacement vers le bas (-1 en Y)
                    }
                }
            }

            // Créer une copie temporaire de la liste _enemyMissiles
            var enemyMissilesCopy = new List<Missile>(_enemyMissiles);

            // Déplacer les missiles ennemis vers le bas
            foreach (var missile in enemyMissilesCopy)
            {
                missile.MoveEnemy(_enemyMissiles);
            }
        }


        private void MoveShipMissiles()
        {
            foreach (var missile in _shipMissiles)
            {
                missile.Move();
            }
        }


        private void ShootFromShip()
        {

            int missileX = _shipPositionX + _shipSymbol.Length / 2; // Centre du vaisseau
            int missileY = _shipPositionY - 1; // Au-dessus du vaisseau
            int missileSpeed = 2; // Vitesse du missile
            char missileSpeedChar = '|';
            _shipMissiles.Add(new Missile(missileX, missileY, missileSpeed ,missileSpeedChar));
        }




       private void ShootFromEnemy(int enemyX, int enemyY)
        {
            int missileX = enemyX + _enemySymbol.Length / 2; // Centre de l'ennemi
            int missileY = enemyY + 1; // Bas de l'ennemi
            int missileSpeed = 2; // Vitesse du missile
            char misileSpeedChar = '|'; 
            _enemyMissiles.Add(new Missile(missileX, missileY, missileSpeed,'|'));
        }
      

        private void Draw()
        {

            // Effacer l'écran en dessinant des espaces
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth));
            }

            // Dessiner les murs
            DrawWalls();

            Console.SetCursorPosition(_shipPositionX, _shipPositionY);

            Console.Write(_shipSymbol);

            foreach (var missile in _shipMissiles)
            {
                Console.SetCursorPosition(_shipPositionX, _shipPositionY);
                missile.Draw();
            }

           /* foreach (var missile in _enemyMissiles)
            {
                missile.DrawEnemy();
            }*/


            // Dessiner les ennemis
            foreach (var enemyPosition in _enemyPositions)
            {
               
                // Vérifier que les positions ne dépassent pas les limites de la console
                if (enemyPosition[0] >= 0 && enemyPosition[0] < Console.WindowWidth &&
                    enemyPosition[1] >= 0 && enemyPosition[1] < Console.WindowHeight)
                {
                    Console.SetCursorPosition(enemyPosition[0], enemyPosition[1]);
                    Console.Write(_enemySymbol);
                }
               
            }


            // Gérer les collisions avec le mur et effacer les missiles ennemis qui atteignent le bas
            foreach (var missile in _enemyMissiles)
            {
                // Vérifier si le missile atteint le mur
                if (missile.PositionY >= Console.WindowHeight - 1)
                {
                    // Effacer la partie touchée du mur
                    Console.SetCursorPosition(missile.PositionX, Console.WindowHeight - 1);
                    Console.Write(" "); // Effacer une seule case du mur

                    // Retirer le missile de la liste
                    _enemyMissiles.Remove(missile);
                    break; // Sortir de la boucle foreach pour éviter une exception liée à la modification de la collection en cours d'itération
                }
            }

            // Réinitialiser la position du curseur
            Console.SetCursorPosition(0, 0);
       }

        private void DrawWalls()
         {
             // Première ligne de mur
             Console.SetCursorPosition(0, Console.WindowHeight - 8);
             Console.Write("    ▌▌▌▌▌▌▌▌▌▌    ▌▌▌▌▌▌▌▌▌▌    ▌▌▌▌▌▌▌▌▌▌    ▌▌▌▌▌▌▌▌▌▌   ");
             Console.SetCursorPosition(0, Console.WindowHeight - 7);
             Console.Write("    ▌▌▌▌▌▌▌▌▌▌    ▌▌▌▌▌▌▌▌▌▌    ▌▌▌▌▌▌▌▌▌▌    ▌▌▌▌▌▌▌▌▌▌   ");

         }
       
       
    }
}




using System;
using System.Collections.Generic;
using test_jeu;
using System.Windows.Input;


namespace Projet_Hanieh_Mohajerani
{
    internal class SpaceShip
    {
        private int _shipPositionX = 20; // Position initiale du vaisseau
        private int _shipPositionY = 25; // Position initiale du vaisseau
        private string _shipSymbol = "<-->";
        private List<Missile> _shipMissiles = new List<Missile>();
         int PositionY { get;  set; }

        private int _enemyPositionX = 1; // Position initiale de l'ennemi
        private int _enemyPositionY = 1; // Position initiale de l'ennemi
        private string _enemySymbol = "<<==>>";
        private List<Missile> _enemyMissiles = new List<Missile>();
        private bool isright = false;

      // public object Key { get;  set; }
      // public object Keyboard { get;  set; }

        public SpaceShip()
        {

          
            // DrawEnemeie();
            Draw();
          

            while (true)
            {
                // Enemie();
                /*  HandleInput();
                  MoveMissiles();
                  MoveMissilesEnemie();*/

                HandleShipInput();
                HandleEnemyInput();
                MoveShipMissiles();
                MoveEnemyMissiles();
                Draw();
                // DrawEnemeie();
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
                if  (Keyboard.IsKeyDown(Key.Space)) // Tirer vers le haut
                     ShootFromShip();
                 
            } 
        } 
         
         
         
         
         
        

        private void HandleEnemyInput()
        {
            if (_enemyPositionX >= 0 && isright)
            {
                _enemyPositionX++;
            }
            if (_enemyPositionX <= Console.WindowWidth && !isright)
            {
                _enemyPositionX--;
            }
            if (_enemyPositionX <= 0)
            {
                isright = true;
            }
            if (_enemyPositionX >= Console.WindowWidth - _enemySymbol.Length)
            {
                isright = false;
            }

            Random random = new Random();


            if (random.Next(0, 2) == 1) // Tirez avec une certaine probabilité
                ShootFromEnemy();
        }


        private void MoveShipMissiles()
        {
            foreach (var missile in _shipMissiles)
            {
                missile.Move();
            }
        }

        public void MoveEnemyMissiles()
        {
            /*  foreach (var missile in _enemyMissiles)
              {
                  missile.MoveEnemie();
              }*/
            for (int i = _enemyMissiles.Count - 1; i >= 0; i--)
            {
                var missile = _enemyMissiles[i];
                missile.MoveEnemie();

                if (missile.PositionY >= Console.WindowHeight)
                {
                    _enemyMissiles.RemoveAt(i);
                }
            }
        }

        private void ShootFromShip()
        {
          
            int missileX = _shipPositionX + _shipSymbol.Length / 2; // Centre du vaisseau
            int missileY = _shipPositionY - 1; // Au-dessus du vaisseau
            int missileSpeed = 2; // Vitesse du missile
            _shipMissiles.Add(new Missile(missileX, missileY, missileSpeed));
        }


        private void ShootFromEnemy()
        {
            int missileX = _enemyPositionX + _enemySymbol.Length / 2; // Centre de l'ennemi
                                                                      //  int missileY = _enemyPositionY + 1; // En dessous de l'ennemi
            int missileY = _enemyPositionY + 1; // Bas de l'ennemi
            int missileSpeed = 2; // Vitesse du missile
            _enemyMissiles.Add(new Missile(missileX, missileY, missileSpeed));

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

            // Dessiner l'ennemi
            Console.SetCursorPosition(_enemyPositionX, _enemyPositionY);
            Console.Write(_enemySymbol);

            foreach (var missile in _shipMissiles)
            {
                //  Console.SetCursorPosition(_positionX, _positionY);
                missile.Draw();
            }

            // Dessiner les missiles de l'ennemi
            foreach (var missile in _enemyMissiles)
            {
                missile.Draw();
            }
            


        }
        private void DrawWalls()
        {
           
            // Première ligne de mur
            Console.SetCursorPosition(0, Console.WindowHeight - 8);
            Console.Write("    ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌    ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌    ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌     ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌ ▌   ");

            // Réinitialiser la position du curseur
            Console.SetCursorPosition(0, 0);


        }

    }
}





     
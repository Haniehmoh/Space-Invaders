using System;
using System.Collections.Generic;
using test_jeu;

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
        private string _enemySymbol = "<<==>>";
        private List<Missile> _enemyMissiles = new List<Missile>();
        private bool isright =false;
        

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
                  var key = Console.ReadKey().Key;

                  if (key == ConsoleKey.LeftArrow && _shipPositionX > 0)
                      _shipPositionX--;

                  else if (key == ConsoleKey.RightArrow && _shipPositionX + _shipSymbol.Length < Console.WindowWidth)

                    _shipPositionX ++;
                else if (key == ConsoleKey.UpArrow) // Tirer vers le haut
                      ShootFromShip();
              }

           
        }

        private void HandleEnemyInput()
        {
            if (_enemyPositionX >= 0 && isright)
            {
                _enemyPositionX++;
            }
            if(_enemyPositionX<= Console.WindowWidth && !isright)
            {
                _enemyPositionX--;
            }
            if(_enemyPositionX<= 0)
            {
                isright = true;
            }
            if (_enemyPositionX >= Console.WindowWidth - _enemySymbol.Length )
            {
                isright=false;
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

        private void MoveEnemyMissiles()
        {
            foreach (var missile in _enemyMissiles)
            {
                missile.MoveEnemie();
            }
        }

        private void ShootFromShip()
        {
            /*  int missileX = _positionX + _shipSymbol.Length / 2; // Centre du vaisseau
              int missileY = _positionY - 1; // Au-dessus du vaisseau
              int missileSpeed = 2; // Vitesse du missile
              _missiles.Add(new Missile(missileX, missileY, missileSpeed));*/

            int missileX = _shipPositionX + _shipSymbol.Length / 2; // Centre du vaisseau
            int missileY = _shipPositionY - 1; // Au-dessus du vaisseau
            int missileSpeed = 2; // Vitesse du missile
            _shipMissiles.Add(new Missile(missileX, missileY, missileSpeed));
        }


        private void ShootFromEnemy()
        {
              int missileX = _enemyPositionX + _enemySymbol.Length / 2; // Centre de l'ennemi
              int missileY = _enemyPositionY + 1; // En dessous de l'ennemi
              int missileSpeed = 2; // Vitesse du missile
              _enemyMissiles.Add(new Missile(missileX, missileY, missileSpeed));

           /* int missileX = _enemyPositionX + _enemySymbol.Length / 2; // Centre de l'ennemi
            int missileY = _enemyPositionY + _enemySymbol.Length; // Bas de l'ennemi
            int missileSpeed = 2; // Vitesse du missile vers le haut (valeur négative)
            _enemyMissiles.Add(new Missile(missileX, missileY, missileSpeed));*/
        }
        private void Draw()
        {
            Console.Clear();
            // Console.Write(" ");
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

       


     

    }
}


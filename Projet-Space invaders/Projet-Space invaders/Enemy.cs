using System;
using System.Collections.Generic;
using test_jeu;
namespace Projet_Space_invaders
{
 
    
        internal class Enemy
        {
        /* 
         using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Projet_Hanieh_Mohajerani
{
    internal class SpaceShip
    {
        private int _shipPositionX = 20; // Position initiale du vaisseau
        private int _shipPositionY = 25; // Position initiale du vaisseau
        private string _shipSymbol = "<-->";
        private List<Missile> _shipMissiles = new List<Missile>();
        private List<Missile> _enemyMissiles = new List<Missile>();
        private bool isRight = false;
        private string _enemySymbol = "<<==>>";
        private List<int[]> _enemyPositions = new List<int[]>();

        public SpaceShip()
        {
            InitializeEnemies();
            Draw();

            while (true)
            {
                HandleShipInput();
                HandleEnemyInput();
                MoveShipMissiles();
                Draw();
                System.Threading.Thread.Sleep(20);
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

        private void HandleEnemyInput()
        {
            Random random = new Random();

            if (random.Next(0, 100) < 5)
            {
                foreach (var enemyPosition in _enemyPositions)
                {
                    if (enemyPosition[1] == Console.WindowHeight - 1)
                    {
                        ShootFromEnemy(enemyPosition[0], enemyPosition[1]);
                    }
                }
            }

            if (isRight)
            {
                foreach (var enemyPosition in _enemyPositions)
                {
                    enemyPosition[0]++;
                    if (enemyPosition[0] >= Console.WindowWidth - _enemySymbol.Length)
                    {
                        isRight = false;
                        enemyPosition[1]++;
                    }
                }
            }
            else
            {
                foreach (var enemyPosition in _enemyPositions)
                {
                    enemyPosition[0]--;
                    if (enemyPosition[0] <= 0)
                    {
                        isRight = true;
                        enemyPosition[1]++;
                    }
                }
            }
        }

        private void InitializeEnemies()
        {
            int numLines = 4; // Nombre de lignes d'ennemis
            int numEnemiesPerLine = 5; // Nombre d'ennemis par ligne
            int initialX = 5; // Position initiale en X du premier ennemi
            int initialY = 2; // Position initiale en Y du premier ennemi
            int spacingX = 10; // Espacement horizontal entre les ennemis
            int spacingY = 2; // Espacement vertical entre les lignes d'ennemis

            for (int line = 0; line < numLines; line++)
            {
                for (int i = 0; i < numEnemiesPerLine; i++)
                {
                    int x = initialX + i * spacingX;
                    int y = initialY + line * spacingY;
                    _enemyPositions.Add(new int[] { x, y });
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

        private void ShootFromEnemy(int enemyX, int enemyY)
        {
            int missileX = enemyX + _enemySymbol.Length / 2; // Centre de l'ennemi
            int missileY = enemyY + 1; // Bas de l'ennemi
            int missileSpeed = 2; // Vitesse du missile
            _enemyMissiles.Add(new Missile(missileX, missileY, missileSpeed));
        }

        private void MoveShipMissiles()
        {
            foreach (var missile in _shipMissiles)
            {
                missile.Move();
            }
        }

        private void Draw()
        {
            Console.Clear(); // Effacer l'écran

            DrawWalls();
            DrawShip();
            DrawEnemies();
            DrawMissiles();

            // Réinitialiser la position du curseur
            Console.SetCursorPosition(0, 0);
        }

        private void DrawWalls()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 8);
            Console.Write("    ▌▌▌▌▌▌▌▌▌▌    ▌▌▌▌▌▌▌▌▌▌    ▌▌▌▌▌▌▌▌▌▌    ▌▌▌▌▌▌▌▌▌▌   ");
            Console.SetCursorPosition(0, Console.WindowHeight - 7);
            Console.Write("    ▌▌▌▌▌▌▌▌▌▌    ▌▌▌▌▌▌▌▌▌▌    ▌▌▌▌▌▌▌▌▌▌    ▌▌▌▌▌▌▌▌▌▌   ");
        }

        private void DrawShip()
        {
            Console.SetCursorPosition(_shipPositionX, _shipPositionY);
            Console.Write(_shipSymbol);
        }

        private void DrawEnemies()
        {
            foreach (var enemyPosition in _enemyPositions)
            {
                Console.SetCursorPosition(enemyPosition[0], enemyPosition[1]);
                Console.Write(_enemySymbol);
            }
        }

        private void DrawMissiles()
        {
            foreach (var missile in _shipMissiles)
            {
                missile.Draw();
            }

            foreach (var missile in _enemyMissiles)
            {
                missile.Draw();
            }
        }
    }

    internal class Missile
    {
        public int _positionX { get; set; }
        public int _positionY { get; set; }
        public char _symbol = '|';
        private int _speed;

        public Missile(int positionX, int positionY, int speed)
        {
            _positionX = positionX;
            _positionY = positionY;
            _speed = speed;
        }

        public void Move()
        {
            _positionY -= _speed;
        }

        public void Draw()
        {
            if (_positionY >= 0) // Vérifier que le missile est dans la zone d'affichage de la console
            {
                Console.SetCursorPosition(_positionX, _positionY);
                Console.Write(_symbol);
            }
        }
    }
}

         */

    }
}



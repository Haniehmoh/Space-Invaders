////ETML 
///Hanieh Mohajerani 
///date:18.01.2024
///Description:jeu Space Invaders
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Projet_Space_invaders;
using System.Windows.Media.Media3D;
using System.Linq;

namespace Projet_Space_invaders
{
    internal class SpaceShip
    {
        public int _shipPositionX = 20; // Position initiale du vaisseau
        public int _shipPositionY = 25; // Position initiale du vaisseau
        public string _shipSymbol = "<-->";
        private List<Missile> _shipMissiles = new List<Missile>();
        private int _enemyPositionX = 1; // Position initiale de l'ennemi
        private int _enemyPositionY = 1; // Position initiale de l'ennemi
        private string _enemySymbol = "<*>";
        private List<Missile> _enemyMissiles = new List<Missile>();
        private bool isright = true;
        private List<int[]> _enemyPositions = new List<int[]>();
        private List<Missile> _missile = new List<Missile>();
        private List<int[]> _wallPositions = new List<int[]>();   // Déclaration de la liste des positions des murs
        private int _shipHits = 0; // Variable pour compter les touches sur le vaisseau
        public SpaceShip()
        {
            Console.SetWindowSize(80, 30); // Définir la taille de la fenêtre de console

            // Ajouter les positions des murs à la liste
            //for (int i = 0; i < 2; i++)
            //{
            //    for (int j = 0; j < 50; j += 15)
            //    {
            //        _wallPositions.Add(new int[] { j, Console.WindowHeight - 8 + i });
            //    }
            //}

            while (true)
            {
                HandleShipInput();
                HandleEnemyInput();
                MoveShipMissiles();
                Draw();
                System.Threading.Thread.Sleep(50); // Petit délai pour ne pas surcharger le CPU
            }
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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
            if (newEnemyPositionX >= Console.WindowWidth - numEnemiesPerLine * spacingX)
            {
                isright = false;
                _enemyPositionY++;
            }
            else if (newEnemyPositionX <= 0) // Modification pour traiter le bord gauche
            {
                isright = true;
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
                        _enemyMissiles.Add(new Position(x + _enemySymbol.Length / 2, y - 1, 1, '|', this)); // Déplacement vers le bas (-1 en Y)
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

        /// <summary>
        /// 
        /// </summary>
        private void ShootFromShip()
        {

            int missileX = _shipPositionX + _shipSymbol.Length / 2; // Centre du vaisseau
            int missileY = _shipPositionY - 1; // Au-dessus du vaisseau
            int missileSpeed = 2; // Vitesse du missile
            char missileSpeedChar = '|';
            _shipMissiles.Add(new Missile(missileX, missileY, missileSpeed, missileSpeedChar));
        }

        /// <summary>
        /// 
        /// </summary>

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
                missile.DrawMissile();
            }

            foreach (var missile in _enemyMissiles)
            {
                missile.DrawEnemy();
            }


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


            // Réinitialiser la position du curseur
            Console.SetCursorPosition(0, 0);
        }


        // Dessiner les murs
        private void DrawWalls()
        {
            // Première ligne de mur
            Console.SetCursorPosition(0, Console.WindowHeight - 8);
            Console.Write("▌▌▌▌▌▌▌▌▌▌    ▌▌▌▌▌▌▌▌▌▌    ▌▌▌▌▌▌▌▌▌▌    ▌▌▌▌▌▌▌▌▌▌   ");

            // Ajout des positions des murs à la liste
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 50; j += 15)
                {
                    _wallPositions.Add(new int[] { j, Console.WindowHeight - 8 + i });
                }
            }
        }

        // Méthodes pour gérer les missiles ennemis
        public void AddEnemyMissile(Missile missile)
        {
            _enemyMissiles.Add(missile);
        }

        public void RemoveEnemyMissile(Missile missile)
        {
            _enemyMissiles.Remove(missile);
        }

        // Méthode pour gérer les collisions avec les missiles ennemis
        // Méthode pour gérer les collisions avec les missiles ennemis et les murs
        public void CheckEnemyMissileCollision()
        {
            // Parcourir la liste des missiles ennemis
            foreach (var missile in _enemyMissiles)
            {
                // Vérifier la collision avec le vaisseau
                if (missile.PositionX >= _shipPositionX &&
                    missile.PositionX < _shipPositionX + _shipSymbol.Length &&
                    missile.PositionY == _shipPositionY)
                {
                    _shipHits++; // Augmenter le compteur de touches sur le vaisseau

                    // Effacer le missile de la liste
                    RemoveEnemyMissile(missile);

                    if (_shipHits >= 5) // Si le vaisseau est touché 5 fois
                    {
                        Console.Clear(); // Effacer la console
                        Console.WriteLine("Game Over! Vous avez été touché 5 fois. Le jeu est terminé."); // Afficher un message de fin de jeu
                        Environment.Exit(0); // Quitter le jeu
                    }
                }

                // Vérifier la collision avec les murs
                foreach (var wallPosition in _wallPositions)
                {
                    if (missile.PositionX == wallPosition[0] && missile.PositionY == wallPosition[1])
                    {
                        // Effacer le missile de la liste
                        RemoveEnemyMissile(missile);
                        // Effacer le mur de la liste
                        _wallPositions.Remove(wallPosition);
                        break; // Sortir de la boucle pour éviter les modifications concurrentes
                    }
                }
            }
        }
        public void ResetShipPosition()
        {
            _shipPositionX = 20; // Réinitialiser la position X du vaisseau
            _shipPositionY = 25; // Réinitialiser la position Y du vaisseau
        }


    }
}



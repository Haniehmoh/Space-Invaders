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

        private int _shipLives = 3;  // Variable pour compter les touches sur le vaisseau
        private DateTime _gameStartTime;

        public Enemy Enemy;
        private List<Wall> _walls = new List<Wall>();
        public Position _position;
        public Missile _missile;
        Missile missil = new Missile(0, 0, 0, '|');
        public int _shipPositionX = 20; // Position initiale du vaisseau
        public int _shipPositionY = 45;
        // public int _shipPositionY = Console.WindowHeight * 5/ 4;
        public string _shipSymbol = "<-->";
        private List<Missile> _shipMissiles = new List<Missile>();
        // private bool _isright = true;

        private int _enemyPositionX = 1; // Position initiale de l'ennemi
        private int _enemyPositionY = 1; // Position initiale de l'ennemi
        private string _enemySymbol = "<*>";
        private List<Missile> _enemyMissiles = new List<Missile>();
        private bool isright = true;

        // private bool isright = true;
        private List<int[]> _enemyPositions = new List<int[]>();
        private List<Enemy> _enemies = new List<Enemy>(); // Ajoutez cette ligne pour créer une liste d'ennemis
        private static bool _gameRunning = true;


        private List<int[]> _wallPositions = new List<int[]>();   // Déclaration de la liste des positions des murs

        public SpaceShip()
        {
            // Création de l'instance de Position avec les paramètres nécessaires
            _position = new Position(0, 0, 1, ' ', this);
            Console.SetWindowSize(50, 50); // Définir la taille de la fenêtre de console
            _gameStartTime = DateTime.Now;


            // Dessiner les murs
            DrawWalls();
            CreateEnemies();

            while (_gameRunning)
            {
                // Gestion des entrées des ennemis
                // HandleEnemyInputes();
               // CreateEnemies();
                // Gestion des entrées du vaisseau
                HandleShipInput();
               
                // MoveEnemiesVertically();
                // Appel de la méthode pour créer les ennemis
                MoveAndShootEnemies(); // Appel de la méthode po
                // Déplacement et dessin du vaisseau
                MoveShipMissiles();
                HandleCollisionWithEnemies();
                
                Draw();

                // Gestion des collisions avec les ennemis
               // HandleCollisionWithEnemies();

                // Vérification du temps écoulé
                if (HasGameTimeElapsed())
                {
                    Console.WriteLine("Game Over");
                    _gameRunning = false;
                }
                System.Threading.Thread.Sleep(50);
            }
        }
        int numLines = 4; // Nombre de lignes d'ennemis
        int numEnemiesPerLine = 5; // Nombre d'ennemis par ligne
        int spacingX = 5; // Espacement horizontal entre les ennemis
        int spacingY = 2; // Espacement vertical entre les lignes d'ennemis
        private void HandleEnemyInputes()
             {
               
                Random random = new Random();

                // Effacer la liste des ennemis existants
                _enemyPositions.Clear();

                // Parcourir chaque ligne d'ennemis
                for (int line = 0; line < numLines; line++)
                {
                    // Parcourir chaque ennemi dans la ligne
                    for (int i = 0; i < numEnemiesPerLine; i++)
                    {
                        // Calculer les coordonnées X et Y de l'ennemi
                        int x = _enemyPositionX + i * spacingX;
                        int y = _enemyPositionY + 3 + line * spacingY;

                        // Ajouter un nouvel ennemi à la liste des ennemis
                        _enemyPositions.Add(new int[] { x, y });

                        // Tirer aléatoirement si l'ennemi est sur la dernière ligne de la console
                        if (line == numLines - 1 && random.Next(0, 100) < 5)
                        {
                            // Créer un missile et l'ajouter à la liste des missiles ennemis
                            _enemyMissiles.Add(new Position(x + _enemySymbol.Length / 2, y - 1, 1, '|', this)); // Déplacement vers le bas (-1 en Y)
                        }
                    }
                }


                // Mettre à jour les positions X des ennemis en fonction de leur direction (vers la gauche ou la droite)
                if (isright)
                {
                    _enemyPositionX++;
                    if (_enemyPositionX + numEnemiesPerLine * spacingX >= Console.WindowWidth)
                    {
                        _enemyPositionY++;
                        isright = false;
                    }
                }
                else
                {
                    _enemyPositionX--;
                    if (_enemyPositionX <= 0)
                    {
                        _enemyPositionY++;
                        isright = true;
                    }
                }

                // Effacer les missiles qui ont touché les murs
                foreach (var missile in _enemyMissiles.ToList())
                {
                    missile.MoveEnemy(_enemyMissiles);
                    // Vérifier la collision avec les murs
                    foreach (var wall in _walls.ToList())
                    {
                        if (missile.PositionX == wall.XPosition && missile.PositionY == wall.YPosition)
                        {
                            // Effacer le mur en le remplaçant par une chaîne vide
                            wall.Symbol = "";

                            // Marquer le missile comme ayant touché le mur
                            missile.HasHitWall = true;

                            // Sortir de la boucle car un missile ne peut toucher qu'un seul mur à la fois
                           // break;
                        }
                    }
                 }
             }


        private bool HasGameTimeElapsed()
        {
            TimeSpan elapsedTime = DateTime.Now - _gameStartTime;
            return elapsedTime.TotalSeconds >= 120;
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


        //private void HandleEnemyInput()
        //{
        //    bool _isright = true;
        //    int numLines = 4; // Nombre de lignes d'ennemis
        //    int numEnemiesPerLine = 5; // Nombre d'ennemis par ligne
        //    int spacingX = 5; // Espacement horizontal entre les ennemis
        //    int spacingY = 2; // Espacement vertical entre les lignes d'ennemis
        //    Random random = new Random();

        //    // Effacer la liste des ennemis existants
        //    _enemies.Clear();

        //    // Parcourir chaque ligne d'ennemis
        //    for (int line = 0; line < numLines; line++)
        //    {
        //        // Parcourir chaque ennemi dans la ligne
        //        for (int i = 0; i < numEnemiesPerLine; i++)
        //        {
        //            // Créer un nouvel ennemi
        //            Enemy enemy = new Enemy(_enemyPositionX + i * spacingX, _enemyPositionY + 3 + line * spacingY, _enemySymbol);


        //            // Ajouter le nouvel ennemi à la liste des ennemis
        //            _enemies.Add(enemy);

        //            // Tirer aléatoirement si l'ennemi est sur la dernière ligne de la console
        //            if (line == numLines - 1 && random.Next(0, 100) < 5)
        //            {
        //                // Créer un missile et l'ajouter à la liste des missiles ennemis
        //                _enemyMissiles.Add(new Position(enemy.XPosition + _enemySymbol.Length / 2, enemy.YPosition - 1, 1, '|', this)); // Déplacement vers le bas (-1 en Y)
        //            }
        //        }
        //    }

        //    foreach (var enemy in _enemies)
        //    {
        //        // Déplacement horizontal
        //        //enemy.XPosition += _isright ? 1 : -1;

        //        // Vérifier si l'ennemi atteint le bord
        //        if (_isright)
        //        {
        //            enemy.XPosition++;
        //            if (enemy.XPosition + numEnemiesPerLine * spacingX >= Console.WindowWidth)
        //            {
        //                // Changer de direction et descendre
        //                enemy.YPosition++;
        //                _isright = false;
        //            }
        //        }
        //        else
        //        {
        //            enemy.XPosition--;
        //            if (enemy.XPosition <= 0)
        //            {
        //                enemy.YPosition++;
        //                _isright = true;
        //            }
        //        }
        //    }

        //    // Effacer les missiles qui ont touché les murs
        //    foreach (var missile in _enemyMissiles)
        //    {
        //        missile.MoveEnemy(_enemyMissiles);
        //        // Vérifier la collision avec les murs
        //        foreach (var wall in _walls.ToList())
        //        {
        //            if (missile.PositionX == wall.XPosition && missile.PositionY == wall.YPosition)
        //            {
        //                // Effacer le mur en le remplaçant par une chaîne vide
        //                wall.Symbol = "";

        //                // Marquer le missile comme ayant touché le mur
        //                missile.HasHitWall = true;

        //                // Sortir de la boucle car un missile ne peut toucher qu'un seul mur à la fois
        //                break;
        //            }
        //        }
        //    }

        //    if (_enemies.Count == 0)
        //    {
        //        HandleEnemyInput();
        //    }
        //    else
        //    {
        //        MoveShipMissiles();
        //    }
        //}




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

        private void CreateEnemies()
        {
            Random random = new Random();

            // Effacer la liste des ennemis existants
            _enemyPositions.Clear();

            // Parcourir chaque ligne d'ennemis
            for (int line = 0; line < numLines; line++)
            {
                // Parcourir chaque ennemi dans la ligne
                for (int i = 0; i < numEnemiesPerLine; i++)
                {
                    // Calculer les coordonnées X et Y de l'ennemi
                    int x = _enemyPositionX + i * spacingX;
                    int y = _enemyPositionY + 3 + line * spacingY;

                    // Ajouter un nouvel ennemi à la liste des ennemis
                    _enemyPositions.Add(new int[] { x, y });

                    // Tirer aléatoirement si l'ennemi est sur la dernière ligne de la console
                    if (line == numLines - 1 && random.Next(0, 100) < 5)
                    {
                        // Créer un missile et l'ajouter à la liste des missiles ennemis
                        _enemyMissiles.Add(new Position(x + _enemySymbol.Length / 2, y - 1, 1, '|', this)); // Déplacement vers le bas (-1 en Y)
                    }
                }
            }
        }
        private void MoveAndShootEnemies()
        {

            Random random = new Random();

           
                // Mettre à jour les positions X des ennemis en fonction de leur direction (vers la gauche ou la droite)
                if (isright)
                {
                    _enemyPositionX++;
                    if (_enemyPositionX + numEnemiesPerLine * spacingX >= Console.WindowWidth)
                    {
                        _enemyPositionY++;
                        isright = false;
                    }
                }
                else
                {
                    _enemyPositionX--;
                    if (_enemyPositionX <= 0)
                    {
                        _enemyPositionY++;
                        isright = true;
                    }
                }
            
              

             // Tirer selon une certaine probabilité
    foreach (var enemy in _enemyPositions.ToList())
    {
        // Supposons que ton symbol ennemi a une largeur de 1 (pour cet exemple)
        if (random.Next(0, 100) < 5)
        {
            // En supposant qu'un missile descend (-1 en Y) et que enemy est un tableau [x,y]
            _enemyMissiles.Add(new Position(enemy[0] + 1 / 2, enemy[1] + 1, 1, '|', this));
        }
    }

            // Effacer les missiles qui ont touché les murs
            foreach (var missile in _enemyMissiles.ToList())
            {
                missile.MoveEnemy(_enemyMissiles);

                // Vérifier la collision avec les murs
                foreach (var wall in _walls.ToList())
                {
                    if (missile.PositionX == wall.XPosition && missile.PositionY == wall.YPosition)
                    {
                        // Effacer le mur en le remplaçant par une chaîne vide
                        wall.Symbol = "";

                        // Marquer le missile comme ayant touché le mur
                        missile.HasHitWall = true;

                        // Sortir de la boucle car un missile ne peut toucher qu'un seul mur à la fois
                       // break;
                    }
                }
            }

            //// Mettre à jour les positions Y des ennemis en fonction de leur direction (vers le bas)
            //foreach (var enemyPosition in _enemyPositions)
            //{
            //    enemyPosition[1]++; // Déplacement vers le bas
            //}
        }
        private void MoveEnemiesVertically()
        {
            // Mettre à jour les positions Y des ennemis en fonction de leur direction (vers le bas)
           
            {
               // enemyPosition[1]++; // Déplacement vers le bas
            }
        }


        private void Draw()
        {
            // Effacer l'écran en dessinant des espaces
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
            // Dessiner le temps écoulé
            TimeSpan elapsedTime = DateTime.Now - _gameStartTime;
            Console.SetCursorPosition(0, 0);
            Console.Write("Time elapsed: " + elapsedTime.ToString(@"mm\:ss"));
            // Afficher le nombre de vies restantes
            Console.SetCursorPosition(0, 1); // Positionner le curseur où vous voulez afficher les vies
            Console.Write("Ship Lives: " + _shipLives);

            // Dessiner les murs
            foreach (var wall in _walls)
            {
                wall.Draw();
            }

            // Dessiner le vaisseau
            Console.SetCursorPosition(_shipPositionX, _shipPositionY);
            Console.Write(_shipSymbol);

            // Dessiner les missiles du vaisseau
            foreach (var missile in _shipMissiles)
            {
                missile.DrawMissile();
            }

            // Dessiner les missiles ennemis
            foreach (var missile in _enemyMissiles)
            {
                missile.DrawEnemy();
            }

            //// Dessiner les ennemis
            foreach (var enemy in _enemyPositions)
            {
                missil.DrawEnemy();
            }

            //// Dessiner les ennemis
            //foreach (var position in _enemyPositions)
            //{
            //    int x = position[0];
            //    int y = position[1];
            //    Console.SetCursorPosition(x, y);
            //    Console.Write(_enemySymbol);
            //}
            // Dessiner les ennemis
            foreach (var position in _enemyPositions)
            {
                Console.SetCursorPosition(position[0], position[1]);
                Console.Write(_enemySymbol);
            }

            // Réinitialiser la position du curseur
            // Console.SetCursorPosition(0, 0);
        }



        private void DrawWalls()
        {

            int[] wallXPositions = { 5, 20, 35 }; // Positions X des murs
            int[] wallYPositions = { Console.WindowHeight - 8, }; // Positions Y des murs

            // Parcourir toutes les positions y des murs
            foreach (int yPosition in wallYPositions)
            {
                // Parcourir toutes les positions x des murs
                foreach (int xPosition in wallXPositions)
                {
                    // Ajouter chaque mur à la liste des murs
                    _walls.Add(new Wall(xPosition, yPosition, "▌"));
                    _walls.Add(new Wall(xPosition + 1, yPosition, "▌"));
                    _walls.Add(new Wall(xPosition + 2, yPosition, "▌"));
                    _walls.Add(new Wall(xPosition + 3, yPosition, "▌"));
                    _walls.Add(new Wall(xPosition + 4, yPosition, "▌"));
                    _walls.Add(new Wall(xPosition + 5, yPosition, "▌"));

                }
            }
        }

        private void MoveShipMissiles()
        {
            foreach (var missile in _shipMissiles.ToList())
            {
                missile.Move();

                // Vérifier la collision avec le mur
                foreach (var wall in _walls.ToList())
                {
                    if (missile.PositionX == wall.XPosition && missile.PositionY == wall.YPosition)
                    {
                        // Effacer le mur en remplaçant son symbole par une chaîne vide
                        wall.Symbol = " ";

                        // Marquer le missile comme ayant touché le mur
                        missile.HasHitWall = true;

                        // Sortir de la boucle car un missile ne peut toucher qu'un seul mur à la fois
                        break;
                    }
                }
            }
        }

        //foreach (var enemy in _enemies.ToList())
        //{
        //    enemy.Draw();

        //    foreach (var m in _shipMissiles.ToList())
        //    {
        //        if (m.PositionX >= _enemyPositionX && m.PositionX < _enemyPositionX + _enemySymbol.Length && m.PositionY == _enemyPositionY)
        //        {
        //            // Effacer l'emplacement de l'ennemi touché en le remplaçant par un espace vide dans la console
        //            Console.SetCursorPosition(_enemyPositionX, _enemyPositionY);
        //            Console.Write(" ");

        //            // Supprimer l'ennemi touché de la liste des ennemis
        //            _enemies.Remove(enemy);

        //            // Marquer le missile comme ayant touché un ennemi
        //            m.HasHitWall = true;
        //        }
        //    }
        //}
        // Vérifier la collision avec les ennemis
        //    foreach (var enemy in _enemies.ToList())
        //    {
        //        if (missile.PositionX >= enemy.XPosition && missile.PositionX < enemy.XPosition + enemy.Symbol.Length && missile.PositionY == enemy.YPosition)
        //        {
        //            // Effacer l'emplacement de l'ennemi touché en le remplaçant par un espace vide dans la console
        //            Console.SetCursorPosition(enemy.XPosition, enemy.YPosition);
        //            Console.Write(" ");

        //            // Supprimer l'ennemi touché de la liste des ennemis
        //            _enemies.Remove(enemy);

        //            // Marquer le missile comme ayant touché un ennemi
        //            missile.HasHitWall = true;

        //            // Sortir de la boucle car un missile ne peut toucher qu'un seul ennemi à la fois
        //            break;
        //        }
        //    }

        //    // Supprimer le missile s'il a touché le mur, un ennemi ou s'il est sorti de l'écran
        //    if (missile.HasHitWall || missile.PositionY < 0)
        //    {
        //        _shipMissiles.Remove(missile);
        //    }
        //}
        //}


        // Méthode pour détecter et gérer les collisions entre les missiles du vaisseau et les petits ennemis
        private void HandleCollisionWithEnemies()
        {
            // Vérifier la collision des missiles du vaisseau avec les petits ennemis
            foreach (var missile in _shipMissiles.ToList())
            {
                foreach (var enemyPosition in _enemyPositions)
                {
                    int enemyX = enemyPosition[0];
                    int enemyY = enemyPosition[1];

                    // Vérifier si le missile touche un petit ennemi
                    if (missile.PositionX >= enemyX && missile.PositionX < enemyX + _enemySymbol.Length && missile.PositionY == enemyY)
                    {
                        // Effacer l'emplacement du petit ennemi touché en le remplaçant par un espace vide dans la console
                        Console.SetCursorPosition(enemyX, enemyY);
                        Console.Write(" ");

                        // Supprimer le petit ennemi touché de la liste des positions des ennemis
                        _enemyPositions.Remove(enemyPosition);

                        // Marquer le missile comme ayant touché un ennemi
                        missile.HasHitWall = true;

                        // Sortir de la boucle car un missile ne peut toucher qu'un seul ennemi à la fois
                        break;
                    }
                }

                // Supprimer le missile s'il a touché un ennemi ou s'il est sorti de l'écran
                if (missile.HasHitWall || missile.PositionY < 0)
                {
                    _shipMissiles.Remove(missile);
                    //}

                }
            }


            //        // Vérifier la collision avec les ennemis
            //        foreach (var enemy in _enemies.ToList())
            //        {
            //            if (missile.PositionX >= enemy.X && missile.PositionX < enemy.X + _enemySymbol.Length && missile.PositionY == enemy.Y)
            //            {// Effacer l'emplacement de l'ennemi touché en le remplaçant par un espace vide dans la console
            //                Console.SetCursorPosition(enemy.X, enemy.Y);
            //                Console.Write(" ");

            //                // Supprimer l'ennemi touché de la liste des ennemis
            //                _enemies.Remove(enemy);


            //                // Marquer le missile comme ayant touché un ennemi
            //                missile.HasHitWall = true;

            //                //// Sortir de la boucle car un missile ne peut toucher qu'un seul ennemi à la fois
            //                //break;
            //            }
            //        }

            //        // Supprimer le missile s'il a touché le mur, un ennemi ou s'il est sorti de l'écran
            //        if (missile.HasHitWall || missile.PositionY < 0)
            //        {
            //            _shipMissiles.Remove(missile);
            //        }
            //    }
            //}



        }
    }
}


    








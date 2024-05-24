///ETML 
///Hanieh Mohajerani 
///date:18.01.2024
///Description: c'est la classe principle que représente le vaisseau spatial contrôlé par le joueur 
using Projet_Space_invaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
[assembly: InternalsVisibleToAttribute("TestUnitaire")]

namespace Projet_Space_invaders
{
    /// <summary>
    /// Classe principale du projet Space Invaders.
    /// </summary>
     class SpaceShip
     {
        /// <summary>
        /// Variable pour compter les touches sur le vaisseau
        /// </summary>
        public int ShipLives = 3;  

        /// <summary>
        /// Temps de début du jeu
        /// </summary>
        private DateTime _gameStartTime;

        /// <summary>
        /// Référence à l'objet Enemy
        /// </summary>
        public Enemy Enemy;

        /// <summary>
        /// Position du vaisseau
        /// </summary>
        public Position Position;

        /// <summary>
        /// Liste des murs
        /// </summary>
        public List<Wall> Walls = new List<Wall>();

        /// <summary>
        ///Missile tiré par le vaisseau  Référence à l'objet Missile
        /// </summary>
        public Missile missile;

        /// <summary>
        ///Instance de missile 
        /// </summary>
        Missile missil = new Missile(0, 0, 0, '|');

        /// <summary>
        /// Position initiale du vaisseau en Y
        /// </summary>
        public int ShipPositionX = 20;

        /// <summary>
        /// Position initiale du vaisseau en X
        /// </summary>
        public int ShipPositionY = 25;

        // public int ShipPositionY = Console.WindowHeight * 5/ 4;

        /// <summary>
        /// Symbole représentant le vaisseau
        /// </summary>
        public string ShipSymbol = "<-->";

        /// <summary>
        /// Liste des missiles du vaisseau
        /// </summary>
        public List<Missile> _shipMissiles = new List<Missile>();
        // private bool _isright = true;

        /// <summary>
        /// Position initiale de l'ennemi en X
        /// </summary>
        private int _enemyPositionX = 1;

        /// <summary>
        /// Position initiale de l'ennemi en Y
        /// </summary>
        private int _enemyPositionY = 1;

        /// <summary>
        /// Symbole représentant l'ennemi
        /// </summary>
        private string _enemySymbol = "<*>";

        /// <summary>
        /// Liste des missiles ennemis
        /// </summary>
        private List<Missile> _enemyMissiles = new List<Missile>();

        /// <summary>
        /// Direction du mouvement des ennemis (true pour droite, false pour gauche)
        /// </summary>
        private bool _isright = true;

        /// <summary>
        /// Liste des positions des ennemis
        /// </summary>
        public List<int[]> enemyPositions = new List<int[]>();

        /// <summary>
        /// Indicateur pour savoir si le jeu est en cours
        public  bool GameRunning = true;

        /// <summary>
        /// Constructeur de la classe SpaceShip.
        /// </summary>
        public SpaceShip()
        {
            // Création de l'instance de Position avec les paramètres nécessaires
            Position = new Position(0, 0, 1, ' ', this);

            // Définir le temps de début du jeu
            _gameStartTime = DateTime.Now;

            // Dessiner les murs
            DrawWalls();

            // Créer les ennemis
            CreateEnemies();

            // Boucle principale du jeu
            while (GameRunning)
            {
                // Gestion des entrées du vaisseau
                HandleShipInput();

                // Déplacement et tir des ennemis
                MoveAndShootEnemies();

                // Déplacement des missiles du vaisseau
                MoveShipMissiles();

                // Gestion des collisions entre les missiles du vaisseau et les ennemis
                HandleCollisionWithEnemies();

                // Dessiner les éléments du jeu
                Draw();

                // Vérification du temps écoulé
                if (HasGameTimeElapsed())
                {
                    Console.WriteLine("Game Over");
                    GameRunning = false;
                }

                // Pause pour ralentir la boucle du jeu
                System.Threading.Thread.Sleep(120);
            }

        }
      
        /// <summary>
        /// Vérifie si le temps de jeu écoulé a dépassé la limite.
        /// </summary>
        /// <returns>True si le temps écoulé dépasse 220 secondes, sinon false.</returns>
        private bool HasGameTimeElapsed()
        {
            TimeSpan elapsedTime = DateTime.Now - _gameStartTime;
            return elapsedTime.TotalSeconds >= 220;
        }


        /// <summary>
        /// Gère les entrées du vaisseau (déplacement et tir).
        /// </summary>
        public void HandleShipInput()
        {
            if (Console.KeyAvailable)
            {
                if (Keyboard.IsKeyDown(Key.Left) && ShipPositionX > 0)
                    ShipPositionX--;
                if (Keyboard.IsKeyDown(Key.Right) && ShipPositionX + ShipSymbol.Length < Console.WindowWidth)
                    ShipPositionX++;
                if (Keyboard.IsKeyDown(Key.Space)) // Tirer vers le haut
                    ShootFromShip();
            }
        }

        /// <summary>
        /// Fait tirer le vaisseau.
        /// </summary>
        public void ShootFromShip()
        {           
            if (_shipMissiles.Count == 0)
            {
                int missileX = ShipPositionX + ShipSymbol.Length / 2; // Centre du vaisseau
                int missileY = ShipPositionY - 1; // Au-dessus du vaisseau
                int missileSpeed = 2; // Vitesse du missile
                char missileSpeedChar = '|';
                _shipMissiles.Add(new Missile(missileX, missileY, missileSpeed, missileSpeedChar));
            }
        }

        /// <summary>
        /// Crée les ennemis à leur position initiale.
        /// </summary>
        private void CreateEnemies()
        {
            Random random = new Random();
            int numLines = 4; // Nombre de lignes d'ennemis
            int numEnemiesPerLine = 5; // Nombre d'ennemis par ligne
            int spacingX = 5; // Espacement horizontal entre les ennemis
            int spacingY = 2; // Espacement vertical entre les lignes d'ennemis

            enemyPositions.Clear();

            for (int line = 0; line < numLines; line++)
            {
                for (int i = 0; i < numEnemiesPerLine; i++)
                {
                    int x = _enemyPositionX + i * spacingX;
                    int y = _enemyPositionY + 3 + line * spacingY;
                    enemyPositions.Add(new int[] { x, y });

                    if (line == numLines - 1 && random.Next(0, 100) < 5)
                    {
                        _enemyMissiles.Add(new Position(x + _enemySymbol.Length / 2, y - 1, 1, '|', this));
                    }
                }
            }
        }

        /// <summary>
        /// Déplace les ennemis horizontalement et tire des missiles.
        /// </summary>
        private void MoveAndShootEnemies()
        {
            Random random = new Random();

            bool needToMoveDown = false; // Indicateur pour savoir si les ennemis doivent descendre d'une ligne

            for (int i = 0; i < enemyPositions.Count; i++)
            {
                int[] enemy = enemyPositions[i];

                if (_isright)
                {
                    if (enemy[0] + _enemySymbol.Length >= Console.WindowWidth)
                    {
                        _isright = false;
                        needToMoveDown = true; // Les ennemis doivent descendre
                        break; // Sortir de la boucle pour éviter de dépasser le bord
                    }
                }
                else
                {
                    if (enemy[0] <= 0)
                    {
                        _isright = true;
                        needToMoveDown = true; // Les ennemis doivent descendre
                        break; // Sortir de la boucle pour éviter de dépasser le bord
                    }
                }
            }

            for (int i = 0; i < enemyPositions.Count; i++)
            {
                int[] enemy = enemyPositions[i];

                if (_isright)
                {
                    enemy[0]++;
                }
                else
                {
                    enemy[0]--;
                }

                if (random.Next(0, 100) < 1)
                {
                    _enemyMissiles.Add(new Position(enemy[0] + _enemySymbol.Length / 2, enemy[1] + 1, 1, '|', this));
                }
            }

            if (needToMoveDown)
            {
                MoveEnemiesVertically();
            }

            foreach (var missile in _enemyMissiles.ToList())
            {
                missile.MoveEnemy(_enemyMissiles);

                foreach (var wall in Walls.ToList())
                {
                    if (missile.PositionX == wall.XPosition && missile.PositionY == wall.YPosition)
                    {
                        wall.Symbol = "";
                        missile.HasHitWall = true;
                    }
                }
            }
            foreach (var enemy in enemyPositions)
            {
                foreach (var wall in Walls)
                {
                    if (enemy[1] == wall.YPosition - 1)
                    {
                        GameOver();
                        return; // Arrêter le jeu
                    }
                }
            }
        }
       
        /// <summary>
        /// affiche le message et sortir de jeu
        /// </summary>
        public void GameOver()
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, 0);
            Console.WriteLine("GAME OVER");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, 1);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);
            Environment.Exit(0); // Terminer l'application
        }

        /// <summary>
        /// Déplace les ennemis verticalement (vers le bas).
        /// </summary>
        public void MoveEnemiesVertically()
        {
            for (int i = 0; i < enemyPositions.Count; i++)
            {
                enemyPositions[i][1]++;
            }
        }

        /// <summary>
        /// creer le jeu et affichage des enemies ,les mures et le ship
        /// </summary>
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
            Console.Write("Ship Lives: " + ShipLives);

            // Dessiner les murs
            foreach (var wall in Walls)
            {
                wall.Draw();
            }

            // Dessiner le vaisseau
            Console.SetCursorPosition(ShipPositionX, ShipPositionY);
            Console.Write(ShipSymbol);

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
            foreach (var enemy in enemyPositions)
            {
                missil.DrawEnemy();
            }
            foreach (var position in enemyPositions)
            {
                Console.SetCursorPosition(position[0], position[1]);
                Console.Write(_enemySymbol);
            }
          
            // Dessiner les ennemis
            foreach (var position in enemyPositions)
            {
                Console.SetCursorPosition(position[0], position[1]);
                Console.Write(_enemySymbol);
            }

        }

        /// <summary>
        /// creer le mures
        /// </summary>
        public void DrawWalls()
        {

            int[] wallXPositions = { 35, 50, 65 }; // Positions X des murs
            int[] wallYPositions = { Console.WindowHeight - 8 }; // Positions Y des murs

            // Parcourir toutes les positions y des murs
            foreach (int yPosition in wallYPositions)
            {
                // Parcourir toutes les positions x des murs
                foreach (int xPosition in wallXPositions)
                {
                    // Ajouter chaque mur à la liste des murs
                    Walls.Add(new Wall(xPosition, yPosition, "▌"));
                    Walls.Add(new Wall(xPosition + 1, yPosition, "▌"));
                    Walls.Add(new Wall(xPosition + 2, yPosition, "▌"));
                    Walls.Add(new Wall(xPosition + 3, yPosition, "▌"));
                    Walls.Add(new Wall(xPosition + 4, yPosition, "▌"));
                    Walls.Add(new Wall(xPosition + 5, yPosition, "▌"));
                    Walls.Add(new Wall(xPosition + 6, yPosition, "▌"));
                    Walls.Add(new Wall(xPosition + 7, yPosition, "▌"));
                    Walls.Add(new Wall(xPosition + 8, yPosition, "▌"));

                }
            }
        }


        /// <summary>
        /// Déplace les missiles du vaisseau.
        /// </summary>
        private void MoveShipMissiles()
        {
            foreach (var missile in _shipMissiles.ToList())
            {
                missile.Move();

                // Vérifier la collision avec le mur
                foreach (var wall in Walls.ToList())
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
            foreach (var missile in _shipMissiles.ToList())
            {
                missile.Move();

                // Vérifier si le missile est sorti de l'écran
                if (missile.PositionY < 0)
                {
                    _shipMissiles.Remove(missile);
                }
            }
        }

        /// <summary>
        /// Gère les collisions entre les missiles du vaisseau et les ennemis.
        /// </summary>
        private void HandleCollisionWithEnemies()
        {
            foreach (var missile in _shipMissiles.ToList())
            {
                foreach (var enemyPosition in enemyPositions.ToList())
                {
                    int enemyX = enemyPosition[0];
                    int enemyY = enemyPosition[1];

                    if (missile.PositionX >= enemyX && missile.PositionX < enemyX + _enemySymbol.Length && missile.PositionY == enemyY)
                    {
                        Console.SetCursorPosition(enemyX, enemyY);
                        Console.Write(" ");
                        enemyPositions.Remove(enemyPosition);
                        missile.HasHitWall = true;
                        break;
                    }
                }

                if (missile.HasHitWall || missile.PositionY < 0)
                {
                    _shipMissiles.Remove(missile);
                }
            }
        }
        
     }
}

/*diagramme UML
-----------------------------------
| SpaceShip |
-----------------------------------
| -ShipLives: int               |
| - _gameStartTime: DateTime |
| -Enemy: Enemy |
| -Position: Position |
| -Walls: List<Wall> |
| -Missile: Missile |
| -missil: Missile |
| -ShipPositionX: int           |
| - ShipPositionY: int           |
| - ShipSymbol: string           |
| - _shipMissiles: List<Missile> |
| -_enemyPositionX: int          |
| - _enemyPositionY: int          |
| - _enemySymbol: string          |
| - _enemyMissiles: List<Missile> |
| -_isright: bool                 |
| - enemyPositions: List<int[]> |
| -GameRunning: static bool     |
-----------------------------------
| +SpaceShip() |
| -CreateEnemies(): void         |
| - MoveAndShootEnemies(): void   |
| - HasGameTimeElapsed(): bool    |
| - HandleShipInput(): void       |
| - ShootFromShip(): void         |
| - CheckEnemyMissileCollisions():void |
| - GameOver(): void              |
| - MoveEnemiesVertically(): void |
| - Draw(): void                  |
| - DrawWalls(): void             |
| - MoveShipMissiles(): void      |
| - HandleCollisionWithEnemies(): void |
-----------------------------------
*/











using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Input;



///ETML
///Author:hanieh Mohajerani
///Date:18.01.2024
///Description:projet jeu space invarde     



namespace Projet_Hanieh_Mohajerani
{




    internal class SpaceShip
    {
       
        private DispatcherTimer _timer;

        //Ajouter un champ playerShip de type SpaceShip.
      
        private Missile missile; //Ajoutez un champ missile de type Missile

        
        public SpaceShip()
        {
            Thread.Sleep(70);
            // Création de la liste d'objets du jeu
            List<GameObject> gameObjects = new List<GameObject>();
            Draw(gameObjects);

            // Initialisation du DispatcherTimer
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(100); // Délai entre chaque tick (100 millisecondes)
            _timer.Tick += Timer_Tick;
            _timer.Start();

            Draw(gameObjects);
        }

        /// <summary>
        /// Champ pour le missile actuel
        /// </summary>
        private Missile _missile; // Champ pour le missile actuel
        /// <summary>
        /// 
        /// </summary>
        public int Lives { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private int _positionY = 25;
        /// <summary>
        /// 
        /// </summary>
        private int _positionX = 30;
        /// <summary>
        /// 
        /// </summary>
        private int _lives;
        /// <summary>
        /// 
        /// </summary>
        private string _shipSymbol = "<-->";
        /// <summary>
        /// 
        /// </summary>
        private int _playerSpeed;

        /// <summary>
        /// lives
        /// </summary>
        public int lives { get; set; }




        /// <summary>
        /// our constat will go left and write
        /// </summary>
        /// 
        // Gérer le tick du DispatcherTimer
        private void Timer_Tick(object sender, EventArgs e)
        {
            if(Console.KeyAvailable)
            {
                // Vérifier si la touche gauche est enfoncée
                if (Keyboard.IsKeyDown(Key.Left))
                {
                    // Déplacer le vaisseau spatial vers la gauche
                    MoveLeft();
                }

                // Vérifier si la touche droite est enfoncée
                if (Keyboard.IsKeyDown(Key.Right))
                {
                    // Déplacer le vaisseau spatial vers la droite
                    MoveRight();
                }
            }
           
        }

        private void MoveRight()
        {
            // Obtenir la largeur de la console
            int consoleWidth = Console.WindowWidth;

            if (_positionX + _shipSymbol.Length < consoleWidth)
            {
                // Effacer la position actuelle
                Write(" ", _positionX, _positionY);

                // Mettre à jour la position
                _positionX++;

                // Afficher le vaisseau spatial à la nouvelle position
                Write(_shipSymbol, _positionX, _positionY);
            }
        }

        // Déplacer le vaisseau spatial vers la gauche
        private void MoveLeft()
        {
            if (_positionX > 0)
            {
                // Effacer la position actuelle
                Write(" ", _positionX, _positionY);

                // Mettre à jour la position
                _positionX--;

                // Afficher le vaisseau spatial à la nouvelle position
                Write(_shipSymbol, _positionX, _positionY);
            }
        }
        void Draw(List<GameObject> gameObjects)
        {
            const string toWrite = "<-->"; // Character to write on-screen.

            int positionX = 25;
            int positionY = 50; // Contains current cursor position.

            bool limit;

            do
            {
                if (Console.KeyAvailable)
                {
                    var command = Console.ReadKey().Key;

                    switch (command)
                    {
                        case ConsoleKey.LeftArrow:
                            if (positionX > 0)
                            {
                                Write(" ", positionX, positionY);
                                positionX--;
                            }
                            break;

                        case ConsoleKey.RightArrow:
                            positionX++;
                            break;

                        case ConsoleKey.UpArrow:
                            if (positionY > 0)
                            {
                                // Tirer un missile vers le haut
                                Shoot(gameObjects, true);
                            }
                            break;

                        case ConsoleKey.Spacebar:
                            // Tirer un missile vers le haut lorsque la touche Espace est enfoncée
                            Shoot(gameObjects, true);
                            break;

                    }
                    // Afficher et mettre à jour la position des missiles
                    foreach (var gameObject in gameObjects)
                    {
                        if (gameObject is Missile)
                        {
                            Missile missile = (Missile)gameObject;
                            Write(missile._mission.ToString(), missile._positionX, missile._positionY);
                            missile.UpdatePosition(); // Mettre à jour la position du missile
                        }
                    }
                    Write(toWrite, positionX, positionY);
                }

            }
            while (IsAlive(true));
        }
        /// <summary>
        /// Wirite position of spaceship
        /// </summary>
        /// <param name="toWrite">Draws the layer</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void Write(string toWrite, int x = 0, int y = 0)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
            Console.Write(toWrite);


        }

        /// <summary>
        /// our live that is 3
        /// </summary>
        /// <param name="isAlive"></param>
        /// <param name="life"></param>
        /// <returns></returns>
        bool IsAlive(bool isAlive, int life = 3)
        {
            if (life > 0)
            { return true; }
            else
            { return false; }
           
        }

        internal class Missile : GameObject
        {
            private int _velocityY; // Vitesse verticale du missile

            public Missile(int positionX, int positionY, char mission, int velocityY)
            {
                _positionX = positionX;
                _positionY = positionY;
                _mission = mission;
                _life = 3;
                _velocityY = velocityY; // Définir la vitesse verticale du missile
            }

            // Méthode pour mettre à jour la position du missile
            public void UpdatePosition()
            {
                _positionY += _velocityY; // Mettre à jour la position verticale en fonction de la vitesse
            }
        }

        public void Shoot(List<GameObject> gameObjects, bool isPlayerMissile)
        {
            int velocityY = isPlayerMissile ? -1 : 1; // Définir la vitesse verticale en fonction de qui tire le missile

            // Vérifier s'il n'y a pas de missile actuellement ou si le missile existant est mort
            if (_missile == null || !_missile.IsAlive())
            {
                // Créer un nouveau missile positionné au milieu du vaisseau
                _missile = new Missile(_positionX, _positionY - 1, '|', velocityY); // Supposons que le vaisseau tire vers le haut

                // Ajouter le nouveau missile aux objets du jeu
                gameObjects.Add(_missile);
            }
        }

    }
}







    


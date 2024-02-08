using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


///ETML
///Author:hanieh Mohajerani
///Date:18.01.2024
///Description:projet jeu space invarde     



namespace Projet_Hanieh_Mohajerani
{




    internal class SpaceShip
    {
        //Ajouter un champ playerShip de type SpaceShip.
        private SpaceShip playerShip;
        private Missile missile; //Ajoutez un champ missile de type Missile

        public SpaceShip()
        {
            Thread.Sleep(70);
            Draw();
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
        void Draw()
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

        public void Shoot(List<GameObject> gameObjects)
        {
            // Vérifier s'il n'y a pas de missile actuellement ou si le missile existant est mort
            if (_missile == null || !_missile.IsAlive())
            {
                // Créer un nouveau missile positionné au milieu du vaisseau
                _missile = new Missile(_positionX, _positionY - 1 , '|'); // Supposons que le vaisseau tire vers le haut


                // Ajouter le nouveau missile aux objets du jeu
                gameObjects.Add(_missile);
            }
        }
        // Méthode Update pour mettre à jour le vaisseau spatial
        public void Update(List<GameObject> gameObjects)
        {
            // Vérifier si le joueur appuie sur la touche Espace
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    // Appeler la méthode Shoot si la touche Espace est enfoncée
                    Shoot(gameObjects);
                }
            }
        }

    }
}







    


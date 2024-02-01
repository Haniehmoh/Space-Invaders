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
        public SpaceShip()
        {
            Thread.Sleep(70);
            Draw();
        }

        /// <summary>
        /// 
        /// </summary>
        public int Lives { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private int _positionY = 10;
        /// <summary>
        /// 
        /// </summary>
        private int _positionX = 0;
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

     /*   public SpaceShip(int positionX, int positionY, string shipsymbol, int Playerspeed)
        {
            // Initialisation de la position initiale
            this._positionX = positionX;
            // Initialisation de la position initiale
            this._positionY = positionY;

            this._shipSymbol = shipsymbol;
            this._playerSpeed = Playerspeed;
        }
        public SpaceShip(int initialLives)
        {
            Lives = initialLives;
        }

        public void DeplacerVersLeBas()
        {
            // Déplacement vers le bas
            _positionY++;
        }

        public bool EstEnBasDeLaFenetre()
        {
            // Vérification si la balle est en bas de la fenêtre
            return _positionY >= Console.WindowHeight;
        }

        public void Afficher()
        {

            // Efface l'écran pour ne montrer qu'une balle à la fois
            Console.SetCursorPosition(_positionX, _positionY);
            Console.Write(_shipSymbol);
        }*/



        /*   public void Update()
           {
               if (Console.KeyAvailable)
               {
                var command = Console.ReadKey().Key;




       // Déplacement vers la gauche
       if (keyInfo.Key == ConsoleKey.LeftArrow)
                   {
                       _positionX -= _playerSpeed;

                       // Vérifier pour ne pas sortir de la zone de jeu à gauche
                       if (_positionX < 0)
                           _positionX = 0;
                   }
                   // Déplacement vers la droite
                   else if (keyInfo.Key == ConsoleKey.RightArrow)
                   {
                       _positionX += _playerSpeed;
           // Vérifier pour ne pas sortir de la zone de jeu à droite
           if (_positionX >= Game1.GameSize - _shipSymbol.Length)
               _positionX = Game1.GameSize - _shipSymbol.Length - 1;
                   }
               }
           else
           {
       // Si aucune touche n'est enfoncée, déplacez le vaisseau vers le bas
       DeplacerVersLeBas();
           }

           // Affiche le vaisseau à la nouvelle position
           Afficher();

               // Délai pour éviter le déplacement trop rapide
               System.Threading.Thread.Sleep(50);
           }
       }*/

        void Draw()
        {
            const string toWrite = "<-->"; // Character to write on-screen.

            int x = 0, y = 10; // Contains current cursor position.

            bool limit;

            do
            {
                if (Console.KeyAvailable)
                {
                    var command = Console.ReadKey().Key;

                    switch (command)
                    {
                        case ConsoleKey.LeftArrow:
                            if (x > 0)
                            {
                                Write(" ", x, y);
                                x--;
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            x++;
                            break;
                    }
                    Write(toWrite, x, y);
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

        bool IsAlive(bool isAlive, int life = 3)
        {
            if (life > 0)
            { return true; }
            else
            { return false; }
        }


    }


}







    


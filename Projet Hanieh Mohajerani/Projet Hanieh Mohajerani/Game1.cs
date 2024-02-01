using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projet_Hanieh_Mohajerani
{

    ///ETML
    ///Author:hanieh Mohajerani
    ///Date:18.01.2024
    ///Description:projet jeu space invarde     

    internal class Game1
    {

      
        // Ajoutez une propriété statique pour définir la taille du jeu
       /* public static int GameSize { get; } = 80;

        private SpaceShip playerShip;
        private List<SpaceShip> balles; // Utilisation de la liste pour stocker les objets du jeu

       public Game1()
        {
            // Initialisation du vaisseau spatial du joueur au milieu de la fenêtre avec 3 vies
            playerShip = new SpaceShip(Console.WindowWidth / 2, Console.WindowHeight - 1, "<-->", 3);

            // Initialisation de la liste des objets du jeu
            balles = new List<SpaceShip>();

            // Ajout du vaisseau spatial du joueur à la liste des objets du jeu
            balles.Add(playerShip);
           
        }*/


        // Le reste de votre code de la classe Game

        static void Main()
        {
            bool delay = true;
            Console.WriteLine(
            "SCORE <> " + "HI-SCORE " + "SCORE <>" + "\n" +
            "0000 " + "0000");

            {
                SpaceShip Player = new SpaceShip();
                Thread.Sleep(70);
            }

            // une liste appelée balles est créée pour stocker les objets de type SpaceShip
          /*  List<SpaceShip> balles = new List<SpaceShip>();



            //crée une boucle infinie qui continuera à s'exécuter jusqu'à ce que le programme soit arrêté.
            while (true)
            {



                // Écoute de l'entrée utilisateur
                if (Console.KeyAvailable)
                {

                    //Cette partie du code vérifie si une touche est enfoncée sur le clavier.
                    //Si c'est le cas, elle crée une nouvelle instance de la classe SpaceShip appelée playShip au milieu de la fenêtre avec le caractère '█' (code ASCII 178) et une vitesse de déplacement de 3.
                    //Si la touche appuyée est la barre d'espace 
                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    // l'objet playShip est ajouté à la liste balles.
                    SpaceShip playShip = new SpaceShip(Console.WindowWidth / 2, 70, "<-->", 3);




                    // Création de l'objet BalleQuiTombe lors de l'appui sur la touche Espace
                    if (keyInfo.Key == ConsoleKey.Spacebar)
                    {
                        //  SpaceShip nouvelleBalle = new SpaceShip(Console.WindowWidth / 2, 178,3);
                        balles.Add(playShip);
                    }
                }
              
                // Déplacement de chaque balle vers le bas
                foreach (SpaceShip balle in balles)
                {
                    balle.DeplacerVersLeBas();
                    // balle.DeplacerVersLeBas();
                    balle.Afficher();
                }

                // Destruction de la balle lorsqu'elle atteint le bas de la fenêtre
                balles.RemoveAll(balle => balle.EstEnBasDeLaFenetre());

               

                // Pause pour éviter une exécution trop rapide du jeu
                System.Threading.Thread.Sleep(100);

               // playerShip.Update();

            }*/

        }

      
    }

    }






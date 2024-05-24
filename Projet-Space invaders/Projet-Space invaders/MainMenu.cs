///ETML 
///Hanieh Mohajerani 
///date:18.01.2024
///Description:classe menu pour affichage de menu le jeu 
using System;

namespace Projet_Space_invaders
{
    /// <summary>
    /// class mainmenu
    /// </summary>
    class MainMenu
    {
        public static void Show()
        {
            Console.WriteLine("Bienvenue dans Space Invaders !");
            Console.WriteLine("1. Commencer une nouvelle partie");
            Console.WriteLine("2. Instructions");
            Console.WriteLine("3. Quitter");

            int choice;
            bool validChoice = false;

            do
            {
                Console.Write("Sélectionnez une option : ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out choice))
                {
                    if (choice >= 1 && choice <= 3)
                    {
                        validChoice = true;
                    }
                    else
                    {
                        Console.WriteLine("Choix invalide. Veuillez sélectionner une option valide.");
                    }
                }
                else
                {
                    Console.WriteLine("Entrée invalide. Veuillez saisir un nombre.");
                }
            } while (!validChoice);

            switch (choice)
            {
                case 1:
                    // Code pour démarrer une nouvelle partie
                    break;
                case 2:
                    ShowInstructions();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
        }

        private static void ShowInstructions()
        {
            Console.WriteLine("=== INSTRUCTIONS ===");
            Console.WriteLine("Déplacez votre vaisseau avec les touches fléchées gauche et droite.");
            Console.WriteLine("Utilisez la barre d'espace pour tirer des missiles.");
            Console.WriteLine("Évitez les missiles ennemis et détruisez les ennemis pour gagner des points.");
            Console.WriteLine("Appuyez sur une touche pour retourner au menu principal.");
            Console.ReadKey(true);
            Console.Clear();
            Show();
        }
    }
}

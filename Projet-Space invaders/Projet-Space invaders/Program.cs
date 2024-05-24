///ETML 
///Hanieh Mohajerani 
///date:18.01.2024
///Description:program de projet
using System;

namespace Projet_Space_invaders
{
    //class program
    class Program
    {

        // Début du programme

        [STAThread] // Indique que le thread d'application est conforme au modèle de thread STA (Single-Threaded Apartment)
        static void Main(string[] args)
        {
            MainMenu.Show();
            // Désactive le défilement vertical de la console
            Console.CursorVisible = false;
            // Définit la taille du buffer de la console pour correspondre à la taille de la fenêtre
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            // Crée une instance du vaisseau spatial
            SpaceShip spaceShip = new SpaceShip();
        }

    }
}


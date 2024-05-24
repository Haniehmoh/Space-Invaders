////ETML 
///Hanieh Mohajerani 
///date:18.01.2024
///Description:classe pour creer les missiles et gerer
using System;
using System.Collections.Generic;
using Projet_Space_invaders;
using System.Windows.Input;

namespace Projet_Space_invaders
{
    /// <summary>
    /// classe Missile
    /// </summary>
     class Missile 
    {
        /// <summary>
        /// Indique si le missile a touché un mur
        /// </summary>
        public bool HasHitWall { get;  set; }

        /// <summary>
        /// Position X du missile
        /// </summary>
        public int _positionX {  get; set; }

        /// <summary>
        /// Position Y du missile
        /// </summary>
        public int _positionY {  get; set; }

        /// <summary>
        /// Symbole représentant le missile
        /// </summary>
        public char _symbol = '|';

        /// <summary>
        /// Vitesse du missile
        /// </summary>
        public int _speed;

        /// <summary>
        /// Liste des missiles
        /// </summary>
        public List<Missile> _missiles = new List<Missile>();

        /// <summary>
        /// Getter pour la position Y
        /// </summary>
        public int PositionY { get { return _positionY; } } // Ajout d'un getter pour la position Y

        /// <summary>
        /// Getter pour la position X
        /// </summary>
        public int PositionX { get { return _positionX; } }

        /// <summary>
        /// Constructeur de la classe Missile
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        /// <param name="speed">Vitesse</param>
        /// <param name="symbol">Symbole</param>
        public Missile(int x, int y, int speed, char symbol )
        {
            _positionX = x;
            _positionY = y;
            _speed = speed;
            _symbol = symbol;
            HasHitWall = false; // Initialisation du booléen à false
        }

        /// <summary>
        /// Déplace le missile vers le bas
        /// </summary>
        /// <param name="missiles">Liste des missiles</param>
        public void MoveEnemy(List<Missile> missiles)
        {
            // Vérifier si le missile est toujours dans la zone d'affichage de la console
            if (_positionY >= 0 && _positionY < Console.WindowHeight &&
                _positionX >= 0 && _positionX < Console.WindowWidth)
            {
                // Effacer la position actuelle du missile
                Console.SetCursorPosition(_positionX, _positionY);
                Console.Write(' '); // Effacer le symbole du missile

                // Mettre à jour la position Y du missile
                _positionY += _speed;

                // Vérifier si le missile est sorti de l'écran
                if (_positionY >= Console.WindowHeight)
                {
                    // Supprimer le missile de la liste
                    missiles.Remove(this);
                }

                // Vérifier si le missile a touché un mur
                foreach (var missile in missiles)
                {
                    if (missile.PositionX == _positionX && missile.PositionY == _positionY)
                    {
                        missile.HasHitWall = true; // Indiquer que le missile a touché un mur
                        break;
                    }
                }
            }
        }



        /// <summary>
        /// Déplace le missile vers le haut
        /// </summary>
        public void Move()
        {
            _positionY -= _speed;
            if (PositionY < 0)
            {
                // Si le missile est sorti de la zone d'affichage, marquez-le pour être supprimé
                HasHitWall = true;
            }
        }

        /// <summary>
        /// Dessine le missile
        /// </summary>
        public void Draw()
        {
            // Dessiner le missile s'il est dans la zone d'affichage de la console
            if (PositionY >= 0 && PositionX >= 0)
            {
                Console.SetCursorPosition(PositionX, PositionY);
                Console.Write(_symbol);
            }
        }

        /// <summary>
        /// Met à jour la position du missile
        /// </summary>
        public void Update()
        {
            // Effacer la position actuelle du missile
            Console.SetCursorPosition(PositionX, PositionY);
            Console.Write(' ');

            // Déplacer le missile vers le haut
            Move();

            // Dessiner le missile à sa nouvelle position
            Draw();
        }


        /// <summary>
        /// Dessine le missile
        /// </summary>
        public void DrawMissile()
        {
            if (_positionY >= 0 && _positionX >= 0) // Vérifier que le missile est dans la zone d'affichage de la console
            {
                Console.SetCursorPosition(_positionX, _positionY);
                Console.Write(_symbol);
            }
        }

        /// <summary>
        /// Dessine le missile ennemi
        /// </summary>
        public void DrawEnemy()
        {
            if (_positionY >= 0 && _positionX >= 0) // Vérifier que le missile est dans la zone d'affichage de la console
            {
                Console.SetCursorPosition(_positionX, _positionY);
                Console.Write(_symbol);
            }
        }

        /// <summary>
        /// Dessine le missile ennemi
        /// </summary>
        public void DrawEnemyMissil()
        {
            if (_positionY >= 0 && _positionX >= 0) // Vérifier que le missile est dans la zone d'affichage de la console
            {
                Console.SetCursorPosition(_positionX, _positionY);
                Console.Write(_symbol);
            }
        }

     }
}

/*Diagramme UML
 * -----------------------------------
|            Missile              |
-----------------------------------
| - HasHitWall: bool              |
| - _positionX: int               |
| - _positionY: int               |
| - _symbol: char                 |
| - _speed: int                   |
| - _missiles: List<Missile>      |
-----------------------------------
| + PositionX: int                |
| + PositionY: int                |
-----------------------------------
| + Missile(int, int, int, char)  |
| + MoveEnemy(List<Missile>)      |
| + Move()                        |
| + Draw()                        |
| + Update()                      |
| + DrawMissile()                 |
| + DrawEnemy()                   |
| + DrawEnemyMissil()             |
-----------------------------------

  */

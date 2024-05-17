﻿////ETML 
///Hanieh Mohajerani 
///date:18.01.2024
///Description:jeu Space Invaders
using System;
using System.Collections.Generic;
using Projet_Space_invaders;
using System.Windows.Input;

namespace Projet_Space_invaders
{
    internal class Missile 
    {
        public bool HasHitWall { get;  set; }

        public int _positionX {  get; set; }

        public int _positionY {  get; set; }

        public char _symbol = '|';

        public int _speed;

        public List<Missile> _missiles = new List<Missile>();

        public int PositionY { get { return _positionY; } } // Ajout d'un getter pour la position Y

        public int PositionX { get { return _positionX; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="speed"></param>
        /// <param name="symbol"></param>
        public Missile(int x, int y, int speed, char symbol )
        {
            _positionX = x;
            _positionY = y;
            _speed = speed;
            _symbol = symbol;
            HasHitWall = false; // Initialisation du booléen à false
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="missiles"></param>
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



    public void Move()
        {
            _positionY -= _speed;
            if (PositionY < 0)
            {
                // Si le missile est sorti de la zone d'affichage, marquez-le pour être supprimé
                HasHitWall = true;
            }
        }

        public void Draw()
        {
            // Dessiner le missile s'il est dans la zone d'affichage de la console
            if (PositionY >= 0 && PositionX >= 0)
            {
                Console.SetCursorPosition(PositionX, PositionY);
                Console.Write(_symbol);
            }
        }


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
        /// 
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
        /// 
        /// </summary>
        public void DrawEnemy()
        {
            if (_positionY >= 0 && _positionX >= 0) // Vérifier que le missile est dans la zone d'affichage de la console
            {
                Console.SetCursorPosition(_positionX, _positionY);
                Console.Write(_symbol);
            }
        }
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

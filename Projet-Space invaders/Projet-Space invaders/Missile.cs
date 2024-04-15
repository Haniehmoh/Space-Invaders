using System;
using System.Collections.Generic;
using test_jeu;
using System.Windows.Input;
namespace test_jeu
{
    internal class Missile
    {
        public int _positionX {  get; set; }
        public int _positionY {  get; set; }
        public char _symbol = '|';
        private int _speed;
        private List<Missile> _missiles = new List<Missile>();
        public int PositionY { get { return _positionY; } } // Ajout d'un getter pour la position Y

        public int PositionX { get { return _positionX; } }
        public Missile(int x, int y, int speed, char symbol)
        {
            _positionX = x;
            _positionY = y;
            _speed = speed;
            _symbol = symbol;
        }

        public Missile(int initialX, int initialY, char symbol)
        {
            _positionX = initialX;
            _positionY = initialY;
            _symbol = symbol;
        }

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
            }
        }


        public void Move()
        {
            _positionY -= _speed;
            if (_positionY < 0) // Vérifier si le missile est sorti de la zone d'affichage
            {
                // Retirer le missile de la liste
                _missiles.Remove(this);
            }
        }


        public void DrawEnemy()
        {
            if (_positionY >= 0 && _positionY < Console.WindowHeight &&
                _positionX >= 0 && _positionX < Console.WindowWidth)
            {
                Console.SetCursorPosition(_positionX, _positionY);
                Console.Write(_symbol);
            }
        }

        public void Draw()
        {
            if (_positionY >= 0 && _positionX >= 0) // Vérifier que le missile est dans la zone d'affichage de la console
            {
                Console.SetCursorPosition(_positionX, _positionY);
                Console.Write(_symbol);
            }
        }

    }
}

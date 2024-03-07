using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_jeu
{
    internal class Missile
    {
        private string _enemie { get; set; }
        private int _positionXenemie { get; set; }
        private int _positionYenemie { get; set; }
        private int _positionX { get; set; }
        private int _positionY { get; set; }
        private char _symbol = '|';
        private int _speed;
        private List<Missile> _missiles = new List<Missile>();
       
        public Missile(int x, int y, int speed)
        {
            _positionX = x;
            _positionY = y;
            _speed = speed;
        }

        public Missile(int positionXenemie, int positionYenemie,string enemie , int speed, List<Missile> missiles) 
        {
           _positionYenemie = positionYenemie;
            _positionXenemie = positionXenemie;
            _enemie = enemie;
            _speed = speed;
            _missiles = missiles;
        }

      /*  public Missile(int positionXenemie, int positionYenemie)
        {
            this.positionXenemie = positionXenemie;
            this.positionYenemie = positionYenemie;
        }*/

        public void Move()
        {
            _positionY -= _speed; // Déplacer selon la vitesse définie
        }

        public void MoveEnemie()
        {
            _positionYenemie += _speed;
        }

        public void Draw()
        {
            if (_positionY >= 0) // Vérifier que le missile est dans la zone d'affichage de la console
            {
                Console.SetCursorPosition(_positionX, _positionY);
                Console.Write(_symbol);


            }

           /* if (_positionYenemie >= 0) // Vérifier que le missile est dans la zone d'affichage de la console
            {
                Console.SetCursorPosition(_positionXenemie, _positionXenemie);
                Console.Write(_enemie);
            }*/
        }

        public void DrawEnemie()
        {
             if (_positionYenemie >= 0) // Vérifier que le missile est dans la zone d'affichage de la console
             {
                 Console.SetCursorPosition(_positionXenemie, _positionXenemie);
                 Console.Write(_enemie);
             }
           

        }

    }
}


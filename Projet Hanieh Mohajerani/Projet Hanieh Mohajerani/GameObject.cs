using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Projet_Hanieh_Mohajerani
{

    internal class GameObject
    {
        private double vitesse; //Vitesse du missile
        private int lives; //Nombre de vie du missile
        public int _positionX;
        public int _positionY;
        public int _lives;
        private Missile _missile;
        public int _life;
        public char _mission= '|';
        public void Draw()
        {

        }
        public bool IsAlive()
        {

            return _life > 0;

        }
       
       
    }


    /// <summary>
    /// la classe Missile qui hérite de la classe GameObject
    /// </summary>
    internal class Missile : GameObject
    {
       
        public Missile(int positionX, int positionY,char mission)
        {
            _positionX = positionX;
            _positionY = positionY;
            _mission = mission;
           // _life = 3;
        }


    }
    
}
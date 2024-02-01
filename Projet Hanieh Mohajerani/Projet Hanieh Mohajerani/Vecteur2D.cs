using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Hanieh_Mohajerani
{
    internal class Vecteur2D
    {
        private double _x; //creer  position x
        private double _y; //creer  position y
        private int _imageWidth; //wight image
        private int _imageHeight; // heigh image
                                  // private Image spaceshipImage;
        private int spaceshipX;
        private int spaceshipY;


        public void DrawImage(double x, double y, int imageWith, int imageHeight)
        {
            // spaceshipImage = Properties.Resources.vaisseau;
            _x = x;
            _y = y;
            _imageWidth = imageWith;
            _imageHeight = imageHeight;

        }
    }
}

///ETML 
///Hanieh Mohajerani 
///date:18.01.2024
///Description:classe  pour tester les methodes dans le projet  Space Invaders
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projet_Space_invaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace TestUnitaire
{
    [TestClass]
    public class SpaceShipTests
    {
        [TestMethod]
        public void TestMissileConstructor()
        {
            // Arrange
            int x = 10;
            int y = 20;
            int speed = 2;
            char symbol = '|';

            // Act
            Missile missile = new Missile(x, y, speed, symbol);

            // Assert
            Assert.AreEqual(x, missile._positionX);
            Assert.AreEqual(y, missile._positionY);
            Assert.AreEqual(speed, missile.Speed);
            Assert.AreEqual(symbol, missile.Symbol);
            Assert.IsFalse(missile.HasHitWall);
        }

        [TestMethod]
        public void TestWallConstructor()
        {
            // Arrange
            int x = 5;
            int y = 10;
            string symbol = "▌";

            // Act
            Wall wall = new Wall(x, y, symbol);

            // Assert
            Assert.AreEqual(x, wall.XPosition);
            Assert.AreEqual(y, wall.YPosition);
            Assert.AreEqual(symbol, wall.Symbol);
        }
        [TestMethod]

        // Méthode de test pour tester le constructeur de Enemy
        public void TestEnemyConstructor()
        {
            // Arrange
            int xEnemy = 5;
            int yEnemy = 10;
            string symbol = "E"; // exemple de symbole

            // Act
            Enemy enemy = new Enemy(xEnemy, yEnemy, symbol);

            // Assert
            Assert.AreEqual(xEnemy, enemy.XPosition);
            Assert.AreEqual(yEnemy, enemy.YPosition);
            Assert.AreEqual(symbol, enemy.Symbol);
        }
    }
}

    

   


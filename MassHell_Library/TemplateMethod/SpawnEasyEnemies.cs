﻿using MassHell_Library.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassHell_Library.TemplateMethod
{
    public class SpawnEasyEnemies : EnemyConveoyr
    {
        private Random pos = new Random();
        public override bool needMages()
        {
            return false;
        }

        public override bool needNinjas()
        {
            return false;
        }

        public override bool needWarriors()
        {
            return true;
        }

        public override void spawnMages(out Tile position, out Item returningItem)
        {
            int heightRandom = pos.Next(1, 720);
            int widthRandom = pos.Next(1, 1280);
            position = new Tile(widthRandom, heightRandom, 0);

            returningItem = new Item();

            EnemyFacotry factory = new MageFactory();
            returningItem = factory.CreateEasy();
            returningItem.Name = "Mage";
        }

        public override void spawnNinjas(out Tile position, out Item returningItem)
        {
            int heightRandom = pos.Next(1, 720);
            int widthRandom = pos.Next(1, 1280);
            position = new Tile(widthRandom, heightRandom, 0);

            returningItem = new Item();

            EnemyFacotry factory = new NinjaFactory();
            returningItem = factory.CreateEasy();
            returningItem.Name = "Ninja";
        }

        public override void spawnWarriors(out Tile position, out Item returningItem)
        {
            int heightRandom = pos.Next(1, 720);
            int widthRandom = pos.Next(1, 1280);
            position = new Tile(widthRandom, heightRandom, 0);

            returningItem = new Item();

            EnemyFacotry factory = new WarriorFactory();
            returningItem = factory.CreateEasy();
            returningItem.Name = "Warrior";
        }
    }
}
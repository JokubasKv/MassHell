using Microsoft.AspNetCore.SignalR;
using MassHell_Library;
using System;

namespace MassHell_Server
{
    public class InnerHolderSingleton
    {
        private static bool instanance = false;
        private static Map map;

        private static class SingletonHolder
        {
            public static InnerHolderSingleton instance = new InnerHolderSingleton();
        }

        public InnerHolderSingleton()
        {
            map = this.CreateMap();
            instanance = true;
            //Console.WriteLine("Singleton initialized into Lazy holder");
        }

        public static InnerHolderSingleton getInstance()
        {
            if (instanance == false)
            {
                //Console.WriteLine("First call to InnerHolderSingleton getInstance()");
            }
            return SingletonHolder.instance;
        }

        public Map CreateMap()
        {
            // Add logic to add rows of tiles with correct coords
            // y = 0 x = 0...1280 then y = 80 x=...1280
            Map map = new Map(720, 1280);
            for (int i = 0; i < 144; i++)
            {
                Tile empty = new Tile();
                map.tiles.Add(empty);
            }
            return map;

        }
    }
}

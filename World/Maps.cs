using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using static SzűcstelepSlayers.Settings;
using Raylib_cs;

namespace SzűcstelepSlayers {
    public static class Maps {

        private static int Width => Settings.ScreenWidth;
        private static int Height => Settings.ScreenHeight;

        public static List<IGameObject> Map1() {

            List<IGameObject> GameObjects = new List<IGameObject>();

            // Főplatform - középen lent
            StaticBody2D MainGround = new StaticBody2D(
                new Vector2(Width * 0.5f, Height * 0.7f),
                new Vector2(Width * 0.7f, Height * 0.25f),
                Color.White
            );
            GameObjects.Add(MainGround);

            // Bal oldali platform - fent bal
            StaticBody2D SideTileLeft = new StaticBody2D(
                new Vector2(Width * 0.25f, Height * 0.35f),
                new Vector2(Width * 0.2f, Height * 0.03f),
                Color.White
            );
            GameObjects.Add(SideTileLeft);

            // Jobb oldali platform - fent jobb
            StaticBody2D SideTileRight = new StaticBody2D(
                new Vector2(Width * 0.75f, Height * 0.35f),
                new Vector2(Width * 0.2f, Height * 0.03f),
                Color.White
            );
            GameObjects.Add(SideTileRight);

            return GameObjects;
        
        }

    }
}

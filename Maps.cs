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

        public static List<IGameObject> Map1() {

            List<IGameObject> GameObjects = new List<IGameObject>();

            StaticBody2D MainGround = new StaticBody2D(
                new Vector2(ScreenWidth / 2, ScreenHeight - 200),
                new Vector2(ScreenWidth, 400),
                Color.Red
            );
            GameObjects.Add(MainGround);

            StaticBody2D SideTileRight = new StaticBody2D(
                new Vector2(
                    MainGround.Position.X + 750,
                    MainGround.Position.Y - MainGround.Size.Y
                ),
                new Vector2(500, 100),
                Color.Black
            );
            GameObjects.Add(SideTileRight);


            StaticBody2D SideTileLeft = new StaticBody2D(
                new Vector2(
                    MainGround.Position.X - 750,
                    MainGround.Position.Y - MainGround.Size.Y
                ),
                new Vector2(500, 100),
                Color.Black
            );
            GameObjects.Add(SideTileLeft);

            return GameObjects;
        }
    }
}

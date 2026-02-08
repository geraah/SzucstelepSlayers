using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using static SzűcstelepSlayers.Settings;

namespace SzűcstelepSlayers {
    public static class Maps {

        public static List<IGameObject> Map1() {
            List<IGameObject> GameObjects = new List<IGameObject>();

            StaticBody2D MainGround = new StaticBody2D(
                    new Vector2(ScreenWidth, ScreenHeight),
                    new Vector2(400, 300)
            );
            GameObjects.Add(MainGround);

            StaticBody2D SideTileRight = new StaticBody2D(
                new Vector2(
                    MainGround.Position.X + MainGround.Size.X * 2,
                    MainGround.Position.Y + MainGround.Size.Y * 2
                ),
                new Vector2(500, 100)
            );
            GameObjects.Add(SideTileRight);


            StaticBody2D SideTileLeft = new StaticBody2D(
                new Vector2(
                    MainGround.Position.X - MainGround.Size.X * 2,
                    MainGround.Position.Y - MainGround.Size.Y * 2
                ),
                new Vector2(500, 100)
            );
            GameObjects.Add(SideTileRight);

            return GameObjects;
        }
    }
}

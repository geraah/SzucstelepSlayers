using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;

namespace SzűcstelepSlayers {
    public class StaticBody2D : IGameObject {

        public Vector2 Position;
        public Vector2 Size;
        public Color BodyColor;

        public Vector2 TopLeft => Position - Size / 2;

        public StaticBody2D(Vector2 PositionValue, Vector2 SizeValue, Color BodyColorValue) {
            Position = PositionValue;
            Size = SizeValue;
            BodyColor = BodyColorValue;
        }

        public void Update() {
            
        }
        public void Draw() {

            Raylib.DrawRectangle(
                (int)TopLeft.X,
                (int)TopLeft.Y,
                (int)Size.X,
                (int)Size.Y,
                BodyColor
            );
            Console.WriteLine("Drawing static body");
        }
    }
}

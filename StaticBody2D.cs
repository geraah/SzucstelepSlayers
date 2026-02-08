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

        public StaticBody2D(Vector2 PositionValue, Vector2 SizeValue) {
            Position = PositionValue;
            Size = SizeValue;
        }

        public void Update() {
            
        }
        public void Draw() {

            Raylib.DrawRectangle(
                (int)(Position.X - Size.X) / 2,
                (int)(Position.Y - Size.Y) / 2,
                (int)Size.X, 
                (int)Size.Y, 
                BodyColor
            );
        }
    }
}

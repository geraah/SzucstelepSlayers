using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SzűcstelepSlayers {
    public class Button : IGameObject {

        private Text text;
        private Color BodyColor;
        private Vector2 Position;
        private Vector2 Size;
        private Action OnClick;

        private Vector2 TopLeft => Position - Size / 2;
        public Button(Font TextFont, string TextContent, int FontSize, Color TextColor, Color BodyColor, Vector2 Position, Vector2 Size, Action OnClick) {
            
            this.text = new Text(TextFont, TextContent, Position, FontSize, TextColor);
            this.BodyColor = BodyColor;
            this.Position = Position;
            this.Size = Size;
            this.OnClick = OnClick;

        }


        private bool IsMouseOver() {
            
            Vector2 MousePosition = Raylib.GetMousePosition();

            return MousePosition.X >= TopLeft.X && MousePosition.X <= TopLeft.X + Size.X &&
                 MousePosition.Y >= TopLeft.Y && MousePosition.Y <= TopLeft.Y + Size.Y;
            
        }
        
        public void Update() {

            if ( IsMouseOver() && Raylib.IsMouseButtonPressed(MouseButton.Left) ) OnClick();

        }

        public void Draw() {

            Raylib.DrawRectangle(
                (int)TopLeft.X,
                (int)TopLeft.Y,
                (int)Size.X,
                (int)Size.Y,
                BodyColor
            );

            text.Draw();
        }
    }
}

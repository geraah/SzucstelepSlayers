using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SzűcstelepSlayers {
    class Text : IGameObject {

        public Font TextFont;
        public string TextContent;
        public int FontSize;
        public Color TextColor;
        public Vector2 Position;
        private int TextWidth;

        public Color OutlineColor;
        public float OutlineThickness;

        public Text(Font TextFont, string TextContent, Vector2 Position, int FontSize, Color TextColor, float OutlineThickness = 0f, Color? OutlineColor = null) {
            
            this.TextFont = TextFont;
            this.TextContent = TextContent;
            this.Position = Position;
            this.FontSize = FontSize;
            this.TextColor = TextColor;

            this.OutlineThickness = OutlineThickness;
            this.OutlineColor = OutlineColor ?? Color.Black;

            TextWidth = (int)Raylib.MeasureTextEx(TextFont, TextContent, FontSize, 0).X;
        
        }

        public void SetContent(string newContent) {
            
            TextContent = newContent;
            TextWidth = (int)Raylib.MeasureTextEx(TextFont, TextContent, FontSize, 0).X;

        }

        private void DrawTextOutline(Vector2 DrawingPosition) {

            for (float i = -OutlineThickness; i <= OutlineThickness; i += OutlineThickness) {
                
                for (float j = -OutlineThickness; j <= OutlineThickness; j += OutlineThickness) {

                    if (i != 0 || j != 0) {

                        Raylib.DrawTextEx(TextFont, TextContent, new Vector2(DrawingPosition.X + i, DrawingPosition.Y + j), FontSize, 0, OutlineColor);
                        
                    }

                }
                
            }

        }

        public void Update() {
        }
        public void Draw() {

            Vector2 DrawingPosition = new Vector2(
                Position.X - TextWidth / 2,
                Position.Y - FontSize / 2
            );

            if (OutlineThickness > 0) DrawTextOutline(DrawingPosition);

            Raylib.DrawTextEx(TextFont, TextContent, DrawingPosition, FontSize, 0, TextColor);

        }

    }
}

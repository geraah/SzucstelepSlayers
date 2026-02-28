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

        public Text(Font TextFont, string TextContent, Vector2 Position, int FontSize, Color TextColor) {
            
            this.TextFont = TextFont;
            this.TextContent = TextContent;
            this.Position = Position;
            this.FontSize = FontSize;
            this.TextColor = TextColor;
            TextWidth = (int)Raylib.MeasureTextEx(TextFont, TextContent, FontSize, 0).X;
        
        }
        public void Update() {
            //TextContentDrawn      //Animated text
            //Thread.Sleep(100);    //Animated text
        }
        public void Draw() {

            Vector2 DrawingPosition = new Vector2(
                Position.X - TextWidth / 2,
                Position.Y - FontSize / 2
            );

            Raylib.DrawTextEx(TextFont, TextContent, DrawingPosition, FontSize, 0, TextColor);
        }

    }
}

using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzűcstelepSlayers {
    class Text : IGameObject {

        public string TextContent;
        public int FontSize;
        public Color TextColor;
        public int TextWidth;
        //string TextContentDrawn;  //Animated text

        public Text(string TextContentInput, int FontSizeInput, Color TextColorInput) {

            TextContent = TextContentInput;
            FontSize = FontSizeInput;
            TextColor = TextColorInput;
            TextWidth = Raylib.MeasureText(TextContent, FontSize);
        }
        public void Update() {
            //TextContentDrawn      //Animated text
            //Thread.Sleep(100);    //Animated text
        }
        public void Draw() {
            
            int Width = (Settings.ScreenWidth - TextWidth) / 2;
            int Height = (Settings.ScreenHeight - FontSize) / 2;

            Raylib.DrawText(TextContent, Width, Height, FontSize, TextColor);
        }

    }
}

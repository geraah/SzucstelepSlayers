using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;
using static SzűcstelepSlayers.Settings;

namespace SzűcstelepSlayers {
    public class Game {

        public List<IGameObject> GameObjects = new List<IGameObject>();
        
        public static Map map = new Map();

        public void LoadMap(int MapNumber) {

            switch (MapNumber) {
                case 1: 
                    map.Load(Maps.Map1());
                    break;
                default:
                    map.Load(Maps.Map1());
                    break;
            }

        }
        public void Update() {

            foreach (var GameObject in GameObjects) {
                GameObject.Update();
            }

        }
        public void Draw() {

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Brown);

            map.Draw();

            string text = "mi a cigány";
            int textWidth = Raylib.MeasureText(text, 20);
            Raylib.DrawText("mi a cigány", (ScreenWidth - textWidth) / 2, (ScreenHeight - 20) / 2, 20, Color.Black);

            foreach (var GameObject in GameObjects) {
                GameObject.Draw();
            }

            Raylib.EndDrawing();

        }
    }
}

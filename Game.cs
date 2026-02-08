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
        
        public Map map = new Map();

        public void LoadMap(int MapNumber) {

            switch (MapNumber) {
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

            foreach (var GameObject in GameObjects) {
                GameObject.Draw();
            }

            Raylib.EndDrawing();

        }
    }
}

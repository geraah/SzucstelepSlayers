using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SzűcstelepSlayers {
    public class Map {

        public List<IGameObject> MapObjects { get; private set; } = new List<IGameObject>();

        public void Load(List<IGameObject> MapObjects) {
            this.MapObjects = MapObjects;
        }
        
        public void Update() {
            foreach (var MapObject in MapObjects) {
                MapObject.Update();
            }
        }
        public void Draw() {
            foreach (IGameObject MapObject in MapObjects) {
                MapObject.Draw();   
            }
        }

    }
}

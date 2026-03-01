using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzűcstelepSlayers {
    public class Playing : IGameObject {

        private List<IGameObject> GameObjects;
        private Map map;

        private StateManager stateManager;

        public Playing(Map map, List<IGameObject> GameObjects, StateManager stateManager) {
            
            this.GameObjects = GameObjects;
            this.map = map;
            this.stateManager = stateManager;
            
        }


        public void Update() {

            map.Update();
            foreach (IGameObject GameObject in GameObjects) {
                GameObject.Update();
            }

        }
        public void Draw() {

            map.Draw();
            foreach (IGameObject GameObject in GameObjects) {
                GameObject.Draw();
            }

        }
    
    }
}

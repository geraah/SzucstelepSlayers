using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using System.Numerics;

namespace SzűcstelepSlayers {
    public class Controls : IGameObject {

        private StateManager stateManager;

        private Text ControlsTitle;

        public Controls(StateManager stateManager) {
            this.stateManager = stateManager;

            Vector2 ControlsTitlePosition = new Vector2(Settings.ScreenWidth / 2f, 300);
            ControlsTitle = new Text(Assets.PersonaFont, "Controls", ControlsTitlePosition, 200, Color.White);

        }

        public void Update() {
            if (Raylib.IsKeyPressed(KeyboardKey.Escape)) stateManager.ChangeState(GameState.StartMenu);
        }

        public void Draw() {

            ControlsTitle.Draw();
        
        }

    }
}

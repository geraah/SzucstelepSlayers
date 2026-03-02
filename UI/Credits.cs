using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;

namespace SzűcstelepSlayers {
    public class Credits : IGameObject{

        private StateManager stateManager;

        private Text CreditsTitle;

        private Background background;

        public Credits(StateManager stateManager) {
            
            this.stateManager = stateManager;

            Vector2 CreditsTitlePosition = new Vector2(Settings.ScreenWidth / 2f, 300);
            CreditsTitle = new Text(Assets.PersonaFont, "Credits", CreditsTitlePosition, 200, Color.White);

            background = new Background();

        }

        public void Update() {
            
            if (Raylib.IsKeyPressed(KeyboardKey.Escape)) stateManager.ChangeState(GameState.StartMenu);
        
        }

        public void Draw() {

            background.Draw();

            CreditsTitle.Draw();

        }

    }
}

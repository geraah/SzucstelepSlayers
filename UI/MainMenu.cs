using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;


namespace SzűcstelepSlayers {
    public class MainMenu : IGameObject {
        
        private Button StartButton;
        private Button ControlsButton;
        private Button CreditsButton;
        private Button ExitButton;

        private StateManager stateManager;

        private void StartButtonOnClick() {
            
            stateManager.ChangeState(GameState.Playing);
        
        }

        private void ControlsButtonOnClick() {

        }

        private void CreditsButtonOnClick() {

        }

        private void ExitButtonOnClick() {

            stateManager.ChangeState(GameState.Exit);

        }

        public MainMenu(StateManager stateManager) {
            
            this.stateManager = stateManager;

            Vector2 StartButtonPosition = new Vector2(Settings.ScreenWidth / 2f, Settings.ScreenHeight / 2f);
            Vector2 ControlsButtonPosition = new Vector2(Settings.ScreenWidth / 2f, Settings.ScreenHeight / 2f + 150);
            Vector2 CreditsButtonPosition = new Vector2(Settings.ScreenWidth / 2f, Settings.ScreenHeight / 2f + 300);
            Vector2 ExitButtonPosition = new Vector2(Settings.ScreenWidth / 2f, Settings.ScreenHeight / 2f + 450);
            
            Vector2 ButtonSize = new Vector2(300, 100);
            int ButtonFontSize = 100;

            StartButton = new Button(Assets.PersonaFont, "Start", ButtonFontSize, Color.White, Color.Black, StartButtonPosition, ButtonSize, StartButtonOnClick);
            ControlsButton = new Button(Assets.PersonaFont, "Controls", ButtonFontSize, Color.White, Color.Black, ControlsButtonPosition, ButtonSize, ControlsButtonOnClick);
            CreditsButton = new Button(Assets.PersonaFont, "Credits", ButtonFontSize, Color.White, Color.Black, CreditsButtonPosition, ButtonSize, CreditsButtonOnClick);
            ExitButton = new Button(Assets.PersonaFont, "Exit", ButtonFontSize, Color.White, Color.Black, ExitButtonPosition, ButtonSize, ExitButtonOnClick);
        
        }
        public void Update() {

            StartButton.Update();
            ControlsButton.Update();
            CreditsButton.Update();
            ExitButton.Update();

        }

        public void Draw() {
            Vector2 GameTitlePosition = new Vector2(Settings.ScreenWidth / 2f, 300);
            Text GameTitle = new Text(Assets.PersonaFont, "Sz\\''/cstelep Sl/-\\yers", GameTitlePosition, 200, Color.Black);
            
            GameTitle.Draw();

            StartButton.Draw();
            ControlsButton.Draw();
            CreditsButton.Draw();
            ExitButton.Draw();
        }


    }
}

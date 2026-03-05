using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;

namespace SzűcstelepSlayers {
    public class GameOver : IGameObject {

        private StateManager stateManager;

        private Text WinnerName;
        private Text WinnerLives;
        private Text MapName;

        private Button RestartButton;
        private Button MainMenuButton;

        private int Width => Settings.ScreenWidth;
        private int Height => Settings.ScreenHeight;


        public GameOver(StateManager stateManager) {

            this.stateManager = stateManager;

            Vector2 WinnerNamePosition = new Vector2(Width / 2, Height * 0.2f);
            Vector2 WinnerLivesPosition = new Vector2(Width / 2, Height * 0.4f);
            Vector2 MapNamePosition = new Vector2(Width / 2, Height * 0.5f);

            int WinnerNameFontSize = 200;
            int WinnerLivesFontSize = 100;
            int MapNameFontSize = 100;

            Color WinnerNameTextColor = Color.White;
            Color WinnerLivesTextColor = Color.White;
            Color MapNameTextColor = Color.White;

            float WinnerNameOutlineThickness = 20f;
            float WinnerLivesOutlineThickness = 7f;
            float MapNameOutlineThickness = 7f;

            Color WinnerNameOutlineColor = Color.Black;
            Color WinnerLivesOutlineColor = Color.Black;
            Color MapNameOutlineColor = Color.Black;

            WinnerName = new Text(Assets.PersonaFont, $"{MatchResults.WinnerName} wInS", WinnerNamePosition, WinnerNameFontSize, WinnerNameTextColor, WinnerNameOutlineThickness, WinnerNameOutlineColor);
            WinnerLives = new Text(Assets.PersonaFont, $"Lives: {MatchResults.WinnerLives}", WinnerLivesPosition, WinnerLivesFontSize, WinnerLivesTextColor, WinnerLivesOutlineThickness,  WinnerLivesOutlineColor);
            MapName = new Text(Assets.PersonaFont, $"Map: {MatchResults.MapName}", MapNamePosition, MapNameFontSize, MapNameTextColor, MapNameOutlineThickness, MapNameOutlineColor);

            int ButtonFontSize = 100;

            Vector2 RestartButtonPosition = new Vector2(Width / 2, Height * 0.65f);
            Vector2 MainMenuButtonPosition = new Vector2(Width / 2, Height * 0.75f);

            Vector2 RestartButtonSize = new Vector2(250, 100);
            Vector2 MainMenuButtonSize = new Vector2(300, 90);

            Action RestartButtonOnClick = () => stateManager.ChangeState(GameState.Playing);
            Action MainMenuButtonOnClick = () => stateManager.ChangeState(GameState.StartMenu);

            RestartButton = new Button(Assets.PersonaFont, "Restart", ButtonFontSize, Color.Black, Color.Blank, RestartButtonPosition, RestartButtonSize, RestartButtonOnClick, 7f, Color.White);
            MainMenuButton = new Button(Assets.PersonaFont, "Main Menu", ButtonFontSize, Color.Red, Color.Blank, MainMenuButtonPosition, MainMenuButtonSize, MainMenuButtonOnClick, 7f, Color.Black);

        }

        public void Update() {

            if (Raylib.IsKeyPressed(KeyboardKey.R)) stateManager.ChangeState(GameState.Playing);
            if (Raylib.IsKeyPressed(KeyboardKey.Escape)) stateManager.ChangeState(GameState.StartMenu);

            RestartButton.Update();
            MainMenuButton.Update();

        }

        public void Draw() {

            WinnerName.Draw();
            WinnerLives.Draw();
            MapName.Draw();

            RestartButton.Draw();
            MainMenuButton.Draw();

        }

    }
}

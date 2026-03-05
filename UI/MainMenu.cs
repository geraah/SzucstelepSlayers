using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;


namespace SzűcstelepSlayers {
    public class MainMenu : IGameObject {
        
        private StateManager stateManager;

        private Button StartButton = null!;
        private Button ControlsButton = null!;
        private Button CreditsButton = null!;
        private Button ExitButton = null!;

        private Text GameTitle = null!;

        private Background background = null!;

        private RenderTexture2D bgRenderTexture;

        private void InitButtons() {

            Vector2 StartButtonPosition = new Vector2(Settings.ScreenWidth / 2f, Settings.ScreenHeight / 2f);
            Vector2 ControlsButtonPosition = new Vector2(Settings.ScreenWidth / 2f, Settings.ScreenHeight / 2f + 150);
            Vector2 CreditsButtonPosition = new Vector2(Settings.ScreenWidth / 2f, Settings.ScreenHeight / 2f + 300);
            Vector2 ExitButtonPosition = new Vector2(Settings.ScreenWidth / 2f, Settings.ScreenHeight / 2f + 450);
            
            Vector2 ButtonSize = new Vector2(300, 100);
            int ButtonFontSize = 100;
            Color ButtonTextColor = Color.Black;
            float ButtonOutlineThickness = 0f;
            Color? ButtonOutlineColor = null;

            Action StartButtonOnClick = () => stateManager.ChangeState(GameState.Playing);
            Action ControlsButtonOnClick = () => stateManager.ChangeState(GameState.Controls);
            Action CreditsButtonOnClick = () => stateManager.ChangeState(GameState.Credits);
            Action ExitButtonOnClick = () => stateManager.ChangeState(GameState.Exit);

            StartButton = new Button(Assets.PersonaFont, "Start", ButtonFontSize, ButtonTextColor, Color.Blank, StartButtonPosition, ButtonSize, StartButtonOnClick, ButtonOutlineThickness, ButtonOutlineColor);
            ControlsButton = new Button(Assets.PersonaFont, "Controls", ButtonFontSize, ButtonTextColor, Color.Blank, ControlsButtonPosition, ButtonSize, ControlsButtonOnClick, ButtonOutlineThickness, ButtonOutlineColor);
            CreditsButton = new Button(Assets.PersonaFont, "Credits", ButtonFontSize, ButtonTextColor, Color.Blank, CreditsButtonPosition, ButtonSize, CreditsButtonOnClick, ButtonOutlineThickness, ButtonOutlineColor);
            ExitButton = new Button(Assets.PersonaFont, "Exit", ButtonFontSize, ButtonTextColor, Color.Blank, ExitButtonPosition, ButtonSize, ExitButtonOnClick, ButtonOutlineThickness, ButtonOutlineColor);
        
        }
        private void InitTitle() {

            Vector2 GameTitlePosition = new Vector2(Settings.ScreenWidth / 2f, 300);
            GameTitle = new Text(Assets.PersonaFont, "Sz\\''/cStEleP SlayerS", GameTitlePosition, 200, Color.Black);
            
        }

        private void InitBackground() {
            background = new Background();
        }

        private void InitTextContrastShader() {
            bgRenderTexture = Raylib.LoadRenderTexture(Settings.ScreenWidth, Settings.ScreenHeight);
        }

        public MainMenu(StateManager stateManager) {
            
            this.stateManager = stateManager;
            InitButtons();
            InitTitle();
            InitBackground();
            InitTextContrastShader();
        
        }

        public void Update() {

            if (Raylib.IsKeyPressed(KeyboardKey.G)) stateManager.ChangeState(GameState.GameOver);

            background.Update();

            StartButton.Update();
            ControlsButton.Update();
            CreditsButton.Update();
            ExitButton.Update();

        }

        public void Draw() {

            Raylib.BeginTextureMode(bgRenderTexture);
            Raylib.ClearBackground(Color.Red);

            background.Draw();
            
            Raylib.EndTextureMode();

            Raylib.DrawTextureRec(
                bgRenderTexture.Texture,
                new Rectangle(0, 0, bgRenderTexture.Texture.Width, -bgRenderTexture.Texture.Height),
                Vector2.Zero,
                Color.White
            );

            Raylib.BeginShaderMode(Assets.TextContrastShader);

            Vector2 resolution = new Vector2(Settings.ScreenWidth, Settings.ScreenHeight);
            Raylib.SetShaderValue(Assets.TextContrastShader, Assets.TextResoltionLocation, resolution, ShaderUniformDataType.Vec2);

            Raylib.SetShaderValueTexture(Assets.TextContrastShader, Assets.TextBgTextureLocation, bgRenderTexture.Texture);

            GameTitle.Draw();

            StartButton.Draw();
            ControlsButton.Draw();
            CreditsButton.Draw();
            ExitButton.Draw();

            Raylib.EndShaderMode();

        }

    }
}

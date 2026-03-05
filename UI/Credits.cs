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

        private Button EscapeButton;

        private Background background;

        private List<Text> creditLines = new List<Text>();

        public Credits(StateManager stateManager) {

            this.stateManager = stateManager;
            background = new Background();

            CreditsTitle = new Text(Assets.PersonaFont, "Credits", new Vector2(Settings.ScreenWidth / 2f, Settings.ScreenHeight * 0.1f), 200, Color.Red, 10f, Color.Black);
            
            Vector2 EscapeButtonSize = new Vector2(250, 110);
            Action EscapeButtonOnClick = () => stateManager.ChangeState(GameState.StartMenu);
            EscapeButton = new Button(Assets.PersonaFont, "Escape", 100, Color.Black, Color.Blank, new Vector2(Settings.ScreenWidth / 2f * 0.2f, Settings.ScreenHeight / 2f * 0.2f), EscapeButtonSize, EscapeButtonOnClick, 5f, Color.White);
            
            (string Role, string Name)[] credits = {

                ("Programming:", "Fodor GergO, Claude AI, Gemini"),
                ("Grafika & UI:", "Fodor GergO, Claude AI, Gemini"),
                ("Zene:", "nincsen"),
                ("Kulon koszonet:", "Claude AI, Gemini, "),
                ("", "es tanar urnak hogy feladta ezt a feladatot, nagyon elveztem"),
                ("Motor:", "C# & Raylib_cs")
                
            };

            float startY = Settings.ScreenHeight * 0.3f;
            float spacing = 90;

            for (int i = 0; i < credits.Length; i++) {
                
                float rowY = startY + (i * spacing);

                creditLines.Add(new Text(Assets.PersonaFont, credits[i].Role,
                    new Vector2(Settings.ScreenWidth / 2f * 0.48f, rowY), 100, Color.Gold, 5f, Color.Orange));

                creditLines.Add(new Text(Assets.PersonaFont, credits[i].Name,
                    new Vector2(Settings.ScreenWidth / 2f * 1.10f, rowY), 100, Color.White, 5f, Color.Black));

            }
        }

        public void Update() {

            EscapeButton.Update();

            if (Raylib.IsKeyPressed(KeyboardKey.Escape)) stateManager.ChangeState(GameState.StartMenu);

        }

        public void Draw() {

            background.Draw();
            CreditsTitle.Draw();
            EscapeButton.Draw();

            foreach (Text line in creditLines) {
                line.Draw();
            }
        }

    }
}

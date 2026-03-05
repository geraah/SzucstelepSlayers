using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Xml.Linq;

namespace SzűcstelepSlayers {
    public class Controls : IGameObject {

        private StateManager stateManager;

        private Text ControlsTitle;

        private List<Text> p1Lines = new List<Text>();
        private List<Text> p2Lines = new List<Text>();
        private List<Text> universalControls = new List<Text>();
        private List<Text> mechanicLines = new List<Text>();

        private Button EscapeButton;

        private string GetKeyName(KeyboardKey key) {

            string keyName = key.ToString();

            if (keyName.StartsWith("Kp")) return keyName.Replace("Kp", "NumPad ");
            if (keyName == "LeftControl") return "L-Ctrl";
            if (keyName == "RightControl") return "R-Ctrl";
            if (keyName == "LeftShift") return "L-Shift";
            if (keyName == "RightShift") return "R-Shift";

            return keyName;

        }

        public Controls(StateManager stateManager) {

            this.stateManager = stateManager;

            ControlsTitle = new Text(Assets.PersonaFont, "Controls", new Vector2(Settings.ScreenWidth / 2f, 80), 150, Color.White, 10f, Color.Beige);

            Vector2 EscapeButtonSize = new Vector2(250, 98);
            Action EscapeButtonOnClick = () => stateManager.ChangeState(GameState.StartMenu);
            EscapeButton = new Button(Assets.PersonaFont, "Escape", 100, Color.Red, Color.Blank, new Vector2(Settings.ScreenWidth / 2f, Settings.ScreenHeight * 0.2f), EscapeButtonSize, EscapeButtonOnClick, 5f, Color.Black);
            
            Text TabTitle = new Text(Assets.PersonaFont, "Tab:", new Vector2(Settings.ScreenWidth / 2f, Settings.ScreenHeight * 0.3f), 100, Color.Black, 10f, Color.Beige);
            Text TabTitleAction = new Text(Assets.PersonaFont, "Show names and lives", new Vector2(Settings.ScreenWidth / 2f, Settings.ScreenHeight * 0.39f), 100, Color.Beige, 5f, Color.Black);

            universalControls.Add(TabTitle);
            universalControls.Add(TabTitleAction);


            float column1X = Settings.ScreenWidth * 0.2f;
            float column2X = Settings.ScreenWidth * 0.8f;
            float startY = 150;

            p1Lines.Add(new Text(Assets.PersonaFont, "Player 1", new Vector2(column1X, startY), 100, Color.Purple, 2.5f, Color.Violet));

            (string Label, string Action)[] p1Controls = {
                ("Move:", $"{ GetKeyName(Settings.PlayerOneControls.Left) } / { GetKeyName(Settings.PlayerOneControls.Right) }"),
                ("Jump:", $"{ GetKeyName(Settings.PlayerOneControls.Jump) }"),
                ("Dash:", $"{ GetKeyName(Settings.PlayerOneControls.Dash) }"),
                ("Attack:", $"{ GetKeyName(Settings.PlayerOneControls.Attack) }")
            };

            for (int i = 0; i < p1Controls.Length; i++) {
                p1Lines.Add(new Text(Assets.PersonaFont, p1Controls[i].Label, new Vector2(column1X - 120, startY + 80 + (i * 70)), 80, Color.Black, 5f, Color.White));

                p1Lines.Add(new Text(Assets.PersonaFont, p1Controls[i].Action, new Vector2(column1X + 120, startY + 80 + (i * 70)), 80, Color.White, 5f, Color.Black));
            }


            p2Lines.Add(new Text(Assets.PersonaFont, "Player 2", new Vector2(column2X, startY), 100, Color.Beige, 2.5f, Color.Brown));

            (string Label, string Action)[] p2Controls = {
                ("Move:", $"{ GetKeyName(Settings.PlayerTwoControls.Left) } / { GetKeyName(Settings.PlayerTwoControls.Right) }"),
                ("Jump:", $"{ GetKeyName(Settings.PlayerTwoControls.Jump) }"),
                ("Dash:", $"{ GetKeyName(Settings.PlayerTwoControls.Dash) }"),
                ("Attack:", $"{ GetKeyName(Settings.PlayerTwoControls.Attack) }")
            };

            for (int i = 0; i < p2Controls.Length; i++) {
                p2Lines.Add(new Text(Assets.PersonaFont, p2Controls[i].Label, new Vector2(column2X - 120, startY + 80 + (i * 70)), 80, Color.Violet, 5f, Color.DarkPurple));
                p2Lines.Add(new Text(Assets.PersonaFont, p2Controls[i].Action, new Vector2(column2X + 120, startY + 80 + (i * 70)), 80, Color.White, 5f, Color.Black));
            }

            float mechY = 550;

            mechanicLines.Add(new Text(Assets.PersonaFont, "HunCutSagOK:", new Vector2(Settings.ScreenWidth / 2f, mechY), 100, Color.Black, 10f, Color.White));

            (string Name, string Desc)[] mechanics = {
                ("SUPER DASH:", "Press JUMP during a DASH for a massive horizontal boost!"),
                ("COYOTE TIME:", "You can still jump for a short time after walking off a ledge."),
                ("WALL JUMP:", "While sliding on a wall, press JUMP to kick away."),
                ("GRAVITY DIVE:", "Hold DOWN or ATTACK in the air to reach the ground faster."),
                ("VARIABLE JUMP:", "Hold JUMP to jump higher, release early for a short hop.")
            };

            for (int i = 0; i < mechanics.Length; i++) {

                float rowY = mechY + 100 + (i * 75);

                mechanicLines.Add(new Text(Assets.PersonaFont, mechanics[i].Name,
                    new Vector2(Settings.ScreenWidth / 2f * 0.3f, rowY), 80, Color.Red, 5f, Color.Black));

                mechanicLines.Add(new Text(Assets.PersonaFont, mechanics[i].Desc,
                    new Vector2(Settings.ScreenWidth / 2f * 1.2f, rowY), 80, Color.White, 5f, Color.Black));
            }

        }

        public void Update() {

            EscapeButton.Update();

            if (Raylib.IsKeyPressed(KeyboardKey.Escape) || Raylib.IsKeyPressed(KeyboardKey.Enter)) {
                stateManager.ChangeState(GameState.StartMenu);
            }
        }

        public void Draw() {

            ControlsTitle.Draw();

            EscapeButton.Draw();

            foreach (Text line in p1Lines) line.Draw();
            foreach (Text line in p2Lines) line.Draw();
            foreach (Text line in universalControls) line.Draw();
            foreach (Text line in mechanicLines) line.Draw();
            
        }
    }
}
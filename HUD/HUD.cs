using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;


namespace SzűcstelepSlayers {
    public class HUD : IGameObject {

        private List<Player> players;

        private bool ShowDetails => Raylib.IsKeyDown(Settings.ShowDetailsButton);

        private List<Text> playerNames;
        private List<Text> playerPercents;

        private int playerNameSize = 100;
        private int playerPercentSize = 70;
        private int playerLivesSize = 10;
        private int lineHeight = 80;
        

        public HUD(List<Player> players) {

            this.players = players;

            playerNames = new List<Text>();
            playerPercents = new List<Text>();

            foreach (Player player in players) {

                Text playerName = new Text(Assets.PersonaFont, player.Name, Vector2.Zero, playerNameSize, Color.White);
                Text playerPercent = new Text(Assets.SmashBrosFont, "0%", Vector2.Zero, playerPercentSize, Color.White);
                
                playerNames.Add(playerName);
                playerPercents.Add(playerPercent);

            }

        }

        private void DrawPlayerName(Text playerName, int centerX, int nameY) {

            if (!ShowDetails) return;

            playerName.Position = new Vector2(centerX, nameY);
            playerName.Draw();

        }

        private void DrawPlayerPercent(Player player, Text percent, int centerX, int percentY) {

            percent.SetContent($"{ (int)player.Damage }%");

            percent.Position = new Vector2(centerX, percentY);

            float damageRatio = MathF.Min(player.Damage / 150f, 1f);

            if (damageRatio < 0.5) {
                
                float percentColor = damageRatio * 2f;
                percent.TextColor = new Color((int)(255 * percentColor), 255, 0, 255);

            } else {

                float percentColor = (damageRatio - 0.5f) * 2f;
                percent.TextColor = new Color(255, (int)(255 * (1f - percentColor)), 0, 255);
            
            }

            percent.Draw();

        }

        private void DrawPlayerLives(Player player, int centerX, int livesY) {

            if (!ShowDetails) return;

            int padding = 26;
            int startX = centerX - (player.Lives * padding) / 2 + padding / 2;

            Color percentColor = Color.White;

            for (int i = 0; i < player.Lives; i++) {

                Raylib.DrawCircle(startX + i * padding, livesY, playerLivesSize, percentColor);

            }

        }

        private void DrawPlayerHUD(Player player, Text playerName, Text playerPercent) {

            int centerX = (int)player.Position.X;
            
            int nameY = (int)player.TopLeft.Y - lineHeight * 3;
            int percentY = (int)player.TopLeft.Y - lineHeight * 2;
            int livesY = (int)player.TopLeft.Y - lineHeight;

            DrawPlayerName(playerName, centerX, nameY);
            DrawPlayerPercent(player, playerPercent, centerX, percentY);
            DrawPlayerLives(player, centerX, livesY);

        }

        public void Update() {

        }

        public void Draw() {

            for (int i = 0; i < players.Count; i++) {

                DrawPlayerHUD(players[i], playerNames[i], playerPercents[i]);

            }

        }

    }
}

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

        private int livesPadding = 32;
        private float livesOutlineThickness = 1.5f;
        private Color livesOutlineColor = Color.Black;

        private int playerNameOffset = 0;
        private int playerPercentOffset = 15;
        private int playerLivesOffset = 0;

        public HUD(List<Player> players) {

            this.players = players;

            playerNames = new List<Text>();
            playerPercents = new List<Text>();

            foreach (Player player in players) {

                Text playerName = new Text(Assets.PersonaFont, player.Name, Vector2.Zero, playerNameSize, Color.White, 5, Color.Black);
                Text playerPercent = new Text(Assets.SmashBrosFont, "0%", Vector2.Zero, playerPercentSize, Color.White, 5, Color.Black);

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
            
            int otherChannels = (int)(255 * (1f - damageRatio));
            percent.TextColor = new Color(255, otherChannels, otherChannels);

            percent.Draw();

        }

        private void DrawPlayerLives(Player player, int centerX, int livesY) {

            if (!ShowDetails) return;

            int startX = centerX - (player.Lives * livesPadding) / 2 + livesPadding / 2;

            Color percentColor = Color.White;

            for (int i = 0; i < player.Lives; i++) {

                int DrawPosition = startX + i * livesPadding;

                if (livesOutlineThickness > 0) Raylib.DrawCircle(DrawPosition, livesY, playerLivesSize * livesOutlineThickness, livesOutlineColor);
                Raylib.DrawCircle(DrawPosition, livesY, playerLivesSize, percentColor);

            }

        }

        private void DrawPlayerHUD(Player player, Text playerName, Text playerPercent) {

            int centerX = (int)player.Position.X;
            
            int nameY = (int)player.TopLeft.Y - lineHeight * 3 + playerNameOffset;
            int percentY = (int)player.TopLeft.Y - lineHeight * 2 + playerPercentOffset;
            int livesY = (int)player.TopLeft.Y - lineHeight + playerLivesOffset;

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

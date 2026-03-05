using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;

namespace SzűcstelepSlayers {
    public class Playing : IGameObject {

        private List<IGameObject> GameObjects;
        private Map map;
        private StateManager stateManager;

        private List<Player> Players = null!;
        private HUD hud;

        private List<StaticBody2D> FilterPlatforms() {

            List<StaticBody2D> platforms = new List<StaticBody2D>();

            foreach (IGameObject GameObject in map.MapObjects) {

                if (GameObject is StaticBody2D staticBody) platforms.Add(staticBody);
                
            }

            return platforms;

        }

        public List<IDamageable> GetDamageables() {

            List<IDamageable> damageables = new List<IDamageable>();

            foreach (Player player in Players) damageables.Add(player);

            foreach (IGameObject GameObject in GameObjects) {

                if (GameObject is IDamageable damageable) damageables.Add(damageable);

            }

            return damageables;

        }

        private void InitPlayers(List<StaticBody2D> platforms) {

            Players = new List<Player>();

            Vector2 P1Position = new Vector2(Settings.ScreenWidth * 0.35f, Settings.ScreenHeight * 0.4f);
            Vector2 P2Position = new Vector2(Settings.ScreenWidth * 0.65f, Settings.ScreenHeight * 0.4f);

            Player P1 = new Player("P1", P1Position, Settings.PlayerOneControls, stateManager, platforms, this);
            Player P2 = new Player("P2", P2Position, Settings.PlayerTwoControls, stateManager, platforms, this);

            Players.Add(P1);
            Players.Add(P2);

        }

        public Playing(Map map, List<IGameObject> GameObjects, StateManager stateManager) {
            
            this.GameObjects = GameObjects;
            this.map = map;
            this.stateManager = stateManager;

            List<StaticBody2D> platforms = FilterPlatforms();
            InitPlayers(platforms);

            this.hud = new HUD(Players);

        }

        private Player FindWinner() {

            return Players.FirstOrDefault(player => player.Lives > 0) ?? Players.Last();

        }

        private void CheckDeathZone(Player player) {

            float deathThreshold = Settings.ScreenHeight + 200;
            float sideThreshold = 300;

            if (player.Position.Y > deathThreshold ||
                player.Position.X < -sideThreshold ||
                player.Position.X > Settings.ScreenWidth + sideThreshold) {

                Vector2 spawnPoint = new Vector2(Settings.ScreenWidth / 2, 200);
                player.Respawn(spawnPoint);

                int playersWithLives = 0;
                foreach (Player p in Players) {

                    if (p.Lives > 0) playersWithLives++;
                    
                }

                if (playersWithLives <= 1) { 

                    Player winner = FindWinner();

                    MatchResults.WinnerName = winner.Name;
                    MatchResults.WinnerLives = winner.Lives;
                    MatchResults.MapName = "SzUcstelep";
                        
                    stateManager.ChangeState(GameState.GameOver);

                }

            }

        }

        public void Update() {

            if (Raylib.IsKeyPressed(KeyboardKey.Escape)) stateManager.ChangeState(GameState.StartMenu);

            map.Update();
            foreach (IGameObject GameObject in GameObjects) GameObject.Update();
            foreach (Player player in Players) {

                player.Update();

                CheckDeathZone(player);

            } 

        }

        public void Draw() {

            map.Draw();
            foreach (IGameObject GameObject in GameObjects) GameObject.Draw();
            foreach (Player player in Players) player.Draw();
            hud.Draw();

        }
    
    }
}

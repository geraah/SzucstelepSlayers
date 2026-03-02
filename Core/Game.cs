using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;
using static SzűcstelepSlayers.Settings;

namespace SzűcstelepSlayers {
    public class Game {

        public StateManager stateManager = new StateManager();
        
        public List<IGameObject> GameObjects = new List<IGameObject>();
        public Map map = new Map();
        
        private MainMenu? mainMenu;
        private Controls? controls;
        private Credits? credits;
        private Playing? playing;
        
        private GameState currentState;

        public Game() {
            stateManager.OnStateChanged += OnStateChanged;
            stateManager.ChangeState(GameState.StartMenu);
        }


        void CreateOrNull(GameState newState) {

            mainMenu = null;
            controls = null;
            credits = null;
            playing = null;
            
            switch (newState) {

                case GameState.StartMenu:

                    mainMenu = new MainMenu(stateManager);
                    break;

                case GameState.Controls:

                    controls = new Controls(stateManager);
                    break;
                    
                case GameState.Credits:

                    credits = new Credits(stateManager);
                    break;

                case GameState.Playing:

                    playing = new Playing(map, GameObjects, stateManager);
                    break;

                case GameState.Paused:
                    
                    break;
                    
                case GameState.Options:
                    
                    break;
                    
            }

        }

        void OnStateChanged(GameState newState) {
            
            currentState = newState;

            CreateOrNull(currentState);
            
        }

        public void LoadMap(int MapNumber) {

            switch (MapNumber) {

                case 1:
                    
                    map.Load(Maps.Map1());
                    break;

                default:

                    map.Load(Maps.Map1());
                    break;

            }

        }
        public void Update() {

            if (currentState == GameState.Paused) return;

            mainMenu?.Update();
            controls?.Update();
            credits?.Update();
            playing?.Update();

        }
        public void Draw() {

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            mainMenu?.Draw();
            controls?.Draw();
            credits?.Draw();
            playing?.Draw();

            Raylib.EndDrawing();

        }
    }
}

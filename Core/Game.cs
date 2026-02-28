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
        
        public static Map map = new Map();
        public MainMenu mainMenu;
        
        private GameState currentState = GameState.StartMenu;

        public Game() {
            mainMenu = new MainMenu(stateManager);
            stateManager.OnStateChanged += OnStateChanged;
        }

        void OnStateChanged(GameState newState) {
            currentState = newState;
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


            switch (currentState) {

                case GameState.StartMenu:
                    
                    mainMenu.Update();
                    break;

                case GameState.Playing:
                    
                    map.Update();
                    
                    foreach (var GameObject in GameObjects) {
                        GameObject.Update();
                    }

                    break;

                case GameState.Paused:
                    
                    break;

                case GameState.Options:
                    
                    break;

                default:
                    
                    break;

            }


        }
        public void Draw() {

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Brown);


            switch (currentState) {

                case GameState.StartMenu:
                    
                    mainMenu.Draw();
                    break;
                
                case GameState.Playing:

                    map.Draw();

                    foreach (var GameObject in GameObjects) {
                        GameObject.Draw();
                    }

                    break;
                
                case GameState.Paused:
                    
                    break;
                
                case GameState.Options:
                
                    break;
                
                default:

                    break;

            }

            Raylib.EndDrawing();

        }
    }
}

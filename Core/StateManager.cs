using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzűcstelepSlayers {
    public class StateManager {
        public GameState CurrentState { get; private set;} = GameState.StartMenu;

        public event Action<GameState>? OnStateChanged;

        public void ChangeState(GameState newState) {

            CurrentState = newState;
            OnStateChanged?.Invoke(newState);
            
        }

    }
}

using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzűcstelepSlayers {
    public static class Settings {

        public static int ScreenWidth = 1920;
        public static int ScreenHeight = 1080;

        public static KeyboardKey ShowDetailsButton = KeyboardKey.Tab;

        public static PlayerControls PlayerOneControls = new PlayerControls {

            Left = KeyboardKey.A,
            Right = KeyboardKey.D,
            Up = KeyboardKey.W,
            Down = KeyboardKey.S,
            Jump = KeyboardKey.Space,
            Dash = KeyboardKey.LeftShift,
            Attack = KeyboardKey.L

        };
        public static PlayerControls PlayerTwoControls = new PlayerControls {

            Left = KeyboardKey.H,
            Right = KeyboardKey.K,
            Down = KeyboardKey.J,
            Jump = KeyboardKey.RightAlt,
            Dash = KeyboardKey.V,
            Attack = KeyboardKey.Kp4

        };
    
    }
}

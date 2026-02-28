using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace SzűcstelepSlayers {
    public static class Assets {

        public static Font PersonaFont;

        public static void Load() {
            PersonaFont = Raylib.LoadFontEx("Assets/Fonts/Expose-Regular.otf", 256, null, 0);
        }
    }
}

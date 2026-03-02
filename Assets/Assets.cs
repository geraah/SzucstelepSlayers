using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace SzűcstelepSlayers {
    public static class Assets {

        public static Font PersonaFont;

        public static Shader StarsShader;
        public static int StarsTimeLocation;
        public static int StarsResolutionLocation;

        public static Shader TextContrastShader;
        public static int TextBgTextureLocation;
        public static int TextResoltionLocation;

        public static void Load() {
            
            PersonaFont = Raylib.LoadFontEx("Assets/Fonts/Expose-Regular.otf", 256, null, 0);
            
            StarsShader = Raylib.LoadShader(null, "Assets/Shaders/stars.frag");
            StarsTimeLocation = Raylib.GetShaderLocation(StarsShader, "time");
            StarsResolutionLocation = Raylib.GetShaderLocation(StarsShader, "resolution");

            TextContrastShader = Raylib.LoadShader(null, "Assets/Shaders/text_contrast.frag");
            TextBgTextureLocation = Raylib.GetShaderLocation(TextContrastShader, "bgTexture");
            TextResoltionLocation = Raylib.GetShaderLocation(TextContrastShader, "resolution");

        }
    }
}

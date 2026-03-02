using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;

namespace SzűcstelepSlayers {
    public class Background : IGameObject {

        private float Width => Settings.ScreenWidth;
        private float Height => Settings.ScreenHeight;

        //private float ThinLineStart => Width * 0.30f;
        //private float ThinLineEnd => Width * 0.35f;
        //private float ThickLineStart => Width * 0.45f;
        //private float ThickLineEnd => Width * 0.55f;
        //private float StarAreaStart => Width * 0.65f;
        private float ThinLineStart => Width * 0.45f;
        private float ThinLineEnd => Width * 0.50f;
        private float ThickLineStart => Width * 0.55f;
        private float ThickLineEnd => Width * 0.65f;
        private float StarAreaStart => Width * 0.70f;

        private float Skew => Height * -0.45f;


        private RenderTexture2D starTexture;
        private float time = 0;

        private Texture2D whiteTexture;


        public Background() {

            starTexture = Raylib.LoadRenderTexture(Settings.ScreenWidth, Settings.ScreenHeight);

            whiteTexture = Raylib.LoadTextureFromImage(Raylib.GenImageColor(1, 1, Color.White));

        }

        public void DrawThinLine() {

            Raylib.DrawTriangle(
                new Vector2(ThinLineEnd, 0),
                new Vector2(ThinLineStart, 0),
                new Vector2(ThinLineStart + Skew, Height),
                Color.White
            );
            Raylib.DrawTriangle(
                new Vector2(ThinLineEnd, 0),
                new Vector2(ThinLineStart + Skew, Height),
                new Vector2(ThinLineEnd + Skew, Height),
                Color.White
            );

        }

        public void DrawThickLine() {

            Raylib.DrawTriangle(
                new Vector2(ThickLineEnd, 0),
                new Vector2(ThickLineStart, 0),
                new Vector2(ThickLineStart + Skew, Height),
                Color.White
            );
            Raylib.DrawTriangle(
                new Vector2(ThickLineEnd, 0),
                new Vector2(ThickLineStart + Skew, Height),
                new Vector2(ThickLineEnd + Skew, Height),
                Color.Black
            );

        }

        public void DrawStarArea() {

            //Raylib.BeginShaderMode(Assets.StarsShader);
            //    Raylib.DrawTexturePro(
            //        whiteTexture,
            //        new Rectangle(0, 0, 1, 1),
            //        new Rectangle(StarAreaStart, 0, Width - StarAreaStart, Height),
            //        Vector2.Zero,
            //        0,
            //        Color.White
            //    );
            //Raylib.EndShaderMode();

            Raylib.BeginShaderMode(Assets.StarsShader);

            Raylib.DrawTriangle(
                new Vector2(Width, 0),
                new Vector2(StarAreaStart, 0),
                new Vector2(StarAreaStart + Skew, Height),
                Color.White
            );
            Raylib.DrawTriangle(
                new Vector2(Width, 0),
                new Vector2(StarAreaStart + Skew, Height),
                new Vector2(Width, Height),
                Color.White
            );

            Raylib.EndShaderMode();


        }

        public void Update() {

            time += Raylib.GetFrameTime();

            Vector2 resolution = new Vector2(Settings.ScreenWidth, Settings.ScreenHeight);
            Raylib.SetShaderValue(Assets.StarsShader, Assets.StarsTimeLocation, time, ShaderUniformDataType.Float);
            Raylib.SetShaderValue(Assets.StarsShader, Assets.StarsResolutionLocation, resolution, ShaderUniformDataType.Vec2);

        }

        public void Draw() {

            DrawThinLine();
            DrawThickLine();
            DrawStarArea();

        }
    }
}

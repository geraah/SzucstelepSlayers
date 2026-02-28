using Raylib_cs;

namespace SzűcstelepSlayers;

internal class Program {
    // STAThread is required if you deploy using NativeAOT on Windows - See https://github.com/raylib-cs/raylib-cs/issues/301
    [System.STAThread]



    public static void Main() {

        Raylib.SetConfigFlags(ConfigFlags.UndecoratedWindow);
        Raylib.InitWindow(Settings.ScreenWidth, Settings.ScreenHeight, "Hoki");
        Assets.Load();

        Game game = new Game();
        game.LoadMap(1);

        while (game.stateManager.CurrentState != GameState.Exit) {

            game.Update();
            game.Draw();

        }

        Raylib.CloseWindow();

    }
}
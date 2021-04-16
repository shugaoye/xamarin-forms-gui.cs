namespace Terminal.Gui.Forms.Sample
{
    public class Program
    {
        public static System.Action running = MainApp;
        static void Main()
        {
            System.Console.OutputEncoding = System.Text.Encoding.Default;

            while (running != null)
            {
                running.Invoke();
            }
            Application.Shutdown();
        }

        public static void MainApp()
        {
            int margin = 0;

            // Application.UseSystemConsole = true;
            Application.Init();
            Forms.Init();
            var app = new App();
            var window = new FormsWindow("Xamarin.Forms gui.cs Backend");

            var top = Application.Top;
            var bottom = new Label("This should go on the bottom of the same top-level!");
            window.Add(bottom);
            var bottom2 = new Label("This should go on the bottom of another top-level!");
            top.Add(bottom2);

            top.LayoutComplete += (e) => {
                bottom.X = window.X;
                bottom.Y = Pos.Bottom(window) - Pos.Top(window) - margin;
                bottom2.X = Pos.Left(window);
                bottom2.Y = Pos.Bottom(window);
            };

            window.KeyPress += Win_KeyPress;

            window.LoadApplication(app);
            top.Add(CreateStatusBar());
            Application.Run();
        }

        static StatusBar CreateStatusBar()
        {
            var top = Application.Top;

            var statusBar = new StatusBar(new StatusItem[] {
                new StatusItem(Key.F1, "~F1~ Help", () => Help()),
                new StatusItem(Key.F2, "~F2~ Load", Load),
                new StatusItem(Key.F3, "~F3~ Save", Save),
                new StatusItem(Key.CtrlMask | Key.Q, "~^Q~ Quit", () => { if (Quit ()) { running = null; top.Running = false; } })
            });

            return statusBar;

            static bool Quit()
            {
                var n = MessageBox.Query(50, 7, "Quit Demo", "Are you sure you want to quit this demo?", "Yes", "No");
                return n == 0;
            }

            static void Help()
            {
                MessageBox.Query(50, 7, "Help", "This is a small help\nBe kind.", "Ok");
            }

            static void Load()
            {
                MessageBox.Query(50, 7, "Load", "This is a small load\nBe kind.", "Ok");
            }

            static void Save()
            {
                MessageBox.Query(50, 7, "Save", "This is a small save\nBe kind.", "Ok");
            }
        }
        private static void Win_KeyPress(View.KeyEventEventArgs e)
        {
            switch (ShortcutHelper.GetModifiersKey(e.KeyEvent))
            {
                case Key.CtrlMask | Key.T:
/*                    if (menu.IsMenuOpen)
                        menu.CloseMenu();
                    else
                        menu.OpenMenu();
*/                    e.Handled = true;
                    break;
            }
        }
    }
}
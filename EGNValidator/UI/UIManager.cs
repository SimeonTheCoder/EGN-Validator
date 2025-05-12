namespace UI
{
    public class UIManager
    {
        private static string Title = "\r\n\r\n _____ ____ _   _   _____ ___   ___  _     ____  \r\n| ____/ ___| \\ | | |_   _/ _ \\ / _ \\| |   / ___| \r\n|  _|| |  _|  \\| |   | || | | | | | | |   \\___ \\ \r\n| |__| |_| | |\\  |   | || |_| | |_| | |___ ___) |\r\n|_____\\____|_| \\_|   |_| \\___/ \\___/|_____|____/ \r\n\r\n";

        public void Clear()
        {
            Console.Clear();
        }

        public void RenderTitle()
        {
            Console.WriteLine(Title);
        }

        public void RenderMenu(UIMenu menu)
        {
            Console.WriteLine(menu);
        }

        public void Log(string text)
        {
            Console.WriteLine(text);
        }

        public void RenderSeparator()
        {
            Console.WriteLine("========================================");
        }

        public void NextLine()
        {
            Console.WriteLine("");
        }
    }
}
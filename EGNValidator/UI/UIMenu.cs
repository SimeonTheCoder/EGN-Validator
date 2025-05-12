using System.Text;

namespace UI
{
    public class UIMenu
    {
        private string title;
        private string[] options;

        public UIMenu(string title, string[] options)
        {
            this.Title = title;
            this.Options = options;
        }

        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public string[] Options
        {
            get { return this.options; }
            set { this.options = value; }
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine(this.Title);

            for (int i = 0; i < this.Options.Length; i++)
                sb.AppendLine($"{(i + 1)}. {this.Options[i]}");

            return sb.ToString();
        }
    }
}

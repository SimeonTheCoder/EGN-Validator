using System.ComponentModel.DataAnnotations;
using UI;
using ValidatorForEGN;

namespace ValidatorForEGN
{
    class Program
    {
        static int HandleInput(int input, EGNValidator validator, UIManager ui)
        {
            switch (input)
            {
                case 1:
                    UIValidateEGN(input, validator, ui);
                    break;

                case 2:
                    UIGenerateEGN(input, validator, ui);
                    break;

                case 3:

                    break;

                case 4:
                    return -1;

                default:
                    throw new ArgumentException("Invalid menu choice.");
            }

            return 0;
        }

        static void Main(string[] args)
        {
            EGNValidator validator = new EGNValidator();

            UIManager ui = new();

            string[] menuModeSelectOptions = new[]{
                "Validate EGN",
                "Generate all possible EGN",
                "Extract data from EGN",
                "Quit"
            };

            UIMenu selectModeMenu = new(
                "Select mode: ",
                menuModeSelectOptions
            );

            while (true)
            {
                ui.RenderTitle();
                ui.RenderMenu(selectModeMenu);

                int selected = int.Parse(Console.ReadLine());

                if (selected < 0 || selected > menuModeSelectOptions.Length)
                    throw new ArgumentException("Invalid menu choice.");

                ui.Clear();

                ui.NextLine();
                ui.RenderSeparator();
                ui.Log($"Selected: {menuModeSelectOptions[selected - 1]}");
                ui.RenderSeparator();
                ui.NextLine();

                int status = HandleInput(selected, validator, ui);

                if (status == -1) break;

                ui.NextLine();
                ui.RenderSeparator();

                string input = "";

                while (input != "y" && input != "n")
                {
                    try
                    {
                        ui.Log("Continue? (y/n)");
                        ui.RenderSeparator();

                        input = Console.ReadLine();

                        if ((input.Length == 0 || input.Length > 1) || (input != "y" && input != "n"))
                            throw new ArgumentException("Invalid answer!");
                    }
                    catch (ArgumentException ae)
                    {
                        ui.Clear();
                        ui.Log($"ERROR: {ae.Message}");
                        ui.NextLine();
                    }
                }

                if (input == "n")
                    break;

                ui.Clear();
            }

            ui.Clear();
            ui.Log("Sorry to see you go! :'(");
        }

        static void UIGenerateEGN(int input, EGNValidator validator, UIManager ui)
        {
            bool success = false;

            while (!success)
            {
                try
                {
                    ui.Log("Enter date:");
                    DateTime date = DateTime.Parse(Console.ReadLine());

                    ui.NextLine();

                    ui.Log("Enter city:");
                    string city = Console.ReadLine();

                    ui.NextLine();

                    ui.Log("Enter gender (male/female):");

                    string enteredGender = Console.ReadLine();

                    if (enteredGender != "male" && enteredGender != "female")
                        throw new ArgumentException("Invalid gender.");

                    bool isMale = (enteredGender == "male");

                    success = true;

                    ui.NextLine();
                    ui.RenderSeparator();
                    ui.Log("List of valid EGN:");
                    ui.RenderSeparator();
                    ui.NextLine();

                    string[] possibleEGN = validator.Generate(date, city, isMale);

                    foreach (var currEGN in possibleEGN)
                        Console.WriteLine(currEGN);
                }
                catch (Exception e)
                {
                    ui.Clear();
                    ui.Log($"ERROR: {e.Message}");
                    ui.NextLine();
                }
            }
        }

        static void UIValidateEGN(int input, EGNValidator validator, UIManager ui)
        {
            ui.Log("Enter EGN:");
            string egn = Console.ReadLine();

            ui.NextLine();

            ui.Log(validator.Validate(egn) ? "VALID" : "INVALID");
        }
    }
}
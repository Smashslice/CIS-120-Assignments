namespace _17__Files
{
    internal class Program
    {
        //After creating a csv file in the files sub folder with our .cs files, I set it to copy-if-newer, which creates a new vesion in files/bin/debug/net8.0 if there is not a file there or the file is older.
        //The program gets the csv from files/bin/debug/net8.0 from the below code. I also added that csv file to the project on TFS to keep it persistent. 
        //Kept the original blank file with copy-if-newer for redundancy.
        public static string fileName = "gameList.csv"; //Sets the file name to look for in the file path
        public static string filePath = Path.Combine(Environment.CurrentDirectory, fileName); //Concats two strings together to make a filepath readable by compiler. currentdir gets the files/bin/debug/net8.0 file path of the current project.
        static void Main(string[] args)
        { 
            List<Game> games = GetGames();

            bool continueRunning = true;
            while (continueRunning) //Continues running program until 4 is selected
            {
                Console.Clear();
                Console.WriteLine("--------------------\n1. Add a new game\n2. List all current games\n3. Search for a game\n4. Exit\n--------------------");
                string continueProgram = Console.ReadLine();

                switch (continueProgram)
                {
                    case "1":
                        Console.Clear();
                        games.Add(CreateGame());

                        break;
                    case "2":
                        Console.Clear();

                        foreach (var game in games)
                        {
                            Console.WriteLine(game);
                        }
                        Console.WriteLine("\nPress any key to return to menu.");
                        Console.ReadKey();

                        break;
                    case "3":
                        Console.Clear();

                        List<int> indexOfSearch = SearchGames(games);
                        Thread.Sleep(750);

                        for (int i = 0; i < indexOfSearch.Count; i++)
                        {
                            Console.WriteLine(games[indexOfSearch[i]]);
                        }
                        Console.WriteLine("All matches printed, press any key to return to menu.");
                        Console.ReadKey();

                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Saving data . . .");
                        Thread.Sleep(1000);
                        Console.WriteLine("50% complete . . .");

                        SaveGames(games);

                        Thread.Sleep(1500);
                        Console.WriteLine("Data saved . . .\nPress any key to exit.");
                        Console.ReadKey();

                        continueRunning = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Please enter 1, 2, 3 or 4.\nPress any key to return to menu.");
                        Console.ReadKey();
                        break;
                }
            }
        }
        /// <summary>
        /// Creates a new local instance of the class Game
        /// </summary>
        /// <returns>Game</returns>
        public static Game CreateGame()
        {
            Game newGame = new();
            newGame.name = HandleStringInput("Please enter the name of the game.");
            newGame.genre = HandleStringInput("Please enter the genre of the game.");
            newGame.developer = HandleStringInput("Please enter the Developer of the game.");
            newGame.releaseDate = HandleYearInput("Please enter the year the game was released.");
            newGame.activeDevelopment = HandleBoolInput("Is the game still in development? Yes/No.");
            newGame.gameProviders = HandleGameProviders();

            return newGame;
        }
        /// <summary>
        /// Handles general string input validation.
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public static string HandleStringInput(string prompt)
        {
            string output;
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine(prompt);

                    output = Console.ReadLine();
                    output = output.Replace(",", ""); //Replaces all comma's with blank text to avoid formatting errors in storing data to csv

                    break;
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press any key to return to last input menu.");
                    Console.ReadKey();
                }
            }
            return output;
        }
        /// <summary>
        /// Handles Year input validation for the Game class property
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns>int</returns>
        /// <exception cref="ArgumentException"></exception>
        public static int HandleYearInput(string prompt)
        {
            int input;
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine(prompt);

                    if (!int.TryParse(Console.ReadLine(), out input))
                    {
                        throw new ArgumentException("Invalid input. Please only input whole, positive numbers.");
                    }

                    if (input < 1958 || input > 2026)
                    {
                        throw new ArgumentException("Please input a valid year for the publish date. Accepted dates are 1958-2026");
                    }

                    break;
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press any key to return to last input menu.");
                    Console.ReadKey();
                }
            }
            return input;
        }
        /// <summary>
        /// Handles True/False statements from the user via Yes/No questions
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns>bool</returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool HandleBoolInput(string prompt)
        {
            string input;
            bool output = false;

            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine(prompt);

                    input = Console.ReadLine().ToLower();

                    if (input != "yes" && input != "no")
                    {
                        throw new ArgumentException("Invalid input. Please only enter Yes or No");
                    }

                    if (input == "yes")
                    {
                        output = true;
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press any key to return to last input menu.");
                    Console.ReadKey();
                }
            }
            return output;
        }
        /// <summary>
        /// Handles Game Providers for the Game class property
        /// </summary>
        /// <returns>string</returns>
        public static string HandleGameProviders()
        {
            string gameProviders = "blank";

            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("What platforms/providers is this game available on?\n--------------------");
                    Console.WriteLine("1. Steam PlayStation and Xbox\n2. Xbox exclusive\n3. Playstation Exclusive\n4. Steam exclusive\n5. Other");
                    
                    string input = Console.ReadLine();
                    bool continueMenu = true;

                    while (continueMenu)
                    {
                        switch (input)
                        {
                            case "1":
                                gameProviders = "Steam Playstation and Xbox";
                                continueMenu = false;

                                break;
                            case "2":
                                gameProviders = "Xbox exclusive";
                                continueMenu = false;

                                break;
                            case "3":
                                gameProviders = "Playstation Exclusive";
                                continueMenu = false;

                                break;
                            case "4":
                                gameProviders = "Steam exclusive";
                                continueMenu = false;

                                break;
                            case "5":
                                Console.Clear();
                                gameProviders = HandleStringInput("Please enter the platforms/providers where this game is avaiable.");
                                continueMenu = false;

                                break;
                            default:
                                Console.WriteLine("Please enter 1, 2, 3, 4 or 5.\nPress any key to return to input.");
                                Console.ReadKey();
                                break;
                        }
                    }

                    break;
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press any key to return to last input menu.");
                    Console.ReadKey();
                }
            }
            return gameProviders;
        }
        /// <summary>
        /// Gets properties for Game class, parses to readable data from a csv file and puts them in a list
        /// </summary>
        /// <returns>List Game</returns>
        public static List<Game> GetGames()
        {
            List<Game> gamesInput = new();

            StreamReader reader = new(filePath);

            while (!reader.EndOfStream)
            {
                string Line = reader.ReadLine();
                string[] lineData = Line.Split(",");

                Game game = new();

                game.name = lineData[0];
                game.genre = lineData[1];
                game.developer = lineData[2];
                game.releaseDate = int.Parse(lineData[3]);
                game.activeDevelopment = bool.Parse(lineData[4]);
                game.gameProviders = lineData[5];

                gamesInput.Add(game);
            }

            reader.Close();
            return gamesInput;
        }
        /// <summary>
        /// Saves properties from Game class, outputs to csv file and saves
        /// </summary>
        /// <param name="listToSave"></param>
        public static void SaveGames(List<Game> listToSave)
        {
            StreamWriter writer = new StreamWriter(filePath);
            foreach (var game in listToSave)
            {
                writer.WriteLine($"{game.name},{game.genre},{game.developer},{game.releaseDate},{game.activeDevelopment},{game.gameProviders}");
            }
            writer.Close();
        }
        /// <summary>
        /// Searches for a game in the list given by argument listToSearch. Adds the index of games matching search terms to list. Search options are Name, Developer or Genre.
        /// </summary>
        /// <param name="listToSearch"></param>
        /// <returns>List int</returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<int> SearchGames(List<Game> listToSearch)
        {
            string searchFor = "blank";
            bool foundGame = false;
            List<int> indexOfGames = new List<int>();

            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("How would you like to search for the game?\n---------------------\n1. Name\n2. Developer\n3. Genre");

                    string input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("Please enter the name of the game you wish to search for.");

                            searchFor = Console.ReadLine().ToLower();
                            for (int i = 0; i < listToSearch.Count; i++)
                            {
                                if (searchFor == listToSearch[i].name.ToLower()) 
                                {
                                    foundGame = true;
                                    indexOfGames.Add(i);
                                }
                            }
                            break;
                        case "2":
                            Console.Clear();
                            Console.WriteLine("Please enter the Developer of the game you wish to search for.");

                            searchFor = Console.ReadLine().ToLower();
                            for (int i = 0; i < listToSearch.Count; i++)
                            {
                                if (searchFor == listToSearch[i].developer.ToLower()) 
                                {
                                    foundGame = true;
                                    indexOfGames.Add(i);
                                }
                            }
                            break;
                        case "3":
                            Console.Clear();
                            Console.WriteLine("Please enter the Genre of the game you wish to search for.");

                            searchFor = Console.ReadLine().ToLower();
                            for (int i = 0; i < listToSearch.Count; i++)
                            {
                                if (searchFor == listToSearch[i].genre.ToLower()) 
                                {
                                    foundGame = true;
                                    indexOfGames.Add(i);
                                }
                            }
                            break;
                        default:
                            throw new ArgumentException("Invalid input. Accepted input is \"name\" or \"developer\"");
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press any key to return to last input menu.");
                    Console.ReadKey();
                }
            }
            if (!foundGame)
            {
                Console.Clear();
                Console.WriteLine("No matches found.\nPress any key to return to menu.");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"\nPrinting list of games matching {searchFor}, please wait.\n\n");
            }
            return indexOfGames;
        }
    }
}

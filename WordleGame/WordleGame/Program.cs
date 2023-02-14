using WordleProject;

namespace WordleGame
{
    class Start
    {
        static void Main(string[] args)

        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("--        My Wordle!           --");
            Console.WriteLine("-- Guess the Wordle in 6 tries --");
            Console.WriteLine("---------------------------------");
            Start.ComfirmStartGame();

        }
        public static void ComfirmStartGame()
        {
            // Stage 3: input "y" to play game or "n" to stop game
            Console.WriteLine("Would you like to play My Wordle [y|n]?");
            string comfirm = null;
            comfirm = Console.ReadLine();
            bool checkComfirm = true;
            if (comfirm == "y")
            {
                Game wordleGame = new Game();
                wordleGame.WordleGame();
            }
            else if (comfirm == "n")
            {
                Console.WriteLine("No worries... another time perhaps... :)");
                checkComfirm = false;
            }
            else // Stage 11 : just "y" and "n" to active game
            {
                ComfirmStartGame();
            }
        }
    }

    class Game
    {
        //Stage 1 random word
        static string GetWord()
        {
            wordList wordList = new wordList();
            var random = new Random();
            var list = new List<string>(wordList.words);
            int index = random.Next(list.Count);
            string wordle = list[index];
            Console.WriteLine("Wordle is: " + wordle);
            Console.WriteLine("-------------\n| - - - - - |");
            return wordle;
        }

        string wordToGuess = Game.GetWord();
        bool win = false;
        int turn = 0;
        char[] correctLetter = new char[10]; char[] used = new char[23];
        int posCorrect = 0; int posUsed = 0;
        public void WordleGame()
        {
            while (turn < 6) // Stage 9 : the limit of turnplay
            {
                Console.Write("\nPlease enter your guess - attempt" + (turn + 1) + " : ");
                string input = null;
                //Get input word
                wordList wordList = new wordList();
                input = Console.ReadLine();
                //Check if the input has 5 characters
                if (String.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please input your guess!");
                }
                else
                {
                    if (wordList.words.Contains(input))
                    {
                        turn++;
                        Console.WriteLine(input);
                        // Check input data equals keyword 
                        if (input == wordToGuess)
                        {
                            Console.WriteLine("\nSolved in  " + (turn) + " tries! Well done!");
                            win = true;
                            break;
                        }
                        else
                        {
                            int correctSpot = 0;
                            int wrongSpot = 0;
                            for (int i = 0; i < wordToGuess.Length; i++)
                            {
                                if (wordToGuess[i] == input[i])
                                {
                                    //Stage 4: Replace "^" to the correct letter (correct:  both letter and position)
                                    Console.Write('^');
                                    // Stage 7: count the correct spot letter
                                    correctSpot++;
                                    // Stage 8: array correct letters
                                    correctLetter[posCorrect] = input[i]; posCorrect++;
                                }
                                else
                                {
                                    if (wordToGuess.Contains(input[i]))
                                    {
                                        //Stage 5 Point out the position of the wrong word in the keyword
                                        Console.Write('*');
                                        // Stage 8: array used letters
                                        if (used.Contains(input[i]) == false) { used[posUsed] = input[i]; posUsed++; }
                                        // Stage 7: count the wrong spot letter
                                        wrongSpot++;
                                    }
                                    else
                                    {
                                        // Stage 6: Returns results with no words in the keyword 
                                        Console.Write('-');
                                        // Stage 8: array used letters
                                        if (used.Contains(input[i]) == false) { used[posUsed] = input[i]; posUsed++; }
                                    }

                                }
                                if (i == wordToGuess.Length - 1)
                                {
                                    Console.Write("");
                                }
                            }
                            //Print Correct spot And Wrong spot 
                            Console.WriteLine("\nCorrect spot(^): " + correctSpot + "\nWrong spot(*): " + wrongSpot);

                            // Print Correct letters
                            Console.Write("Correct letters: "); foreach (char c in correctLetter) { Console.Write(c + " "); }

                            // Print used letters
                            string resUsed = new string(used);
                            for (int c = 0; c < correctLetter.Length; c++) { if (used.Contains(correctLetter[c])) resUsed = resUsed.Replace(correctLetter[c], ' '); }
                            used = resUsed.ToCharArray();
                            Console.Write("\nUsed letters: "); foreach (char c in used) { if (c != ' ') Console.Write(c + " "); }

                            WordleGame();
                        }
                    }
                    else if (input.Length != wordToGuess.Length)
                    {
                        Console.WriteLine("Five letter words only please. Try again!");
                        WordleGame();
                    }
                    else //  the guess is not valid word
                    {
                        Console.WriteLine("Not word in list! Try again!");
                        WordleGame();
                    }
                }
            }
            // Stage 9 : the limit of turnplay
            Console.WriteLine("\nOh no! Better luck next time!\nThe wordle was: " + wordToGuess);
            Start.ComfirmStartGame();
        }
    }
}


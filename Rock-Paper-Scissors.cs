using System;

namespace RockPaperScissors
{
    class Program
    {
        enum Choice { Rock = 1, Paper, Scissors }

        static void Main()
        {
            var rand = new Random();

            Console.WriteLine("🎮 Rock, Paper, Scissors Game!");
            bool playAgain = true;

            while (playAgain)
            {
                Console.Write("Choose (1) Rock, (2) Paper, (3) Scissors: ");
                if (!int.TryParse(Console.ReadLine(), out int userInput)
                    || userInput < 1 || userInput > 3)
                {
                    Console.WriteLine("Invalid input. Try again.\n");
                    continue;
                }

                Choice user = (Choice)userInput;
                Choice cpu = (Choice)rand.Next(1, 4);

                Console.WriteLine($"You: {user}  |  CPU: {cpu}");

                if (user == cpu)
                    Console.WriteLine("It's a tie!");
                else if ((user == Choice.Rock && cpu == Choice.Scissors) ||
                         (user == Choice.Paper && cpu == Choice.Rock) ||
                         (user == Choice.Scissors && cpu == Choice.Paper))
                    Console.WriteLine("✅ You win!");
                else
                    Console.WriteLine("❌ You lose!");

                Console.Write("\nPlay again? (y/n): ");
                string ans = Console.ReadLine().Trim().ToLower();
                playAgain = (ans == "y" || ans == "yes");
                Console.WriteLine();
            }

            Console.WriteLine("Thanks for playing! Press Enter to exit.");
            Console.ReadLine();
        }
    }
}
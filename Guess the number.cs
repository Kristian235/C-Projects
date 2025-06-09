using System;

namespace GuessTheNumberApp
{
    class Program
    {
        static void Main()
        {
            var random = new Random();
            int target = random.Next(1, 101);  // 1–100 inclusive
            int guess = 0;
            int attempts = 0;

            Console.WriteLine("Welcome to Guess the Number!");
            Console.WriteLine("I'm thinking of a number between 1 and 100.\n");

            while (guess != target)
            {
                Console.Write("Enter your guess: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out guess) || guess < 1 || guess > 100)
                {
                    Console.WriteLine("❗ Invalid input. Please enter a number between 1 and 100.\n");
                    continue;
                }

                attempts++;

                if (guess < target)
                    Console.WriteLine("📈 Too low!\n");
                else if (guess > target)
                    Console.WriteLine("📉 Too high!\n");
                else
                    Console.WriteLine($"🎉 Correct! You guessed {target} in {attempts} attempts.\n");
            }

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}
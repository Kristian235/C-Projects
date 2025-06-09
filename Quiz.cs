using System;
using System.Collections.Generic;

namespace ConsoleQuizApp
{
    class QuizQuestion
    {
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
        public int CorrectIndex { get; set; }

        public bool CheckAnswer(int choice) => choice == CorrectIndex;
    }

    class Program
    {
        static void Main()
        {
            var questions = new List<QuizQuestion>
            {
                new QuizQuestion {
                    QuestionText = "What is the capital of France?",
                    Options = new List<string>{"Berlin", "Madrid", "Paris", "Rome"},
                    CorrectIndex = 2
                },
                new QuizQuestion {
                    QuestionText = "Which planet is known as the Red Planet?",
                    Options = new List<string>{"Earth", "Jupiter", "Mars", "Venus"},
                    CorrectIndex = 2
                },
                new QuizQuestion {
                    QuestionText = "What is the largest ocean on Earth?",
                    Options = new List<string>{"Atlantic", "Indian", "Arctic", "Pacific"},
                    CorrectIndex = 3
                },
            };

            int score = 0;

            for (int i = 0; i < questions.Count; i++)
            {
                var q = questions[i];
                Console.WriteLine($"Q{i + 1}. {q.QuestionText}");
                for (int j = 0; j < q.Options.Count; j++)
                    Console.WriteLine($"  {j + 1}. {q.Options[j]}");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice)
                       || choice < 1 || choice > q.Options.Count)
                    Console.Write("Please enter a valid number: ");

                if (q.CheckAnswer(choice - 1))
                {
                    Console.WriteLine("✅ Correct!\n");
                    score++;
                }
                else
                    Console.WriteLine($"❌ Incorrect. Correct answer is: {q.Options[q.CorrectIndex]}\n");
            }

            Console.WriteLine($"Quiz finished! 🎉 You scored {score} of {questions.Count}.");
            Console.Write("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}
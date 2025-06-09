using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class TaskItem
{
    public string Id { get; set; }
    public string Description { get; set; }
}

class KanbanBoard
{
    public string ProjectName { get; set; }
    public List<TaskItem> ToDo { get; set; } = new();
    public List<TaskItem> InProgress { get; set; } = new();
    public List<TaskItem> Done { get; set; } = new();
    public string SaveFile { get; set; }

    public void AddTask(TaskItem task)
    {
        if (ToDo.Count < 10)
            ToDo.Add(task);
        else
            Console.WriteLine("To Do колоната е пълна!");
    }

    public void MoveTaskForward(string column, int index)
    {
        List<TaskItem> source = column switch
        {
            "ToDo" => ToDo,
            "InProgress" => InProgress,
            _ => null
        };

        List<TaskItem> target = column switch
        {
            "ToDo" => InProgress,
            "InProgress" => Done,
            _ => null
        };

        if (source == null || target == null) return;
        if (index < 0 || index >= source.Count) return;

        if (target.Count >= 10 && column != "InProgress")
        {
            Console.WriteLine("Следващата колона е пълна!");
            return;
        }

        TaskItem task = source[index];
        source.RemoveAt(index);

        if (column == "InProgress" && Done.Count >= 10)
            Done.RemoveAt(0); // Remove oldest if Done is full

        target.Add(task);
    }

    public void Save()
    {
        var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(SaveFile, json);
    }

    public static KanbanBoard Load(string fileName)
    {
        var json = File.ReadAllText(fileName);
        var board = JsonSerializer.Deserialize<KanbanBoard>(json);
        board.SaveFile = fileName;
        return board;
    }
}

class Program
{
    static KanbanBoard board;

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Clear();

        Console.WriteLine("Изберете опция:");
        Console.WriteLine("1. Нова дъска");
        Console.WriteLine("2. Зареди дъска");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            Console.Write("Име на проекта: ");
            string name = Console.ReadLine();
            Console.Write("Име на файл за запазване: ");
            string file = Console.ReadLine();

            board = new KanbanBoard
            {
                ProjectName = name,
                SaveFile = file
            };
        }
        else if (choice == "2")
        {
            Console.Write("Име на файл: ");
            string file = Console.ReadLine();
            if (File.Exists(file))
                board = KanbanBoard.Load(file);
            else
            {
                Console.WriteLine("Файлът не съществува!");
                return;
            }
        }

        Run();
    }

    static void Run()
    {
        while (true)
        {
            Console.Clear();
            PrintBoard();
            Console.WriteLine("[F1] Добави | [F5] Запази | [F6] Зареди | [F7] Премести | [ESC] Изход");

            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.F1)
                AddTask();
            else if (key == ConsoleKey.F5)
                board.Save();
            else if (key == ConsoleKey.F6)
            {
                Console.Write("Име на файл: ");
                var file = Console.ReadLine();
                board = KanbanBoard.Load(file);
            }
            else if (key == ConsoleKey.F7)
                MoveTask();
            else if (key == ConsoleKey.Escape)
            {
                board.Save();
                break;
            }
        }
    }

    static void AddTask()
    {
        if (board.ToDo.Count >= 10)
        {
            Console.WriteLine("To Do е пълна. Натисни клавиш за продължение...");
            Console.ReadKey();
            return;
        }

        Console.Write("Task ID: ");
        string id = Console.ReadLine();
        Console.Write("Описание: ");
        string desc = Console.ReadLine();

        board.AddTask(new TaskItem { Id = id, Description = desc });
    }

    static void MoveTask()
    {
        Console.Write("От коя колона? (ToDo/InProgress): ");
        string col = Console.ReadLine();

        Console.Write("Номер на задача: ");
        if (int.TryParse(Console.ReadLine(), out int num))
        {
            board.MoveTaskForward(col, num - 1);
        }
        else
        {
            Console.WriteLine("Невалиден номер!");
        }
    }

    static void PrintBoard()
    {
        Console.WriteLine($"Проект: {board.ProjectName}");
        Console.WriteLine(new string('-', 70));
        Console.WriteLine($"| {"To Do",-20} | {"In Progress",-20} | {"Done",-20} |");
        Console.WriteLine(new string('-', 70));

        for (int i = 0; i < 10; i++)
        {
            string todo = i < board.ToDo.Count ? $"{i + 1}. {board.ToDo[i].Id}" : "";
            string inprog = i < board.InProgress.Count ? $"{i + 1}. {board.InProgress[i].Id}" : "";
            string done = i < board.Done.Count ? $"{i + 1}. {board.Done[i].Id}" : "";

            Console.WriteLine($"| {todo,-20} | {inprog,-20} | {done,-20} |");
        }

        Console.WriteLine(new string('-', 70));
    }
}
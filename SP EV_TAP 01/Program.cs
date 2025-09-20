using System;
using System.Diagnostics;

List<string> blackList = new List<string>();

while (true)
{
    Console.WriteLine("\n==== Console Task Manager ====");
    Console.WriteLine("1. Get List all Process");
    Console.WriteLine("2. Start process by Name");
    Console.WriteLine("3. Kill process by Id");
    Console.WriteLine("4. Kill processes by Name");
    Console.WriteLine("5. Add Black list");
    Console.WriteLine("6. Remove from Black list");
    Console.WriteLine("7. Show Black list");
    Console.WriteLine("8. Exit");
    Console.Write("Choose option: ");

    string? choice = Console.ReadLine();
    Console.Clear();

    switch (choice)
    {
        case "1": GetAllProcesses(); break;
        case "2": StartProcessByName(); break;
        case "3": KillProcessById(); break;
        case "4": KillProcessByName(); break;
        case "5": AddToBlacklist(); break;
        case "6": RemoveFromBlacklist(); break;
        case "7": ShowBlacklist(); break;
        case "8": thankYouMessage(); return;
        default: Console.WriteLine("Invalid choice!"); break;
    }
}
void GetAllProcesses()
{
    var processes = Process.GetProcesses();
    Console.WriteLine("=== Running Processes ===\n");
    foreach (var process in processes)
    {
        Console.WriteLine($"{process.Id} - {process.ProcessName} -> Threads: {process.Threads.Count}");
    }
}

void StartProcessByName()
{
    Console.Write("Enter process name ( Example: calc ): ");
    string? name = Console.ReadLine();

    if (blackList.Contains(name))
    {
        Console.WriteLine($"{name} is in blacklist, cannot be started!");
        return;
    }

    try
    {
        Process.Start(name);
        Console.WriteLine($"{name} started successfully!");
    }
    catch
    {
        Console.WriteLine("Could not start process!");
    }
}

void KillProcessById()
{
    Console.Write("Enter Process Id: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        try
        {
            var process = Process.GetProcessById(id);
            if (blackList.Contains(process.ProcessName))
            {
                Console.WriteLine($"Process {process.ProcessName} is in blacklist and cannot be killed!");
                return;
            }

            process.Kill();
            Console.WriteLine($"Process {id} killed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    else Console.WriteLine("Invalid Id!");
}

void KillProcessByName()
{
    Console.Write("Enter Process Name: ");
    string name = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(name)) return;

    try
    {
        var processes = Process.GetProcessesByName(name);
        foreach (var process in processes)
        {
            if (blackList.Contains(process.ProcessName))
            {
                Console.WriteLine($"Process {process.ProcessName} is in blacklist and cannot be killed!");
                continue;
            }
            process.Kill();
            Console.WriteLine($"Killed {process.ProcessName} ({process.Id})");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

void AddToBlacklist()
{
    Console.Write("Enter process name to blacklist: ");
    string name = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(name) && !blackList.Contains(name))
    {
        blackList.Add(name);
        Console.WriteLine($"{name} added to blacklist.");
    }
}

void RemoveFromBlacklist()
{
    ShowBlacklist();
    Console.Write("Enter process name to remove from blacklist: ");
    string? name = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(name) && blackList.Contains(name))
    {
        blackList.Remove(name);
        Console.WriteLine($"{name} removed from blacklist.");
    }
}

void ShowBlacklist()
{
    
    Console.WriteLine("=== Blacklist ===");
    if (blackList.Count == 0)
        Console.WriteLine("Empty!");
    else
        foreach (var proc in blackList)
            Console.WriteLine(proc);
}

void thankYouMessage()
{

    Console.Clear();

    ConsoleColor[] colors = {
        ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Magenta,
        ConsoleColor.Green, ConsoleColor.Blue
        };

    Console.WriteLine("\n\n\n");
    string[] art = {
                         "\t                                                                          ",
                         "\t                                                                          ",
                         "\t████████╗██╗  ██╗ █████╗ ███╗   ██╗██╗  ██╗    ██╗   ██╗ ██████╗ ██╗   ██╗",
                         "\t╚══██╔══╝██║  ██║██╔══██╗████╗  ██║██║ ██╔╝    ╚██╗ ██╔╝██╔═══██╗██║   ██║",
                         "\t   ██║   ███████║███████║██╔██╗ ██║█████╔╝      ╚████╔╝ ██║   ██║██║   ██║",
                         "\t   ██║   ██╔══██║██╔══██║██║╚██╗██║██╔═██╗       ╚██╔╝  ██║   ██║██║   ██║",
                         "\t   ██║   ██║  ██║██║  ██║██║ ╚████║██║  ██╗       ██║   ╚██████╔╝╚██████╔╝",
                         "\t   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝       ╚═╝    ╚═════╝  ╚═════╝ "
        };

    for (int i = 0; i < colors.Length; i++)
    {
        Console.Clear();
        Console.ForegroundColor = colors[i];
        Console.WriteLine("\n\n");

        foreach (var line in art)
        {
            Console.WriteLine("\t\t" + line);
        }

        Console.WriteLine("\n\n\t\t\t\t\t     Thank you for visiting!");
        Console.WriteLine("\t\t\t\t\t     Hope to see you again soon!");
        Console.WriteLine("\t\t\t\t\t     Goodbye and good luck! 💫");

        Thread.Sleep(700);
        Console.ForegroundColor = ConsoleColor.Black;
    }
}
using SP_EV_TAP_02;

using LibraryContext db = new();



#region Menu
List<string> menuItems =
    ["Get All",
    "Add",
    "Edit",
    "Exit"
];
ConsoleKeyInfo key;
int count = 0;
for (int i = 0; i < menuItems.Count; i++)
{
    if (i == count)
        Console.WriteLine($"> {menuItems[i]}");
    else Console.WriteLine(menuItems[i]);
}
while (true)
{
    key = Console.ReadKey();
    Console.Clear();
    switch (key.Key)
    {
        case ConsoleKey.UpArrow:
            count--;
            break;

        case ConsoleKey.DownArrow:
            count++;
            break;
        case ConsoleKey.Enter:
            if (count == 0) GetAll();
            else if (count == 1) Add();
            else if (count == 2) Edit();
            else if (count == 3) return 0;
            break;

    }
    if (count < 0) count = menuItems.Count - 1;
    for (int i = 0; i < menuItems.Count; i++)
    {
        if (i == count % menuItems.Count)
            Console.WriteLine($"> {menuItems[i]}");
        else Console.WriteLine(menuItems[i]);
    }
}

void Loading()
{
    bool loadingFinished = false;


    Thread loadingThread = new Thread(() =>
    {
        for (int i = 0; i < 101; i++)
        {
            Console.Clear();
            Console.WriteLine($"Loading... {i}%");
            Thread.Sleep(2);
        }
        loadingFinished = true;
    });

    loadingThread.Start();

    Thread.Sleep(5000);

    loadingFinished = true;
    loadingThread.Join();

}

void GetAll()
{
    Loading();
    foreach (var i in db.Authors)
    {
        Console.WriteLine($"{i.Id} - {i.FirstName} {i.LastName}");
    }
    Console.WriteLine("\nPress Enter to return...");
    Console.ReadKey();
}
void Add()
{
    Console.WriteLine("Add Author name: ");
    string Firstname = Console.ReadLine();
    Console.WriteLine("Add Author surname: ");
    string Lastname = Console.ReadLine();

    var NewAuthor = new Author { LastName = Lastname, FirstName = Firstname };

    db.Authors.Add(NewAuthor);
    db.SaveChanges();
    Loading();
    Console.WriteLine("The author was successfully added.");
}
void Edit()
{
    Console.WriteLine("Enter Author Id:");
    int id = int.Parse(Console.ReadLine());
    var author = db.Authors.FirstOrDefault(a => a.Id == id);

    Console.WriteLine("Add new FirstName:");
    string newFirstName = Console.ReadLine();
    Console.WriteLine("Add new LastName:");
    string newLastName = Console.ReadLine();

    author.FirstName = newFirstName;
    author.LastName = newLastName;

    db.Authors.Update(author);
    db.SaveChanges();
    Loading();
    Console.WriteLine("\nPress Enter to return...");
    Console.ReadKey();
    Console.WriteLine("The author was successfully edited.");

}

#endregion


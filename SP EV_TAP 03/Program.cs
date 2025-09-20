using CancellationTokenSource cts = new();
CancellationToken cancellationToken = cts.Token;


ThreadPool.QueueUserWorkItem(a =>
{
    try
    {
        Download(cancellationToken);
    }
    catch (OperationCanceledException ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine("Yukleme prosesi dayandirildi!");
    }
});

var key = Console.ReadKey();

if (key.Key == ConsoleKey.Enter)
{
    cts.Cancel();
    Thread.Sleep(1000);
}
Console.ReadKey();
void Download(CancellationToken token)
{
    Console.WriteLine("Yukleme baslayir...");
    Thread.Sleep(1000);
    for (int i = 0; i < 100; i++)
    {
        token.ThrowIfCancellationRequested();
        Console.WriteLine($"{i}%");
        Thread.Sleep(10);
        Console.Clear();
    }
    Console.WriteLine("Yukleme bitdi...");
}

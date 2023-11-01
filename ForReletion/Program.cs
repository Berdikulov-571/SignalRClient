using Microsoft.AspNetCore.SignalR.Client;
namespace SignalR
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HubConnection? connection = new HubConnectionBuilder().WithUrl("https://localhost:7052/messenger").Build();

            connection.On<string, string>("Recieve", (name, message) =>
            {
                Console.WriteLine($"{name}: {message}");
            });

            connection.On<string>("Welcome", message =>
            {
                Console.WriteLine(message);
            });

            connection.On<string>("Joined", message =>
            {
                Console.WriteLine(message);
            });

            await connection.StartAsync();
            Console.Write("Name: ");
            string name = Console.ReadLine();
            await connection.SendAsync("Start", name);

            while (true)
            {
                string? console = Console.ReadLine();

                await connection.SendAsync("SendMessage", console);
            }
        }
    }
}
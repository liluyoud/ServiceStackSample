using Escola.Core.Commands;
using ServiceStack;

var client = new JsonServiceClient("https://localhost:7278");

var response = await client.GetAsync(new QueryCitiesCommand());

var cities = response.Results;

foreach (var city in cities)
{
    Console.WriteLine(city.Name);
}

Console.ReadKey();

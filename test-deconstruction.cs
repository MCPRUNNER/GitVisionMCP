using GitVisionMCP.Services;
using Microsoft.Extensions.Logging;
using System;

// Create logger
using var factory = LoggerFactory.Create(builder => builder.AddConsole());
var logger = factory.CreateLogger<DeconstructionService>();
var locationLogger = factory.CreateLogger<LocationService>();

// Create services
var locationService = new LocationService(locationLogger);
var deconstructionService = new DeconstructionService(logger, locationService);

try
{
    Console.WriteLine("Testing DeconstructionService");
    Console.WriteLine("============================");

    // Test the service with our sample controller
    var result = deconstructionService.AnalyzeController("TestUsersController.cs");

    if (result != null)
    {
        Console.WriteLine("Controller Analysis Result:");
        Console.WriteLine(result);
        Console.WriteLine("\n✅ DeconstructionService working correctly!");
    }
    else
    {
        Console.WriteLine("❌ Failed to analyze controller");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Error: {ex.Message}");
    Console.WriteLine($"Stack trace: {ex.StackTrace}");
}

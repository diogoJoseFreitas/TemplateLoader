using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

// Define a class to represent your configuration settings
public class AppSettings
{
    public string ApiKey { get; set; }
    public int MaxRetries { get; set; }
    public bool EnableFeatureX { get; set; }
    public DatabaseSettings Database { get; set; }
}

public class DatabaseSettings
{
    public string ConnectionString { get; set; }
    public int TimeoutSeconds { get; set; }
}

public class JsonConfigManager
{
    private readonly string _configFilePath;

    public JsonConfigManager(string configFileName = "appsettings.json")
    {
        // Get the directory of the current executable and combine it with the config file name.
        // This ensures the config file is alongside the application.
        _configFilePath = Path.Combine(AppContext.BaseDirectory, configFileName);
    }

    /// <summary>
    /// Creates a default JSON configuration file if it doesn't exist.
    /// </summary>
    public async Task CreateDefaultConfigAsync()
    {
        if (!File.Exists(_configFilePath))
        {
            Console.WriteLine($"Creating default JSON configuration file: {_configFilePath}");
            var defaultSettings = new AppSettings
            {
                ApiKey = "your_default_api_key_here",
                MaxRetries = 3,
                EnableFeatureX = true,
                Database = new DatabaseSettings
                {
                    ConnectionString = "Server=.;Database=MyDb;Integrated Security=True;",
                    TimeoutSeconds = 30
                }
            };

            // Configure JsonSerializerOptions for pretty printing
            var options = new JsonSerializerOptions { WriteIndented = true };

            // Serialize the settings object to a JSON string
            string jsonString = JsonSerializer.Serialize(defaultSettings, options);

            // Write the JSON string to the file asynchronously
            await File.WriteAllTextAsync(_configFilePath, jsonString);
            Console.WriteLine("Default JSON config file created successfully.");
        }
        else
        {
            Console.WriteLine($"JSON configuration file already exists at: {_configFilePath}");
        }
    }

    /// <summary>
    /// Reads the JSON configuration file and returns the AppSettings object.
    /// </summary>
    /// <returns>The AppSettings object, or null if the file cannot be read.</returns>
    public async Task<AppSettings> ReadConfigAsync()
    {
        if (!File.Exists(_configFilePath))
        {
            Console.WriteLine($"Error: JSON configuration file not found at {_configFilePath}");
            return null;
        }

        try
        {
            // Read the entire file content asynchronously
            string jsonString = await File.ReadAllTextAsync(_configFilePath);

            // Deserialize the JSON string back into an AppSettings object
            var settings = JsonSerializer.Deserialize<AppSettings>(jsonString);
            Console.WriteLine("JSON configuration loaded successfully.");
            return settings;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error parsing JSON configuration: {ex.Message}");
            return null;
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error reading JSON file: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Updates a specific setting in the configuration and saves the file.
    /// Note: This reads the whole file, modifies, and writes back. For very large configs,
    /// consider more sophisticated partial update strategies if performance is critical.
    /// </summary>
    /// <param name="newApiKey">The new API key to set.</param>
    public async Task UpdateApiKeyAsync(string newApiKey)
    {
        var settings = await ReadConfigAsync();
        if (settings != null)
        {
            settings.ApiKey = newApiKey;
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(settings, options);
            await File.WriteAllTextAsync(_configFilePath, jsonString);
            Console.WriteLine($"API Key updated to '{newApiKey}' in JSON config.");
        }
    }
}

// // Example usage in a main method (e.g., in Program.cs)
// public class Program
// {
//     public static async Task Main(string[] args)
//     {
//         Console.WriteLine("--- JSON Configuration Example ---");
//         var jsonConfigManager = new JsonConfigManager();

//         // 1. Create default config if it doesn't exist
//         await jsonConfigManager.CreateDefaultConfigAsync();

//         // 2. Read config
//         AppSettings settings = await jsonConfigManager.ReadConfigAsync();

//         if (settings != null)
//         {
//             Console.WriteLine("\nLoaded JSON Settings:");
//             Console.WriteLine($"API Key: {settings.ApiKey}");
//             Console.WriteLine($"Max Retries: {settings.MaxRetries}");
//             Console.WriteLine($"Enable Feature X: {settings.EnableFeatureX}");
//             Console.WriteLine($"DB Connection String: {settings.Database.ConnectionString}");
//             Console.WriteLine($"DB Timeout: {settings.Database.TimeoutSeconds} seconds");

//             // 3. Update a setting and save
//             await jsonConfigManager.UpdateApiKeyAsync("new_updated_api_key_123");

//             // 4. Read again to confirm update
//             settings = await jsonConfigManager.ReadConfigAsync();
//             if (settings != null)
//             {
//                 Console.WriteLine("\nLoaded JSON Settings after update:");
//                 Console.WriteLine($"API Key: {settings.ApiKey}");
//             }
//         }
//         Console.WriteLine("\n--- End JSON Configuration Example ---\n");
//     }
// }

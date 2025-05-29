using System.Text.Json;

namespace TemplateLoader.Settings
{
    public static class ConfigManager
    {
        private static string configFileName = "TemplateLoaderSettings.json";
        private static string _configFilePath = Path.Combine(AppContext.BaseDirectory, configFileName);
        public static AppSettings? ReadConfig()
        {
            if (!File.Exists(_configFilePath))
                return null;

            try
            {
                string jsonString = File.ReadAllText(_configFilePath);

                var settings = JsonSerializer.Deserialize<AppSettings>(jsonString);

                Console.WriteLine("Configuração JSON carregada com sucesso.");

                return settings;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Erro ao analisar a configuração JSON: {ex.Message}");
                return null;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Erro ao ler o arquivo JSON: {ex.Message}");
                return null;
            }

        }

        public static void WriteConfig(AppSettings appSettings)
        {
            Console.WriteLine($"Criando arquivo de configuração JSON padrão: {_configFilePath}");

            // Configura JsonSerializerOptions para formatação bonita (indentação)
            var options = new JsonSerializerOptions { WriteIndented = true };

            // Serializa o objeto de configurações para uma string JSON
            string jsonString = JsonSerializer.Serialize(appSettings, options);

            // Escreve a string JSON no arquivo (síncronamente)
            File.WriteAllText(_configFilePath, jsonString); // Alterado de await File.WriteAllTextAsync
            Console.WriteLine("Arquivo de configuração JSON padrão criado com sucesso.");
            
        }
        
    }
}
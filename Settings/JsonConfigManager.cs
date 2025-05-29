using System.Text.Json;

namespace TemplateLoader.Settings
{
    public class JsonConfigManager : IConfigManager
    {
        private string _configFileName;
        private string _configFilePath;

        public JsonConfigManager(string configFileName = "TemplateLoaderSettings.json")
        {
            _configFileName = configFileName;
            _configFilePath = Path.Combine(AppContext.BaseDirectory, _configFileName);
        }

        public bool IsConfigured()
        {
            return File.Exists(_configFilePath);
        }

        public AppSettings? ReadConfig()
        {
            if (!IsConfigured())
                return null;

            try
            {
                string jsonString = File.ReadAllText(_configFilePath);

                var settings = JsonSerializer.Deserialize<AppSettings>(jsonString);

                Console.WriteLine($"Carregando templates disponíveis em: {settings.TemplateFolder}");

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

        public void WriteConfig(AppSettings appSettings)
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
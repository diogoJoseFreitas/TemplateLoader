using System;
using System.IO;
using System.Text.Json;
using TemplateLoader.Settings;
// Removed: using System.Threading.Tasks; // Não é necessário para operações síncronas

// Define uma classe para representar as configurações da sua aplicação

public class JsonConfigManager
{
    private readonly string _configFilePath;

    public JsonConfigManager(string configFileName = "TemplateLoaderSettings.json")
    {
        // Obtém o diretório do executável atual e combina com o nome do arquivo de configuração.
        // Isso garante que o arquivo de configuração esteja junto com a aplicação.
        _configFilePath = Path.Combine(AppContext.BaseDirectory, configFileName);
    }

    /// <summary>
    /// Cria um arquivo de configuração JSON padrão se ele não existir.
    /// </summary>
    public void CreateDefaultConfig() // Alterado de async Task para void
    {
        if (!File.Exists(_configFilePath))
        {
            Console.WriteLine($"Criando arquivo de configuração JSON padrão: {_configFilePath}");
            var defaultSettings = new AppSettings
            {
                TemplateFolder = "your_default_api_key_here"
            };

            // Configura JsonSerializerOptions para formatação bonita (indentação)
            var options = new JsonSerializerOptions { WriteIndented = true };

            // Serializa o objeto de configurações para uma string JSON
            string jsonString = JsonSerializer.Serialize(defaultSettings, options);

            // Escreve a string JSON no arquivo (síncronamente)
            File.WriteAllText(_configFilePath, jsonString); // Alterado de await File.WriteAllTextAsync
            Console.WriteLine("Arquivo de configuração JSON padrão criado com sucesso.");
        }
        else
        {
            Console.WriteLine($"Arquivo de configuração JSON já existe em: {_configFilePath}");
        }
    }

    /// <summary>
    /// Lê o arquivo de configuração JSON e retorna o objeto AppSettings.
    /// </summary>
    /// <returns>O objeto AppSettings, ou null se o arquivo não puder ser lido.</returns>
    public AppSettings ReadConfig() // Alterado de async Task<AppSettings> para AppSettings
    {
        if (!File.Exists(_configFilePath))
        {
            Console.WriteLine($"Erro: Arquivo de configuração JSON não encontrado em {_configFilePath}");
            return null;
        }

        try
        {
            // Lê todo o conteúdo do arquivo (síncronamente)
            string jsonString = File.ReadAllText(_configFilePath); // Alterado de await File.ReadAllTextAsync

            // Desserializa a string JSON de volta para um objeto AppSettings
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

    /// <summary>
    /// Atualiza uma configuração específica e salva o arquivo.
    /// Nota: Isso lê o arquivo inteiro, modifica e o escreve de volta. Para configurações muito grandes,
    /// considere estratégias de atualização parcial mais sofisticadas se o desempenho for crítico.
    /// </summary>
    /// <param name="newApiKey">A nova chave de API a ser definida.</param>
    public void UpdateApiKey(string newApiKey) // Alterado de async Task para void
    {
        var settings = ReadConfig(); // Chamada síncrona
        if (settings != null)
        {
            settings.TemplateFolder = newApiKey;
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(settings, options);
            File.WriteAllText(_configFilePath, jsonString); // Chamada síncrona
            Console.WriteLine($"Chave de API atualizada para '{newApiKey}' na configuração JSON.");
        }
    }
}

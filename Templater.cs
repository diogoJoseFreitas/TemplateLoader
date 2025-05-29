using TemplateLoader.Settings;

namespace TemplateLoader
{
    public class Templater
    {
        private AppSettings Settings;
        private IConfigManager Manager;

        public Templater()
        {
            Manager = new JsonConfigManager("TemplateLoaderSettings.json");
            if (!Manager.IsConfigured())
            {                
                Console.WriteLine("Para seguir, é necessário primeiro configurar a pasta onde serão armazenados os Templates.");
                Configure();
            }
            Settings = Manager.ReadConfig()!;
        }

        public void Configure()
        {
            var folderPath = "";
            do
            {
                Console.WriteLine("Informe o Caminho da pasta que deseja Configurar: ");
                folderPath = Console.ReadLine();

                if (Path.Exists(folderPath)) break;

                Console.WriteLine("Preciso que indique um diretório existente!\nTente Novamente.");
                Console.WriteLine("-".PadRight(30, '-'));
            } while (true);

            Manager.WriteConfig(new AppSettings() { TemplateFolder = folderPath });
            Settings = Manager.ReadConfig()!;
        }

        public void ListConfigs()
        {
            Console.WriteLine("-".PadRight(30, '-'));
            Console.WriteLine($"Pasta que contém os templates: {Settings.TemplateFolder}");
            Console.WriteLine("-".PadRight(30, '-'));            
        }
    }
}
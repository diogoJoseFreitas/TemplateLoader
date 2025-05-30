using TemplateLoader.Settings;

namespace TemplateLoader
{
    public class Templater
    {
        private AppSettings Settings;
        private IConfigManager Manager;

        private List<string> TemplateFolders;

        public Templater()
        {
            Manager = new JsonConfigManager("TemplateLoaderSettings.json");
            if (!Manager.IsConfigured())
            {
                Console.WriteLine("Para seguir, é necessário primeiro configurar a pasta onde serão armazenados os Templates.");
                Configure();
            }
            Settings = Manager.ReadConfig()!;
            TemplateFolders = Directory.GetDirectories(Settings.TemplateFolder).ToList();
        }

        public void Configure()
        {
            string folderPath;
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

        public void ListTemplates()
        {
            TemplateFolders = Directory.GetDirectories(Settings.TemplateFolder).ToList();
            Console.WriteLine("Templates disponíveis:");
            foreach (var (index, item) in TemplateFolders.Index())
            {
                Console.WriteLine($"{index,2} | {Path.GetFileName(item)}");
            }
            Console.WriteLine("-".PadRight(15, '-'));
        }

        public void AddTemplate()
        {
            Console.WriteLine("Informe Arquivo ou pasta que deseja adicionar ao Template:");
            var sourceTemplatePath = Console.ReadLine()?.Replace('"', '\0');
            string description;
            do
            {
                Console.WriteLine("Informe a descrição: ");
                description = Console.ReadLine();
                if (TemplateFolders.IndexOf(description) < 0) break;
                Console.WriteLine("Descrição já está em uso. Deseja Substituir? (Y/N)");
                if ("Yy".Contains(Console.ReadLine() + "")) break;
                Console.WriteLine("Por gentileza inserir outro valor para a descrição.");

            } while (true);
            var newTemplateDir = Path.Join(Settings.TemplateFolder, description);

            if (!Directory.Exists(newTemplateDir)) Directory.CreateDirectory(newTemplateDir);

            if (File.Exists(sourceTemplatePath))
            {
                string destFile = Path.Combine(newTemplateDir, Path.GetFileName(sourceTemplatePath));
                File.Copy(sourceTemplatePath, destFile, overwrite: true);
                Console.WriteLine("Arquivo de Template Criado com Sucesso!");
            }
            else if (Directory.Exists(sourceTemplatePath))
            {
                string destDir = Path.Combine(newTemplateDir, Path.GetFileName(sourceTemplatePath));
                CopyDirectory(sourceTemplatePath, destDir);
            }

            Console.WriteLine("Template adicionado com sucesso!");
        }
        private static void CopyDirectory(string sourceDir, string destDir)
        {
            // Create destination directory if it doesn't exist
            Directory.CreateDirectory(destDir);

            // Copy files
            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destDir, Path.GetFileName(file));
                File.Copy(file, destFile, overwrite: true);
            }

            // Copy subdirectories recursively
            foreach (string subDir in Directory.GetDirectories(sourceDir))
            {
                string destSubDir = Path.Combine(destDir, Path.GetFileName(subDir));
                CopyDirectory(subDir, destSubDir);
            }
        }
    }
}
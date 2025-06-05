using TemplateLoader.Menus;
using TemplateLoader.Utils;
using TemplateLoader.Settings;
using System.Reflection;

namespace TemplateLoader
{
    public class Templater
    {
        private AppSettings Settings;
        private IConfigManager Manager;

        private List<string> TemplateFolders => Directory.GetDirectories(Settings.TemplateFolder).ToList();

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
            string folderPath;
            do
            {
                Console.WriteLine("Informe o Caminho da pasta que deseja Configurar: ");
                folderPath = Console.ReadLine().Replace("\"", "");

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
            var sourceTemplatePath = Console.ReadLine()?.Replace("\"", "");
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
                CopyDirectory(sourceTemplatePath, newTemplateDir);
            }

            Console.WriteLine("Template adicionado com sucesso!");
        }
        public void LoadTemplate()
        {
            var temp = TemplateFolders;
            if (temp.Count == 0)
            {
                Console.WriteLine("Nenhum template disponível, verifique configurações.");
                return;
            }
            var list = temp.AsEnumerable().Select(row => Path.GetFileName(row)).ToList();
            var nTemplate = list.ListAndPickIndex();

            Console.WriteLine("-".PadRight(15, '-'));
            Console.WriteLine($"Opção Selecionada: {list[nTemplate]}");

            Console.WriteLine("Informe a pasta onde deseja Copiar o template: ");
            Settings.PathsHistory.ListItems(5);
            var pasta = Console.ReadLine()?.Replace("\"", "");

            int nHistory;
            if (int.TryParse(pasta, out nHistory))
                pasta = Settings.PathsHistory[nHistory];

            if (!Directory.Exists(pasta))
            {
                Console.WriteLine("Pasta informada não encontrada, tente novamente!");
                return;
            }

            Console.WriteLine($"iniciando cópia do Template para a pasta indicada...");
            Settings.AddPath(pasta);
            Manager.WriteConfig(Settings);
            CopyDirectory(temp[nTemplate], pasta);
        }

        public void DumpHistory()
        {
            Settings.PathsHistory = new List<string>();
            Manager.WriteConfig(Settings);
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
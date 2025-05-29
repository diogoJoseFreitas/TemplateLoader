namespace TemplateLoader.Menus
{

    public abstract class Menu : IAction
    {
        protected List<MenuItem> Itens { get; set; } = [];

        public void Do()
        {
            do
            {
                foreach (var (index, item) in Itens.Index())
                {
                    Console.WriteLine($"{index} | {item}");
                }
                Console.WriteLine("-".PadRight(15, '-'));
                Console.WriteLine("Selecione uma opção: ");
                var option = Console.ReadLine();
                int n = int.Parse(option + "");
                if (n >= 0 && n < Itens.Count)
                {
                    var item = Itens[n];
                    Console.WriteLine($"Opção Selecionada: {item}");
                    Console.WriteLine("-".PadRight(15, '-'));
                    if (item.subMenu != null)
                        item.subMenu.Do();
                }
                    
            } while (true);
        }

        public void AddMenuItem(string text, IAction action = null)
        {
            Itens.Add(new MenuItem() { Text = text, subMenu = action });
        }

    }
}
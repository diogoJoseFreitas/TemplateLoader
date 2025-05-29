namespace TemplateLoader.Menus
{

    public abstract class Menu
    {
        protected List<MenuItem> Itens { get; set; } = [];

        public void Show()
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
                    item.Action?.Invoke();
                }

            } while (true);
        }

        public void AddMenuItem(string text, Action? action = null)
        {
            Itens.Add(new MenuItem() { Text = text, Action = action });
        }
        public void AddMenuItem(string text, Menu subMenu)
        {
            Itens.Add(new MenuItem() { Text = text, Action = subMenu.Show });
        }

        public Menu AddSubMenu(string opcao)
        {
            var subMenu = new SubMenu(this);
            AddMenuItem(opcao, subMenu);
            return subMenu;
        }

    }
}
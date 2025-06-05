using TemplateLoader.Utils;
namespace TemplateLoader.Menus
{

    public abstract class Menu
    {
        protected List<MenuItem> Itens { get; set; } = [];

        public void Show()
        {
            do
            {
                var item = Itens.ListAndPickItem();
            
                Console.WriteLine($"Opção Selecionada: {item}");
                Console.WriteLine("-".PadRight(15, '-'));
                item.Action?.Invoke();
                
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
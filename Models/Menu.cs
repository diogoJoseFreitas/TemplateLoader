namespace TemplateLoader.Models
{
    public interface Action
    {
        public void Do();
    }
    public class MenuItem
    {
        public string text { get; set; }
        public Action? subMenu { get; set; } = null;

        public override string ToString()
        {
            return text;
        }
    }
    
    public class ExitAction : Action
    {
        public void Do()
        {
            Console.WriteLine("Saindo do programa...");
            Environment.Exit(0); 
        }
    }
    public class Menu : Action
    {
        private List<MenuItem> Itens { get; set; } = [];

        public void Do()
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
                Console.WriteLine($"Opção Selecionada {item}");
                if (item.subMenu != null)
                    item.subMenu.Do();
            }
        }

        public void AddMenuItem(string text, Action action = null)
        {
            Itens.Add(new MenuItem() { text = text, subMenu = action });
        }

    }
}
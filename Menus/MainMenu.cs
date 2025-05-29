namespace TemplateLoader.Menus
{
    public class MainMenu : Menu
    {
        public MainMenu()
        {
            AddMenuItem("Sair", ExitAction);
        }
        public void ExitAction()
        {
            Console.WriteLine("Saindo do programa...");
            Environment.Exit(0);            
        }
    }    
}
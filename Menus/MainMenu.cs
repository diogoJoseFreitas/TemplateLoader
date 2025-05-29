namespace TemplateLoader.Menus
{
    public class MainMenu : Menu
    {
        public MainMenu()
        {
            AddMenuItem("Sair", new ExitAction());
        }

        public void Start()
        {
            Do();
        }
            
    }    
}
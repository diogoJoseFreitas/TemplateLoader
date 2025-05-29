namespace TemplateLoader.Menus
{
    public class SubMenu : Menu
    {
        public SubMenu(Menu previousMenu)
        {
            AddMenuItem("Voltar", previousMenu);
        }
    }    
}
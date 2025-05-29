namespace TemplateLoader.Menus
{
    public class MenuItem
    {
        public required string Text { get; set; }
        public IAction? subMenu { get; set; } = null;

        public override string ToString()
        {
            return Text;
        }
    }
    
}
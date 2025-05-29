namespace TemplateLoader.Menus
{
    public class MenuItem
    {
        public required string Text { get; set; }
        public Action? Action { get; set; } = null;

        public override string ToString()
        {
            return Text;
        }
    }
    
}
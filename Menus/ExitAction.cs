namespace TemplateLoader.Menus
{
    public class ExitAction : IAction
    {
        public void Do()
        {
            Console.WriteLine("Saindo do programa...");
            Environment.Exit(0);
        }
    }
}
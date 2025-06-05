namespace TemplateLoader.Settings
{
    public class AppSettings
    {
        public required string TemplateFolder { get; set; }
        public List<string> PathsHistory { get; set; } = [];

        public void AddPath(string path)
        {
            PathsHistory.Remove(path);
            PathsHistory.Insert(0, path);
            PathsHistory = PathsHistory.Take(5).ToList();
        }
    }
    
}
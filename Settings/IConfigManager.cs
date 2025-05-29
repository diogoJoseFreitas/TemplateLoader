namespace TemplateLoader.Settings
{
    public interface IConfigManager
    {
        public AppSettings? ReadConfig();
        public void WriteConfig(AppSettings appSettings);
        public bool IsConfigured();
    }
}
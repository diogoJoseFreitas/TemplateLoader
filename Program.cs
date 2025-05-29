// See https://aka.ms/new-console-template for more information

using TemplateLoader.Menus;

var menuMain = new MainMenu();

var menuConfigs = menuMain.AddSubMenu("Configurações");
menuConfigs.AddMenuItem("Configurar Pasta de Templates");
menuConfigs.AddMenuItem("Listar Pasta de Templates");

var menuCopiarTemplate = menuMain.AddSubMenu("Copiar Template");


Console.WriteLine("Hello, World!");

menuMain.Show();

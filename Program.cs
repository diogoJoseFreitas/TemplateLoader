// See https://aka.ms/new-console-template for more information

using TemplateLoader.Menus;

var menuMain = new MainMenu();
var menuConfigs = new SubMenu(menuMain);
var menuCopiarTemplate = new SubMenu(menuMain);

menuMain.AddMenuItem("Copiar Template", menuCopiarTemplate);
menuMain.AddMenuItem("Configurações", menuConfigs);

menuConfigs.AddMenuItem("Configurar Pasta de Templates");
menuConfigs.AddMenuItem("Listar Pasta de Templates");


Console.WriteLine("Hello, World!");

menuMain.Do();

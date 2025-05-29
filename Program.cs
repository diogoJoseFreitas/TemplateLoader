// See https://aka.ms/new-console-template for more information

using TemplateLoader.Models;

var menuConfigs = new Menu();
var menuMain = new Menu();
var menuCopiarTemplate = new Menu();

menuConfigs.AddMenuItem("Configurar Pasta de Templates");
menuConfigs.AddMenuItem("Listar Pasta de Templates");


menuCopiarTemplate.AddMenuItem("Voltar", menuMain);


menuMain.AddMenuItem("Sair", new ExitAction());
menuMain.AddMenuItem("Copiar Template", menuCopiarTemplate);
menuMain.AddMenuItem("Configurações", menuConfigs);

Console.WriteLine("Hello, World!");

menuMain.Do();

// See https://aka.ms/new-console-template for more information

using TemplateLoader.Models;

var menuConfigs = new Menu();
var menuMain = new Menu();

menuConfigs.AddMenuItem("Voltar", menuMain);
menuConfigs.AddMenuItem("Configurar Pasta de Templates");


menuMain.AddMenuItem("Sair", new ExitAction());
menuMain.AddMenuItem("Configurações", menuConfigs);
menuMain.AddMenuItem("Voltar");

Console.WriteLine("Hello, World!");

menuMain.Do();

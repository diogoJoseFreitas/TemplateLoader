using TemplateLoader.Menus;
using TemplateLoader;

Console.WriteLine(
@"
 _________  _______   _____ ______   ________  ___       ________  _________  _______      
|\___   ___\\  ___ \ |\   _ \  _   \|\   __  \|\  \     |\   __  \|\___   ___\\  ___ \     
\|___ \  \_\ \   __/|\ \  \\\__\ \  \ \  \|\  \ \  \    \ \  \|\  \|___ \  \_\ \   __/|    
     \ \  \ \ \  \_|/_\ \  \\|__| \  \ \   ____\ \  \    \ \   __  \   \ \  \ \ \  \_|/__  
      \ \  \ \ \  \_|\ \ \  \    \ \  \ \  \___|\ \  \____\ \  \ \  \   \ \  \ \ \  \_|\ \ 
       \ \__\ \ \_______\ \__\    \ \__\ \__\    \ \_______\ \__\ \__\   \ \__\ \ \_______\
        \|__|  \|_______|\|__|     \|__|\|__|     \|_______|\|__|\|__|    \|__|  \|_______|
                ___       ________  ________  ________  _______   ________                                
               |\  \     |\   __  \|\   __  \|\   ___ \|\  ___ \ |\   __  \                               
               \ \  \    \ \  \|\  \ \  \|\  \ \  \_|\ \ \   __/|\ \  \|\  \                              
                \ \  \    \ \  \\\  \ \   __  \ \  \ \\ \ \  \_|/_\ \   _  _\                             
                 \ \  \____\ \  \\\  \ \  \ \  \ \  \_\\ \ \  \_|\ \ \  \\  \|                            
                  \ \_______\ \_______\ \__\ \__\ \_______\ \_______\ \__\\ _\                            
                   \|_______|\|_______|\|__|\|__|\|_______|\|_______|\|__|\|__|                           " +"\n\n");
        
Console.WriteLine("-".PadRight(30, '-'));

var templateLoader = new Templater();

var menuMain = new MainMenu();
menuMain.AddMenuItem("Copiar Template");
menuMain.AddMenuItem("Listar Templates existentes", templateLoader.ListTemplates);
menuMain.AddMenuItem("Adicionar Novo Template", templateLoader.AddTemplate);

var menuConfigs = menuMain.AddSubMenu("Configurações");
menuConfigs.AddMenuItem("Configurar Pasta de Templates", templateLoader.Configure);
menuConfigs.AddMenuItem("Listar Configurações atuais", templateLoader.ListConfigs);


menuMain.Show();
namespace TemplateLoader.Utils
{
    public static class Extensions
    {
        public static int ListAndPickItem<T>(this List<T> options)
        {
            int selectedOption;
            do
            {
                for (int index = 0; index < options.Count; index ++)
                {
                    Console.WriteLine($"{index,2} | {options[index]}");
                }

                Console.WriteLine("-".PadRight(15, '-'));
                Console.WriteLine("Selecione uma opção: ");
                var answer = Console.ReadLine();

                if (int.TryParse(answer, out selectedOption) &&
                selectedOption >= 0 && selectedOption < options.Count) return selectedOption;

                Console.WriteLine("Opção inválida, por favor escolha uma das opções listadas a seguir.");


            } while (true);
            return -1;
        }
    }
}
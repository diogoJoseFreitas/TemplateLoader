namespace TemplateLoader.Utils
{
    public static class Extensions
    {
        public static void ListItems<T>(this List<T> options, int limit = -1)
        {
            var lenght = (limit > 0 && limit < options.Count) ? limit : options.Count;
            for (int index = 0; index < lenght; index++)
            {
                Console.WriteLine($"{index,2} | {options[index]}");
            }

        }
        public static int ListAndPickIndex<T>(this List<T> options)
        {
            int selectedOption;
            do
            {
                options.ListItems();

                Console.WriteLine("-".PadRight(15, '-'));
                Console.WriteLine("Selecione uma opção: ");
                var answer = Console.ReadLine();

                if (int.TryParse(answer, out selectedOption) &&
                selectedOption >= 0 && selectedOption < options.Count) return selectedOption;

                Console.WriteLine("Opção inválida, por favor escolha uma das opções listadas a seguir.");


            } while (true);
            return -1;
        }

        public static T ListAndPickItem<T>(this List<T> options) {
            T selectedOption;
            do
            {
                options.ListItems();

                Console.WriteLine("-".PadRight(15, '-'));
                Console.WriteLine("Selecione uma opção: ");
                var answer = Console.ReadLine();
                int n;
                if (int.TryParse(answer, out n) && n >= 0 && n < options.Count)
                    return options[n];

                Console.WriteLine("Opção inválida, por favor escolha uma das opções listadas a seguir.");
            } while (true);
        }
    }
}
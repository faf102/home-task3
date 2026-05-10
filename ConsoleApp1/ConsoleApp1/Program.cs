using System;
using System.IO;

class Program
{
    static int count;
    public class phonename
    {
        public string name { get; set; }
        public string num { get; set; }
    }

    public class phonebook
    {

        private static phonename[] _contacts = new phonename[count];
        private int _count = 0;

        public static void list()
        {
            if (count == 0) Console.WriteLine("Список пуст");
            else
            {
                for (int i = 0; i < count; i++)
                {
                                        Console.WriteLine($"{i + 1}. Имя: {_contacts[i].name} | Номер: {_contacts[i].num}");
                }
            }
        }
        public static void rewrite() // изменение списка
        {
            while (true)
            {
                phonebook.list();
                Console.WriteLine("Номер изменяемого контакта");
                string put = Console.ReadLine();
                if (!int.TryParse(put, out int number)) continue;

                Console.WriteLine($"{number - 1}. Имя: {_contacts[number].name}" + "Напишите новое имя");
                _contacts[number].name = Console.ReadLine();

                Console.WriteLine($" Номер: {_contacts[number].num}" + "Напишите новый номер");
                _contacts[number].num = Console.ReadLine();
                return ;
            }
        }
        public static void totxt()//запись в txt
        {
            if (count == 0) Console.WriteLine("Список пуст");
            else
            {
                string[] txt = new string[count + 1];
                for (int i = 0; i < (count+1); i++)
                { 

                    if (i > 0) txt[i] = $" {_contacts[i-1].name} | {_contacts[i-1].num}";
                                        if (i == 0)
                                         {
                                          string s = Convert.ToString(count);
                                          txt[0] = s ;
                                          
                                           }

                    File.WriteAllLines("phonebook.txt", txt);
                    Console.WriteLine("Файл сохраняется сюда: " + Directory.GetCurrentDirectory());
                }
            }
        }
        public static void oftxt() // чтение из txt
        {
            if (File.Exists("phonebook.txt"))
            {
                string[] savedLines = File.ReadAllLines("phonebook.txt");

                if (savedLines.Length == 0) return;

                count = int.Parse(savedLines[0]);

                for (int i = 1; i <= count; i++)
                {
                   
                    
                    string[] parts = savedLines[i].Split('|');

                    
                    if (parts.Length != 2) continue;

                    int index = i - 1;

                    
                    if (index < _contacts.Length)
                    {
                        _contacts[index] = new phonename
                        {
                            name = parts[0],
                            num = parts[1]
                        };
                    }
                }
            }
        }
        public bool error(phonename contact) //Диагностика ошибок
        {
            if (string.IsNullOrEmpty(contact.name) || contact.num == null)
                return true;

            for (int i = 0; i < _count; i++)
            {
                if (_contacts[i] != null && _contacts[i].num == contact.num)
                    return true;
            }

            return false;
        }

        public bool Add(phonename contact) 
        {
            if (!error(contact) && _count < _contacts.Length)
            {
                _contacts[_count] = contact;
                _count++;
                return true;
            }
            return false;
        }
    }

    public static void write()
    {
        Console.WriteLine("Количестов контактов");

        count = int.Parse(Console.ReadLine());
        
        var book = new phonebook();
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine("ИМЯ,(enter) НОМЕР");

            var contact = new phonename { name = Console.ReadLine(), num =  Console.ReadLine() };

            if (book.Add(contact))
                Console.WriteLine(" Контакт добавлен");
            else
            {
                Console.WriteLine(" Ошибка: пустые поля или номер уже есть.");
                i--;
            }
        }
    }
    
    static void Main()
    {
        while (true)
        { 
            Console.WriteLine("выберите функцию");
            Console.WriteLine("Начать запись новой страницы контактов'1' ");
            Console.WriteLine("Чтение существующих '2' ");
            Console.WriteLine("Редактирование '3' ");
            Console.WriteLine("Сохранение в txt '4' ");
            Console.WriteLine("Чтение из txt '5' ");
            string put = Console.ReadLine();
            if (!int.TryParse(put, out int choose)) continue;
                if (choose == 1) write();
            if (choose == 2) phonebook.list();
            if (choose == 3) phonebook.rewrite();
            if (choose == 4) phonebook.totxt();
            if (choose == 5) phonebook.oftxt();



        }
    }
}
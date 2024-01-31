// Наравцевич Михаил, ПМИ-8, 2 курс.
// Задача C# Множества

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp4
{
    /// <summary>
    /// Абстрактный класс Set
    /// </summary>
    abstract class Set
    {

        protected int max_num_in_set;

        public Set(int a)
        {
            max_num_in_set = a;
        }

        public abstract bool AddElemInSet(int a);
        public abstract bool RemoveElemFromSet(int a);
        public abstract bool CheckExistElemInSet(int a);

        public void FillSet(string s)
        {
            int[] a = s.Split(' ').Select(x => int.Parse(x)).ToArray();
            foreach (int i in a)
            {
                AddElemInSet(i);
            }
        }

        public void FillSet(int[] a)
        {
            foreach (int i in a)
            {
                AddElemInSet(i);
            }

        }

        public void PrintSet()
        {
            for (int i = 0; i <= max_num_in_set; i++)
            {
                if (CheckExistElemInSet(i))
                {
                    Console.Write($"{i} ");
                }

            }
            Console.WriteLine();
        }

    }

    /// <summary>
    /// Класс SimpleSet 
    /// </summary>
    class SimpleSet : Set
    {
        bool[] arr;
        public SimpleSet(int a) : base(a)
        {
            arr = new bool[a + 1];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = false;
            }
        }

        public override bool AddElemInSet(int a)
        {
            if (a > max_num_in_set || a < 0)
                throw new Exception("В множество пытаются добавить элемент, превышающий максимально допустимое значение");
            arr[a] = true;
            return true;
        }

        public override bool CheckExistElemInSet(int a)
        {
            if (arr[a] == true)
                return true;
            return false;
        }

        public override bool RemoveElemFromSet(int a)
        {
            arr[a] = false;
            return true;
        }

        /// <summary>
        /// Объединения множеств
        /// </summary>
        /// <param name="a">1 множество</param>
        /// <param name="b">2 множество</param>
        /// <returns>Объединение множеств a и b</returns>
        public static SimpleSet operator +(SimpleSet a, SimpleSet b)
        {
            int max_num;
            if (a.max_num_in_set > b.max_num_in_set)
                max_num = a.max_num_in_set;
            else
                max_num = b.max_num_in_set;

            SimpleSet output = new SimpleSet(max_num);

            for (int i = 0; i <= max_num; i++)
            {
                output.arr[i] = false;
            }

            for (int i = 0; i <= a.max_num_in_set; i++)
            {
                if (a.arr[i])
                    output.arr[i] = true;
            }

            for (int i = 0; i <= b.max_num_in_set; i++)
            {
                if (b.arr[i])
                    output.arr[i] = true;
            }

            return output;
        }
        public static SimpleSet operator -(SimpleSet a, SimpleSet b)
        {
            SimpleSet output = new SimpleSet(1);
            return output;
        }

        /// <summary>
        /// Пересечение множеств
        /// </summary>
        /// <param name="a">1 множество</param>
        /// <param name="b">2 множество</param>
        /// <returns>Пересечение множеств a и b</returns>
        public static SimpleSet operator *(SimpleSet a, SimpleSet b)
        {
            int min_num;
            SimpleSet output;
            if (a.max_num_in_set < b.max_num_in_set)
            {
                min_num = a.max_num_in_set;
                output = new SimpleSet(min_num);
                for (int i = 0; i <= min_num; i++)
                {
                    output.arr[i] = false;
                }
                for (int i = 0; i <= a.max_num_in_set; i++)
                {
                    if (a.arr[i] && b.arr[i])
                        output.arr[i] = true;
                }
            }

            else
            {
                min_num = b.max_num_in_set;
                output = new SimpleSet(min_num);
                for (int i = 0; i <= min_num; i++)
                {
                    output.arr[i] = false;
                }
                for (int i = 0; i <= b.max_num_in_set; i++)
                {
                    if (a.arr[i] && b.arr[i])
                        output.arr[i] = true;
                }
            }

            return output;


        }
    }



    class MultiSet : Set
    {
        int[] arr;
        public MultiSet(int a) : base(a)
        {
            arr = new int[a + 1];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = 0;
            }
        }

        public override bool AddElemInSet(int a)
        {
            if (a > max_num_in_set || a < 0)
                throw new Exception("В множество пытаются добавить элемент, превышающий максимально допустимое значение");
            arr[a] += 1;
            return true;
        }

        public override bool CheckExistElemInSet(int a)
        {
            if (arr[a] > 0)
                return true;
            return false;
        }

        public override bool RemoveElemFromSet(int a)
        {
            if (arr[a] != 0)
                arr[a] -= 1;
            return true;
        }


    }


    internal class Program
    {
        static int variant_set, variant_input, max_number, menu2_inp, tmp;
        static string input_s, file_path;
        static Set t;
        static SimpleSet t1, t2, t3, t4;
        static void test_operator_methods()
        {
            Console.WriteLine("Введите множество А:");
            input_s = Console.ReadLine();
            max_number = input_s.Split(' ').Select(x => int.Parse(x)).ToArray().Max();
            t1 = new SimpleSet(max_number);
            t1.FillSet(input_s);
            
            Console.WriteLine("Введите множество B:");
            input_s = Console.ReadLine();
            max_number = input_s.Split(' ').Select(x => int.Parse(x)).ToArray().Max();
            t2 = new SimpleSet(max_number);
            t2.FillSet(input_s);

            Console.WriteLine("Множество C = A + B:");
            t3 = t1 + t2;
            t3.PrintSet();

            Console.WriteLine("Множество D = A * B:");
            t4 = t1 * t2;
            t4.PrintSet();
        }
        static void menu()
        {
            Console.WriteLine("-----------ГЛАВНОЕ МЕНЮ-----------");

            Console.WriteLine("Выберите вариант представления:");
            Console.WriteLine("1 - Множество с логическим массивом");
            Console.WriteLine("2 - Мультимножество");
            variant_set = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Выберите вариант ввода множества:");
            Console.WriteLine("1 - Ввод в видео одной строки");
            Console.WriteLine("2 - Ввод из файла");
            variant_input = Convert.ToInt32(Console.ReadLine());

            if (variant_input == 1)
            {
                Console.WriteLine("Введите строку с числами, например: 8 68 3");
                input_s = Console.ReadLine();
                max_number = input_s.Split(' ').Select(x => int.Parse(x)).ToArray().Max();

                if (variant_set == 1)
                {
                    t = new SimpleSet(max_number);
                }
                else
                {
                    t = new MultiSet(max_number);
                }
                t.FillSet(input_s);
            }
            else
            {
                Console.WriteLine(@"Введите путь к файлу в таком виде C:\Users\m-nar\Desktop\t.txt");
                file_path = Console.ReadLine();

                StreamReader dataStream = new StreamReader(file_path);
                string datasample;
                int[] arr_numbers;
                List<int> numbers_from_file = new List<int>();
                while ((datasample = dataStream.ReadLine()) != null)
                {
                    numbers_from_file.Add(Convert.ToInt32(datasample));
                }
                dataStream.Close();
                arr_numbers = numbers_from_file.ToArray();
                max_number = arr_numbers.Max();
                if (variant_set == 1)
                {
                    t = new SimpleSet(max_number);
                }
                else
                {
                    t = new MultiSet(max_number);
                }
                t.FillSet(arr_numbers);
            }

            while (true)
            {
                Console.WriteLine("-----------МЕНЮ-----------");
                Console.WriteLine("[1] - добавить элемент,\n[2] - исключить элемент,\n[3] - проверить наличие элемента,\n[4] - вывести элементы множества,\n[5] - Выход");
                menu2_inp = Convert.ToInt32(Console.ReadLine());

                if (menu2_inp == 1)
                {
                    Console.WriteLine("Введите элемент для добавления:");
                    tmp = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        t.AddElemInSet(tmp);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                }

                if (menu2_inp == 2)
                {
                    Console.WriteLine("Введите элемент для исключения:");
                    tmp = Convert.ToInt32(Console.ReadLine());
                    t.RemoveElemFromSet(tmp);

                }

                if (menu2_inp == 3)
                {
                    Console.WriteLine("Введите элемент для проверки его наличия:");
                    tmp = Convert.ToInt32(Console.ReadLine());
                    if (t.CheckExistElemInSet(tmp))
                    {
                        Console.WriteLine($"Элемент {tmp} есть во множестве");
                    }
                    else
                    {
                        Console.WriteLine($"Элемента {tmp} нет во множестве");
                    }

                }

                if (menu2_inp == 4)
                {
                    t.PrintSet();

                }

                if (menu2_inp == 5)
                {
                    break;

                }

                Console.WriteLine();
                Console.WriteLine();
            }


        }
        static void Main(string[] args)
        {

            menu();

            Console.WriteLine("Тестирование объединения и пересечения множеств");
            
            test_operator_methods();


            Console.WriteLine("Нажмите любую клавишу, чтобы выйти...");
            Console.ReadKey();
        }
    }
}

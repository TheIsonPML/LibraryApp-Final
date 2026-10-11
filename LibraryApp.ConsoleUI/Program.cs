using System;
using LibraryApp.Core;

namespace LibraryApp.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Logic logic = new Logic();
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== БИБЛИОТЕКА (Консоль) ===");
                Console.WriteLine("1. Показать все книги");
                Console.WriteLine("2. Добавить книгу");
                Console.WriteLine("3. Редактировать книгу");
                Console.WriteLine("4. Удалить книгу");
                Console.WriteLine("5. Найти книги по автору");
                Console.WriteLine("6. Группировать по жанрам");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\n--- Все книги ---");
                        foreach (var book in logic.ReadAllBooks())
                            Console.WriteLine(book.ToString());
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.WriteLine("\n--- Добавление ---");
                        Console.Write("Название: "); string title = Console.ReadLine();
                        Console.Write("Автор: "); string author = Console.ReadLine();
                        Console.Write("Жанр: "); string genre = Console.ReadLine();
                        logic.CreateBook(new Book { Title = title, Author = author, Genre = genre });
                        Console.WriteLine("Книга добавлена! Нажмите клавишу...");
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Write("\nВведите ID для редактирования: ");
                        if (int.TryParse(Console.ReadLine(), out int idUpd))
                        {
                            Console.Write("Новое название: "); string t = Console.ReadLine();
                            Console.Write("Новый автор: "); string a = Console.ReadLine();
                            Console.Write("Новый жанр: "); string g = Console.ReadLine();
                            logic.UpdateBook(new Book { Id = idUpd, Title = t, Author = a, Genre = g });
                            Console.WriteLine("Обновлено!");
                        }
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Write("\nВведите ID для удаления: ");
                        if (int.TryParse(Console.ReadLine(), out int idDel))
                        {
                            logic.DeleteBook(idDel);
                            Console.WriteLine("Удалено!");
                        }
                        Console.ReadKey();
                        break;
                    case "5":
                        Console.Write("\nВведите автора: ");
                        string searchAuthor = Console.ReadLine();
                        var found = logic.GetBooksByAuthor(searchAuthor);
                        Console.WriteLine($"\nНайдено: {found.Count}");
                        foreach (var b in found) Console.WriteLine(b.ToString());
                        Console.ReadKey();
                        break;
                    case "6":
                        Console.WriteLine("\n--- Группировка по жанрам ---");
                        var groups = logic.GroupBooksByGenre();
                        foreach (var group in groups)
                        {
                            Console.WriteLine($"\nЖанр: {group.Key}");
                            foreach (var book in group.Value)
                                Console.WriteLine($"  - {book.Title} ({book.Author})");
                        }
                        Console.ReadKey();
                        break;
                    case "0": exit = true; break;
                }
            }
        }
    }
}
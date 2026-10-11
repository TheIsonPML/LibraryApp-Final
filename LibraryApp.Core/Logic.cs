using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApp.Core
{
    public class Logic
    {
        private List<Book> _books = new List<Book>();
        private int _nextId = 1;

        public Logic()
        {
            // Тестовые данные для наглядности
            CreateBook(new Book { Title = "Война и мир", Author = "Толстой", Genre = "Роман" });
            CreateBook(new Book { Title = "Преступление и наказание", Author = "Достоевский", Genre = "Роман" });
            CreateBook(new Book { Title = "Космос", Author = "Саган", Genre = "Наука" });
        }

        // 1. Создание
        public void CreateBook(Book book)
        {
            book.Id = _nextId++;
            _books.Add(book);
        }

        // 2. Чтение
        public List<Book> ReadAllBooks()
        {
            return _books.ToList();
        }

        // 3. Изменение
        public void UpdateBook(Book updatedBook)
        {
            var existingBook = _books.FirstOrDefault(b => b.Id == updatedBook.Id);
            if (existingBook != null)
            {
                existingBook.Title = updatedBook.Title;
                existingBook.Author = updatedBook.Author;
                existingBook.Genre = updatedBook.Genre;
            }
        }

        // 4. Удаление
        public void DeleteBook(int id)
        {
            _books.RemoveAll(b => b.Id == id);
        }

        // Бизнес-функция 1: Поиск по автору
        public List<Book> GetBooksByAuthor(string author)
        {
            return _books.Where(b => b.Author.ToLower().Contains(author.ToLower())).ToList();
        }

        // Бизнес-функция 2: Группировка по жанрам
        public Dictionary<string, List<Book>> GroupBooksByGenre()
        {
            return _books.GroupBy(b => b.Genre)
                         .ToDictionary(g => g.Key, g => g.ToList());
        }
    }
}
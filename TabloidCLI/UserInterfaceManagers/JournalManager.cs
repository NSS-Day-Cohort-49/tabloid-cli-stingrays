using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;

        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List Entries");
            Console.WriteLine(" 2) Add Entry");
            Console.WriteLine(" 3) Edit Entry");
            Console.WriteLine(" 4) Delete Entry");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Delete();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            List<Journal> entries = _journalRepository.GetAll();
            //foreach (Journal entry in entries)
            //{
            //    Console.WriteLine($"{entry.Id} {entry.Title}");
            //}

            for ( int i = 0; i< entries.Count; i++)
            {
                Journal j = entries[i];
                Console.WriteLine($"{i + 1} ) {j.Title} {j.Content}");
            }
        }

        private void Add()
        {
            Console.WriteLine("New Entry");
            Journal entry = new Journal();

            Console.Write("Title: ");
            entry.Title = Console.ReadLine();

            Console.Write("Your thoughts: ");
            entry.Content = Console.ReadLine();

            entry.CreateDateTime = DateTime.Now;

            _journalRepository.Insert(entry);
        }

        private Journal Choose(string prompt)
        {
            if (prompt == null)
            {
                prompt = "Please Choose a Journal Entry";
            }
            Console.WriteLine(prompt);
            List<Journal> journals = _journalRepository.GetAll();

            for (int i = 0; i < journals.Count; i++)
            {
                Journal j = journals[i];
                Console.WriteLine($"{i +1} {j.Title} ");
            }
            Console.Write("> ");
            try
            {
                return journals[int.Parse(Console.ReadLine()) - 1];
            }
            catch
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Edit ()
        {
            Journal journalToEdit = Choose("Which entry would you like to edit?");
            if (journalToEdit == null)
            {
                return;
            }

            Console.Write("New Journal Title (To keep the same leave blank) : ");
            string newTitle = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newTitle))
            {
                journalToEdit.Title = newTitle;
            }

            Console.Write("New Journal Content (To keep the same leave blank) : ");
            string newContent = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newContent))
            {
                journalToEdit.Content = newContent;
            }

            _journalRepository.Edit(journalToEdit);
        }

        private void Delete ()
        {
            List<Journal> journals = _journalRepository.GetAll();

            for ( int i = 0; i < journals.Count; i++)
            {
                Journal j = journals[i];
                Console.WriteLine($"{i + 1} ) {j.Title} {j.Content}");
            }

            Console.Write("Please Choose an entry to delete : ");
            int arrayPosition = (int.Parse(Console.ReadLine())-1); 

            _journalRepository.Remove(journals[arrayPosition].Id);

        }
    }
}

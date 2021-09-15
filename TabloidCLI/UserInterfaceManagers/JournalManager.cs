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
            foreach (Journal entry in entries)
            {
                Console.WriteLine(entry);
            }
        }

        private void Add()
        {
            Console.WriteLine("New Entry");
            Journal entry = new Journal();

            Console.Write("Title: ");
            entry.Title = Console.ReadLine();

            Console.Write("Your thoughts: ");
            entry.TextContent = Console.ReadLine();

            entry.CreationDate = DateTime.Now;

            _journalRepository.Insert(entry);
        }
    }
}

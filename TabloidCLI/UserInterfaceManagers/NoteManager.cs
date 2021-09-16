using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Repositories;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    class NoteManager : IUserInterfaceManager
    {

        private IUserInterfaceManager _parentUI;
        private NoteRepository _noteRepository;
        private bool color { get; set; } = true;


        public NoteManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _noteRepository = new NoteRepository(connectionString);
        }


        public IUserInterfaceManager Execute()
        {
            if (color)
            {
                Console.Clear();
            }

            Console.WriteLine("Note Manager Menu");
            Console.WriteLine("1 ) List Notes ");
            Console.WriteLine("2 ) Add Note");
            Console.WriteLine("3 ) Remove Note");
            Console.WriteLine("0 ) Go Back");

            Console.Write("> ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    List();
                    color = false;
                    return this;
                case "2":
                    //Add();
                    color = false;
                    return this;
                case "3":
                    //Remove();
                    color = false;
                    return this;
                case "0":
                    return _parentUI;
                default:
                    color = false;
                    Console.WriteLine("Invaild Selection");
                    return this;

            }


        }
            private void List()
            {
                List<Note> notes = _noteRepository.GetAll();

                foreach(Note n in notes)
                {
                    Console.WriteLine(n);
                }

            }

        public void Add()
        {

        }

        public void Remove()
        {
            
        }




    }
}

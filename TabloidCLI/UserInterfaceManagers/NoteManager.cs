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


        public NoteManager(IUserInterfaceManager parentUI, string connectionString, int blogId)
        {
            _parentUI = parentUI;
            _noteRepository = new NoteRepository(connectionString);
        }


        public IUserInterfaceManager Execute()
        {
            Console.Clear();

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
                    return this;
                case "2":
                    //Add();
                    return this;
                case "3":
                    //Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
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




    }
}

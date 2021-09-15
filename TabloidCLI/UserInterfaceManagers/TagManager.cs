using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class TagManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private TagRepository _tagRepository;

        public TagManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _tagRepository = new TagRepository(connectionString);
            
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Tag Menu");
            Console.WriteLine(" 1) List Tags");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Edit Tag");
            Console.WriteLine(" 4) Remove Tag");
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
                    Remove();
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
            List<Tag> tags = _tagRepository.GetAll();

          for(int i = 0; i < tags.Count; i++)
            {
                Tag t = tags[i];
                Console.WriteLine($"{i + 1} ) {t.Name}");
            }
        }

        private Tag Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Tag";
            }

            Console.WriteLine(prompt);

            List<Tag> tags = _tagRepository.GetAll();

            for (int i = 0; i < tags.Count; i++)
            {
                Tag t = tags[i];
                Console.WriteLine($" {i + 1} ) {t.Name}");
            }
            Console.Write(">  ");
            int chosenOne = int.Parse(Console.ReadLine());

            try
            {
                return tags[chosenOne -1];
            }
            catch
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }

          
        }

        private void Add()
        {
            Console.WriteLine("Please enter the name for your tag");
            string newTagName = Console.ReadLine();
            Tag newTag = new Tag();
            newTag.Name = newTagName;
            try
            {
                _tagRepository.Insert(newTag);
            }
            catch
            {
                Console.WriteLine("Opps!!! Its not you. Its me.");
            }

        }

        private void Edit()
        {
            Tag chosenOne = Choose("Which tag would you like to choose?");

            Console.WriteLine("New Name (Leave blank to keep the same) : ");
            //string name = Console.ReadLine();
            //if (!string.IsNullOrWhiteSpace(name)
            //    {
                
            //}

        }

        private void Remove()
        {
            List();
            Console.WriteLine("Please choose the Tag you would like to delete.");
            int chosenOne = (int.Parse(Console.ReadLine()) -1);

            _tagRepository.Delete(chosenOne);
        }
    }
}

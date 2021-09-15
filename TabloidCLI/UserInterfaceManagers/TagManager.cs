﻿using System;
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
            throw new NotImplementedException();
        }

        private void Remove()
        {
            throw new NotImplementedException();
        }
    }
}

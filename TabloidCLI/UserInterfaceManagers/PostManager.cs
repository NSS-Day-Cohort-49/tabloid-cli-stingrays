using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Add Post");
           

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;

                case "2":
                    AddPost();
                    return this;

                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        public void List()
        {
            List<Post> posts = _postRepository.GetAll();
            foreach (Post p in posts)
            {
                Console.WriteLine($"{p.Id} ) {p.Title} {p.Url} {p.PublishDateTime} ");
            }
        }

        public void AddPost()
        {

            Post post = new Post();

            Console.WriteLine("Please enter the title of your post.");
            post.Title = Console.ReadLine();
            Console.WriteLine("Please enter the url of your post");
            post.Url = Console.ReadLine();
            post.PublishDateTime = new DateTime();

            List<Author> authors = _authorRepository.GetAll();
            Console.WriteLine("Please select an Author for this post");
            foreach (Author a in authors)
            {
                Console.WriteLine($"{a.Id} ) {a.FullName}");
            }
            Console.Write("> ");
        

            post.Author = _authorRepository.Get(int.Parse(Console.ReadLine()));

            Console.WriteLine("Please select a blog for this post");

            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog b in blogs)
            {
                Console.WriteLine($"{b.Id} {b.Title}");
            }
            Console.WriteLine("> ");


            post.Blog = _blogRepository.Get(int.Parse(Console.ReadLine()));

            _postRepository.Insert(post);

            
        }


    }
}

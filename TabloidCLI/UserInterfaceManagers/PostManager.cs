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
        public bool color { get; set; } = true;

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
            if (color)
            {
                Console.Clear();
            }

            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Add Post");
            Console.WriteLine(" 3) Delete Post");
            Console.WriteLine(" 4) Edit Post");
            Console.WriteLine(" 0) Go Back");


            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    color = false;
                    return this;

                case "2":
                    AddPost();
                    color = false;
                    return this;

                case "3":
                    DeletePost();
                    color = false;
                    return this;
                case "4":
                    Edit();
                    return this;
                case "0":
                    return _parentUI;

                default:
                    Console.WriteLine("Invalid Selection");
                    color = false;
                    return this;
            }
        }

        private void List()
        {
            List<Post> posts = _postRepository.GetAll();
            foreach (Post p in posts)
            {
                Console.WriteLine($"{p.Id} ) {p.Title} {p.Url} {p.PublishDateTime} ");
            }
        }

        private void AddPost()
        {

            Post post = new Post();

            Console.WriteLine("Please enter the title of your post.");
            post.Title = Console.ReadLine();
            Console.WriteLine("Please enter the url of your post");
            post.Url = Console.ReadLine();
            Console.WriteLine("Please enter the publish date/time in MM/DD/YYYY format");
            post.PublishDateTime = DateTime.Parse(Console.ReadLine());

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
        private Post Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Author:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Edit()
        {
            Post postToEdit = Choose("Which post would you like to edit?");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New Title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                postToEdit.Title = title;
            }
            Console.Write("New URL (blank to leave unchanged: ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                postToEdit.Url = url;
            }
            Console.Write("New Publish Date/Time (blank to leave unchanged: ");
            string dateTime = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(dateTime))
            {
                postToEdit.PublishDateTime = DateTime.Parse(dateTime);
            }

            Console.WriteLine("Please select an Author for this post (blank to leave unchanged: ");
            List<Author> authors = _authorRepository.GetAll();
            foreach (Author a in authors)
            {
                Console.WriteLine($"{a.Id} ) {a.FullName}");
            }
            Console.Write("> ");
            string author = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(author))
            {
                postToEdit.Author = authors[int.Parse(author) - 1];
            }

            Console.WriteLine("Please select a Blog for this post (blank to leave unchanged: ");
            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog b in blogs)
            {
                Console.WriteLine($"{b.Id} ) {b.Title}");
            }
            Console.Write("> ");
            string blog = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(blog))
            {
                postToEdit.Blog = blogs[int.Parse(blog) -1];
            }

            _postRepository.Update(postToEdit);


        }

        private void DeletePost()
        {
            Post postToDelete = Choose("Which post would you like to delete?");
            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
            }
        }


    }
}

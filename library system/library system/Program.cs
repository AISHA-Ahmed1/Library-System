using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace library_system
{
    struct Book
    {
        public string Name;
        public int Id;
        public int Totalquantity;
        public int Borrowedquantity;
        public Book(int id,string name, int totalquantity)
        {
            Name = name;
            Id = id;
            Totalquantity = totalquantity;
            Borrowedquantity = 0;
        }
    }
         struct User
        {
            public string Name;
            public int Id;
            public List<int> BorrowedBooks;
            public User(string name,int id)
            {
                Name = name;
                Id = id;
                BorrowedBooks = new List<int>();//to link it with totalval
            }
        }
    
    internal class Program
    {
       static List<Book> books = new List<Book>();//to store books name id and librareval and totalval
       static Dictionary<int, string> bookdictionary = new Dictionary<int, string>();//id =>key,book name=>value,search books by id
        static List<User> users = new List<User>();//store users name id to link it with bookbroowed
        static Dictionary<string, string> userbookmap = new Dictionary<string, string>();

        static int menuChoocs()
        {
            
             
                Console.WriteLine("enter your menu choice [1,10]:");
                string input = Console.ReadLine();
                if(int.TryParse(input,out int choice) && choice>=1 && choice <= 10)
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine("invalid input");
                return menuChoocs();
                }
            
            
        }
        static void printLibrarymenu()
        {
            Console.WriteLine("library menu:"+"\n"+"1) add_book"+"\n"+"2) search book by prefix"+"\n"+
                "3) print who borrowed book by name"+"\n"+"4) print library by id"+"\n"+
                "5) print library by name"+"\n"+
                "6) add user"+"\n"+"7) user borrowed book"+"\n"+
                "8) user return book"+"\n"+"9) print users"+"\n"+"10) exit");
        }
        static void Main(string[] args)
        {
            printLibrarymenu();
            
            while (true)
            {
                int userchose;
                do
                {
                    userchose = menuChoocs();
                }
                while (userchose == -1);


                if (userchose == 1)
                {
                    AddBook();
                    
                }
                else if (userchose == 2)
                {
                    searchbooks_prefix();
                }
                else if (userchose == 3)
                {
                    print_whoborrowedbook_by_name();
                }
                else if (userchose == 4)
                {
                    print_library_by_id();
                }
                else if (userchose == 5)
                {
                    print_library_by_name();
                }
                else if (userchose == 6)
                {
                    add_useres();
                }
                else if (userchose == 7)
                {
                    user_borrow_book();
                }
                else if (userchose == 8)
                {
                    user_return_book();
                }
                else if (userchose == 9)
                {
                    print_users();
                }
                else if (userchose == 10)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("invalid choice");
                }
            }
        }
        static void AddBook()//1-
        {
            Console.Write("enter book info: id ,name , totalquantity");
            int id = int.Parse(Console.ReadLine());
            string name = Console.ReadLine();
            int totalquantity = int.Parse(Console.ReadLine());
            Book newbook = new Book(id,name,totalquantity);
            books.Add(newbook);
            bookdictionary[id] = name;
            
        }
        static void searchbooks_prefix()//-2
        {
            Console.WriteLine("enter book name prefix");
            string prefix = Console.ReadLine();
            var foundbook = books.Where(b => b.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)).ToList();//new list to store matchingbooks
            if (foundbook.Count > 0)
            {
                foreach(var book in foundbook)
                {
                    Console.WriteLine(book.Name);
                }
            }
            else {
                Console.WriteLine("not found ");
                
            }
        }
        static void print_whoborrowedbook_by_name()//-3
        {
            Console.WriteLine("enter book name");
            string bookname = Console.ReadLine();
            var borrowedusers = userbookmap.Where(entry => string.Equals(entry.Value, bookname, StringComparison.OrdinalIgnoreCase)).Select(enttry => enttry.Key).ToList();
            if (borrowedusers.Count > 0)
            {
                foreach (var user in borrowedusers)
                {
                    Console.WriteLine(user);
                }
            }
            else
            {
                Console.WriteLine("no users borrowed this book ");
            }
           
        }
        static void print_library_by_id()//-4
        {
            var storedbooks = books.OrderBy(b => b.Id).ToList();//new list for stored books ids
            foreach (var book in storedbooks)
            {
                Console.WriteLine($"id= {book.Id},name={book.Name},totalquanty={book.Totalquantity},borrowedquanty={book.Borrowedquantity}" );
            }
            
        }
        static void print_library_by_name()//-5
        {
            var storedbooks = books.OrderBy(b => b.Name).ToList();//new list for stored books names
            foreach (var book in storedbooks)
            {
                Console.WriteLine($"id= {book.Id},name={book.Name},totalquanty={book.Totalquantity},borrowedquanty={book.Borrowedquantity}");
            }
            
        }
        static void add_useres()//-6
        {
            Console.WriteLine("enter user name & national id ");
            string name = Console.ReadLine();
            int id = int.Parse(Console.ReadLine());
            User newuser = new User(name, id);
            users.Add(newuser);
        }
        static void user_borrow_book()//-7
        {
            Console.WriteLine("enter user name and book name");
            string username = Console.ReadLine();
            string borrowedbookname = Console.ReadLine();
            var book = books.FirstOrDefault(b => string.Equals(b.Name, borrowedbookname, StringComparison.OrdinalIgnoreCase));
            if (book.Name != null)
            {
                if (book.Totalquantity > book.Borrowedquantity)
                {
                    book.Borrowedquantity++;

                }
                Console.WriteLine("not avalibalb");
                
            }
        }
            static void user_return_book()//-8
        {
            Console.WriteLine("enter user name and book name");
            string username = Console.ReadLine();
            string returnedbookname = Console.ReadLine();
            var book = books.FirstOrDefault(b => string.Equals(b.Name,returnedbookname, StringComparison.OrdinalIgnoreCase));
            if (book.Name != null)
            {
                if (book.Totalquantity > book.Borrowedquantity)
                {
                    book.Borrowedquantity--;

                }
                Console.WriteLine("not avalibalb");
            }
            
        }
        static void print_users()//-9
        {
            foreach (var user in users)
            {
                Console.WriteLine($"name:{user.Name},id:{user.Id},borrowed bookes:");
                if (user.BorrowedBooks.Count>0)
                {
                    foreach (var bookid in user.BorrowedBooks)
                    {
                        Console.WriteLine($"{bookdictionary[bookid]} (id:{bookid})");
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("no books found");
                }
            }
        }
    }
} 


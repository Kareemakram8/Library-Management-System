using System;
using System.Collections.Generic;
using System.Linq;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int NumberOfCopies { get; set; }

    public Book(string title, string author, int numberOfCopies)
    {
        Title = title;
        Author = author;
        NumberOfCopies = numberOfCopies;
    }

    public override string ToString()
    {
        return $"{Title} by {Author} (Copies: {NumberOfCopies})";
    }
}

public class Member
{
    public int MemberID { get; set; }
    public string Name { get; set; }
    private List<Book> borrowedBooks = new List<Book>();

    public Member(int memberID, string name)
    {
        MemberID = memberID;
        Name = name;
    }

    public void BorrowBook(Book book)
    {
        borrowedBooks.Add(book);
    }

    public void ReturnBook(Book book)
    {
        borrowedBooks.Remove(book);
    }

    public List<Book> GetBorrowedBooks()
    {
        return borrowedBooks;
    }

    public override string ToString()
    {
        return $"{Name} (ID: {MemberID})";
    }
}

public class Librarian
{
    public int EmployeeID { get; set; }
    public string Name { get; set; }
    public List<Book> Books { get; private set; } = new List<Book>();
    public List<Member> Members { get; private set; } = new List<Member>();

    public Librarian(int employeeID, string name)
    {
        EmployeeID = employeeID;
        Name = name;
    }

    // Book Management
    public void AddBook(Book book)
    {
        Books.Add(book);
    }

    public void RemoveBook(Book book)
    {
        Books.Remove(book);
    }

    public void DisplayBooks()
    {
        Console.WriteLine("Books in the library:");
        foreach (var book in Books)
        {
            Console.WriteLine(book);
        }
    }

    // Member Management
    public void RegisterMember(Member member)
    {
        Members.Add(member);
    }

    public void DisplayMembers()
    {
        Console.WriteLine("Library Members:");
        foreach (var member in Members)
        {
            Console.WriteLine(member);
        }
    }

    // Borrowing System
    public void BorrowBook(Member member, Book book)
    {
        if (Books.Contains(book) && book.NumberOfCopies > 0)
        {
            member.BorrowBook(book);
            book.NumberOfCopies--;
            Console.WriteLine($"{member.Name} borrowed {book.Title}");
        }
        else
        {
            Console.WriteLine("Book is not available.");
        }
    }

    public void ReturnBook(Member member, Book book)
    {
        if (member.GetBorrowedBooks().Contains(book))
        {
            member.ReturnBook(book);
            book.NumberOfCopies++;
            Console.WriteLine($"{member.Name} returned {book.Title}");
        }
        else
        {
            Console.WriteLine("This book was not borrowed by the member.");
        }
    }
}

public class Program
{
    public static void Main()
    {
        Librarian librarian = new Librarian(1, "John Smith");

        // Add initial books
        librarian.AddBook(new Book("1984", "George Orwell", 5));
        librarian.AddBook(new Book("To Kill a Mockingbird", "Harper Lee", 3));

        // Register initial members
        librarian.RegisterMember(new Member(101, "Alice"));
        librarian.RegisterMember(new Member(102, "Bob"));

        while (true)
        {
            Console.WriteLine("\nLibrary Management System");
            Console.WriteLine("1. Display Books");
            Console.WriteLine("2. Display Members");
            Console.WriteLine("3. Borrow Book");
            Console.WriteLine("4. Return Book");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    librarian.DisplayBooks();
                    break;
                case "2":
                    librarian.DisplayMembers();
                    break;
                case "3":
                    Console.Write("Enter Member ID to borrow: ");
                    if (int.TryParse(Console.ReadLine(), out int borrowMemberID))
                    {
                        Console.Write("Enter Book Title to borrow: ");
                        string borrowBookTitle = Console.ReadLine();
                        Member borrowMember = librarian.Members.FirstOrDefault(m => m.MemberID == borrowMemberID);
                        Book borrowBook = librarian.Books.FirstOrDefault(b => b.Title.Equals(borrowBookTitle, StringComparison.OrdinalIgnoreCase));
                        if (borrowMember != null && borrowBook != null)
                        {
                            librarian.BorrowBook(borrowMember, borrowBook);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Member ID or Book Title.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Member ID.");
                    }
                    break;
                case "4":
                    Console.Write("Enter Member ID to return: ");
                    if (int.TryParse(Console.ReadLine(), out int returnMemberID))
                    {
                        Console.Write("Enter Book Title to return: ");
                        string returnBookTitle = Console.ReadLine();
                        Member returnMember = librarian.Members.FirstOrDefault(m => m.MemberID == returnMemberID);
                        Book returnBook = librarian.Books.FirstOrDefault(b => b.Title.Equals(returnBookTitle, StringComparison.OrdinalIgnoreCase));
                        if (returnMember != null && returnBook != null)
                        {
                            librarian.ReturnBook(returnMember, returnBook);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Member ID or Book Title.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Member ID.");
                    }
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}

using System;

namespace ConsoleApp1.Kutuphane;

public class LibraryManager
{
    // book1..book5 değişkenleri yerine diziler (YapilacaklarListesi'ndeki gibi)
    static string[] books = new string[5];

    // ===== SORU 3: ÖDÜNÇ ALINMIŞ OLARAK İŞARETLEME (1/4) — kitapların durumunu tutan dizi =====
    static bool[] isBorrowed = new bool[5];   // books[i] ödünç alındıysa isBorrowed[i] = true
    // ===== SORU 3 (1/4) SONU =====

    // ===== SORU 2: ÖDÜNÇ ALMA LİMİTİ (1/3) — sayaç ve sınır =====
    static int borrowedCount = 0;
    static int maxBorrow = 3;
    // ===== SORU 2 (1/3) SONU =====

    // ===== SORU 1: ARAMA ÖZELLİĞİ (1/2) — kitabı başlığa göre bulan yardımcı metot =====
    // Kitabın dizideki yerini bulur, yoksa -1 döner
    static int FindBook(string title)
    {
        for (int i = 0; i < books.Length; i++)
        {
            if (!string.IsNullOrEmpty(books[i]) && books[i].ToLower() == title.ToLower())
            {
                return i;
            }
        }
        return -1;
    }
    // ===== SORU 1 (1/2) SONU =====

    static void AddBook()
    {
        for (int i = 0; i < books.Length; i++)
        {
            if (string.IsNullOrEmpty(books[i]))
            {
                Console.WriteLine("Enter the title of the book to add:");
                books[i] = Console.ReadLine();
                isBorrowed[i] = false;
                return;
            }
        }
        Console.WriteLine("The library is full. No more books can be added.");
    }

    static void RemoveBook()
    {
        Console.WriteLine("Enter the title of the book to remove:");
        int index = FindBook(Console.ReadLine());

        if (index == -1)
        {
            Console.WriteLine("Book not found.");
        }
        else if (isBorrowed[index])
        {
            Console.WriteLine("This book is borrowed. It must be returned before it can be removed.");
        }
        else
        {
            books[index] = "";
        }
    }

    // ===== SORU 1: ARAMA ÖZELLİĞİ (2/2) — kullanıcıdan başlık alıp sonucu gösteren metot =====
    static void SearchBook()
    {
        Console.WriteLine("Enter the title of the book to search:");
        int index = FindBook(Console.ReadLine());

        if (index == -1)
        {
            Console.WriteLine("This book is not in the collection.");
        }
        else if (isBorrowed[index])
        {
            Console.WriteLine("\"" + books[index] + "\" is in the collection, but it is currently borrowed.");
        }
        else
        {
            Console.WriteLine("\"" + books[index] + "\" is available.");
        }
    }
    // ===== SORU 1 (2/2) SONU =====

    // Ödünç alma metodu: SORU 2 ve SORU 3'ün kodları bu metodun içinde
    static void BorrowBook()
    {
        // ===== SORU 2: ÖDÜNÇ ALMA LİMİTİ (2/3) — 3 kitap sınırı kontrolü =====
        if (borrowedCount >= maxBorrow)
        {
            Console.WriteLine("You have already borrowed " + maxBorrow + " books. Return one before borrowing another.");
            return;
        }
        // ===== SORU 2 (2/3) SONU =====

        Console.WriteLine("Enter the title of the book to borrow:");
        int index = FindBook(Console.ReadLine());

        if (index == -1)
        {
            Console.WriteLine("Book not found.");
        }
        else if (isBorrowed[index])
        {
            Console.WriteLine("This book is already borrowed.");
        }
        else
        {
            // ===== SORU 3: ÖDÜNÇ ALINMIŞ OLARAK İŞARETLEME (2/4) — kitabı ödünç alındı yap =====
            isBorrowed[index] = true;
            // ===== SORU 3 (2/4) SONU =====

            // ===== SORU 2: ÖDÜNÇ ALMA LİMİTİ (3/3) — sayacı artır / iadede azalt =====
            borrowedCount++;
            Console.WriteLine("You borrowed \"" + books[index] + "\". Borrowed books: " + borrowedCount + "/" + maxBorrow);
            // (iade edilince ReturnBook içinde borrowedCount-- ile azaltılıyor)
            // ===== SORU 2 (3/3) SONU =====
        }
    }

    // Ödünç alınan kitabı iade etme
    static void ReturnBook()
    {
        Console.WriteLine("Enter the title of the book to return:");
        int index = FindBook(Console.ReadLine());

        if (index == -1)
        {
            Console.WriteLine("Book not found.");
        }
        else if (!isBorrowed[index])
        {
            Console.WriteLine("This book was not borrowed.");
        }
        else
        {
            // ===== SORU 3: ÖDÜNÇ ALINMIŞ OLARAK İŞARETLEME (3/4) — iadede işareti kaldır =====
            isBorrowed[index] = false;
            // ===== SORU 3 (3/4) SONU =====
            borrowedCount--;
            Console.WriteLine("You returned \"" + books[index] + "\". Borrowed books: " + borrowedCount + "/" + maxBorrow);
        }
    }

    static void DisplayBooks()
    {
        Console.WriteLine("Available books:");
        for (int i = 0; i < books.Length; i++)
        {
            if (!string.IsNullOrEmpty(books[i]))
            {
                // ===== SORU 3: ÖDÜNÇ ALINMIŞ OLARAK İŞARETLEME (4/4) — listede "(borrowed)" göster =====
                if (isBorrowed[i])
                {
                    Console.WriteLine(books[i] + " (borrowed)");
                }
                else
                {
                    Console.WriteLine(books[i]);
                }
                // ===== SORU 3 (4/4) SONU =====
            }
        }
    }

    // Program.cs menüsünden çağrılan giriş noktası (eski Main)
    public static void Calistir()
    {
        while (true)
        {
            Console.WriteLine("What would you like to do? (add/remove/search/borrow/return/exit)");
            string action = Console.ReadLine().ToLower();

            switch (action)
            {
                case "add":
                    AddBook();
                    break;
                case "remove":
                    RemoveBook();
                    break;
                case "search":
                    SearchBook();
                    break;
                case "borrow":
                    BorrowBook();
                    break;
                case "return":
                    ReturnBook();
                    break;
                case "exit":
                    return;
                default:
                    Console.WriteLine("Invalid action. Please type 'add', 'remove', 'search', 'borrow', 'return', or 'exit'.");
                    break;
            }

            DisplayBooks();
        }
    }
}

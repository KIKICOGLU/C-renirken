using ConsoleApp1.Async;
using ConsoleApp1.Kutuphane;
using ConsoleApp1.OOP;
using ConsoleApp1.TasarimKaliplari;
using ConsoleApp1.Temeller;
using Pastel;

// Yeni konu eklemek için sadece bu listeye bir satır ekle.
var konular = new List<(string Ad, Func<Task> Calistir)>
{
    ("Person (Temeller)",            () => { PersonTesti.Calistir(); return Task.CompletedTask; }),
    ("Yapılacaklar Listesi",         () => { YapilacaklarListesi.Calistir(); return Task.CompletedTask; }),
    ("Hayvanlar (OOP)",              () => { HayvanlarTesti.Calistir(); return Task.CompletedTask; }),
    ("Async Örnek",                  AsyncTesti.Calistir),
    ("Tasarım Kalıpları",            () => { TasarimKaliplariTesti.Calistir(); return Task.CompletedTask; }),
    ("Kütüphane Yönetimi (Final)",   () => { LibraryManager.Calistir(); return Task.CompletedTask; }),
};

while (true)
{
    Console.WriteLine("\n=== KONU MENÜSÜ ===".Pastel("#00BFFF"));
    for (int i = 0; i < konular.Count; i++)
        Console.WriteLine($"{i + 1}. {konular[i].Ad}");
    Console.WriteLine("0. Çıkış");
    Console.Write("Seçim: ");

    string? secim = Console.ReadLine();
    if (secim == "0") break;

    if (int.TryParse(secim, out int no) && no >= 1 && no <= konular.Count)
    {
        Console.WriteLine();
        await konular[no - 1].Calistir();
    }
    else
    {
        Console.WriteLine("Geçersiz seçim.".Pastel("#FF5555"));
    }
}

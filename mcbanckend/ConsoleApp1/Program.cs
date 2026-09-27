using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class YapilacaklarListesi
{

    public static string[] gorevler = new string[10];
    public static int gorevSayisi = 0;

    public static void GorevEkle()
    {
        Console.WriteLine("Yeni bir görev girin:");
        gorevler[gorevSayisi] = Console.ReadLine();
        gorevSayisi++;
    }

    public static void GorevleriGoruntule()
    {
        for (int i = 0; i < gorevSayisi; i++)
        {
            Console.WriteLine((i + 1) + ". " + gorevler[i]);
        }
    }

    public static void GoreviTamamla()
    {
        Console.WriteLine("Tamamlandı olarak işaretlenecek görevin numarasını girin:");
        int gorevNumarasi = int.Parse(Console.ReadLine()) - 1;

        if (gorevNumarasi >= 0 && gorevNumarasi < gorevSayisi)
        {
            gorevler[gorevNumarasi] = gorevler[gorevNumarasi] + " (Tamamlandı)";
            Console.WriteLine("Görev tamamlandı olarak işaretlendi.");
        }
        else
        {
            Console.WriteLine("Geçersiz görev numarası.");
        }
    }

    public static void Main(string[] args)
    {
        bool calisiyor = true;

        while (calisiyor)
        {
            Console.WriteLine("Ne yapmak istersiniz?");
            Console.WriteLine("1. Görev ekle");
            Console.WriteLine("2. Görevleri görüntüle");
            Console.WriteLine("3. Görevi tamamlandı olarak işaretle");
            Console.WriteLine("4. Çıkış");

            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    GorevEkle();
                    break;
                case "2":
                    GorevleriGoruntule();
                    break;
                case "3":
                    GoreviTamamla();
                    break;
                case "4":
                    calisiyor = false;
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                    break;
            }
        }
    }
}
using System;
using System.Collections.Generic;

namespace TasarimKaliplariCalismasi
{
    /* 
     ===================================================================================
     TASARIM KALIPLARI (DESIGN PATTERNS) ÇALIŞMA NOTLARI
     ===================================================================================
     Bu dosya, yazılım geliştirmede sıkça karşılaşılan problemlere karşı üretilmiş 
     standart çözüm yolları olan Tasarim Kalıplarını anlamak için hazırlanmıştır.
     Her bir kalıp farklı bir senaryo üzerinden incelenmiştir.
     ===================================================================================
    */

    #region 1. SINGLETON (Tekil) TASARIM KALIBI
    /*
     * KULLANIM AMACI: Bir sınıfın uygulama çalıştığı sürece SADECE BİR TANE örneğinin (instance) 
     * oluşturulduğundan emin olmak ve bu örneğe global bir erişim noktası sağlamaktır.
     * 
     * YENİ ÖRNEK SENARYO: Uygulama genelinde loglama yapan bir 'Logger' (Kayıtçı) sınıfı. 
     * Her sınıfın kendi Logger'ını üretmesi yerine, sistemdeki tek bir Logger üzerinden 
     * dosyaya veya ekrana log yazılması bellek yönetimi ve dosya çakışmaları açısından daha güvenlidir.
     */

    public class Logger
    {
        // Kendi türünden statik bir nesne tutuyoruz. (Henüz oluşturulmadı)
        private static Logger _instance;

        // Çoklu iş parçacığı (multi-threading) ortamlarında aynı anda iki nesne 
        // oluşturulmasını engellemek için bir kilit nesnesi.
        private static readonly object _lockObject = new object();

        // Kurucu metodu (constructor) 'private' yapıyoruz ki, 
        // dışarıdan "new Logger()" şeklinde yeni nesne üretilemesin.
        private Logger() { }

        // Sınıfın tek örneğine ulaşılacak global erişim metodu
        public static Logger GetInstance()
        {
            // Eğer nesne daha önce oluşturulmamışsa içeri gir
            if (_instance == null)
            {
                // Aynı anda gelen thread'leri sıraya sok
                lock (_lockObject)
                {
                    // Çift kontrol (Double-check locking) mekanizması
                    if (_instance == null)
                    {
                        _instance = new Logger();
                    }
                }
            }
            return _instance;
        }

        public void LogYaz(string mesaj)
        {
            Console.WriteLine($"[SİSTEM LOG] : {mesaj}");
        }
    }
    #endregion

    #region 2. ADAPTER (Adaptör) TASARIM KALIBI
    /*
     * KULLANIM AMACI: Birbiriyle uyumsuz arayüzlere (interface) sahip sınıfların 
     * birlikte çalışmasını sağlamaktır. Mevcut bir sınıfın kodunu değiştirmeden, 
     * onu beklenen başka bir arayüze uydurur.
     * 
     * YENİ ÖRNEK SENARYO: Yeni nesil bir e-ticaret sitemiz var ve ödemeleri "IModernOdeme" 
     * arayüzü ile alıyoruz. Ancak sisteme çok eski bir altyapısı olan "EskiBankaSistemi"ni 
     * entegre etmemiz gerekiyor. Eski sistemin metotları modern arayüzümüzle uyuşmuyor.
     */

    // İstemcinin (uygulamamızın) beklediği hedef arayüz
    public interface IModernOdeme
    {
        void OdemeYap(decimal tutar);
    }

    // Uyumsuz olan eski sistem (Adaptee / Uyarlanacak sınıf)
    public class EskiBankaSistemi
    {
        // Eski sistemin metot ismi ve parametreleri bizimkinden farklı
        public void EskiAltyapiOdemeGonder(float miktar, string paraBirimi)
        {
            Console.WriteLine($"Eski sistem üzerinden {miktar} {paraBirimi} tahsil edildi.");
        }
    }

    // Adaptör sınıfımız. Modern arayüzü uygular ama arka planda eski sistemi çalıştırır.
    public class BankaAdapter : IModernOdeme
    {
        private EskiBankaSistemi _eskiSistem;

        public BankaAdapter(EskiBankaSistemi eskiSistem)
        {
            _eskiSistem = eskiSistem;
        }

        public void OdemeYap(decimal tutar)
        {
            // Modern arayüzden gelen çağrıyı, eski sistemin anlayacağı şekle çeviriyoruz
            float eskiSistemTutari = (float)tutar;
            _eskiSistem.EskiAltyapiOdemeGonder(eskiSistemTutari, "TL");
        }
    }
    #endregion

    #region 3. FACTORY (Fabrika) TASARIM KALIBI
    /*
     * KULLANIM AMACI: Nesne oluşturma mantığını istemciden gizlemek ve 
     * oluşturulacak nesnenin tipine çalışma zamanında (runtime) karar vermektir.
     * 
     * YENİ ÖRNEK SENARYO: Bir taşıt üretim fabrikası (AracFabrikasi).
     * İstemci sadece "Bana Araba ver" veya "Bana Motor ver" der, arka planda 
     * new Araba() mi yoksa new Motor() mu yapılacağı fabrikada gizlidir.
     */

    // Ortak üst sınıf veya arayüz
    public abstract class Arac
    {
        public abstract void KornaCal();
    }

    public class Araba : Arac
    {
        public override void KornaCal()
        {
            Console.WriteLine("Araba: Düt düt!");
        }
    }

    public class Motosiklet : Arac
    {
        public override void KornaCal()
        {
            Console.WriteLine("Motosiklet: Bip bip!");
        }
    }

    // Üretimi gerçekleştiren fabrika sınıfı
    public class AracFabrikasi
    {
        public static Arac AracUret(string aracTipi)
        {
            if (aracTipi.ToLower() == "araba")
            {
                return new Araba();
            }
            else if (aracTipi.ToLower() == "motor")
            {
                return new Motosiklet();
            }
            else
            {
                throw new ArgumentException("Bilinmeyen araç tipi istendi!");
            }
        }
    }
    #endregion

    #region 4. OBSERVER (Gözlemci) TASARIM KALIBI
    /*
     * KULLANIM AMACI: Bir nesnenin durumu değiştiğinde, bu nesneye bağımlı olan (onu gözlemleyen)
     * diğer tüm nesnelerin otomatik olarak uyarılmasını (haberdar edilmesini) sağlar.
     * 
     * YENİ ÖRNEK SENARYO: Bir Hava Durumu İstasyonu (Subject/Yayıncı) ve bu istasyondan 
     * veri bekleyen mobil uygulamalar (Observer/Aboneler). Hava değiştiğinde tüm uygulamalara 
     * otomatik bildirim gider.
     */

    // Abone (Observer) arayüzü
    public interface IAbone
    {
        void BildirimAl(string havaDurumu);
    }

    // Somut Aboneler (Mobil uygulamalar, haber siteleri vs.)
    public class MobilUygulama : IAbone
    {
        private string _uygulamaAdi;
        public MobilUygulama(string ad) { _uygulamaAdi = ad; }

        public void BildirimAl(string havaDurumu)
        {
            Console.WriteLine($"{_uygulamaAdi} Bildirim: Yeni hava durumu '{havaDurumu}' olarak güncellendi.");
        }
    }

    // Yayıncı (Subject) sınıfı
    public class HavaDurumuIstasyonu
    {
        // Gözlemcilerin listesini tutarız
        private List<IAbone> _aboneler = new List<IAbone>();

        // Abone ekleme
        public void AboneEkle(IAbone abone)
        {
            _aboneler.Add(abone);
        }

        // Abone çıkarma
        public void AboneCikar(IAbone abone)
        {
            _aboneler.Remove(abone);
        }

        // Durum değiştiğinde tüm aboneleri bilgilendiren metot
        public void HavaDurumuDegisti(string yeniHavaDurumu)
        {
            Console.WriteLine($"\n--- HAVA DURUMU İSTASYONU GÜNCELLEMESİ: {yeniHavaDurumu} ---");
            foreach (var abone in _aboneler)
            {
                abone.BildirimAl(yeniHavaDurumu);
            }
        }
    }
    #endregion

    #region TEST / KULLANIM (Main Metodu)
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SINGLETON TEST ===");
            Logger log1 = Logger.GetInstance();
            Logger log2 = Logger.GetInstance();
            log1.LogYaz("Uygulama başlatıldı.");
            // log1 ve log2 bellekte aynı referansı gösterir
            Console.WriteLine($"log1 ve log2 aynı nesne mi? : {object.ReferenceEquals(log1, log2)}\n");

            Console.WriteLine("=== ADAPTER TEST ===");
            EskiBankaSistemi eskiBanka = new EskiBankaSistemi();
            IModernOdeme odemeSistemi = new BankaAdapter(eskiBanka);
            odemeSistemi.OdemeYap(150.50m); // Modern sistemi çağırdık, eski sistemi çalıştırdı.
            Console.WriteLine();

            Console.WriteLine("=== FACTORY TEST ===");
            Arac benimArabam = AracFabrikasi.AracUret("araba");
            benimArabam.KornaCal();
            Arac benimMotorum = AracFabrikasi.AracUret("motor");
            benimMotorum.KornaCal();
            Console.WriteLine();

            Console.WriteLine("=== OBSERVER TEST ===");
            HavaDurumuIstasyonu istasyon = new HavaDurumuIstasyonu();
            
            MobilUygulama iosApp = new MobilUygulama("iOS Hava Durumu");
            MobilUygulama androidApp = new MobilUygulama("Android Hava Durumu");

            istasyon.AboneEkle(iosApp);
            istasyon.AboneEkle(androidApp);

            // İstasyon güncellendiğinde tüm abonelere (uygulamalara) bildirim gidecek
            istasyon.HavaDurumuDegisti("Güneşli, 25 Derece");
            istasyon.HavaDurumuDegisti("Yağmurlu, 15 Derece");

            Console.ReadLine();
        }
    }
    #endregion
}
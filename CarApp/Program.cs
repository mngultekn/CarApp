using System;
using System.Collections.Generic;

namespace AracYonetimSistemi
{
    class Arac
    {
        public string Model { get; set; }
        public string Tip { get; set; } 
        public decimal Fiyat { get; set; }

        public Arac(string model, string tip, decimal fiyat)
        {
            Model = model;
            Tip = tip;
            Fiyat = fiyat;
        }

        public override string ToString()
        {
            return $"Model: {Model}, Tip: {Tip}, Fiyat: {Fiyat:C}";
        }
    }

    class Musteri
    {
        public string AdSoyad { get; set; }
        public string Telefon { get; set; }

        public Musteri(string adSoyad, string telefon)
        {
            AdSoyad = adSoyad;
            Telefon = telefon;
        }

        public override string ToString()
        {
            return $"Müşteri: {AdSoyad}, Telefon: {Telefon}";
        }
    }

    class Yonetic
    {
        private List<Arac> araclar = new List<Arac>();
        private List<Musteri> musteriler = new List<Musteri>();
        private Random random = new Random();

        private readonly string[] aracModeller = { "Audi A4", "BMW X5", "Mercedes C200", "Ford Ranger", "Toyota Hilux", "Renault Clio", "Volkswagen Golf", "Opel Astra", "Audi Q7" , "BMW M4" , "Cherry Tico 8" , "Wolkswagen Scirocco" , "Toyata Carolla" , "Ford Tourneo Connect" , "Honda Civic"};
        private readonly string[] isimler = { "Ali", "Ayşe", "Mehmet", "Fatma", "Ahmet", "Elif", "Can", "Deniz" , "Emirhan" , "Yağız" , "Egemen" , "Sami" , "Kadir"};
        private readonly string[] soyisimler = { "Yılmaz", "Kaya", "Demir", "Şahin", "Çelik", "Aydın", "Koç", "Turan" , "Yakar" , "Garaca" , "Pehlivan"};

        public string RandomAracEkle()
        {
            string model = aracModeller[random.Next(aracModeller.Length)];
            string tip = random.Next(2) == 0 ? "Araba" : "Kamyonet" ;
            decimal fiyat = random.Next(100000, 1000000); 

            araclar.Add(new Arac(model, tip, fiyat));
        }

        public void RandomMusteriEkle()
        {
            string ad = isimler[random.Next(isimler.Length)];
            string soyad = soyisimler[random.Next(soyisimler.Length)];
            string adSoyad = $"{ad} {soyad}";
            string telefon = $"05{random.Next(30, 55)}{random.Next(10000000, 99999999)}";

            musteriler.Add(new Musteri(adSoyad, telefon));
        }

        public void AracEkle()
        {
            Console.Write("Model: ");
            string model = Console.ReadLine();

            Console.Write("Tip (Araba/Kamyonet): ");
            string tip = Console.ReadLine();

            Console.Write("Fiyat: ");
            decimal fiyat;
            while (!decimal.TryParse(Console.ReadLine(), out fiyat) || fiyat <= 0)
            {
                Console.Write("Geçerli bir fiyat girin: ");
            }

            araclar.Add(new Arac(model, tip, fiyat));
            Console.WriteLine($"{model} başarıyla eklendi!\n");
        }

        public void MusteriEkle()
        {
            Console.Write("Müşteri Adı ve Soyadı: ");
            string adSoyad = Console.ReadLine();

            Console.Write("Telefon: ");
            string telefon = Console.ReadLine();

            musteriler.Add(new Musteri(adSoyad, telefon));
            Console.WriteLine($"{adSoyad} başarıyla eklendi!\n");
        }

        public void AraclariListele()
        {
            if (araclar.Count == 0)
            {
                Console.WriteLine("Kayıtlı araç yok!\n");
                return;
            }

            Console.WriteLine("--- Araçlar Listesi ---");
            foreach (var arac in araclar)
            {
                Console.WriteLine(arac);
            }
            Console.WriteLine();
        }

        public void MusterileriListele()
        {
            if (musteriler.Count == 0)
            {
                Console.WriteLine("Kayıtlı müşteri yok!\n");
                return;
            }

            Console.WriteLine("--- Müşteriler Listesi ---");
            foreach (var musteri in musteriler)
            {
                Console.WriteLine(musteri);
            }
            Console.WriteLine();
        }

        public void AracSil(string model)
        {
            var arac = araclar.Find(a => a.Model.Equals(model, StringComparison.OrdinalIgnoreCase));
            if (arac != null)
            {
                araclar.Remove(arac);
                Console.WriteLine($"{model} başarıyla silindi!\n");
            }
            else
            {
                Console.WriteLine($"{model} bulunamadı!\n");
            }
        }

        public void MusteriSil(string adSoyad)
        {
            var musteri = musteriler.Find(m => m.AdSoyad.Equals(adSoyad, StringComparison.OrdinalIgnoreCase));
            if (musteri != null)
            {
                musteriler.Remove(musteri);
                Console.WriteLine($"{adSoyad} başarıyla silindi!\n");
            }
            else
            {
                Console.WriteLine($"{adSoyad} bulunamadı!\n");
            }
        }

        public void AracSat(string model)
        {
            var arac = araclar.Find(a => a.Model.Equals(model, StringComparison.OrdinalIgnoreCase));
            if (arac != null)
            {
                Console.WriteLine("--- Satış İndirimleri ---");
                Console.WriteLine("1. %5 indirim");
                Console.WriteLine("2. %10 indirim");
                Console.WriteLine("3. %15 indirim");
                Console.Write("İndirimi seçin: ");
                int indirimSecim = int.Parse(Console.ReadLine());

                decimal indirimOrani = indirimSecim switch
                {
                    1 => 0.05m,
                    2 => 0.10m,
                    3 => 0.15m,
                    _ => 0m
                };

                decimal indirimTutari = arac.Fiyat * indirimOrani;
                decimal satisTutari = arac.Fiyat - indirimTutari;

                Console.WriteLine($"Satış İndirimi: {indirimTutari:C}");
                Console.WriteLine($"Satış Tutarı: {satisTutari:C}\n");

                araclar.Remove(arac); 
            }
            else
            {
                Console.WriteLine($"{model} bulunamadı!\n");
            }
        }

        public void AracKirala(string model)
        {
            var arac = araclar.Find(a => a.Model.Equals(model, StringComparison.OrdinalIgnoreCase));
            if (arac != null)
            {
                Console.Write("Kiralama süresi (gün): ");
                int kiralamaSuresi = int.Parse(Console.ReadLine());

                Console.WriteLine("--- Kiralama İndirimleri ---");
                Console.WriteLine("1. %5 indirim (7 gün üstü)");
                Console.WriteLine("2. %10 indirim (14 gün üstü)");

                decimal gunlukKiralamaFiyati = arac.Fiyat / 10; 
                decimal kiralamaTutari = gunlukKiralamaFiyati * kiralamaSuresi;
                decimal indirimOrani = kiralamaSuresi switch
                {
                    > 14 => 0.10m,
                    > 7 => 0.05m,
                    _ => 0m
                };

                decimal indirimTutari = kiralamaTutari * indirimOrani;
                decimal odenecekTutar = kiralamaTutari - indirimTutari;

                Console.WriteLine($"Kiralama İndirimi: {indirimTutari:C}");
                Console.WriteLine($"Kiralama Tutarı: {odenecekTutar:C}\n");
            }
            else
            {
                Console.WriteLine($"{model} bulunamadı!\n");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Yonetic yonetic = new Yonetic();
            bool devam = true;
            for (int i = 0; i < 5; i++)
            {
                yonetic.RandomAracEkle();
                yonetic.RandomMusteriEkle();
            }

            while (devam)
            {
                Console.WriteLine("--- Araç Yönetim Sistemi ---");
                Console.WriteLine("1. Araçları Listele");
                Console.WriteLine("2. Müşterileri Listele");
                Console.WriteLine("3. Araç Ekle");
                Console.WriteLine("4. Müşteri Ekle");
                Console.WriteLine("5. Araç Sat");
                Console.WriteLine("6. Araç Kirala");
                Console.WriteLine("7. Araç Sil");
                Console.WriteLine("8. Müşteri Sil");
                Console.WriteLine("9. Çıkış");
                Console.Write("Seçiminizi yapın: ");
                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        yonetic.AraclariListele();
                        break;
                    case "2":
                        yonetic.MusterileriListele();
                        break;
                    case "3":
                        yonetic.AracEkle();
                        break;
                    case "4":
                        yonetic.MusteriEkle();
                        break;
                    case "5":
                        Console.Write("Satılacak aracın modeli: ");
                        string satilacakModel = Console.ReadLine();
                        yonetic.AracSat(satilacakModel);
                        break;
                    case "6":
                        Console.Write("Kiralayacak aracın modeli: ");
                        string kiralanacakModel = Console.ReadLine();
                        yonetic.AracKirala(kiralanacakModel);
                        break;
                    case "7":
                        Console.Write("Silinecek aracın modeli: ");
                        string silinecekModel = Console.ReadLine();
                        yonetic.AracSil(silinecekModel);
                        break;
                    case "8":
                        Console.Write("Silinecek müşterinin adı ve soyadı: ");
                        string silinecekMusteri = Console.ReadLine();
                        yonetic.MusteriSil(silinecekMusteri);
                        break;
                    case "9":
                        devam = false;
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim, tekrar deneyin.");
                        break;
                }
            }
            Console.WriteLine("Çıkış yapılıyor...");
        }
    }
}

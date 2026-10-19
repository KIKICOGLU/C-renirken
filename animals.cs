using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 1. KURALLAR (Interface)
public interface IAnimal
{
    void Eat();
}

// 2. TEMEL SINIF (Interface'i uygulayan ana şablon)
public class Animal : IAnimal
{
    public virtual void MakeSound()
    {
        Console.WriteLine("Some generic animal sound");
    }

    public void Eat()
    {
        Console.WriteLine("Some generic animal food");
    }
}

// 3. ALT SINIFLAR (Animal'dan miras alanlar)
public class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Hav hav!");
    }

    // Uyarı: Animal sınıfındaki Eat metodu 'virtual' olmadığı için burada 'new' kullanarak onu eziyoruz (gizliyoruz).
    public new void Eat()
    {
        Console.WriteLine("Köpek et yer.");
    }
}

public class Cat : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Miyav!");
    }

    public new void Eat()
    {
        Console.WriteLine("Kedi mama yer.");
    }
}

// 4. TEST ALANI (Kumandadan çağrılacak asıl sınıfımız)
public class HayvanlarTesti
{
    // Program.cs'den sadece bu metodu çağıracağız
    public static void Calistir()
    {
        Dog myDog = new Dog();
        Cat myCat = new Cat();

        myDog.MakeSound();
        myDog.Eat();

        Console.WriteLine("---");

        myCat.MakeSound();
        myCat.Eat();
    }
}
        
    
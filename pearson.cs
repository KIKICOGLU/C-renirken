using System.Security.Cryptography;

public class Person
{
        public string Name{get;set;}
        public int Age{get;set;}

        public void greet()
    {
        Console.WriteLine( "hello " + Name + " nabersin kankim");
    }
}

        
        public class Program{
    public static void Main(string[] args)
    {
        // Create the first Person object
        Person friend = new Person();
        friend.Name = "eren kikic";
        friend.Age = 30;
        
        // Create the second Person object
        Person colleague = new Person();
        colleague.Name = "ayla kikic";
        colleague.Age = 25;
        colleague.greet();

        
        Person newPerson = new Person();
        newPerson.Name = "Mike Johnson";
        newPerson.Age = 35;

    friend.greet();
    colleague.greet();
    newPerson.greet();

    }
}
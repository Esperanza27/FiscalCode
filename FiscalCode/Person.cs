namespace FiscalCode;

/*public enum Sex
{ 
    Male,
    Female
}*/
public abstract class Person
{
    public string Name { get;}
    public string Surname { get;}
    public string PlaceOfBirth { get;}
    public DateOnly DateOfBirth { get;}

    public Person(string name, string surname, string placeOfBirth, DateOnly dateOfBirth)
    { 
        const int NameMinLength = 2;
        const int SurnameMinLength = 2;
        const int PlaceOfBirthMinLength = 2;


        ArgumentNullException.ThrowIfNull( name );
        ArgumentNullException.ThrowIfNull( surname );
        ArgumentNullException.ThrowIfNull( placeOfBirth );

        name = name.Trim();
        surname = surname.Trim();
        placeOfBirth = placeOfBirth.Trim();

        if (name.Length < NameMinLength)
            throw new ArgumentException("The name must contain at least 2 letters");
        if (surname.Length < SurnameMinLength)
            throw new ArgumentException("The name must contain at least 2 letters");
        if (placeOfBirth.Length < PlaceOfBirthMinLength)
            throw new ArgumentException("The name must contain at least 2 letters");

        Name = name;
        Surname = surname;
        PlaceOfBirth = placeOfBirth;
        DateOfBirth = dateOfBirth;

    }

    public Person(Person person) : this(person.Name, person.Surname, person.PlaceOfBirth, person.DateOfBirth) { }
    
   
}
public class Man : Person
{
    public Man(Person person) : base(person) { }
    public Man(string name, string surname, string placeOfBirth, DateOnly dateOfBirth) : base(name, surname, placeOfBirth, dateOfBirth){}
}

public class Woman : Person
{
    public Woman(Person person) : base(person) { }
    public Woman(string name, string surname, string placeOfBirth, DateOnly dateOfBirth) : base(name, surname, placeOfBirth, dateOfBirth) { }

}
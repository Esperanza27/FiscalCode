namespace FiscalCode;

public class PersonUtils
{
    
    public static string GetFiscalSurname(string surname)
    {
        string surnameFiscal = $"{FiscalCodeUtils.GetConsonants(surname.ToUpper())}"+$"{FiscalCodeUtils.GetVowels(surname.ToUpper())}";
        return surnameFiscal.Length >= 3 ? surnameFiscal[..3] : surnameFiscal.PadRight(3, 'X');
    }

    public static string GetFiscalName(string name)
    {
        string consonants = FiscalCodeUtils.GetConsonants(name.ToUpper());
        string nameFiscal = string.Empty;
        if (consonants.Length >= 4)
        {
            nameFiscal = $"{consonants[0]}{consonants[2]}{consonants[3]}";
        }
        else
        {
            nameFiscal = $"{consonants}{FiscalCodeUtils.GetVowels(name.ToUpper())}";
        }
        return nameFiscal.Length >= 3 ? nameFiscal[..3] : nameFiscal.PadRight(3, 'X');

    }
    public static string GetFiscalDateOfBirth(DateOnly dateOfBirth)
    {
        string month = FiscalCodeUtils.Month(dateOfBirth.Month).ToString();
        string year = (dateOfBirth.Year).ToString().Substring(2);

        return $"{year}{month}";
    }
    public static string GetFiscalDayOfBirth(Person person)
    {
        int day = person.DateOfBirth.Day;

        if (person is Man)
        {
            return day <= 9 ? $"0{day}" : day.ToString();
        }
        if (person is Woman)
        {
            return (day + 40).ToString();
        }
        return day.ToString();

    }

    public static string GetFiscalPlaceOfBirth(string place)
    {
        place = place.ToUpper();
        // Cerca nei comuni italiani
        if (CadastralCodes.PlaceToCodeMap.ContainsKey(place))
        {
            return CadastralCodes.PlaceToCodeMap[place];
        }

        // Se non trovato nei comuni italiani, cerca nei paesi esteri
        if (CadastralCodes.ForeignCountryCodes.ContainsKey(place))
        {
            return CadastralCodes.ForeignCountryCodes[place];
        }

        // Se il luogo non è trovato in nessuno dei due dizionari, genera un'eccezione
        throw new KeyNotFoundException($"Il luogo di nascita '{place}' non è stato trovato nei codici Belfiore.");
    }

    public static string GetFiscalCode(Person person)
    {
        return $"{GetFiscalSurname(person.Surname)}" +
            $"{GetFiscalName(person.Name)}" +
            $"{GetFiscalDateOfBirth(person.DateOfBirth)}" +
            $"{GetFiscalDayOfBirth(person)}" +
            $"{GetFiscalPlaceOfBirth(person.PlaceOfBirth)}";
    }

}

/*
  using Microsoft.VisualBasic;
using System.Xml.Linq;

namespace FiscalCode;

internal class PersonUtils
{
    public static string GetFiscalCode(Person person)
    {
        return $"{GetSurname(person)}{GetFiscalName(person)}{GetDateOfBirth(person)}{GetDayOfBirth(person)}";
    }

    public static string GetSurname(Person person) 
    {   
        string surname = person.Surname.ToUpper();
        string surnameFiscal = $"{Consonants(surname)}{Vowels(surname)}";

        return surnameFiscal.Length >=3? surnameFiscal[..3] : surnameFiscal.PadRight(3,'X'); 
    }

    public static string GetFiscalName(Person person)
    {
        string name = person.Name.ToUpper();
        string consonants = Consonants(name);
        string nameFiscal = "";
        if (consonants.Length >= 4)
        {
            nameFiscal = $"{consonants[0]}{consonants[2]}{consonants[3]}";
        }
        else
        {
            nameFiscal = $"{Consonants(name)}{Vowels(name)}";
        }
        return nameFiscal.Length >= 3 ? nameFiscal[..3] : nameFiscal.PadRight(3, 'X');

    }
    public static string GetDateOfBirth(Person person) 
    {
        DateOnly data = person.DateOfBirth;
        string month = Month(data.Month).ToString();
        string year = (data.Year).ToString();
        string dateFiscal = month + year;

        return dateFiscal; 
    }
    public static string GetDayOfBirth(Person person)
    {   
        int day = person.DateOfBirth.Day;

        if (person is Man)
        { 
            return day <= 9 ? day.ToString().Insert(0,"0") : day.ToString();
        }
        if (person is Woman) 
        {
            return (day + 40).ToString();
        }
        return day.ToString();
     
    }

    public static string GetPlaceOfBirth(Person person)
    {

        return "";
    }

    //utils
    public static string Consonants( string value)
    {
        string consonants = "";
        const string Vowels = "AEIOU";
        foreach (var c in value)
        {
            if (!Vowels.Contains(c))
            {
                consonants += c;
            }
            else
            {
                continue;
            }
        }
        // string fiscal = (consonants + vowels); //.Substring(0, 3);
        return consonants;

    }
    public static string Vowels(string value)
    {
        string vowels = "";
        const string Vowels = "AEIOU";
        foreach (var c in value)
        {
            if (Vowels.Contains(c))
            {
                vowels += c;
            }
        }
        return vowels;

    }
    public static char Month(int month)
    {
        switch (month)
        { 
        case 01: return 'A';
        case 02: return 'B';
        case 03: return 'C';
        case 04: return 'D';
        case 05: return 'E';
        case 06: return 'H';
        case 07: return 'L';
        case 08: return 'M';
        case 09: return 'P';
        case 10: return 'R';
        case 11: return 'S';
        case 12: return 'T';
        default: throw new ArgumentOutOfRangeException(nameof(month), "Il valore deve essere compreso tra 1 e 12.");
        }
    }

}
 */
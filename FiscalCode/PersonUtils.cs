namespace FiscalCode;

public class PersonUtils
{
    public static string GetFiscalSurname(string surname)
    {
        string surnameFiscal = $"{FiscalCodeUtils.GetConsonants(surname.ToUpper())}" + $"{FiscalCodeUtils.GetVowels(surname.ToUpper())}";
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

    public static char GetControlCharacter(string temporyCode)
    {
       return FiscalCodeUtils.GetCodeAlphanumericCharacters(temporyCode);
    }

    public static string GetTemporaryCode(Person person)
    {
        return $"{GetFiscalSurname(person.Surname)}" +
            $"{GetFiscalName(person.Name)}" +
            $"{GetFiscalDateOfBirth(person.DateOfBirth)}" +
            $"{GetFiscalDayOfBirth(person)}" +
            $"{GetFiscalPlaceOfBirth(person.PlaceOfBirth)}";
    }

    public static string GetFiscalCode(Person person)
    {   
        string temporyCode = GetTemporaryCode(person);
        char code = GetControlCharacter(temporyCode);
        return $"{temporyCode}{code}"; 
    }

}//class


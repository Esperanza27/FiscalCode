namespace FiscalCode;

public class FiscalCodeUtils
{
    //utils
    public static string GetConsonants(string value)
    {
        const string Vowels = "AEIOU";
        const string Numbers = "123456789";
        string consonants = string.Empty;

        foreach (var c in value)
        {
            if (!Vowels.Contains(c) && !Numbers.Contains(c)) consonants += c;
        }
        return consonants;
    }

    public static string GetVowels(string value)
    {
        const string VowelsString = "AEIOU";
        string vowels = string.Empty;
        foreach (var c in value)
        {
            if (VowelsString.Contains(c)) vowels += c;
        }
        return vowels;
    }

    public static char Month(int month)
    {
        return month switch
        {
            1 => 'A',
            2 => 'B',
            3 => 'C',
            4 => 'D',
            5 => 'E',
            6 => 'H',
            7 => 'L',
            8 => 'M',
            9 => 'P',
            10 => 'R',
            11 => 'S',
            12 => 'T',
            _ => throw new ArgumentOutOfRangeException(nameof(month), "Il valore deve essere compreso tra 1 e 12.")
        };
    }

    private static int GetNumberAlphanumericCharacters(string temporaryCode)
    {
        int dispari=0;
        int pari = 0;
        for (int i = 0; i < temporaryCode.Length; i++)
        {
            if (i % 2 == 0)
            {
                dispari += AlphanumericCharacters.OddAlphanumericCharacters[temporaryCode[i]];
            }
            else
            {
                pari += AlphanumericCharacters.EvenAlphanumericCharacters[temporaryCode[i]];
            }
        }

        return (dispari + pari)% 26;
    }

    public static char GetCodeAlphanumericCharacters(string temporaryCode) 
    {
        int codeNumber = GetNumberAlphanumericCharacters(temporaryCode);
        char code = AlphanumericCharacters.RestAlphanumericCharacters[codeNumber];
        return code;
    }

}//class

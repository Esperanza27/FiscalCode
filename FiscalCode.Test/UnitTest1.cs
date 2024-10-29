using FluentAssertions;

namespace FiscalCode.Test
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("Rossi", "RSS")]
        [InlineData("rosi", "RSO")]
        [InlineData("fo", "FOX")]
        [InlineData("Foo", "FOO")]
        [InlineData("12345", "XXX")]// REQUEST

        public void GetFiscalSurname_test(string surname, string expected)
        {
            PersonUtils.GetFiscalSurname(surname).Should().Be(expected);
        }
        [Theory]
        [InlineData("Marco", "MRC")]
        [InlineData("Mario", "MRA")]
        [InlineData("gianfranco", "GFR")]
        [InlineData("giancarlo", "GCR")]
        [InlineData("LuZ", "LZU")]
        [InlineData("Lu", "LUX")]

        public void GestFiscalName_Test(string name, string expected)
        {
            PersonUtils.GetFiscalName(name).Should().Be(expected);
        }

        [Theory]
        [InlineData("1985-11-15", "85S")]
        [InlineData("2000-01-10", "00A")]
        [InlineData("1999-12-25", "99T")]
        [InlineData("2022-03-10", "22C")]
        [InlineData("1990-06-27", "90H")]
        public void GetFiscalDateOfBirth_test(string dateOfBirthString, string expected)
        {
            DateOnly dateOfBirth = DateOnly.Parse(dateOfBirthString);
            PersonUtils.GetFiscalDateOfBirth(dateOfBirth).Should().Be(expected);
        }

        [Theory]
        [InlineData("1985-11-05", "05")]
        [InlineData("2000-01-10", "10")]
        [InlineData("1999-12-25", "25")]
        [InlineData("2022-03-10", "10")]
        [InlineData("1990-06-27", "27")]
        public void GetFiscalDayOfBirth_Man_test(string dateOfBirthString, string expected)
        {
            DateOnly dateOfBirth = DateOnly.Parse(dateOfBirthString);
            Person personMan = new Man("Mario", "rossi", "Roma", dateOfBirth);
            PersonUtils.GetFiscalDayOfBirth(personMan).Should().Be(expected);
        }

        [Theory]
        [InlineData("1985-11-05", "45")]
        [InlineData("2000-01-10", "50")]
        [InlineData("1999-12-25", "65")]
        [InlineData("2022-03-12", "52")]
        [InlineData("1990-06-27", "67")]
        public void GetFiscalDayOfBirth_Woman_test(string dateOfBirthString, string expected)
        {
            DateOnly dateOfBirth = DateOnly.Parse(dateOfBirthString);
            Person personWoman = new Woman("Maria", "verdi", "Roma", dateOfBirth);
            PersonUtils.GetFiscalDayOfBirth(personWoman).Should().Be(expected);
        }
        [Theory]
        [InlineData("ROMA", "H501")]
        [InlineData("Milano", "F205")]
        [InlineData("Napoli", "F839")]
        [InlineData("Firenze", "D612")]
        [InlineData("Perù", "Z405")]
        [InlineData("Spagna", "Z404")]
        [InlineData("Stati Uniti", "Z404")]
        //[InlineData("Colombia", "Il luogo di nascita 'COLOMBIA' non è stato trovato nei codici Belfiore.")]

        public void GetFiscalPlaceOfBirth_test(string place, string expected)
        {
            PersonUtils.GetFiscalPlaceOfBirth(place).Should().Be(expected);
        }


        [Theory]
        [InlineData("Marco", "rossi", "ROMA", "1985-11-05", "RSSMRC85S05H501")]
        [InlineData("Mario", "fo", "spagna", "2000-01-10", "FOXMRA00A10Z404")]
        [InlineData("gianfranco", "rosi", "Firenze", "1990-06-27", "RSOGFR90H27D612")]
        public void GetCodFis_Man_test(string name, string surname, string placeOfBirth, string dateOfBirthString, string expected)
        {
            DateOnly dateOfBirth = DateOnly.Parse(dateOfBirthString);
            Person personMan = new Man(name, surname, placeOfBirth, dateOfBirth);
            PersonUtils.GetFiscalCode(personMan).Should().Be(expected);
        }

        [Theory]
        [InlineData("Tiziana", "rossi", "ROMA", "1985-11-05", "RSSTZN85S45H501")]
        [InlineData("Chiara", "verdi", "spagna", "2000-01-10", "VRDCHR00A50Z404")]
        [InlineData("ANNALISA", "rosi", "Firenze", "1990-06-27", "RSONLS90H67D612")]
        public void GetCodFis_Woman_test(string name, string surname, string placeOfBirth, string dateOfBirthString, string expected)
        {
            DateOnly dateOfBirth = DateOnly.Parse(dateOfBirthString);
            Person personWoman = new Woman(name, surname, placeOfBirth, dateOfBirth);
            PersonUtils.GetFiscalCode(personWoman).Should().Be(expected);
        }

        // test con IEnumerable

        public static IEnumerable<object[]> CodFisTestData =>
            new List<Object[]>
            {
            new Object[] {new Man("Marco", "rossi", "ROMA",DateOnly.Parse("1985-11-05")), "RSSMRC85S05H501" },
            new Object[] {new Man("Mario", "fo", "spagna", DateOnly.Parse("2000-01-10")), "FOXMRA00A10Z404"},
            new Object[] {new Man("gianfranco", "rosi", "Firenze", DateOnly.Parse("1990 - 06 - 27")),"RSOGFR90H27D612"},
            new Object[] {new Woman("Tiziana ", "rossi", "ROMA", DateOnly.Parse("1985-11-05")), "RSSTZN85S45H501"},
            new Object[] {new Woman("Chiara", "verdi","spagna", DateOnly.Parse("2000-01-10")), "VRDCHR00A50Z404"},
            new Object[] {new Woman("ANNALISA", "rosi", "Firenze",DateOnly.Parse("1990 - 06 - 27")), "RSONLS90H67D612"},

            };

        [Theory]
        [MemberData(nameof(CodFisTestData))]

        public void GetFiscalCode_Test(Person person, string expected)
        {
            PersonUtils.GetFiscalCode(person).Should().Be(expected);
        }


    }
}
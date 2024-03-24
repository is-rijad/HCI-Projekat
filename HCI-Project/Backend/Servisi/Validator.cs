using System.Text.RegularExpressions;

namespace Backend.Servisi;

public class Validator
{
    public bool ValidirajEmail(string email)
    {
        var regex = new Regex("[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}");
        return regex.IsMatch(email);
    }

    public bool ValidirajLozinku(string lozinka)
    {
        var regex = new Regex("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$");
        return regex.IsMatch(lozinka);
    }

    public bool ValidirajText(string text)
    {
        var regex = new Regex("[A-Za-z]+");
        return regex.IsMatch(text);
    }
}
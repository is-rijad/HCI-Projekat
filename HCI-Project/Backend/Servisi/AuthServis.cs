using Backend.Data;
using Backend.Data.Modeli;
using Backend.Endpoints.Korisnici.Uloguj;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Backend.Servisi;

public class AuthServis
{
    private readonly HCIDBContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccesor;

    public AuthServis(HCIDBContext context, IHttpContextAccessor accessor)
    {
        _dbContext = context;
        _httpContextAccesor = accessor;
    }

    public Tokeni? GetCookie()
    {
        var cookie = _httpContextAccesor.HttpContext?.Request.Cookies["auth-token"];
        if (cookie == null)
            return null;
        var token = JsonConvert.DeserializeObject<Tokeni>(cookie);
        if (token == null)
            return null;
        return token;
    }

    public async Task<KorisnickiNalog?> GetNalogInfo()
    {
        var token = GetCookie();
        if (token == null)
            return null;
        var record = await _dbContext.Tokeni.Include(t => t.KorisnickiNalog)
            .FirstOrDefaultAsync(t => t.Token == token.Token);
        if (record == null)
            return null;
        return record.KorisnickiNalog;
    }

    public async Task<Tokeni> UlogujKorisnika(UlogujKorisnikaEndpointReq req)
    {
        var korisnik = await _dbContext.KorisnickiNalozi.FirstOrDefaultAsync(k => k.Email == req.Email);
        var token = string.Empty;
        if (korisnik == null) throw new Exception("Korisnicki podaci nisu ispravni!");

        Tokeni tokenObj;
        if (korisnik.Lozinka == req.Lozinka)
        {
            token = GenerisiToken();
            tokenObj = new Tokeni
            {
                Token = token,
                IsLogiran = true,
                KorisnickiNalogId = korisnik.Id,
                Vrijeme = DateTime.Now
            };
            await _dbContext.Tokeni.AddAsync(tokenObj);
            await _dbContext.SaveChangesAsync();
            tokenObj.KorisnickiNalog = await _dbContext.KorisnickiNalozi.FindAsync(korisnik.Id);
        }
        else
        {
            throw new Exception("Korisnicki podaci nisu ispravni!");
        }

        return tokenObj;
    }

    public async Task<bool> OdjaviKorisnika()
    {
        var token = GetCookie();
        if (token == null)
            return false;

        var record = await _dbContext.Tokeni.FirstOrDefaultAsync(t => t.Token == token.Token);
        if (record == null)
            return false;
        record.IsLogiran = false;
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IsLogiran()
    {
        var token = GetCookie();
        if (token == null)
            return false;
        var record = await _dbContext.Tokeni.FirstOrDefaultAsync(t => t.Token == token.Token);
        if (record == null)
            return false;
        return record.IsLogiran;
    }

    private string GenerisiToken()
    {
        var malaSlova = "qwertzuioplkjhgfdsayxcvbnm";
        var velikaSlova = "QWERTZUIOPLKJHGFDSAYXCVBNM";
        var brojevi = "0123456789";
        var specijalniZnakovi = "!#$%&/()=?*.,-_:;";
        var znakovi = malaSlova + velikaSlova + specijalniZnakovi + brojevi;
        var rezultat = "";
        for (var i = 0; i < 20; i++) rezultat += znakovi[Random.Shared.Next(znakovi.Length)];
        return rezultat;
    }
}
using Backend.Data;
using Backend.Data.Modeli;
using Backend.Endpoints.Rezervacije.ProvjeriRezervaciju;
using Backend.Servisi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Endpoints.Sobe.GetSobe;

[Route("Sobe/GetSobe")]
public class GetSobeEndpoint : BaseEndpoint<GetSobeReq, GetSobeRes>
{
    private readonly HCIDBContext _dbContext;
    private readonly ProvjeriRezervaciju _provjeriRezervaciju;

    public GetSobeEndpoint(HCIDBContext context,
        ProvjeriRezervaciju provjeriRezervaciju)
    {
        _dbContext = context;
        _provjeriRezervaciju = provjeriRezervaciju;
    }

    [HttpPost]
    public override async Task<GetSobeRes> Akcija([FromBody] GetSobeReq req)
    {
        var listaSoba = await _dbContext.Sobe.ToListAsync();
        var sveSobe = new List<int>();
        foreach (var soba in listaSoba)
            if (FiltrirajSobe(soba, req))
                sveSobe.Add(soba.Id.Value);

        var sobe = new GetSobeRes();
        foreach (var soba in sveSobe)
        {
            var rezultat = await _provjeriRezervaciju.Provjeri(new ProvjeriRezervacijuEndpointReq
            {
                SobaId = soba,
                BrojDjece = req.BrojDjece,
                BrojOsoba = req.BrojOdraslih,
                DatumDolaska = req.DatumPrijave,
                DatumOdlaska = req.DatumOdjave,
                SobaAranzmanId = null
            });
            if (rezultat is ProvjeriRezervacijuEndpointRes)
            {
                var rezultatRes = rezultat as ProvjeriRezervacijuEndpointRes;
                sobe.Sobe.Add(new PregledSobeRes
                {
                    Id = rezultatRes!.Soba!.Id.Value,
                    BrojGostiju = req.BrojOdraslih,
                    Cijena = rezultatRes.DetaljiRezervacije!.Cijena,
                    NazivSobe = rezultatRes.Soba.NazivSobe,
                    Opis = rezultatRes.Soba.Opis
                });
            }
        }

        if (req.FilterPoCijeni == 1)
            sobe.Sobe = sobe.Sobe.OrderBy(s => s.Cijena).ToList();
        else if (req.FilterPoCijeni == 2)
            sobe.Sobe = sobe.Sobe.OrderByDescending(s => s.Cijena).ToList();


        if (sobe.Sobe.Count == 0)
        {
            sobe.Status = 404;
            sobe.Message = "Nažalost, nije pronađena nijedna soba.";
        }

        return sobe;
    }

    private bool FiltrirajSobe(Soba soba, GetSobeReq req)
    {
        if (req.Balkon)
            if (!soba.Balkon)
                return false;
        if (req.Bazen)
            if (!soba.Bazen)
                return false;
        if (req.BesplatnoOtkazivanje)
            if (!soba.BesplatnoOtkazivanje)
                return false;
        if (req.DozvoljeniLjubimci)
            if (!soba.DozvoljeniLjubimci)
                return false;
        if (req.Klima)
            if (!soba.Klima)
                return false;
        if (req.Minibar)
            if (!soba.Minibar)
                return false;
        if (req.PrilagodjenInvalidima)
            if (!soba.PrilagodjenInvalidima)
                return false;
        if (req.Spa)
            if (!soba.Spa)
                return false;
        if (req.Teretana)
            if (!soba.Teretana)
                return false;
        if (req.AranzmanId != 0)
        {
            var aranzman = soba.Aranzmani.Find(a => a.AranzmanId == req.AranzmanId);
            if (aranzman == null)
                return false;
        }

        if (req.BrojBracnihKreveta != 0)
        {
            var krevet =
                soba.Kreveti.Find(a => a.Krevet?.Tip == "Bračni" && a.BrojKreveta == req.BrojBracnihKreveta);
            if (krevet == null)
                return false;
        }

        if (req.BrojObicnihKreveta != 0)
        {
            var krevet =
                soba.Kreveti.Find(a => a.Krevet?.Tip == "Obični" && a.BrojKreveta == req.BrojObicnihKreveta);
            if (krevet == null)
                return false;
        }

        if (req.BrojPomocnihKreveta != 0)
        {
            var krevet = soba.Kreveti.Find(a =>
                a.Krevet?.Tip == "Pomoćni" && a.BrojKreveta == req.BrojPomocnihKreveta);
            if (krevet == null)
                return false;
        }

        return true;
    }
}
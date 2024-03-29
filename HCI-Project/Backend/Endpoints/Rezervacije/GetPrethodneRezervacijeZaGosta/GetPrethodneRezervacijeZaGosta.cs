﻿using Backend.Data;
using Backend.Data.Modeli;
using Backend.Endpoints.Rezervacije.GetBuduceRezervacijeZaGosta;
using Backend.Filteri;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Backend.Endpoints.Rezervacije.GetPrethodneRezervacijeZaGosta;

[Route("GetPrethodneRezervacijeZaGosta")]
public class GetPrethodneRezervacijeZaGosta : BaseEndpoint<NoRequest, GetPrethodneRezervacijeZaGostaRes>
{
    private readonly HCIDBContext _dbContext;

    public GetPrethodneRezervacijeZaGosta(HCIDBContext context)
    {
        _dbContext = context;
    }

    [AuthFilter]
    [HttpGet]
    public override async Task<GetPrethodneRezervacijeZaGostaRes> Akcija([FromQuery] NoRequest req)
    {
        var token = JsonConvert.DeserializeObject<Tokeni>(HttpContext.Request.Cookies["auth-token"]!);
        var gostId = (await _dbContext.Tokeni.FirstOrDefaultAsync(t => t.Token == token.Token)).KorisnickiNalogId;

        var response = new GetPrethodneRezervacijeZaGostaRes();
        var rezervacije = await _dbContext.ZauzeteSobe
            .Where(zs => zs.GostId == gostId && zs.DatumDolaska.Date <= DateTime.Today).ToListAsync();
        if (rezervacije.Count == 0)
        {
            response.Status = 404;
            response.Message = "Nije pronađena nijedna prethodna rezervacija!";
        }

        response.Rezervacije = new List<RezervacijaModel>();
        foreach (var rezervacija in rezervacije)
            response.Rezervacije.Add(new RezervacijaModel
            {
                Aranzman = rezervacija!.SobaAranzman!.Aranzman!.NazivAranzmana,
                NazivSobe = rezervacija.Soba!.NazivSobe,
                OpisSobe = rezervacija.Soba.Opis,
                Rezervacija = rezervacija
            });
        return response;
    }
}
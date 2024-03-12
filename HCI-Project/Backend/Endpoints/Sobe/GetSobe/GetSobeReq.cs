namespace Backend.Endpoints.Sobe.GetSobe;

public class GetSobeReq
{
    public DateTime DatumPrijave { get; set; }
    public DateTime DatumOdjave { get; set; }
    public int BrojOdraslih { get; set; }
    public int BrojDjece { get; set; }


    public bool BesplatnoOtkazivanje { get; set; }
    public bool Klima { get; set; }
    public bool Bazen { get; set; }
    public bool Spa { get; set; }
    public bool PrilagodjenInvalidima { get; set; }
    public bool Teretana { get; set; }
    public bool DozvoljeniLjubimci { get; set; }
    public bool Minibar { get; set; }
    public bool Balkon { get; set; }

    public int FilterPoCijeni { get; set; }
    public int AranzmanId { get; set; }
    public int BrojBracnihKreveta { get; set; }
    public int BrojObicnihKreveta { get; set; }
    public int BrojPomocnihKreveta { get; set; }
}
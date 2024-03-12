namespace Backend.Endpoints.Rezervacije.NapraviRezervaciju;

public class NapraviRezervacijuEndpointReq
{
    public int SobaId { get; set; }
    public int? SobaAranzmanId { get; set; }
    public int BrojOsoba { get; set; }
    public int BrojDjece { get; set; }
    public DateTime DatumDolaska { get; set; }
    public DateTime DatumOdlaska { get; set; }
}
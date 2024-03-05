using Backend.Data.Modeli;

namespace Backend.Endpoints.Sobe.GetSobe
{
    public class GetSobeRes : BaseResponse
    {
        public List<PregledSobeRes> Sobe { get; set; } = new List<PregledSobeRes>();
    }

    public class PregledSobeRes
    {
        public int Id { get; set; }
        public string NazivSobe { get; set; }
        public float Cijena { get; set; }
        public int BrojGostiju { get; set; }
        public string Opis { get; set; }
    }
}

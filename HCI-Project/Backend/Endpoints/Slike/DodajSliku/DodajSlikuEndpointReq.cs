namespace Backend.Endpoints.Slike.DodajSliku {
    public class DodajSlikuEndpointReq {
        public int SobaId { get; set; }
        public IFormFile Slika { get; set; }
    }
}

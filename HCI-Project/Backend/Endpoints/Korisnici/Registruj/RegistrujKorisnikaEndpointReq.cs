namespace Backend.Endpoints.Korisnici.Registruj {
    public class RegistrujKorisnikaEndpointReq {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Drzava { get; set; }
        public string Grad { get; set; }
    }
}

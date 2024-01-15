using Backend.Data.Modeli;

namespace Backend.Endpoints.Cijene.GetCijenuZaId {
    public class GetCijenuZaIdEndpointRes : BaseResponse
    {
        public List<Cijena>? Cijene { get; set; } = null;
    }
}

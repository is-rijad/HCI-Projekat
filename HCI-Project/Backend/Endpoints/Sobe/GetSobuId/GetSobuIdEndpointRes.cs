using Backend.Data.Modeli;

namespace Backend.Endpoints.Sobe.GetSobuId {
    public class GetSobuIdEndpointRes : BaseResponse
    {
        public Soba? soba { get; set; } = null;
    }
}

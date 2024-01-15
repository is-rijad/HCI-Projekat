using Backend.Data.Modeli;

namespace Backend.Endpoints.Kreveti.GetAllZaSobu
{
    public class GetAllKreveteZaSobuRes : BaseResponse
    {
        public List<SobaKrevet>? Kreveti { get; set; } = null;
    }
}

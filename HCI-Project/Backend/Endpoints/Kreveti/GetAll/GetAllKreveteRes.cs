using Backend.Data.Modeli;

namespace Backend.Endpoints.Kreveti.GetAll
{
    public class GetAllKreveteRes : BaseResponse
    {
        public List<Krevet>? Kreveti { get; set; } = null;
    }
}

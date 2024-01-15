using Backend.Data.Modeli;

namespace Backend.Endpoints.Aranzmani.GetAllZaSobu
{
    public class GetAllAranzmaneZaSobuRes : BaseResponse
    {
        public List<SobaAranzman>? Aranzmani { get; set; } = null;
    }
}

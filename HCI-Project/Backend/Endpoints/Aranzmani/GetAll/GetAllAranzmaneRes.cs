using Backend.Data.Modeli;

namespace Backend.Endpoints.Aranzmani.GetAll;

public class GetAllAranzmaneRes : BaseResponse
{
    public List<Aranzman>? Aranzmani { get; set; } = null;
}
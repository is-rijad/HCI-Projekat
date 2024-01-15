using Microsoft.AspNetCore.Mvc;

namespace Backend.Endpoints {
    [ApiController]
    public abstract class BaseEndpoint<Treq, Tres> : ControllerBase
    {
        public abstract Task<Tres> Akcija(Treq req);
    }
}

namespace GestionTareas.API.Middleware
{
    public class EntityMiddleware
    {
        public bool Succes {  get; set; } 
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public EntityMiddleware(bool succes, string message, int statusCode)
        {
            Succes = succes;
            Message = message;
            StatusCode = statusCode;
        }
    }
}

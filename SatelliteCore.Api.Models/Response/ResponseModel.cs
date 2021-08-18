
namespace SatelliteCore.Api.Models.Response
{
    public struct ResponseModel <T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Content { get; set; }

        public ResponseModel(bool success, string message, T content)
        {
            Success = success;
            Message = message;
            Content = content;
        }

    }
}

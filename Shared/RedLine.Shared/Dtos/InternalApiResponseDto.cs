using System.Net;
using System.Text.Json.Serialization;

namespace RedLine.Shared.Dtos;

public class InternalApiResponseDto<T>
{
    public FriendlyMessageDto FriendlyMessageDto { get; set; }
    public T Payload { get; private set; }
    public List<string> ErrorMessages { get; private set; }

    [JsonIgnore]
    public int HttpStatusCode;

    [JsonIgnore]
    public bool HasError { get; private set; }

    public static InternalApiResponseDto<T> Success(T payload, int httpStatusCode)
    {
        return new InternalApiResponseDto<T> { Payload = payload, HttpStatusCode = httpStatusCode, HasError = false };
    }

    public static InternalApiResponseDto<T> Success(int httpStatusCode)
    {
        return new InternalApiResponseDto<T> { Payload = default(T), HttpStatusCode = httpStatusCode, HasError = false };
    }

    public static InternalApiResponseDto<T> Fail(List<string> errorMessages, int httpStatusCode)
    {
        return new InternalApiResponseDto<T> { ErrorMessages = errorMessages, HttpStatusCode = httpStatusCode, HasError = true };
    }

    public static InternalApiResponseDto<T> Fail(string errorMessages, int httpStatusCode)
    {
        return new InternalApiResponseDto<T> { ErrorMessages = new List<string>() { errorMessages }, HttpStatusCode = httpStatusCode, HasError = true };
    }
}

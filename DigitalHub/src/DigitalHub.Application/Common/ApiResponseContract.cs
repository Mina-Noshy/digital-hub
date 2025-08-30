using DigitalHub.Domain.Utilities;

namespace DigitalHub.Application.Common;

public sealed class ApiResponseContract
{
    public ResultType Code { get; private set; }
    public string Title { get; private set; }
    public string Message { get; private set; }
    public object Data { get; private set; }

    private ApiResponseContract(ResultType _code, string _message, object _data)
        => (Code, Data, Title, Message) = (_code, _data, _code.ToString(), _message.StartsWith("msg-") ? ConfigurationHelper.GetLocalizedMessageString(_message) : _message);

    internal static ApiResponseContract Create(ResultType _code, string _message, object _data)
        => new ApiResponseContract(_code, _message, _data);
}

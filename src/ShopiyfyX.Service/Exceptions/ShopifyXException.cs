namespace ShopiyfyX.Service.Exceptions;

public class ShopifyXException : Exception
{
    public int StatusCode { get; set; }

    public ShopifyXException(int code, string message) : base(message)
    {
        StatusCode = code;
    }
}

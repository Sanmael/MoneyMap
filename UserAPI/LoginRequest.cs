using Business.Requests;

public class LoginRequest : BaseRequest
{
    public required string UserName { get; set; }
    public required string Password { get; set; }    
}
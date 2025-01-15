namespace Business.Requests.User
{
    public class CreateUserRequest : BaseRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Occupation { get; set; }
        public decimal Salary { get; set; }
        public decimal Balance { get; set; }
        public string UserName { get; set; }        
    }
}
﻿namespace Business.DTOS
{
    public class ClaimsDTO
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email {  get; set; }
        public string Token { get; set; }
    }
}
﻿using Domain.Cards.Entities;

namespace Domain.Base.Entities
{
    public class User : BaseEntitie
    {        
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Occupation { get; set; }
        public decimal Salary { get; set; }
        public decimal Balance { get; set; }
        public List<Card>? Cards { get; set; } = new List<Card> { };
    }
}
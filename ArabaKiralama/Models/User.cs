﻿namespace ArabaKiralama1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; } = string.Empty;
        //public string FullName { get; set; }
    }
}
﻿namespace JWT_TokenAuthentication_UserEmployee.Models
{
    public class LoginRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
﻿namespace BackendApi.Contracts.Users
{
    public class GetUserResponse
    {
        public int Role_id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        public int Created_by { get; set; }
        public DateTime Created_at { get; set; }
        public int Changed_by { get; set; }
        public DateTime Changed_at { get; set; }
        public int Deleted_by { get; set; }
        public DateTime Deleted_at { get; set; }
    }
}

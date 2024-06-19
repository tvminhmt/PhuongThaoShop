using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Shared.Core.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserPass { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public Nullable<short> OrganId { get; set; }
        public Nullable<byte> RankId { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Comments { get; set; }
        public Nullable<byte> GenderId { get; set; }
        public Nullable<byte> UserStatusId { get; set; }
        public byte UserTypeId { get; set; }
        public Nullable<short> DefaultActionId { get; set; }
        public Nullable<System.DateTime> BirthDay { get; set; }
        public Nullable<System.DateTime> CrDateTime { get; set; }
        public byte Correlated { get; set; }
        [NotMapped]
        public List<UserProfile> lUsers;
    }
}

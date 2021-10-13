using Stregsystem.Exceptions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Stregsystem.Models
{
    public class User : IComparable<User>
    {

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }

        // contructer
        public User(int id, string firstname, string lastname, string username, string email, decimal balance)
        {
            Id = id;
            Firstname = firstname ?? throw new ArgumentNullException();
            Lastname = lastname ?? throw new ArgumentNullException();
            Username = IsValidUsername(username) ? username : throw new UsernameNotValidException();
            Email = IsValidEmail(email) ? email : throw new EmailNotValidException();
            Balance = balance;
        }

   

        private bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            // stackoverflow regex yoink
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        private bool IsValidUsername(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            // stackoverflow regex yoink
            return Regex.IsMatch(strIn, @"^[0-9a-z_]+$");
        }

        // allows sort (icomp...)
        public int CompareTo([AllowNull] User other)
        {
            return (other.Id < this.Id ? 1 : (other.Id > this.Id) ? -1 : 0);
        }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Id == user.Id &&
                   Firstname == user.Firstname &&
                   Lastname == user.Lastname &&
                   Username == user.Username &&
                   Email == user.Email &&
                   Balance == user.Balance;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Firstname, Lastname, Username, Email, Balance);
        }

        public override string ToString()
        {
            return $"{Firstname} {Lastname} ({Username})";
        }
    }
}
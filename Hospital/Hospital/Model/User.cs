using System;

namespace Model
{
    public class User
    {
        public User(string jmbg, string firstName, string lastName, string username, string password, string email, string address, DateTime dateOfBirth)
        {
            this.Jmbg = jmbg;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.Address = address;
            this.DateOfBirth = dateOfBirth;
        }

        public User()
        {

        }

        public String Jmbg
        {
            get
            ;
            set
            ;
        }

        public String FirstName
        {
            get
            ;
            set
            ;
        }

        public String LastName
        {
            get
            ;
            set
            ;
        }

        public String Username
        {
            get
            ;
            set
            ;
        }

        public String Password
        {
            get
            ;
            set
            ;
        }

        public String Email
        {
            get
            ;
            set
            ;
        }

        public String Address
        {
            get
            ;
            set
            ;
        }

        public DateTime DateOfBirth
        {
            get
            ;
            set
            ;
        }

    }
}
namespace Hospital.ViewModels.DTO
{
    public class UserViewModel : ValidationBase
    {
        private string username;

        private string password;

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }


        public UserViewModel()
        {
        }

        protected override void ValidateSelf()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                this.ValidationErrors["Username"] = "You have to enter your username.";
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                this.ValidationErrors["Password"] = "You have to enter your password.";
            }
            else if (Password.Length < 8)
            {
                this.ValidationErrors["Password"] = "Your password has to have at least 8 characters.";
            }
        }
    }
}

namespace Stregsystem.Models
{
    public class UserBalanceNotification
    {
        public User User { get; set; }

        public UserBalanceNotification(User user)
        {
            User = user;
        }
        // model for data on balance low warnings
        // for passing through event
    }
}
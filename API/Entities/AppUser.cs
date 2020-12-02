namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; } //abbreviated form of get{return myvar;} set{myvar = value}

        public string UserName { get; set; }

        //both variables below are byte[] because this is what is going to be returned when I calculate
        //the hash
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}
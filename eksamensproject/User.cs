namespace EksOP
{
    public class User : IComparable
    {
        private static int prevId = 0;
        private string _firstname;
        private string _lastname;
        private string _email;
        private string _username;
        public string FirstName 
        {
            get => _firstname;
            set => _firstname = value ?? throw new NullReferenceException($"{value} was null"); 
        }
        public string LastName 
        { 
            get => _lastname;
            set => _lastname = value ?? throw new NullReferenceException($"{value} was null"); 
        }
        public int Id { get; set; }
        public string UserName 
        { 
            get => _username;
            set 
            {   
                char legalchar = '_';
                char[] charArray = value.ToCharArray();
                for (int i = 0; i < charArray.Length; i++)
                {
                    if (Char.IsLetterOrDigit(charArray[i]) == false) 
                    {
                        if (charArray[i] != legalchar) { throw new Exception($"{value} contains illegal char"); }
                    }
                }
                _username = value;
            } 
        }
        public string Email 
        { 
            get => _email;
            set 
            {
                if (value.Contains("@") == true) 
                { 
                    List<char> local = value.Split('@')[0].ToList<char>();
                    List<char> domain = value.Split('@')[1].ToList<char>();
                    if (domain.First().Equals('.') == true || domain.First().Equals('-') == true) { throw new Exception($"{value} contains {domain.First()} char in beginning"); }
                    if (domain.Last().Equals('.') == true || domain.Last().Equals('-') == true) { throw new Exception($"{value} contains {domain.First()} char in end"); }

                    foreach (char c in local) 
                    {
                        if (Char.IsLetterOrDigit(c) == false) 
                        {
                            if (c != '_' && c!= '.' && c!='-') { throw new Exception($"{value} contains illegal char"); }
                        }
                    }
                    foreach (char c in domain)
                    {
                        if (Char.IsLetterOrDigit(c) == false)
                        {
                            if (c != '.' && c != '-') { throw new Exception($"{value} contains illegal char"); }
                        }
                    }

                    _email = value;
                }
            } 
        }
        public decimal Balance { get; set; }
        public User(string firstname, string lastname, string email, string username) 
        { 
            this.FirstName = firstname;
            this.LastName = lastname;   
            this.Email = email;
            this.UserName = username;
            this.Balance = 0;
            this.Id = prevId;
            prevId++;
        }
        public override string ToString() 
        {
            return $"{FirstName} {LastName} ({Email})";
        }
        public override bool Equals(object? user)
        {
            if (user == null) { throw new NullReferenceException($"{user} is null"); }
            User user1 = user as User;
            if (this.Id == user1.Id) { return true; }
            else { return false; }
        }
        public override int GetHashCode()
        {
            return this.UserName.GetHashCode();
        }

        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;

            User OtherUser = obj as User;
            if (OtherUser != null)
            { 
                return this.Id.CompareTo(OtherUser.Id);
            }
            else 
            { 
                throw new ArgumentException("Object is not a User");
            }
        }
    }
}
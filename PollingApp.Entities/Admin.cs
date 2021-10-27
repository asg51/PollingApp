namespace PollingApp.Entities
{
    public class Admin
    {
        public string Key { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Index { get; set; }

        public Admin(string key, string password, string name,string surname, int index)
        {
            Key = key;
            Password = password;
            Name = name;
            Surname = surname;
            Index = index;
        }
    }
}

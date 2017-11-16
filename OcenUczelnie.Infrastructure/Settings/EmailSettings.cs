namespace OcenUczelnie.Infrastructure.Settings
{
    public class EmailSettings: ISettings
    {
        public string Host { get; set; }
        public bool EnableSsl { get; set; }
        public int Port { get; set; }
        public int Timeout { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
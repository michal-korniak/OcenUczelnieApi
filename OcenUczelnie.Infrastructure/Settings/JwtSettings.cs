namespace OcenUczelnie.Infrastructure.Settings
{
    public class JwtSettings: ISettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
    }
}
namespace AspKnP231.Models.User
{
    public class KdfViewModel
    {
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public string? DerivedKey { get; set; }
    }
}
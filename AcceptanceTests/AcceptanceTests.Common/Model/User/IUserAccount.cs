namespace AcceptanceTests.Common.Model.User
{
    public interface IUserAccount
    {
        string Key { get; set; }
        string Role { get; set; }
        string AlternativeEmail { get; set; }
        string Firstname { get; set; }
        string Lastname { get; set; }
        string DisplayName { get; set; }
        string Username { get; set; }
    }
}

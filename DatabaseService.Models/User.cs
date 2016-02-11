namespace DatabaseService.Models {
  public class User {
    public int Id { get; set; }
    public string Username { get; set; }
    public string Photo { get; set; }
    public string RoleName { get; set; }
    public string Email { get; set; }
    public string HashedPassword { get; set; }
    public string Name { get; set; }
    public int Active { get; set; }
    public string RefreshToken { get; set; }
  }
}

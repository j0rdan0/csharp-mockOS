namespace mockOSApi.Models;

public class MockFile {
    public string Name { get; set;}

    public List<byte[]> Data {get;set;}
    public FilePermissions[] Permissions { get; set;}

    public User User {get;set;}

}

public enum FilePermissions {
    READ,
    WRITE,
    EXECUTE
};
namespace mockOSApi.Models;

public interface IFileSystem
{
    public void Open();
    public void Create();
    public void Read();
    public void Write();
    public void Delete();
}

public class MockOsFileSystem
{

}
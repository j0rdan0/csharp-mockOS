namespace mockOSApi.Models;

public interface IThread
{
    public void Run();
}
public class MockThread : IThread
{
    public int Tid { get; set; }
    public string? Name { get; set; }

    public string? Function { get; set; }

    public Stack<byte[]> Stack { get; set; } // needs to be allocated by VM manager, size of 1024 

    public MockProcess Parent { get; set; }

    public MockThread()
    {

    }


    public void Run()
    {

    }

}
namespace mockOSApi.Models;

public abstract class OSObject
{
    public int Handle { get; set; }
    public override string ToString() => String.Format($"{GetType().ToString()}" + " object");
}


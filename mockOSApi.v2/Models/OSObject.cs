namespace mockOSApi.Models;

public abstract class OSObject
{
    public int Handle { get; set; }
    public override string ToString() => String.Format($"{GetType().ToString()}" + " object");

    public DateTime? CreationTime { get; set; }

    public string? ErrorMessage { get; set; } // used to communicate error messages to view layer
}


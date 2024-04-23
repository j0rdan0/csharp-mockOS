namespace mockOSApi.Models;

public class Group
{

    public int Gid { get; set; }
    public string Name { get; set; }

    public Capabilities Capabilities { get; set; }
    public List<User> Members;
}

public enum Capabilities
{ // will base authorization on group Capabilities, which I might even group into policies { FS_POLICY, TASK_POLICY, NETWORK_POLICY}
    READ_FILESYSTEM,
    WRITE_FILESYSTEM,
    CREATE_PROCESS,
    TERMINATE_PROCESS,
    READ_PACKAGE,
    WRITE_PACKAGE,
    CHANGE_PRIO,

    GOD_MODE
}
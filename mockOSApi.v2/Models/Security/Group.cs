namespace mockOSApi.Models;

public class Group
{

    public int Gid { get; set; }
    public string Name { get; set; }

    public Capability[] Capabilities{ get; set; }
    public List<User> Members;
}

public enum Capability
{ // will base authorization on group Capabilities, which I might even group into policies { FS_POLICY, TASK_POLICY, NETWORK_POLICY}
    READ_FILESYSTEM,
    WRITE_FILESYSTEM,
    CREATE_PROCESS,
    TERMINATE_PROCESS,
    READ_NETWORK_PACKAGE,
    WRITE_NETWORK_PACKAGE,
    CHANGE_PRIO,
    GOD_MODE
}
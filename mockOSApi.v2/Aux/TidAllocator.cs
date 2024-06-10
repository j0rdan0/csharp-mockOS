namespace mockOSApi.Utils;

public class TidAllocator
{
    private readonly Dictionary<int, HashSet<int>> processTidMap;
    private readonly Dictionary<int, List<int>> availableTids;

    public TidAllocator()
    {
        processTidMap = new Dictionary<int, HashSet<int>>();
        availableTids = new Dictionary<int, List<int>>();
    }

    public int AddTid(int processId) // need to use highest tid search as well, same as for pid, since this is not persistent
    {
        if (!processTidMap.ContainsKey(processId))
        {
            processTidMap[processId] = new HashSet<int>();
            availableTids[processId] = new List<int>();
        }

        int newTid;
        if (availableTids[processId].Count > 0)
        {
            newTid = availableTids[processId][0];
            availableTids[processId].RemoveAt(0);
        }
        else
        {
            newTid = processTidMap[processId].Count > 0 ? processTidMap[processId].Max() + 1 : 1;
        }

        processTidMap[processId].Add(newTid);
        return newTid;
    }

    public void RecycleTid(int processId, int tid)
    {
        if (processTidMap.ContainsKey(processId) && processTidMap[processId].Contains(tid))
        {
            processTidMap[processId].Remove(tid);
            availableTids[processId].Add(tid);
        }
    }
}

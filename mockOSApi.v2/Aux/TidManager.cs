namespace mockOSApi.Utils;

public class TidManager
{
    private readonly Dictionary<int, HashSet<int>> processTidMap;
    private readonly Dictionary<int, List<int>> availableTids;

    public TidManager()
    {
        processTidMap = new Dictionary<int, HashSet<int>>();
        availableTids = new Dictionary<int, List<int>>();
    }

    public int AddTid(int processId)
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

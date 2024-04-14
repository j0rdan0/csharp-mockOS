using mockOSApi.Models;
using mockOSApi.Data;



namespace mockOSApi.Repository;

public interface IProcessRepository
{
    IEnumerable<MockProcess> GetAll();
    MockProcess? GetProcessByPid(int pid);
    Task CreateProcess();
    void KillProcess(MockProcess proc);
    void ChangePriority(int prio, int pid);
    public Task CreateProcess(MockProcess proc);
    public Task Save();
};

public class ProcessRepositoryDb : IProcessRepository
{

    private readonly OSDbContext _dbContext;
    public IEnumerable<MockProcess> GetAll()
    {
        return _dbContext.MockProcesses.ToList();
    }
    public MockProcess? GetProcessByPid(int pid)
    {
        return _dbContext.MockProcesses.Where<MockProcess>(p => p.Pid == pid).FirstOrDefault();

    }
    public async Task CreateProcess()
    {
        await _dbContext.AddAsync<MockProcess>(new MockProcess("/bin/bash", null));

    }
    public async Task CreateProcess(MockProcess proc)
    {
        await _dbContext.AddAsync<MockProcess>(proc);

    }

    public void KillProcess(MockProcess proc)
    {
        _dbContext.Remove<MockProcess>(proc);
        _dbContext.SaveChanges();

    }

    public void ChangePriority(int prio, int pid)
    {
        var proc = GetProcessByPid(pid);
        proc.Priority = prio;
        _dbContext.SaveChanges();

    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public ProcessRepositoryDb(OSDbContext dbContext)
    {
        _dbContext = dbContext;
    }

};

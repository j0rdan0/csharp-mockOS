using mockOSApi.Models;
using mockOSApi.Data;

namespace mockOSApi.Repository;

public interface IProcessRepository
{
    IEnumerable<MockProcess> GetAll();
    MockProcess? GetProcessByPid(int pid);
    public List<MockProcess>? GetProcessByName(string name);
    Task CreateProcess();
    void KillProcess(MockProcess proc);
    MockProcess? ChangePriority(int prio, int pid);
    public Task CreateProcess(MockProcess proc);
    public Task Save();
    public bool ProcessExists(int pid);
};

public class ProcessRepositoryDb : IProcessRepository
{
    private readonly OSDbContext _dbContext;
    public IEnumerable<MockProcess> GetAll() => _dbContext.MockProcesses.OrderBy(p => p.Pid).ToList();

    public MockProcess? GetProcessByPid(int pid) => _dbContext.MockProcesses.Where<MockProcess>(p => p.Pid == pid).FirstOrDefault();

    // I should parse Image into tokens and check only last token, this is not exact
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    public List<MockProcess> GetProcessByName(string name) => _dbContext.MockProcesses.Where<MockProcess>(p => p.Image.Contains(name)).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

    //create a dummy process
    public async Task CreateProcess()
    {
        await _dbContext.AddAsync<MockProcess>(new MockProcess(String.Empty, null));
        await _dbContext.SaveChangesAsync();

    }
    public async Task CreateProcess(MockProcess proc)
    {
        await _dbContext.AddAsync<MockProcess>(proc);
        await _dbContext.SaveChangesAsync();
    }

    public void KillProcess(MockProcess proc)
    {
        _dbContext.Remove<MockProcess>(proc);
        _dbContext.SaveChanges();
    }

    public MockProcess? ChangePriority(int prio, int pid)
    {
        var proc = GetProcessByPid(pid);
        if (proc == null)
        {
            return null;
        }
        proc.Priority = prio;
        _dbContext.SaveChanges();
        return proc;
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public bool ProcessExists(int pid) => _dbContext.MockProcesses.Any(p => p.Pid == pid);

    public ProcessRepositoryDb(OSDbContext dbContext)
    {
        _dbContext = dbContext;
    }

};

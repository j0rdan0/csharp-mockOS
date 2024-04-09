using mockOSApi.Models;
using mockOSApi.Data;

namespace mockOSApi.Repository;

public interface IProcessRepository {
    IEnumerable<MockProcess> GetAll();
    MockProcess? GetProcessByPid(int pid);
    Task CreateProcess();
    void KillProcess(MockProcess proc);
    void UpdateProcess(int pid);
     public Task Save();
};

public class ProcessRepositoryDb: IProcessRepository {

    private readonly OSDbContext _dbContext;
    public IEnumerable<MockProcess> GetAll() {
        return _dbContext.MockProcesses.ToList();
    }
    public MockProcess? GetProcessByPid(int pid) {
        return _dbContext.Find<MockProcess>(pid);
    }
   public  async Task CreateProcess() {
         await _dbContext.AddAsync<MockProcess>(new MockProcess("/bin/bash",null));

    }
    public void KillProcess(MockProcess proc) {
        _dbContext.Remove(proc);

    }

    public void UpdateProcess(int pid) {
        
    }

    public async Task Save() {
        await _dbContext.SaveChangesAsync();
    }

    public ProcessRepositoryDb(OSDbContext dbContext) {
        _dbContext = dbContext;
    }

};

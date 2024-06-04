
using mockOSApi.Models;
using mockOSApi.Data;
using Microsoft.EntityFrameworkCore;

public interface IThreadRepository
{
    public Task Save();
    public bool ThreadExists(int tid);
    public void TerminateThread(MockThread t);
    public Task CreateThread(MockThread t);
    public MockThread? GetThreadByTid(int tid);
    public int GetThreadCount();
    public IEnumerable<MockThread> GetAll();
}

public class ThreadRepositoryDb : IThreadRepository
{
    private readonly OSDbContext _dbContext;
    public ThreadRepositoryDb(OSDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }
    public bool ThreadExists(int tid) => _dbContext.MockThreads.Any(t => t.Tid == tid);
    public void TerminateThread(MockThread t)
    {
        _dbContext.Remove<MockThread>(t);
        _dbContext.SaveChanges();
    }
    public async Task CreateThread(MockThread t)
    {
        await _dbContext.AddAsync<MockThread>(t);
        await _dbContext.SaveChangesAsync();
    }
    public MockThread? GetThreadByTid(int tid) => _dbContext.MockThreads.Include(m => m.Parent).Where<MockThread>(t => t.Tid == tid).FirstOrDefault();
    public int GetThreadCount() => _dbContext.MockThreads.Count<MockThread>();
    public IEnumerable<MockThread> GetAll() => _dbContext.MockThreads.Include(m => m.Parent).OrderBy(t => t.Tid).ToList();
}
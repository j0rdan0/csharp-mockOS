using System.Text.Json;
using mockOSApi.Repository;
using mockOSApi.Models;

namespace mockOSApi.Services;


public interface IProcessHandler {
    public IEnumerable<MockProcess> AllProcesses { get; }

    public MockProcess? GetProcessByPid(int pid);
      public Task CreateProcess(MockProcess process);
      public Task KillProcess(MockProcess proc);
      public void ChangePriority(int prio,int pid);
}

public class ProcessHandler: IProcessHandler {

    public readonly IProcessRepository _repository;

    public ProcessHandler(IProcessRepository procRepo) {
         _repository = procRepo;

    }

    public IEnumerable<MockProcess>? AllProcesses
    {
#pragma warning disable CS8766 

        get
#pragma warning restore CS8766 

        {

            if (_repository.GetAll().ToList().Count == 0)
            {
                return null;
            }
            return _repository.GetAll().ToList();
        }
    }

    public MockProcess? GetProcessByPid(int pid) => _repository.GetProcessByPid(pid);

    public async Task CreateProcess(MockProcess process) {
        await _repository.CreateProcess(process);
        await _repository.Save();
    }

    public async Task KillProcess(MockProcess proc) {
        _repository.KillProcess(proc);
        await _repository.Save();
    }

    public void ChangePriority(int prio,int pid) {
        if (prio == 50) { // default priority
            return;
        }
        if (prio > 90) {
            prio = 90; // max priority
        }
        
        _repository.ChangePriority(prio,pid);
        
    }
}
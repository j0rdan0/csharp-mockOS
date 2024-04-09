using System.Text.Json;
using mockOSApi.Repository;
using mockOSApi.Models;

namespace mockOSApi.Services;


public interface IProcessHandler {
     public IEnumerable<MockProcess> GetAllProcesses();
     public MockProcess? GetProcessByPid(int pid);
}

public class ProcessHandler: IProcessHandler {

    public readonly IProcessRepository _repository;

    public ProcessHandler(IProcessRepository procRepo) {
         _repository = procRepo;

    }

    public IEnumerable<MockProcess>? GetAllProcesses() {
        
        if (_repository.GetAll().ToList().Count == 0){
            return null;
        }
        return _repository.GetAll().ToList();
    }

    public MockProcess? GetProcessByPid(int pid) => _repository.GetProcessByPid(pid);
}
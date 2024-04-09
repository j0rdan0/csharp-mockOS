using System.Text.Json;
using mockOSApi.Repository;

namespace mockOSApi.Services;


public interface IProcessHandler {
     public string GetAllProcesses();
}

public class ProcessHandler: IProcessHandler {

    public readonly IProcessRepository _repository;

    public ProcessHandler(IProcessRepository procRepo) {
         _repository = procRepo;

    }

    public string GetAllProcesses() {
        
        if (_repository.GetAll().ToList().Count == 0){
            return String.Empty;
        }
        return String.Join(" ",JsonSerializer.Serialize(_repository.GetAll()));
    }

}
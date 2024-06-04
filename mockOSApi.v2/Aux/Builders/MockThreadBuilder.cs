using mockOSApi.Repository;


namespace mockOSApi.Models;

public interface IMockThreadBuilder
{
    public MockThreadBuilder AddTid();
    public MockThreadBuilder AddThreadStatus();
    public MockThreadBuilder AddExitCode();
    public MockThreadBuilder AddStartFunction(string? function);
    public MockThreadBuilder AddStack();
    public MockThreadBuilder AddParentProcess(MockProcess parent);
}


public class MockThreadBuilder : IMockThreadBuilder
{

    private MockThread _thread = new MockThread();
    private IThreadRepository _repository;
    private IErrorMessage _errorMessageService;
    public MockThreadBuilder(IThreadRepository repository, IErrorMessage errorMessageService)
    {
        _repository = repository;
        _errorMessageService = errorMessageService;
    }

    public void Reset()
    {
        _thread = new MockThread();
    }
    public MockThreadBuilder AddTid() {
        


        return this;
     }
    public MockThreadBuilder AddThreadStatus()
    {
        _thread.Status = ThreadStatus.SLEEPING;
        return this;
    }

    public MockThreadBuilder AddExitCode()
    {
        _thread.ExitCode = 0;
        return this;
    }

    public MockThreadBuilder AddStartFunction(string? function)
    {
        if (function == null)
        {
            _thread.ErrorMessage = _errorMessageService.GetMessage("ERR_NOFUNC");
            // still create the thread, since again, mock OS, but at least log the error
        }
        _thread.StartFunction = function;
        return this;
    }

     public MockThreadBuilder AddCreationTime()
    {
        _thread.CreationTime = DateTime.Now;
        return this;
    }

    public MockThreadBuilder AddStack()
    {

        // TBD when implementing VM manager
        return this;
    }

    public MockThreadBuilder AddParentProcess(MockProcess parent) {
        _thread.Parent = parent;
        return this;

     }

     public MockThread Build()
    {
        MockThread t = _thread;
        Reset();
        return t;
    }

}
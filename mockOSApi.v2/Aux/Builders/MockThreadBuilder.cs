using MediatR;
using mockOSApi.Requests;


namespace mockOSApi.Models;

public interface IMockThreadBuilder
{
    public MockThreadBuilder AddTid();
    public MockThreadBuilder AddThreadStatus();
    public MockThreadBuilder AddExitCode();
    public MockThreadBuilder AddStartFunction(string? function);
    public MockThreadBuilder AddStack();
    public MockThreadBuilder AddParentProcess(MockProcess parent);
    public MockThreadBuilder AddName(string? name);
}


public class MockThreadBuilder : IMockThreadBuilder
{
    private MockThread _thread = new MockThread();

    private readonly IMediator _mediator;
    private IThreadRepository _repository;
    private IErrorMessage _errorMessageService;
    public MockThreadBuilder(IThreadRepository repository, IErrorMessage errorMessageService, IMediator mediator)
    {
        _repository = repository;
        _errorMessageService = errorMessageService;
        _mediator = mediator;
    }

    public string GenerateThreadName(int tid)
    {
        return String.Format("thread" + tid.ToString() + "_" + Guid.NewGuid().ToString());
    }
    public void Reset()
    {
        _thread = new MockThread();
    }
    public MockThreadBuilder AddTid()
    {
        return this;
    }

    public MockThreadBuilder AddName(string? name)
    {
        if (name != null)
        {
            _thread.Name = name;
            return this;
        }

        _thread.Name = GenerateThreadName(_thread.Tid);
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
        var req = new AllocateStackRequest();
        req.Thread = _thread;
        _mediator.Send(req);
        return this;
    }

    public MockThreadBuilder AddParentProcess(MockProcess parent)
    {
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
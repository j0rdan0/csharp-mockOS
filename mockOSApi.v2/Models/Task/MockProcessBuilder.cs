using mockOSApi.Repository;

namespace mockOSApi.Models;

public interface IMockProcessBuilder
{
    public MockProcessBuilder AddPriority(int? priority);
    public MockProcessBuilder AddPid();
    public MockProcessBuilder AddProcessStatus();
    public MockProcessBuilder AddExitCode();
    public MockProcessBuilder AddDefaultFds();
    public MockProcessBuilder AddImage(string? image);
    public MockProcessBuilder AddCreationTime();
    public MockProcessBuilder AddArguments(string[]? arguments);
    public MockProcessBuilder AddUserContext(User user);
    public MockProcess Build();

}

public class MockProcessBuilder : IMockProcessBuilder
{
    private MockProcess _proc = new MockProcess();
    private IProcessRepository _repository;

    private IErrorMessage _errorMessageService;

    // get empty MockProcess object
    public MockProcessBuilder(IProcessRepository repository, IErrorMessage errorMessageService)
    {
        _repository = repository;

        _errorMessageService = errorMessageService;
        Reset();
    }

    public void Reset()
    {
        _proc = new MockProcess();
    }

    public MockProcessBuilder AddPriority(int? priority)
    {
        if (priority == null)
        {
            _proc.Priority = MockProcess.DEFAULT_PRIORITY;
            return this;
        }
        _proc.Priority = priority;
        return this;
    }

    public MockProcessBuilder AddPid()
    {
        var count = _repository.GetProcessCount();

        if (!_repository.ProcessExists(count))
        {
            _proc.Pid = ++count;
        }
        else
        {
            var newPid = _repository.GetHigestPid();
            _proc.Pid = ++newPid;
        }
        return this;
    }

    public MockProcessBuilder AddProcessStatus()
    {
        _proc.Status = ProcessStatus.SLEEPING;
        return this;
    }
    public MockProcessBuilder AddExitCode()
    {
        _proc.ExitCode = 0;
        return this;
    }

    public MockProcessBuilder AddDefaultFds()
    {
        _proc.FileDescriptors = new List<int>();
        foreach (var fd in Enum.GetValues(typeof(DefaultFd)))
        {
            if (_proc.FileDescriptors != null)
                _proc.FileDescriptors.Add((int)fd);
        }
        return this;
    }
    public MockProcessBuilder AddImage(string? image)
    {
        if (image == null)
        {
            _proc.IsService = true; // System processes do not export image path to usermode
            return this;
        }

        if (!Path.Exists(image) && !_proc.IsService)
        {
            _proc.ErrorMessage = _errorMessageService.GetMessage("PATH_ERROR");
            // still creates a process since this is a mockOS, but at least performs the checking and sets the appropriate ErrorMessage for logging
        }
        _proc.Image = image;
        return this;
    }

    public MockProcessBuilder AddCreationTime()
    {
        _proc.CreationTime = DateTime.Now;
        return this;
    }

    public MockProcessBuilder AddArguments(string[]? arguments)
    {
        if (arguments != null)
        {
            _proc.Args = arguments;
            return this;
        }
        return this;

    }
    public MockProcessBuilder AddUserContext(User user)
    {
        _proc.User = user;
        return this;
    }

    public MockProcess Build()
    {
        MockProcess proc = _proc;
        Reset();
        return proc;
    }

}
using mockOSApi.Models;
using mockOSApi.DTO;
using AutoMapper;
using mockOSApi.Utils;

namespace mockOSApi.Services;

public interface IThreadService
{
    public bool SuspendThread(int tid);
    public void Run(string[]? param);
    public Task<MockThreadDto>? CreateThread(MockThreadCreationDto thread);
    public bool TerminateThread(int tid);
}
public class ThreadService : IThreadService
{

    public readonly IMapper _mapper;
    public readonly IThreadRepository _repository;
    public readonly IMockThreadBuilder _builder;
    public readonly IProcessService _processService;

    private static readonly TidAllocator tidManager = new TidAllocator();
    public ThreadService(IMapper automaper, IThreadRepository repository, IMockThreadBuilder builder, IProcessService processService)
    {
        _mapper = automaper;
        _repository = repository;
        _builder = builder;
        _processService = processService;
    }

    public void Run(string[]? param) { }
    public string? StartFunction { get; set; }
    public async Task<MockThreadDto>? CreateThread(MockThreadCreationDto thread)
    {
        MockProcess? parent = _processService.GetProcessByPid(thread.ParentPid);
        if (parent == null)
        {
            return null;
        }
        var newThread = _builder
        .AddParentProcess(parent)
        .AddTid()
        .AddThreadStatus()
        .AddExitCode()
        .AddStartFunction(thread.StartFunction)
        .AddCreationTime()
        .AddStack()
        .Build();

        if(newThread == null || parent == null) {
            Console.WriteLine("***************null process or thread");
        } else {
            Console.WriteLine("********** not null thread");
            try  {
                parent.Threads.Add(newThread);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
            
        }

        await _repository.CreateThread(newThread);
        return _mapper.Map<MockThread, MockThreadDto>(newThread);
    }
    public bool SuspendThread(int tid)
    {
        return false;
    }
    public bool TerminateThread(int tid)
    {
        MockThread? t = GetThreadByTid(tid);
        if (t == null) {
            return false;
        }
        tidManager.RecycleTid(t.Parent.Pid,tid); // recycle the TID;
        _repository.TerminateThread(t);
        return true;
    }

    public MockThreadDto GetThreadDtoByTid(int tid) => _mapper.Map<MockThreadDto>(_repository.GetThreadByTid(tid));
    public MockThread? GetThreadByTid(int tid) => _repository.ThreadExists(tid) ? _repository.GetThreadByTid(tid) : null;

}
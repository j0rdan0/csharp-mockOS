using mockOSApi.Models;

namespace mockOSApi.Services;


public interface IThreadService {

    public MockThread CreateThread();

}
public class ThreadService {

    public string GenerateThreadName(int tid) {
        return String.Format("thread" + tid.ToString() + "_" + Guid.NewGuid().ToString());
    }

    public MockThread CreateThread(string? name) { 
        return new MockThread();
    }

}
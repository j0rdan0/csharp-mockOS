using mockOSApi.Models;

public interface IMemoryAllocatorService {
    public MockThreadStack CreateStack(MockThread thread);
}

public  class MemoryAllocatorService: IMemoryAllocatorService {
    // to use LinkedList for allocating the heap chunk a process uses; this should first be reserved, only if access is attempted to be actually allocated;
    // chance to implementing a very basic dynamic allocator class
    // linkedlist of free chunks for heap of size 1024 lets say

public MockThreadStack CreateStack(MockThread thread) {
    MockThreadStack stack = new MockThreadStack();
    stack.Stack = new List<byte[]> {};
    thread.Stack = stack;
    return stack;
}
}
To Do:

- to finish all the entity objects
- to implement mediator for communication between objects - process-threads etc
- implement booting process (on app startup)
- implement init process (pid 1)
- implement cleanup process ( to delete processes from db that are not marked as IsService)


- Requirements:

public Enum ProcessOperation {
 WriteTextToFile
 MoveFile
 CreateFile
}
 
 public class MockProcessDto()
{
    public int Pid { get; set; }
    public string? Image { get; set; }
 
    public ProcState Status { get; set; }
 
    public int Priority { get; set; }
 
   public ProcessOperation operation {get; set; }
   public Dictionary<string, string> ProcessArgs { get; set; }
 
}
https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-8.0&tabs=visual-studio

[4/16 1:11 PM] Horace Duma
TickAllProcesses()
[4/16 1:11 PM] Horace Duma
TickProcess(process) {
  
 
}
[4/16 1:12 PM] Horace Duma
switch (process.operation):
 case WriteTextToFile: ExecuteWriteTextToFile(process)
[4/16 1:13 PM] Horace Duma
ExecuteWriteTextToFile(process) {
  var fileName = process.ProcessArgs["fileName"];
 
  // find FileEntry in DB by using fileName
 
[4/16 1:13 PM] Horace Duma
// modify FileEntry contents based on ProcessArgs
[4/16 1:13 PM] Horace Duma
var contentToWrite = process.ProcessArgs["contentToWrite"]
 
fileEntry.content = fileEntry.content + contentToWrite

[4/16 1:39 PM] Horace Duma
FileEntry model
[4/16 1:44 PM] Horace Duma
Finalize Process Model
Implement Scheduling Algorithm
Create Thread Model
Create FileEntry Model
[4/16 1:46 PM] Horace Duma
SchedulingService
[4/16 1:48 PM] Horace Duma
ISchedulingService
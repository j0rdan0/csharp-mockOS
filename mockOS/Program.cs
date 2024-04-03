
using static System.Console;
using MockOS;
public class Program {

    static async Task Main(string[] args){
            WriteLine("TBD");

          List<Process> procList = new();
            foreach(var i in Enumerable.Range(0,10)) {
                procList.Add(new Process("/usr/bin/whoami",null));
                WriteLine($"got process with PID: {procList[i].GetPid()}");
                await procList[i].Start();
            }
           

    } 
}
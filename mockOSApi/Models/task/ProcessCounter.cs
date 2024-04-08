namespace mockOSApi.Models;
using System.ComponentModel.DataAnnotations;



public class ProcessCounter {

    [Key]
    public int Id {get ;set ;}
    
    public int ProcCounter {get;set;}
}
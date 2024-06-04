using System.ComponentModel.DataAnnotations;

namespace mockOSApi.Models;

public class MockThreadStack: OSObject {

    [Key]
    public int Id {get; set;}
    public List<byte[]> Stack { get; set; } 
}
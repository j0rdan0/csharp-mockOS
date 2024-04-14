using System.Security.Cryptography;

namespace mockOSApi.Models;

public abstract class OSObject
{


    /* Fields and properties
    *******************************
    */

    public int Handle { get; set; }

    /*
    Methods
    ********************************
    */

    public override string ToString()
    {
        return $"{GetType().ToString()}" + " object";
    }



}


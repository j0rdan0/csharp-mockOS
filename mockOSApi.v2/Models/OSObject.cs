using System.Security.Cryptography;

namespace mockOSApi.Models;

public abstract class OSObject
{


    /* Fields and properties
    *******************************
    */
    private int _handle;
    public int Handle
    {
        get => _handle;
        set => _handle = value;
    }
    public int HashCode
    {
        get; set;
    }

    /*
    Constructor members
    ********************************
    */

    public OSObject()
    {
        SetHashCode();
    }

    /*
    Methods
    ********************************
    */

    public override string ToString()
    {
        return $"{GetType().ToString()}" + " object";
    }

    public override int GetHashCode()
    {
        return this.HashCode;

    }


    private void SetHashCode()
    {

        byte[] source = new byte[14];
        new Random().NextBytes(source);
        using (MD5 md5 = MD5.Create())
        {
            byte[] hashed = md5.ComputeHash(source);
            this.HashCode = BitConverter.ToInt32(hashed, 0);
        }

    }
}


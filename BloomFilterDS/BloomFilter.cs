using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace BloomFilterDS;

public class BloomFilter
{
    private readonly BitArray bitArray;
    private int length;

    public BloomFilter(int length)
    {
        this.length = length;
        bitArray = new BitArray(length);
    }

    public void Add(string username)
    {
        int index_md5 = Md5(username);
        int index_sha1 = Sha1(username);
        int index_sha256 = Sha256(username);
        int index_sha512 = Sha512(username);
        bitArray[index_md5] = true;
        bitArray[index_sha1] = true;
        bitArray[index_sha256] = true;
        bitArray[index_sha512] = true;
    }

    public bool Contains(string username)
    {
        int index_md5 = Md5(username);
        int index_sha1 = Sha1(username);
        int index_sha256 = Sha256(username);
        int index_sha512 = Sha512(username);

        return bitArray[index_md5] && bitArray[index_sha1] && 
            bitArray[index_sha256] && bitArray[index_sha512];
    }

    private int Md5(string input)
    {
        MD5 md5 = MD5.Create();
        byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
        string hash = BitConverter.ToString(data).Replace("-", "");

        int result = ConvertHexNumberToInteger(hash);

        return result;
    }

    private int Sha1(string input)
    {
        SHA1 sha1 = SHA1.Create();
        byte[] data = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
        string hash = BitConverter.ToString(data).Replace("-", "");

        int result = ConvertHexNumberToInteger(hash);

        return result;
    }

    private int Sha256(string input)
    {
        SHA256 sha256 = SHA256.Create();
        byte[] data = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        string hash = BitConverter.ToString(data).Replace("-", "");

        int result = ConvertHexNumberToInteger(hash);

        return result;
    }

    private int Sha512(string input)
    {
        SHA512 sha512 = SHA512.Create();
        byte[] data = sha512.ComputeHash(Encoding.UTF8.GetBytes(input));
        string hash = BitConverter.ToString(data).Replace("-", "");

        int result = ConvertHexNumberToInteger(hash);

        return result;
    }

    private int ConvertHexNumberToInteger(string hexNumber)
    {
        int result = 0;
        
        foreach(var ch in hexNumber)
        {
            int integerValue = ('A' <= ch && ch <= 'F') ? (ch - 'A' + 10) : (ch - '0');
            result = (result * 16 + integerValue) % this.length;
        }

        return result;
    }
}

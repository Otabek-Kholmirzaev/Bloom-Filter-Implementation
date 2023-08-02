using BloomFilterDS;

internal class Program
{
    private static readonly int bloom_filter_array_length = 1_000_000;
    private static readonly int usernames_count = 1_000_000; 

    private static void Main(string[] args)
    {
        var bloomFilter = new BloomFilter(bloom_filter_array_length);
        
        List<string> usernames = GenerateUsernames(usernames_count);
        
        int falsePositiveCount = 0;
        foreach (var username in usernames)
        {
            if (bloomFilter.Contains(username))
            {
                falsePositiveCount++;
            }
            else
            {
                bloomFilter.Add(username);
            }
        }

        double falsePositiveProbability = (double)falsePositiveCount / (double)usernames_count * 100;
        Console.WriteLine("False Positive Probability: {0}", falsePositiveProbability);
    }

    private static List<string> GenerateUsernames(int count) => 
        Enumerable.Range(1, count).Select(x => "username" + x).ToList();
}
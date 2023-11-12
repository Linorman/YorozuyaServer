using StackExchange.Redis;

namespace YorozuyaServer.utils;

public class RedisUtil
{
    public static ConnectionMultiplexer? Connection;
    
    public RedisUtil()
    {
        Init();
    }

    private static void Init()
    {
        string connectionString = "localhost:6379";
        try
        {
            Connection = ConnectionMultiplexer.Connect(connectionString);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    private static IDatabase GetDatabase()
    {
        return Connection.GetDatabase();
    }
    
    public static bool Set(string key, string value)
    {
        return GetDatabase().StringSet(key, value);
    }
    
    public static string? Get(string key)
    {
        return GetDatabase().StringGet(key);
    }
    
    public static bool Delete(string key)
    {
        return GetDatabase().KeyDelete(key);
    }
    
    public static bool Exists(string key)
    {
        return GetDatabase().KeyExists(key);
    }
}
using StackExchange.Redis;

namespace YorozuyaServer.utils;

public class RedisUtil
{
    private ConnectionMultiplexer? Connection;
    
    public RedisUtil()
    {
        Init();
    }

    private void Init()
    {
        string connectionString = "redis:6379";
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
    
    private IDatabase GetDatabase()
    {
        return Connection.GetDatabase();
    }
    
    public bool Set(string key, string value)
    {
        var redisDb = GetDatabase();
        bool tag = redisDb.StringSet(value, key);
        return redisDb.StringSet(key, value) && tag;
    }

    public string? GetKey(string value)
    {
        var redisDb = GetDatabase();
        return redisDb.StringGet(value);
    }
    
    public string? Get(string key)
    {
        return GetDatabase().StringGet(key);
    }
    
    public bool Delete(string key)
    {
        string? value = GetDatabase().StringGet(key);
        if (value == null)
        {
            return false;
        }
        bool tag = GetDatabase().KeyDelete(key);
        return tag && GetDatabase().KeyDelete(value);
    }
    
    public bool Exists(string key)
    {
        return GetDatabase().KeyExists(key);
    }
}
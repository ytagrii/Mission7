using System;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Mission7.Infastructure
{
    //this is the class to keep track of sessions. Basically it builds a JSON file
    //that can then read data for me so data isnt lost while a user uses the site. 
    public static class SessionExtensions
    {
       public static void SetJson (this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

       public static T GetJson<T> (this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null ? default(T) : JsonSerializer.Deserialize<T>(sessionData);
        }
    }
}

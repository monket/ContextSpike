using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContextSpike.Tests
{
    public class TestServerFactory : IDisposable
    {
        public TestServer TestServer { get; }
        public HttpClient HttpClient { get; }
        
        public CharacterContext Context { get; }

        public TestServerFactory()
        {
            // Different name each time to prevent in memory db instances from conflicting
            var name = GenerateRandomString();
            var options = new DbContextOptionsBuilder<CharacterContext>().UseInMemoryDatabase(name).Options;
            Context = new CharacterContext(options);
            var whb = new WebHostBuilder().ConfigureServices(sc =>
            {
                sc.Add(new ServiceDescriptor(typeof(CharacterContext), Context));
            })
                .UseStartup<TestStartup>();

            TestServer = new TestServer(whb);
            HttpClient = TestServer.CreateClient();
        }
        

        public void Dispose()
        {
            TestServer?.Dispose();
            HttpClient?.Dispose();
            Context?.Dispose();
        }
        
        public static string GenerateRandomString(int length = 20)
        {
            // hacked from here: https://www.ryadel.com/en/c-sharp-random-password-generator-asp-net-core-mvc/
            string[] randomChars = {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                "!@$?_-"                        // non-alphanumeric
            };
            var rand = new Random(Environment.TickCount);
            var chars = new List<char>();

            chars.Insert(rand.Next(0, chars.Count),
                randomChars[0][rand.Next(0, randomChars[0].Length)]);

            chars.Insert(rand.Next(0, chars.Count),
                randomChars[1][rand.Next(0, randomChars[1].Length)]);

            chars.Insert(rand.Next(0, chars.Count),
                randomChars[2][rand.Next(0, randomChars[2].Length)]);

            
            for (var i = chars.Count; i < length; i++)
            {
                var rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
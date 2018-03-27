using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace ContextSpike.Tests
{
    [Collection("TestServerFactoryFixture")]
    public class CharacterControllerTest
    {
        private readonly TestServerFactory _factory;

        public CharacterControllerTest(TestServerFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task SavesCharacter()
        {
            var entity = new Character {Id = 1, Name = "Yoda"};
            var entityString = JsonConvert.SerializeObject(entity);

            var response = await _factory.HttpClient.PostAsync("/",new StringContent(entityString, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            _factory.Context.Characters.Count().ShouldBe(1);
        }
    }
}

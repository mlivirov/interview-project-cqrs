using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using TestProject.Application.Services;
using TestProject.Infrastructure.Services;

namespace TestProject.UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            var searchService = new BingSearchService(Options.Create<SearchServiceOptions>(new SearchServiceOptions
            {
                OutputLimit = 5
            }));

            await searchService.SearchAsync("test", CancellationToken.None);
            Assert.Pass();
        }
    }
}
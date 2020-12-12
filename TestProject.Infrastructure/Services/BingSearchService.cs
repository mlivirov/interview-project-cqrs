using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestProject.Application.Services;
using TestProject.Domain.Models;

namespace TestProject.Infrastructure.Services
{
    public class BingSearchService : ISearchService
    {
        private readonly IOptions<SearchServiceOptions> _options;

        public BingSearchService(IOptions<SearchServiceOptions> options)
        {
            _options = options;
        }

        public Task<IEnumerable<SearchResultEntry>> SearchAsync(string searchPhrase, CancellationToken cancellationToken)
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless", "--disable-extensions", "--no-sandbox");
            using var driver = new ChromeDriver(options);

            driver.Navigate().GoToUrl("https://www.bing.com/");
            var queryInput = driver.FindElementByCssSelector("input[name='q']");
            queryInput.SendKeys(searchPhrase);
            queryInput.Submit();

            var result = new List<SearchResultEntry>();
            var resultsBlock = driver.FindElementsByCssSelector("li[class='b_algo']");
            foreach (var resultBlock in resultsBlock)
            {
                var anchor = resultBlock.FindElement(By.CssSelector("h2 > a"));
                result.Add(new SearchResultEntry
                {
                    Title = anchor.Text,
                    Link = anchor.GetProperty("href"),
                    SearchEngine = "Bing",
                });
            }

            driver.Quit();

            return Task.FromResult(result.Take(_options.Value.OutputLimit));
        }
    }
}
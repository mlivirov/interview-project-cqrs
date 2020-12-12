using System;
using System.Collections.Generic;
using System.IO;
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
    public class GoogleSearchService : ISearchService
    {
        private readonly IOptions<SearchServiceOptions> _options;

        public GoogleSearchService(IOptions<SearchServiceOptions> options)
        {
            _options = options;
        }

        public Task<IEnumerable<SearchResultEntry>> SearchAsync(string searchPhrase, CancellationToken cancellationToken)
        {
            using var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.google.com/");
            var queryInput = driver.FindElementByCssSelector("input[name='q']");
            queryInput.SendKeys(searchPhrase);
            queryInput.Submit();

            var result = new List<SearchResultEntry>();
            var resultsBlock = driver.FindElementsByCssSelector("[class='g']");
            foreach (var resultBlock in resultsBlock)
            {
                var anchor = resultBlock.FindElement(By.CssSelector("a[ping]"));
                result.Add(new SearchResultEntry
                {
                    Title = anchor.FindElement(By.TagName("h3")).Text,
                    Link = anchor.GetProperty("href"),
                    SearchEngine = "Google",
                });
            }

            driver.Quit();

            return Task.FromResult(result.Take(_options.Value.OutputLimit));
        }
    }
}
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Fundo.Core.Dtos;
using Fundo.Core.Entities;
using Xunit;

namespace Fundo.Services.Tests.Integration
{
    public class LoanManagementControllerTests(WebApplicationFactory<Fundo.Applications.WebApi.Startup> factory)
        : IClassFixture<WebApplicationFactory<Fundo.Applications.WebApi.Startup>>
    {
        private readonly HttpClient _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        [Fact]
        public async Task CreateLoan_ShouldReturnExpectedResult()
        {
            var request = new
            {
                amountRequested = 3241,
                accountHolderId = 3
            };

            var response = await _client.PostAsJsonAsync("/loans", request, cancellationToken: TestContext.Current.CancellationToken);
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task GetLoans_ShouldReturnExpectedResult()
        {
            var response = await _client.GetAsync("/loans", TestContext.Current.CancellationToken);
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadFromJsonAsync<List<LoanDetailsDto>>(cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(body);
            Assert.NotEmpty(body);
        }
        
        [Fact]
        public async Task GetLoanDetails_ShouldReturnExpectedResult()
        {
            var response = await _client.GetAsync("/loans/1", TestContext.Current.CancellationToken);
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadFromJsonAsync<LoanDetailsDto>(cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(body);
            Assert.Equal(1, body.Id);
            Assert.Equal(nameof(LoanStatus.Active).ToLower(), body.Status);
            Assert.True(body.AmountRequested > 0);
        }
    }
}
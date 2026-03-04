using Nager.Moco.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Nager.Moco
{
    public class MocoClient : IMocoClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public MocoClient(
            IHttpClientFactory httpClientFactory,
            string mocoCustomerDomain,
            string apiToken)
        {
            this._jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };

            this._httpClient = httpClientFactory.CreateClient();
            this._httpClient.BaseAddress = new Uri($"https://{mocoCustomerDomain}.mocoapp.com");
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", $"token={apiToken}");
        }

        public async Task<Company[]?> GetCompaniesAsync(
            CancellationToken cancellationToken = default)
        {
            using var httpResponseMessage = await this._httpClient.GetAsync("/api/v1/companies", cancellationToken);

            httpResponseMessage.EnsureSuccessStatusCode();

            return await httpResponseMessage.Content.ReadFromJsonAsync<Company[]>(this._jsonSerializerOptions, cancellationToken);
        }

        public async Task<Company?> GetCompanyAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            using var httpResponseMessage = await this._httpClient.GetAsync($"/api/v1/companies/{id}", cancellationToken);

            httpResponseMessage.EnsureSuccessStatusCode();

            return await httpResponseMessage.Content.ReadFromJsonAsync<Company>(this._jsonSerializerOptions, cancellationToken);
        }

        public async Task<bool> DeleteCompanyAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            using var httpResponseMessage = await this._httpClient.DeleteAsync($"/api/v1/companies/{id}", cancellationToken);

            return httpResponseMessage.IsSuccessStatusCode;
        }

        public async Task<Company?> CreateCompanyAsync(
            CompanyCreateRequest createRequest,
            CancellationToken cancellationToken = default)
        {
            using var httpResponseMessage = await this._httpClient.PostAsJsonAsync("/api/v1/companies", createRequest, this._jsonSerializerOptions, cancellationToken);

            httpResponseMessage.EnsureSuccessStatusCode();

            return await httpResponseMessage.Content.ReadFromJsonAsync<Company>(cancellationToken);
        }

        public async Task<Invoice?> CreateInvoiceAsync(
            InvoiceCreateRequest createRequest,
            CancellationToken cancellationToken = default)
        {
            using var httpResponseMessage = await this._httpClient.PostAsJsonAsync("/api/v1/invoices", createRequest, this._jsonSerializerOptions, cancellationToken);

            httpResponseMessage.EnsureSuccessStatusCode();

            return await httpResponseMessage.Content.ReadFromJsonAsync<Invoice>(cancellationToken);
        }

        public async Task<Invoice?> GetInvoiceAsync(
            int invoiceId,
            CancellationToken cancellationToken = default)
        {
            using var httpResponseMessage = await this._httpClient.GetAsync($"/api/v1/invoices/{invoiceId}", cancellationToken);
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }

            return await httpResponseMessage.Content.ReadFromJsonAsync<Invoice>(this._jsonSerializerOptions, cancellationToken);
        }

        public async Task<bool> SendInvoiceAsync(
            int invoiceId,
            InvoiceSendEmailRequest invoiceSendEmailRequest,
            CancellationToken cancellationToken = default)
        {
            using var httpResponseMessage = await this._httpClient.PostAsJsonAsync($"/api/v1/invoices/{invoiceId}/send_email", invoiceSendEmailRequest, this._jsonSerializerOptions, cancellationToken);

            var json = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);

            return httpResponseMessage.IsSuccessStatusCode;
        }
    }
}

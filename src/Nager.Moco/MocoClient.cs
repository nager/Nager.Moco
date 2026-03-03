using Nager.Moco.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Nager.Moco
{
    public class MocoClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public MocoClient(
            string mocoCustomerDomain,
            string apiToken)
        {
            this._jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };

            this._httpClient = new HttpClient();
            this._httpClient.BaseAddress = new Uri($"https://{mocoCustomerDomain}.mocoapp.com");
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", $"token={apiToken}");
        }

        public async Task<Company[]?> GetCompaniesAsync(
            CancellationToken cancellationToken = default)
        {
            var items = await this._httpClient.GetFromJsonAsync<Company[]>("/api/v1/companies", cancellationToken);
            return items;
        }

        public async Task<Company?> GetCompanyAsync(
            int customerId,
            CancellationToken cancellationToken = default)
        {
            using var httpResponseMessage = await this._httpClient.GetAsync($"/api/v1/companies/{customerId}", cancellationToken);
            return await httpResponseMessage.Content.ReadFromJsonAsync<Company>(this._jsonSerializerOptions, cancellationToken);
        }

        public async Task CreateCompanyAsync(
            CompanyCreateRequest createRequest,
            CancellationToken cancellationToken = default)
        {
            using var httpResponseMessage = await this._httpClient.PostAsJsonAsync("/api/v1/companies", createRequest, this._jsonSerializerOptions, cancellationToken);

            var json = await httpResponseMessage.Content.ReadFromJsonAsync<Company>(cancellationToken);
        }

        public async Task CreateInvoiceAsync(
            InvoiceCreateRequest createRequest,
            CancellationToken cancellationToken = default)
        {
            using var httpResponseMessage = await this._httpClient.PostAsJsonAsync("/api/v1/invoices", createRequest, this._jsonSerializerOptions, cancellationToken);

            var json = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
        }

        public async Task<Invoice?> GetInvoiceAsync(
            string invoiceId,
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
            string invoiceId,
            InvoiceSendEmailRequest invoiceSendEmailRequest,
            CancellationToken cancellationToken = default)
        {
            using var httpResponseMessage = await this._httpClient.PostAsJsonAsync($"/api/v1/invoices/{invoiceId}/send_email", invoiceSendEmailRequest, this._jsonSerializerOptions, cancellationToken);

            var json = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);

            return httpResponseMessage.IsSuccessStatusCode;
        }
    }
}

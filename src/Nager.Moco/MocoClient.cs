using Nager.Moco.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Nager.Moco
{
    /// <summary>
    /// Moco Client
    /// </summary>
    public class MocoClient : IMocoClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        /// <summary>
        /// Moco Client
        /// </summary>
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

        /// <inheritdoc />
        public async Task<Company[]?> GetCompaniesAsync(
            CancellationToken cancellationToken = default)
        {
            using var httpResponseMessage = await this._httpClient.GetAsync("/api/v1/companies", cancellationToken);

            httpResponseMessage.EnsureSuccessStatusCode();

            return await httpResponseMessage.Content.ReadFromJsonAsync<Company[]>(this._jsonSerializerOptions, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Company?> GetCompanyAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            using var httpResponseMessage = await this._httpClient.GetAsync($"/api/v1/companies/{id}", cancellationToken);

            httpResponseMessage.EnsureSuccessStatusCode();

            return await httpResponseMessage.Content.ReadFromJsonAsync<Company>(this._jsonSerializerOptions, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteCompanyAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            using var httpResponseMessage = await this._httpClient.DeleteAsync($"/api/v1/companies/{id}", cancellationToken);

            return httpResponseMessage.IsSuccessStatusCode;
        }

        /// <inheritdoc />
        public async Task<Company?> CreateCompanyAsync(
            CompanyCreateRequest createRequest,
            CancellationToken cancellationToken = default)
        {
            using var httpResponseMessage = await this._httpClient.PostAsJsonAsync("/api/v1/companies", createRequest, this._jsonSerializerOptions, cancellationToken);

            httpResponseMessage.EnsureSuccessStatusCode();

            return await httpResponseMessage.Content.ReadFromJsonAsync<Company>(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Invoice?> CreateInvoiceAsync(
            InvoiceCreateRequest createRequest,
            CancellationToken cancellationToken = default)
        {
            using var httpResponseMessage = await this._httpClient.PostAsJsonAsync("/api/v1/invoices", createRequest, this._jsonSerializerOptions, cancellationToken);

            httpResponseMessage.EnsureSuccessStatusCode();

            return await httpResponseMessage.Content.ReadFromJsonAsync<Invoice>(cancellationToken);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public async Task<Invoice[]?> GetInvoicesAsync(
            InvoiceQueryFilter? invoiceQueryFilter = null,
            CancellationToken cancellationToken = default)
        {
            var queryParameters = new Dictionary<string, string>();

            if (invoiceQueryFilter is not null)
            {
                if (invoiceQueryFilter.CompanyId is not null)
                {
                    queryParameters.Add("company_id", $"{invoiceQueryFilter.CompanyId}");
                }
                if (invoiceQueryFilter.ProjectId is not null)
                {
                    queryParameters.Add("project_id", $"{invoiceQueryFilter.ProjectId}");
                }
                if (invoiceQueryFilter.DateFrom is not null)
                {
                    queryParameters.Add("date_from", $"{invoiceQueryFilter.DateFrom:yyyy-MM-dd}");
                }
                if (invoiceQueryFilter.DateTo is not null)
                {
                    queryParameters.Add("date_to", $"{invoiceQueryFilter.DateTo:yyyy-MM-dd}");
                }
            }

            string queryParams;
            using (var content = new FormUrlEncodedContent(queryParameters))
            {
                queryParams = await content.ReadAsStringAsync(cancellationToken);
            }

            var url = new StringBuilder();
            url.Append("/api/v1/invoices");
            if (!string.IsNullOrEmpty(queryParams))
            {
                url.Append("?");
                url.Append(queryParams);
            }

            using var httpResponseMessage = await this._httpClient.GetAsync(url.ToString(), cancellationToken);
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }

            //TODO: Forward information add Global Page Parameter
            httpResponseMessage.Headers.TryGetValues("X-Page", out var xPage);
            httpResponseMessage.Headers.TryGetValues("X-Per-Page", out var xPerPage);
            httpResponseMessage.Headers.TryGetValues("X-Total", out var xTotal);

            return await httpResponseMessage.Content.ReadFromJsonAsync<Invoice[]>(this._jsonSerializerOptions, cancellationToken);
        }

        /// <inheritdoc />
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

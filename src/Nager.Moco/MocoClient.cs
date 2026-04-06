using Nager.Moco.Models;
using Nager.Moco.QueryFilters;
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
        /// <param name="httpClientFactory"></param>
        /// <param name="mocoAccount"></param>
        /// <param name="apiToken"></param>
        public MocoClient(
            IHttpClientFactory httpClientFactory,
            string mocoAccount,
            string apiToken)
        {
            this._jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };

            this._httpClient = httpClientFactory.CreateClient();
            this._httpClient.BaseAddress = new Uri($"https://{mocoAccount}.mocoapp.com");
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", $"token={apiToken}");
        }

        private int GetPagingTotal(HttpResponseMessage httpResponseMessage)
        {
            //httpResponseMessage.Headers.TryGetValues("X-Page", out var xPage);
            //httpResponseMessage.Headers.TryGetValues("X-Per-Page", out var xPerPage);
            httpResponseMessage.Headers.TryGetValues("X-Total", out var xTotal);

            if (xTotal is null)
            {
                return 0;
            }

            if (xTotal.Count() != 1)
            {
                return 0;
            }

            if (!int.TryParse(xTotal.First(), out var total))
            {
                return 0;
            }

            return total;
        }

        private async Task<string> BuildQueryAsync(
            string endpointUrl,
            Dictionary<string, string> queryParameters)
        {
            string queryParams;
            using var content = new FormUrlEncodedContent(queryParameters);
            queryParams = await content.ReadAsStringAsync();

            var url = new StringBuilder();
            url.Append(endpointUrl);
            if (!string.IsNullOrEmpty(queryParams))
            {
                url.Append('?');
                url.Append(queryParams);
            }

            return url.ToString();
        }

        /// <inheritdoc />
        public async Task<PagingInfo<Company>> GetCompaniesAsync(
            int page = 1,
            CancellationToken cancellationToken = default)
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "page", $"{page}" }
            };

            var url = await this.BuildQueryAsync("/api/v1/companies", queryParameters);

            using var httpResponseMessage = await this._httpClient.GetAsync(url, cancellationToken);

            httpResponseMessage.EnsureSuccessStatusCode();

            var items = await httpResponseMessage.Content.ReadFromJsonAsync<Company[]>(this._jsonSerializerOptions, cancellationToken);
            var total = this.GetPagingTotal(httpResponseMessage);

            return new PagingInfo<Company>
            {
                CurrentPage = page,
                Total = total,
                Items = items ?? []
            };
        }

        /// <inheritdoc />
        public async Task<Company?> GetCompanyAsync(
            int companyId,
            CancellationToken cancellationToken = default)
        {
            using var httpResponseMessage = await this._httpClient.GetAsync($"/api/v1/companies/{companyId}", cancellationToken);

            httpResponseMessage.EnsureSuccessStatusCode();

            return await httpResponseMessage.Content.ReadFromJsonAsync<Company>(this._jsonSerializerOptions, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteCompanyAsync(
            int companyId,
            CancellationToken cancellationToken = default)
        {
            using var httpResponseMessage = await this._httpClient.DeleteAsync($"/api/v1/companies/{companyId}", cancellationToken);

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
        public async Task<InvoiceDetail?> GetInvoiceAsync(
            int invoiceId,
            CancellationToken cancellationToken = default)
        {
            using var httpResponseMessage = await this._httpClient.GetAsync($"/api/v1/invoices/{invoiceId}", cancellationToken);
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }

            return await httpResponseMessage.Content.ReadFromJsonAsync<InvoiceDetail>(this._jsonSerializerOptions, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<PagingInfo<Invoice>> GetInvoicesAsync(
            int page = 1,
            InvoiceQueryFilter? queryFilter = null,
            CancellationToken cancellationToken = default)
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "page", $"{page}" }
            };

            if (queryFilter is not null)
            {
                if (queryFilter.CompanyId is not null)
                {
                    queryParameters.Add("company_id", $"{queryFilter.CompanyId}");
                }
                if (queryFilter.ProjectId is not null)
                {
                    queryParameters.Add("project_id", $"{queryFilter.ProjectId}");
                }
                if (queryFilter.DateFrom is not null)
                {
                    queryParameters.Add("date_from", $"{queryFilter.DateFrom:yyyy-MM-dd}");
                }
                if (queryFilter.DateTo is not null)
                {
                    queryParameters.Add("date_to", $"{queryFilter.DateTo:yyyy-MM-dd}");
                }
            }

            var url = await this.BuildQueryAsync("/api/v1/invoices", queryParameters);

            using var httpResponseMessage = await this._httpClient.GetAsync(url, cancellationToken);
            httpResponseMessage.EnsureSuccessStatusCode();

            var items = await httpResponseMessage.Content.ReadFromJsonAsync<Invoice[]>(this._jsonSerializerOptions, cancellationToken);
            var total = this.GetPagingTotal(httpResponseMessage);

            return new PagingInfo<Invoice>
            {
                CurrentPage = page,
                Total = total,
                Items = items ?? []
            };
        }

        /// <inheritdoc />
        public async Task<bool> SendInvoiceAsync(
            int invoiceId,
            InvoiceSendEmailRequest invoiceSendEmailRequest,
            CancellationToken cancellationToken = default)
        {
            using var httpResponseMessage = await this._httpClient.PostAsJsonAsync($"/api/v1/invoices/{invoiceId}/send_email", invoiceSendEmailRequest, this._jsonSerializerOptions, cancellationToken);
            httpResponseMessage.EnsureSuccessStatusCode();

            //var json = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);

            return httpResponseMessage.IsSuccessStatusCode;
        }

        /// <inheritdoc />
        public async Task<PagingInfo<InvoicePayment>> GetInvoicePaymentsAsync(
            int page = 1,
            InvoicePaymentQueryFilter? queryFilter = null,
            CancellationToken cancellationToken = default)
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "page", $"{page}" }
            };

            if (queryFilter is not null)
            {
                if (queryFilter.DateFrom is not null)
                {
                    queryParameters.Add("date_from", $"{queryFilter.DateFrom:yyyy-MM-dd}");
                }
                if (queryFilter.DateTo is not null)
                {
                    queryParameters.Add("date_to", $"{queryFilter.DateTo:yyyy-MM-dd}");
                }
            }

            var url = await this.BuildQueryAsync("/api/v1/invoices/payments", queryParameters);

            using var httpResponseMessage = await this._httpClient.GetAsync(url, cancellationToken);
            httpResponseMessage.EnsureSuccessStatusCode();

            var items = await httpResponseMessage.Content.ReadFromJsonAsync<InvoicePayment[]>(this._jsonSerializerOptions, cancellationToken);
            var total = this.GetPagingTotal(httpResponseMessage);

            return new PagingInfo<InvoicePayment>
            {
                CurrentPage = page,
                Total = total,
                Items = items ?? []
            };
        }

        /// <inheritdoc />
        public async Task<PagingInfo<Receipt>> GetReceiptsAsync(
            int page = 1,
            ReceiptQueryFilter? queryFilter = null,
            CancellationToken cancellationToken = default)
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "page", $"{page}" }
            };

            if (queryFilter is not null)
            {
                if (queryFilter.ProjectId is not null)
                {
                    queryParameters.Add("project_id", $"{queryFilter.ProjectId}");
                }
                if (queryFilter.DateFrom is not null)
                {
                    queryParameters.Add("from", $"{queryFilter.DateFrom:yyyy-MM-dd}");
                }
                if (queryFilter.DateTo is not null)
                {
                    queryParameters.Add("to", $"{queryFilter.DateTo:yyyy-MM-dd}");
                }
            }

            var url = await this.BuildQueryAsync("/api/v1/receipts", queryParameters);

            using var httpResponseMessage = await this._httpClient.GetAsync(url, cancellationToken);
            httpResponseMessage.EnsureSuccessStatusCode();

            var items = await httpResponseMessage.Content.ReadFromJsonAsync<Receipt[]>(this._jsonSerializerOptions, cancellationToken);
            var total = this.GetPagingTotal(httpResponseMessage);

            return new PagingInfo<Receipt>
            {
                CurrentPage = page,
                Total = total,
                Items = items ?? []
            };
        }

        /// <inheritdoc />
        public async Task<PagingInfo<Purchase>> GetPurchasesAsync(
            int page = 1,
            PurchaseQueryFilter? queryFilter = null,
            CancellationToken cancellationToken = default)
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "page", $"{page}" }
            };

            if (queryFilter is not null)
            {
                if (queryFilter.CompanyId is not null)
                {
                    queryParameters.Add("company_id", $"{queryFilter.CompanyId}");
                }

                if (queryFilter.DateFrom is not null && queryFilter.DateTo is not null)
                {
                    queryParameters.Add("date", $"{queryFilter.DateFrom:yyyy-MM-dd}:{queryFilter.DateTo:yyyy-MM-dd}");
                }
            }

            var url = await this.BuildQueryAsync("/api/v1/purchases", queryParameters);

            using var httpResponseMessage = await this._httpClient.GetAsync(url, cancellationToken);
            httpResponseMessage.EnsureSuccessStatusCode();

            var items = await httpResponseMessage.Content.ReadFromJsonAsync<Purchase[]>(this._jsonSerializerOptions, cancellationToken);
            var total = this.GetPagingTotal(httpResponseMessage);

            return new PagingInfo<Purchase>
            {
                CurrentPage = page,
                Total = total,
                Items = items ?? []
            };
        }
    }
}

using Nager.Moco.Models;
using Nager.Moco.QueryFilters;

namespace Nager.Moco
{
    public interface IMocoClient
    {
        #region Company

        Task<Company?> CreateCompanyAsync(
            CompanyCreateRequest createRequest,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteCompanyAsync(
            int companyId,
            CancellationToken cancellationToken = default);

        Task<PagingInfo<Company>> GetCompaniesAsync(
            int page = 1,
            CancellationToken cancellationToken = default);

        Task<Company?> GetCompanyAsync(
            int companyId,
            CancellationToken cancellationToken = default);

        #endregion

        #region Invoice

        Task<PagingInfo<Invoice>> GetInvoicesAsync(
            int page = 1,
            InvoiceQueryFilter? queryFilter = null,
            CancellationToken cancellationToken = default);

        Task<InvoiceDetail?> GetInvoiceAsync(
            int invoiceId,
            CancellationToken cancellationToken = default);

        Task<Invoice?> CreateInvoiceAsync(
            InvoiceCreateRequest createRequest,
            CancellationToken cancellationToken = default);

        Task<PagingInfo<InvoicePayment>> GetInvoicePaymentsAsync(
            int page = 1,
            InvoicePaymentQueryFilter? queryFilter = null,
            CancellationToken cancellationToken = default);

        Task<bool> UpdateInvoicePaymentAsync(
            int resourceId,
            InvoicePaymentUpdateRequest updateRequest,
            CancellationToken cancellationToken = default);


        Task<bool> CreateInvoicePaymentAsync(
            InvoicePaymentCreateRequest createRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Send Invoice Mail
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="invoiceSendEmailRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> SendInvoiceAsync(
            int invoiceId,
            InvoiceSendEmailRequest invoiceSendEmailRequest,
            CancellationToken cancellationToken = default);

        #endregion

        #region Purchases

        Task<PagingInfo<Purchase>> GetPurchasesAsync(
            int page = 1,
            PurchaseQueryFilter? queryFilter = null,
            CancellationToken cancellationToken = default);

        #endregion

        #region Receipt

        Task<PagingInfo<Receipt>> GetReceiptsAsync(
            int page = 1,
            ReceiptQueryFilter? queryFilter = null,
            CancellationToken cancellationToken = default);

        #endregion
    }
}

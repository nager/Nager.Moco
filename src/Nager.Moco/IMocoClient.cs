using Nager.Moco.Models;

namespace Nager.Moco
{
    public interface IMocoClient
    {
        #region Company

        Task<Company?> CreateCompanyAsync(
            CompanyCreateRequest createRequest,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteCompanyAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<Company[]?> GetCompaniesAsync(
            CancellationToken cancellationToken = default);

        Task<Company?> GetCompanyAsync(
            int id,
            CancellationToken cancellationToken = default);

        #endregion

        #region Invoice

        Task<Invoice[]?> GetInvoicesAsync(
            InvoiceQueryFilter? queryFilter = null,
            CancellationToken cancellationToken = default);

        Task<InvoiceDetail?> GetInvoiceAsync(
            int invoiceId,
            CancellationToken cancellationToken = default);

        Task<Invoice?> CreateInvoiceAsync(
            InvoiceCreateRequest createRequest,
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

        Task<Purchase[]?> GetPurchasesAsync(
            PurchaseQueryFilter? queryFilter = null,
            CancellationToken cancellationToken = default);

        #endregion

        #region Receipt

        Task<Receipt[]?> GetReceiptsAsync(
            ReceiptQueryFilter? queryFilter = null,
            CancellationToken cancellationToken = default);

        #endregion
    }
}

using Nager.Moco.Models;

namespace Nager.Moco
{
    public interface IMocoClient
    {
        Task<Company?> CreateCompanyAsync(
            CompanyCreateRequest createRequest,
            CancellationToken cancellationToken = default);

        Task<Invoice?> CreateInvoiceAsync(
            InvoiceCreateRequest createRequest,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteCompanyAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<Company[]?> GetCompaniesAsync(
            CancellationToken cancellationToken = default);

        Task<Company?> GetCompanyAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<Invoice?> GetInvoiceAsync(
            int invoiceId,
            CancellationToken cancellationToken = default);

        Task<bool> SendInvoiceAsync(
            int invoiceId,
            InvoiceSendEmailRequest invoiceSendEmailRequest,
            CancellationToken cancellationToken = default);
    }
}

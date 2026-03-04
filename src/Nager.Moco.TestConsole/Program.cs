using Microsoft.Extensions.DependencyInjection;
using Nager.Moco;
using Nager.Moco.Models;

var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

var mocoClient = new MocoClient(httpClientFactory, "mycustomdomain", "my_api_key");

var companies = await mocoClient.GetCompaniesAsync();

var companyCreateRequest = new CompanyCreateRequest
{
    Name = "My Test Company",
    Currency = "EUR",
    CountryCode = "DE",
    VatIdentifier = ""
};

var newCompany1 = await mocoClient.CreateCompanyAsync(companyCreateRequest);

var isDeleted = await mocoClient.DeleteCompanyAsync(newCompany1.Id);

var newCompany2 = await mocoClient.CreateCompanyAsync(companyCreateRequest);

var invoiceCreateRequest = new InvoiceCreateRequest
{
    CustomerId = newCompany2.Id,
    RecipientAddress = "Test Address 1b",
    Date = "2026-04-01",
    DueDate = "2026-04-01",
    Title = "Auto Invoice1",
    Tax = 0,
    Currency = "EUR",
    Items =
    [
        new InvoiceItem
        {
            Type = "title",
            Title = "Hours"
        },
        new InvoiceItem
        {
            Type = "item",
            Title = "Hosting",
            Quantity = 1,
            Unit = "Stk",
            UnitPrice = 10
        }
    ]
};

var invoice = await mocoClient.CreateInvoiceAsync(invoiceCreateRequest);

var sendResponse = await mocoClient.SendInvoiceAsync(invoice.Id, new InvoiceSendEmailRequest
{
    EmailsTo = "",
    Subject = "test",
    Text = "test"
});

Console.WriteLine("process done");

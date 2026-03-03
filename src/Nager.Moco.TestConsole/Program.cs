using Nager.Moco;
using Nager.Moco.Models;

var mocoClient = new MocoClient("mycustomdomain", "my_api_key");
var companies = await mocoClient.GetCompaniesAsync();

await mocoClient.CreateCompanyAsync(new CompanyCreateRequest
{
    Name = "My Test Company",
    Currency = "EUR",
});

var invoiceCreateRequest = new InvoiceCreateRequest
{
    CustomerId = "763125795", //K0005
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

await mocoClient.CreateInvoiceAsync(invoiceCreateRequest);

await mocoClient.SendInvoiceAsync("invoiceId", new InvoiceSendEmailRequest
{
    EmailsTo = "",
    Subject = "",
    Text = ""
});

Console.WriteLine("process done");

# Nager.Moco

A lightweight .NET client for the [MOCO API](https://everii-group.github.io/mocoapp-api-docs/). This library simplifies the interaction with MOCO, allowing you to manage companies, invoices, and more directly from your C# applications.

## Installation

You can install the package via NuGet Package Manager:

```bash
dotnet add package Nager.Moco
```

## Features

* Simple authentication using API keys.
* Manage **Companies** (List, Create, Delete).
* Manage **Invoices** (Create, Send via Email).
* Asynchronous implementation (`Task`-based).

## Usage

### Initialize the Client

To start using the API, you need your MOCO subdomain and an API key (Personal API Token).

```csharp
using Nager.Moco;

var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

var mocoClient = new MocoClient(httpClientFactory, "mycustomdomain", "my_api_key");
```

### Examples

#### Working with Companies

```csharp
using Nager.Moco.Models;

// Get all companies
var companies = await mocoClient.GetCompaniesAsync();

// Create a new company
var companyCreateRequest = new CompanyCreateRequest
{
    Name = "My Test Company",
    Currency = "EUR",
    CountryCode = "DE",
    VatIdentifier = "DE123456789"
};

var newCompany = await mocoClient.CreateCompanyAsync(companyCreateRequest);

// Delete a company
var isDeleted = await mocoClient.DeleteCompanyAsync(newCompany.Id);

```

#### Creating and Sending an Invoice

```csharp
var invoiceCreateRequest = new InvoiceCreateRequest
{
    CustomerId = 12345, // ID of the company
    RecipientAddress = "Test Address 1b",
    Date = "2026-04-01",
    DueDate = "2026-04-01",
    Title = "Auto Invoice",
    Tax = 19,
    Currency = "EUR",
    Items = new []
    {
        new InvoiceItem
        {
            Type = "title",
            Title = "Development Services"
        },
        new InvoiceItem
        {
            Type = "item",
            Title = "Hosting Fees",
            Quantity = 1,
            Unit = "Stk",
            UnitPrice = 10.00
        }
    }
};

// Create the invoice
var invoice = await mocoClient.CreateInvoiceAsync(invoiceCreateRequest);

// Send the invoice via Email
var sendResponse = await mocoClient.SendInvoiceAsync(invoice.Id, new InvoiceSendEmailRequest
{
    EmailsTo = "client@example.com",
    Subject = "Your Invoice",
    Text = "Please find your invoice attached."
});

```

## Requirements

* .NET 10.0 or higher
* A valid MOCO account

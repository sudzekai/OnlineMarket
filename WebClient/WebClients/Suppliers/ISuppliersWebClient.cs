using DTO.Models.Suppliers;
using WebClient.WebClients.WebClient;

namespace WebClient.WebClients.Suppliers
{
    public interface ISuppliersWebClient : IWebClientBase<SupplierFullDto, SupplierActionDto, SupplierActionDto>
    {
    }
}
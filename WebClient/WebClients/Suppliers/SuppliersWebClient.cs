using DTO.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.Text;
using WebClient.WebClients.WebClient;

namespace WebClient.WebClients.Suppliers
{
    public class SuppliersWebClient : WebClientBase<SupplierFullDto, SupplierActionDto, SupplierActionDto>, ISuppliersWebClient
    {
        public SuppliersWebClient(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}

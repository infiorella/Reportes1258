using Colegio1258.Repositories.Abstract;
using System.Data;

namespace Colegio1258.Repositories.Implementation
{
    public class Personal : IPersonal
    {
        private IConfiguration configuration;


        DataTable IPersonal.CustomerDataTable(string path)
        {
            throw new NotImplementedException();
        }

        string IPersonal.DocumentUpload(IFormFile formFile)
        {
            throw new NotImplementedException();
        }

        void IPersonal.ImportCustomer(DataTable customer)
        {
            throw new NotImplementedException();
        }
    }
}

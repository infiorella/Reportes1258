using System.Data;

namespace Colegio1258.Repositories.Abstract
{
    public interface IPersonal
    {
        string DocumentUpload(IFormFile formFile);

        DataTable CustomerDataTable(string path);

        void ImportCustomer(DataTable customer);
    }
}

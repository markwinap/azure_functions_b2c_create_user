using System.Threading.Tasks;
using AzureService.Model;

namespace AzureService.Repositories
{
    public interface IIdentityRepository
    {
        Task<ResponseModel> PostUser(UserModel user);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using LazyGenerals.Server.Models.Entities;
using MongoDB.Driver;

namespace LazyGenerals.Server.Models
{
    public interface IClientContext
    {
        Client CreateClient(string username, string pass);
        IEnumerable<Client> GetAllClients();
        Task<Client> GetClient(string name);
        ReplaceOneResult ChangePassword(string username, string newPass);
        DeleteResult DeleteClient(string username);
    }
}
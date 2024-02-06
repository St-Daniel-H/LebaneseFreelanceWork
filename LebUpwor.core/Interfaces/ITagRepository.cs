using LebUpwor.core.Models;
using startup.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Interfaces
{
    public interface ITagRepository : IRepositoryRepository<Tag>
    {
        Task<IEnumerable<Tag>> GetAll();
        Task<Tag> GetTagByName(String name);
        Task<IEnumerable<Tag>> GetTagsByMakerId(int userId);
        Task<IEnumerable<Tag>> GetTagsBySimilarName(string name);
    }
}

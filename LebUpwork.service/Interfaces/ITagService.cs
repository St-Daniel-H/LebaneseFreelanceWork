using LebUpwor.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwork.service.Interfaces
{
    public interface ITagService
    {
        Task<Tag> CreateTag(Tag tag);
        Task DeleteTag(Tag tag);
        Task<IEnumerable<Tag>> GetAll();
        Task<Tag> GetTagByName(String name);
        Task<IEnumerable<Tag>> GetTagsBySimilarName(String name);

        Task<IEnumerable<Tag>> GetTagsByMakerId(int userId);

        Task CommitChanges();
    }
}

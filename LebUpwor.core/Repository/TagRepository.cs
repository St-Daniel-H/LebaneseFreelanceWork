using LebUpwor.core.Interfaces;
using LebUpwor.core.Models;
using Microsoft.EntityFrameworkCore;
using startup.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Repository
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(UpworkLebContext context)
        : base(context)
        { }
        private UpworkLebContext UpworkLebContext
        {
            get { return Context as UpworkLebContext; }
        }

        public async Task<IEnumerable<Tag>> GetAll()
        {
            return await UpworkLebContext.Tags
               .ToListAsync();
        }
        public async Task<Tag> GetTagByName(string name)
        {
            return await UpworkLebContext.Tags
         .Where(u => u.TagName.ToLower() == name.ToLower())
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Tag>> GetTagsByMakerId(int userId)
        {
            return await UpworkLebContext.Tags
                .Where(u=> u.AddedByUserId == userId)
              .ToListAsync();
        }
        public async Task<Tag> GetTagById(int Id)
        {
            return await UpworkLebContext.Tags.Where(u => u.TagId == Id).SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<Tag>> GetTagsBySimilarName(string name)
        {
            return await UpworkLebContext.Tags
                .Where(u => u.TagName.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

    }
}

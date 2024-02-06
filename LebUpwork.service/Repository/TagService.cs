using LebUpwor.core.Interfaces;
using LebUpwor.core.Models;
using LebUpwork.service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwork.service.Repository
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TagService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<Tag> GetTagById(int Id)
        {
            return await _unitOfWork.Tags.GetTagById(Id);
        }
        public async Task<IEnumerable<Tag>> GetAll()
        {
            return await _unitOfWork.Tags.GetAll();
        }
        public async Task<IEnumerable<Tag>> GetTagsBySimilarName(String name)
        {
            return await _unitOfWork.Tags.GetTagsBySimilarName(name);
        }
        public async Task<Tag> GetTagByName(String name)
        {
            return await _unitOfWork.Tags.GetTagByName(name);
        }
        public async Task<IEnumerable<Tag>> GetTagsByMakerId(int userId)
        {
            return await _unitOfWork.Tags.GetTagsByMakerId(userId);
        }

        public async Task<Tag> CreateTag(Tag tag)
        {
            await _unitOfWork.Tags.AddAsync(tag);
            await _unitOfWork.CommitAsync();
            return tag;
        }

        public async Task DeleteTag(Tag tag)
        {
            _unitOfWork.Tags.Remove(tag);
            await _unitOfWork.CommitAsync();
        }

        public async Task CommitChanges()
        {
            await _unitOfWork.CommitAsync();
        }
    }
}

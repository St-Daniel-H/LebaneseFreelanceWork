using AutoMapper;
using LebUpwor.core.Models;
using LebUpwor.core.Repository;
using LebUpwork.Api.Resources;
using LebUpwork.Api.Resources.Save;
using LebUpwork.Api.Resources.Update;

namespace LebUpwork.Api.Mapping

{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //Domain to source
            CreateMap<User, UserResources>();
            CreateMap<Job, JobResources>();
            CreateMap<Message, MessageResources>();
            CreateMap<CashOutHistory, CashOutHistoryResources>();
            CreateMap<AppliedToTask, AppliedToTaskResources>();
            CreateMap<TokenHistory, TokenHistoryResources>();
            CreateMap<Role, RoleResources>();
            CreateMap<Tag, TagResources>();
            //Source to domain
            CreateMap<UserResources, User>();
          
            CreateMap<JobResources, Job>();
            CreateMap<MessageResources, Message>();
            CreateMap<CashOutHistoryResources, CashOutHistory>();
            CreateMap<AppliedToTaskResources, AppliedToTask>();
            CreateMap<TokenHistoryResources, TokenHistory>();
            CreateMap<RoleResources, Role>();
            CreateMap<TagResources, Tag>();

            CreateMap<UserSignupResources, User>();
            CreateMap<UserLoginResources, User>();

            CreateMap<SaveJobResources, Job>();
            CreateMap<SaveMessageResources, Message>();
            CreateMap<SaveCashOutHistoryResources, CashOutHistory>();
            CreateMap<SaveAppliedToTaskResources, AppliedToTask>();
            CreateMap<SaveTokenHistoryResources, TokenHistory>();
            CreateMap<SaveTagResources, Tag>();

            CreateMap<UpdateUserProfilePicture, User>();
            CreateMap<UpdateUserCV, User>();
            CreateMap<UpdateUserTags, User>();
            CreateMap<UpdateUserTagsResource, Tag>();
            CreateMap<UpdateJobResource, Job>();
        }
    }
}

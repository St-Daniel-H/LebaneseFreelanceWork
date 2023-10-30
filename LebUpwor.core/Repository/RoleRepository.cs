using LebUpwor.core.Interfaces;
using LebUpwor.core.Models;
using startup.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Repository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(UpworkLebContext context)
          : base(context)
        { }
    }

}

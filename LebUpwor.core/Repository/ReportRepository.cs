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
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        public ReportRepository(UpworkLebContext context): base(context)
        { }
        private UpworkLebContext UpworkLebContext
        {
            get { return Context as UpworkLebContext; }
        }
    }
}

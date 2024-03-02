using LebUpwor.core.Models;
using Microsoft.AspNetCore.Mvc;
using startup.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Interfaces
{
    public interface INewJobRepository : IRepositoryRepository<NewJob>
    {
        //public async Task<IActionResult> DeductTokensFromUser(int userId);
    }
}

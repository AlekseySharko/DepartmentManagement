using System;
using System.Threading.Tasks;
using DepartmentManagementModels.OperationResults;
using Microsoft.EntityFrameworkCore;

namespace DepartmentManagementEfCore.Repositories
{
    static class EfRepoHelper
    {
        public static async Task<OperationResult> InvokeManagingExceptions(Func<Task> func, string errorMessage)
        {
            try
            {
                await func();
            }
            catch (DbUpdateException)
            {
                return OperationResult.Failed(errorMessage);
            }

            return OperationResult.Successful();
        }
    }
}

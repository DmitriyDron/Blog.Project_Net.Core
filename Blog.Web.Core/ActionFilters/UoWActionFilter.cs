using Blog.DAL.EntityFramework;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Web.Core.ActionFilters
{
    public class UoWActionFilter : ActionFilterAttribute
    {
        private readonly ApplicationDbContext _dbContext;

        public UoWActionFilter(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var dataModificationRequestTypes = new[] { "post", "put", "delete" };
            if (!dataModificationRequestTypes.Contains(context.HttpContext.Request.Method, StringComparer.InvariantCultureIgnoreCase) ||
                context.Exception != null ||
                !context.ModelState.IsValid)
            {
                return;
            }

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
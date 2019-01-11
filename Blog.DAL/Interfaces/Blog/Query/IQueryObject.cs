using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DAL.Interfaces.Blog.Query
{
    public interface IQueryObject
    {
        string SortBy { get; set; }
        bool IsSortAscending { get; set; }
        int Page { get; set; }
        int PageSize { get; set; }
    }
}

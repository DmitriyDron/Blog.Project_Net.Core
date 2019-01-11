using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.BLL.Utilities.Collections
{
    public interface IPagedList<T>
    {
        int TotalCount { get; set; }
        IList<T> Items { get; set; }
    }
}

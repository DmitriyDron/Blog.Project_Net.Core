using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.BLL.DTO
{
    public class PagedListInput
    {
        public PagedListInput()
        {
            PageIndex = 0;
            PageSize = 10;
        }

        public string Filter { get; set; }

        public string SortBy { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
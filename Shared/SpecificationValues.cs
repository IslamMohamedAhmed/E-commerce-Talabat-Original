using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class SpecificationValues
    {
        private const int DefaultPageIndex = 1;
        private const int DefaultPageSize = 5;
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public sortingValues? sort { get; set; }

        private int pageSize = DefaultPageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > DefaultPageSize ? DefaultPageSize : value; }
        }

        private int pageIndex = DefaultPageIndex;

        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }

        public string? Search { get; set; }


    }

    public enum sortingValues
    {
        priceasc,
        pricedesc,
        nameasc,
        namedesc,
    }
}

using Cot.Web.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Cot.Web.Models
{
    public class ListViewModel<TEntity> where TEntity : IEntity
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public int? PagesCount { get; set; }
        public int? FirstItemOnPage { get; set; }
        public int? LastItemOnPage { get; set; }
        public int? TotalItemsCount { get; set; }
        public string SortField { get; set; }
        public string SortOrder { get; set; }
        public string FilterField { get; set; }
        public string FilterText { get; set; }
        public IPagedList<TEntity> Items { get; set; }
    }
}

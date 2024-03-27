using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOModels
{
    public class ProductFilterRequestDTO
    {
        public string? Name { get; init; } = null;
        public string? GroupName { get; init; } = null;
        public int? GroupID { get; init; } = null;
        public bool IncludeInactive { get; init; } = false;
        public string SortBy { get; init; } = null;
        public bool SortAsc { get; init; } = true; 
    }
}

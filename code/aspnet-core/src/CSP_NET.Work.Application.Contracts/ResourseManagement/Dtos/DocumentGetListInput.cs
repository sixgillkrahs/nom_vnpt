using CTIN.Abp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSP_NET.Work.ResourseManagement.Dtos
{
    public class DocumentGetListInput: PagedAndSortedResultRequestDto
    {
        public string? filter { get; set; }
        public string? UpdateBy { get; set; }
    }
}

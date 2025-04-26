using CTIN.Abp.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.SyncCtin.DTO
{
    public class CompanyInfo
    {
        public CompanyInfo()
        {
            //Contract = new HashSet<Contract>();
            //Department = new HashSet<Department>();
        }

        public int id { get; set; }
        public string companyCode { get; set; }
        public string companyName { get; set; }
        public string companyName_EN { get; set; }
        public string shortName { get; set; }
        public string tradingName { get; set; }
        public int? businessTypeId { get; set; }
        public int? parentId { get; set; }
        public Int64? managerId { get; set; }
        public int? typeId { get; set; }
        public CompanyInfo_DataJson data { get; set; }
        public CompanyInfo_DataContactJson dataContact { get; set; }
        public CompanyInfo_DataBankJson dataBank { get; set; }
        public DataDbJson dataDb { get; set; }

        public class CompanyInfo_DataJson
        {
            public virtual string affiliatedOrganization { get; set; }
            public int? rank { get; set; }
            public string generalManager { get; set; }
            public string director { get; set; }
            public string chiefAccountant { get; set; }
            public string president { get; set; }
            public string partyPresident { get; set; }
            public string unionPresident { get; set; }
            public string youthUnionPresident { get; set; }
            public string certificateNo { get; set; }
            public DateTime? certificateDate { get; set; }
            public string certificatePlace { get; set; }
            public string taxCode { get; set; }
            public string representative { get; set; }
            public string position { get; set; }
            public decimal? regulationCapital { get; set; }
            public decimal? stockPrice { get; set; }
            public int? branchIndex { get; set; }
            public string description { get; set; }
            public int? index { get; set; }

            public bool hasChild { get; set; }

            public string parentTag { get; set; }
        }

        public class CompanyInfo_DataContactJson
        {
            public string phone { get; set; }
            public string email { get; set; }
            public string fax { get; set; }
            public string website { get; set; }
            public string address { get; set; }
            public int? countryId { get; set; }
            public int? provinceId { get; set; }
            public int? districtId { get; set; }
            public int? communeId { get; set; }
        }

        public class CompanyInfo_DataBankJson
        {
            public string bankName { get; set; }
            public string bankCode { get; set; }
            public string branch { get; set; }
            public string bankAddress { get; set; }
            public string accountNo { get; set; }
            public string accountName { get; set; }
        }

        public virtual TypeOfBusiness BusinessType { get; set; }
        //public virtual ICollection<Contract> Contract { get; set; }
        //public virtual ICollection<Department> Department { get; set; }

        public virtual string companyNameUnsigned { get; set; }
        public virtual string shortNameUnsigned { get; set; }
        public virtual string tradingNameUnsigned { get; set; }
        public Guid? TenantId { get; set; }
    }


    public class TypeOfBusiness
    {
        public TypeOfBusiness()
        {
            CompanyInfo = new HashSet<CompanyInfo>();
        }

        public int id { get; set; }
        public TypeOfBusiness_DataJson data { get; set; }
        public DataDbJson dataDb { get; set; }

        public class TypeOfBusiness_DataJson
        {
            public string businessTypeCode { get; set; }
            public string businessTypeName { get; set; }
            public virtual int? orderIndex { get; set; }
            public virtual string description { get; set; }
        }
        public string businessTypeNameUnsigned { get; set; }

        public virtual ICollection<CompanyInfo> CompanyInfo { get; set; }
        public Guid? TenantId { get; set; }
    }
}

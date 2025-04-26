using CTIN.Abp.MultiTenancy;
using Newtonsoft.Json.Linq;
using Scriban.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.SyncCtin.DTO
{
    public class ProjGeneral : IMultiTenant
    {
        public string id { get; set; }
        public long? amProjectId { get; set; }
        public long? pmProjectId { get; set; }
        public int? UserPurchaseId { get; set; }
        public int? TechnicalSupportId { get; set; }
        public int? TechnicalSupportOffice { get; set; }
        public int? idKam { get; set; }
        public int? officePeform { get; set; }
        public long? presaleId { get; set; }
        public long? pmoId { get; set; }
        public long? steerId { get; set; } // chỉ đạo trực tiê[s
        public int? projgroupId { get; set; }
        public int? investorId { get; set; } // khách hàng
        public string projCode { get; set; }
        public string bidder { get; set; }
        public string projName { get; set; }
        public string projShortName { get; set; }
        public string descriptionProject { get; set; }
        public DateTime? expectedBidInvitation { get; set; }
        public int? officeId { get; set; }
        public int? investsId { get; set; }
        public int? projectTypeId { get; set; }
        public int? prodId { get; set; }
        public int? directOpponent { get; set; }
        public string enemy { get; set; }//Đối thủ trực tiếp
        public List<MainProductsJsonModel> mainProducts { get; set; }
        public DateOfJson dateOfPreBidding { get; set; }
        public DateOfJson dateOfBidding { get; set; }
        public DateOfJson dateOfContract { get; set; } //  hợp đồng
        public DateOfJson dateOfNegotiate { get; set; }
        public DateOfJson dateOfDeployment { get; set; }
        public long? profileEmpBiddingId { get; set; }
        public bool isPublic { get; set; } // mặc định là chưa puclic 0
        public bool isRefuse { get; set; } // mặc định là dự án ko hủy bỏ (theo dự án)
        public List<VersionJsonModel> version { get; set; }
        public string createdBy { get; set; }
        public DateTime? createdDate { get; set; }
        public string modifiedBy { get; set; }
        public DateTime? modifiedDate { get; set; }
        public long? collectorFileId { get; set; } // nhân viên đấu thầu hoặc người tổng hợp hồ sơ
        public int? delete { get; set; }
        public ContractingJson contracting { get; set; } // kí kết hợp đồng
        public long? approveId { get; set; } // phê duyệt dự án
        public long? approvePriceContractId { get; set; } // phê duyệt giá
        public string testersId { get; set; } // danh sách người kiểm tra
        public long? leaderGroupId { get; set; } // trưởng nhóm
        public bool isLeaderSeen { get; set; } // trưởng nhóm đã xem
        public int? projPercent { get; set; }

        //public virtual Employees amProjectdata { get; set; }
        //public virtual Employees pmProjectdata { get; set; }
        //public virtual Employees leaderGroup { get; set; }
        //public virtual Employees presaledata { get; set; }
        //public virtual Employees pmodata { get; set; }
        //public virtual Employees approvedata { get; set; }
        //public virtual Employees steerdata { get; set; }
        //public virtual Employees approvePriceContract { get; set; }
        //public virtual ProjectGroup projgroup { get; set; }
        //public virtual Partner partner { get; set; }
        //public virtual Partner directOpponentdata { get; set; }
        //public virtual Office office { get; set; }
        //public virtual Invests invests { get; set; }
        //public virtual ProjectType projectType { get; set; }
        //public virtual ProductServices prod { get; set; }
        //public virtual Approval approval { get; set; }

        
        public long? CloseBy { get; set; }
        public DateTime? CloseDate { get; set; }
        public string CloseDes { get; set; }
        public int? CloseStatus { get; set; }
        public Guid? TenantId { get; set; }
    }

    public class ContractingJson
    {
        public bool isSuccess { get; set; }
        public DateTime signedDate { get; set; }
        public string note { get; set; }
    }

    public class DateOfJson
    {
        public DateTime begin { get; set; }
        public DateTime end { get; set; }
    }

    public class MainProductsJsonModel
    {
        public string mainProduct { get; set; }
        public string prodOnMarket { get; set; }
    }

    public class VersionJsonModel
    {
        //public ProjPhaseEnum? projectPhase { get; set; }
        public int sttVersion { get; set; }
        public string nameVersion { get; set; }
        public bool isDraft { get; set; }
        public DateTime createdDate { get; set; }
        public JObject data { get; set; }
    }
}

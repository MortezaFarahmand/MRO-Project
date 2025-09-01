using _0_Framework.Domain;
using BasicDatanManagement.Domain.ApprovalAutorityAgg;
using System;

namespace BasicDatanManagement.Domain.ApprovalAgg
{
    public class Approval : EntityBase
    {
        public string NameEn { get; private set; }
        public string NameFa { get; private set; }
        public string Description { get; private set; }
        public string Address { get; private set; }
        public string WebSite { get; private set; }
        public string Remark { get; private set; }
        public string CodeNo { get; private set; }
        public string ReferenceNo { get; private set; }
        public string Attachment { get; private set; }
        public DateTime IssueDate { get; private set; }
        public int IssueCount { get; private set; }
        public int IssueInterval { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }
        public string CanonicalAddress { get; private set; }
        public string Keywords { get; private set; }
        public ApprovalAuthority ApprovalAutority { get; private set; }
        public string ApprovedBy { get; private set; }  // public Personnel Personnels { get; private set; }


        public Approval(string nameEn, string nameFa, string description, string address, string webSite,
            string remark, string codeNo, string referenceNo, string attachment, string issueDate , int issueCount,
            int issueInterval, string picture, string pictureAlt, string pictureTitle, string metaDescription,
            string slug, string canonicalAddress, string keywords, string approvedBy)
        {
            NameEn = nameEn;
            NameFa = nameFa;
            Description = description;
            Address = address;
            WebSite = webSite;
            Remark = remark;
            CodeNo = codeNo;
            ReferenceNo = referenceNo;
            Attachment = attachment;
            //IssueDate = issueDate;
            IssueCount = issueCount;
            IssueInterval = issueInterval;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            MetaDescription = metaDescription;
            Slug = slug;
            CanonicalAddress = canonicalAddress;
            Keywords = keywords;
            ApprovedBy = approvedBy;
        }
    }
}

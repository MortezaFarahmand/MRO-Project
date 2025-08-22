
using System;

namespace OrganizationManagement.Application.Contracts.Approval
{
    public class CreateApproval
    {
        public string NameEn { get; set; }
        public string NameFa { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string WebSite { get; set; }
        public string Remark { get; set; }
        public string CodeNo { get; set; }
        public string ReferenceNo { get; set; }
        public string Attachment { get; set; }
        public string IssueDate { get; set; }
        public int IssueCount { get; set; }
        public int IssueInterval { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public string CanonicalAddress { get; set; }
        public string Keywords { get; set; }
    }

    public class EditApproval : CreateApproval
    {
        public long Id { get; set; }
    }

    public class ApprovalViewModel
    {
        public string NameEn { get; set; }
        public string NameFa { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string WebSite { get; set; }
        public string Remark { get; set; }
        public string CodeNo { get; set; }
        public string ReferenceNo { get; set; }
        public string Attachment { get; set; }
        public string IssueDate { get; set; }
        public int IssueCount { get; set; }
        public int IssueInterval { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public string CanonicalAddress { get; set; }
        public string Keywords { get; set; }
    }

    public class ApprovalSearchModel
    {
        public string NameEn { get; set; }
        public string NameFa { get; set; }
        public string CodeNo { get; set; }
        public string ReferenceNo { get; set; }
        public string Description { get; set; }
        public string IssueDate { get; set; }
    }
}

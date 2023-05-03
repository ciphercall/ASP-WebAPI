using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicMenu.Models.Table
{
    public class Notice
    {
        public string CompanyId { get; set; }
        public string NoticeDate { get; set; }
        public string NoticeYear { get; set; }
        public string NoticeSerial { get; set; }
        public string NoticeType { get; set; }
        public string NoticeData { get; set; }
        public string EffectFrom { get; set; }
        public string EffectTo { get; set; }
        public string Status { get; set; }

        // COMPID NUMBER(3),
        // NOTICEDT DATE,
        // NOTICEYY   NUMBER(4),
        // NOTICESL NUMBER(8),	--20160001
        // NOTICETP VARCHAR2(10),	--PARTY/SUPPLIER/EMPLOYEE
        //NOTICE     VARCHAR2(200),
        // EFDT DATE,
        // ETDT       DATE,
        // STATUS CHAR(1)
    }

    public class FeedBack
    {
        public string Token { get; set; }
        public string UserCode { get; set; }
        public string PartyId { get; set; }
        public string ImeiNo { get; set; }
        public string FeedBackType { get; set; }
        public string Reasons { get; set; }
        public string Comments { get; set; }
    }
    public class FeedBackReturn
    {
        public string CompanyId { get; set; }
        public string PartyId { get; set; }
        public string Date { get; set; }
        public string Year { get; set; }
        public string SerialNo { get; set; }
        public string FeedBackType { get; set; }
        public string Reasons { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
    }
}
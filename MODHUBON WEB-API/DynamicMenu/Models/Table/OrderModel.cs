using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicMenu.Models.Table
{
    public class OrderModel
    {
        public string Token { get; set; }
        public string UserCode { get; set; }
        public string PartyId { get; set; }
        public string ImeiNo { get; set; }
        public string BillDate { get; set; }
        public List<ItemQty> ItemQtys { get; set; }
    }

    public class ItemQty
    {
        public string Remarks { get; set; }
        public string ItemId { get; set; }
        public decimal Qty { get; set; }
    }
    public class TransactionList
    {
        public string TransactionDate { get; set; }
        public string DateDb { get; set; }
        public string TransactionNo { get; set; }
        public string UserName { get; set; }
        public string BillDate { get; set; }

    }
    public class TransactionDetails
    {
        public string ItemId { get; set; }
        public string ItemNameEng { get; set; }
        public string ItemNameBan { get; set; }
        public string Unit { get; set; }
        public string ImagePath { get; set; }
        public string Qty { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
        public string Remarks { get; set; }

    }
    public class OrderReturn
    {
        public string Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
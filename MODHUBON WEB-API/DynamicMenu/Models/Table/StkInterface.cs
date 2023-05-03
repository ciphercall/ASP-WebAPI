using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DynamicMenu.Functions;

namespace DynamicMenu.Models.Table
{
    public class StkItemDetails
    {
        public List<ItemInterface> Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class ItemInterface
    {
        public string CategoryId { get; set; }
        public string ItemId { get; set; }
        public string ItemNameEng { get; set; }
        public string ItemNameBan { get; set; }
        public string SaleRate { get; set; }
    }
    public class CategoryInterface
    {
        public string CategoryId { get; set; }
        public string CategoryNameEng { get; set; }
        public string CategoryNameBan { get; set; }
    }

}
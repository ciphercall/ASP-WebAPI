using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DynamicMenu.Models.Table
{
    [Table("STK_ITEM")]
    public class StkItem
    {
        [Key, Column(Order = 0)]
        public Int64 COMPID { get; set; }


        [Key, Column(Order = 1)]
        public Int64 CATID { get; set; }

        [Key, Column(Order = 2)]
        public Int64 ITEMID { get; set; }

        [MaxLength(100)]
        public string ITEMNM { get; set; }

        [MaxLength(300)]
        public string ITEMNMB { get; set; }

        [MaxLength(5)]
        public string MUNIT { get; set; }

        
        public decimal BUYRT { get; set; }
        public decimal SALRT { get; set; }


        [Display(Name = "User PC")]
        public string USERPC { get; set; }

        public Int64? USERID { get; set; }

        [Display(Name = "Insert Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? INTIME { get; set; }

        [Display(Name = "Inesrt IP ADDRESS")]
        public string IPADDRESS { get; set; }

        public string INSLTUDE { get; set; }
        public string UPDUSERPC { get; set; }

        [Display(Name = "Update Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UPDINTIME { get; set; }

        public Int64? UPDUSERID { get; set; }

        [Display(Name = "Update IP ADDRESS")]
        public string UPDIPADDRESS { get; set; }

        public string UPDLTUDE { get; set; }



        //        CREATE TABLE STK_ITEM			--IMAGE REQUIRED
        //(
        // COMPID NUMBER(3),		--101
        // CATID NUMBER(5),		--10101, 10102
        // ITEMID NUMBER(8),		--10101001, 10101002
        // ITEMNM VARCHAR2(100),
        // ITEMNMB VARCHAR2(300),		--UNICODE BANGLA
        // MUNIT VARCHAR2(5),
        // BUYRT NUMBER(15,2),
        // SALRT NUMBER(15,2),
        // ..
        // PRIMARY KEY = COMPID, CATID, ITEMID
        //)
    }
}
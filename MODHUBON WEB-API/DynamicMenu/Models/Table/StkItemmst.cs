using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicMenu.Models.Table
{
    [Table("STK_ITEMMST")]
    public class StkItemmst
    {
        [Key, Column(Order = 0)]
        public Int64 COMPID { get; set; }


        [Key, Column(Order = 1)]
        public Int64 CATID { get; set; }


        public string CATNM { get; set; }

        public string CATNMB { get; set; }


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




        /*
            CREATE TABLE STK_ITEMMST
            (
             COMPID		NUMBER(3),		--101
             CATID		NUMBER(5),		--10101, 10102
             CATNM		VARCHAR2(100),
             CATNMB		VARCHAR2(300),		--UNICODE BANGLA
             ..
             PRIMARY KEY = COMPID, CATID
            )
         */

    }
}
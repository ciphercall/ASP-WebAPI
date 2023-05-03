using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicMenu.Stock.Interface
{
    public class StockInterface
    {
        private string pstp;

        public string Pstp
        {
            get { return pstp; }
            set { pstp = value; }
        }

        private string pscd;

        public string Pscd
        {
            get { return pscd; }
            set { pscd = value; }
        }

        private string city;

        public string City
        {
            get { return city; }
            set { city = value; }
        }
        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        private string contactno;

        public string Contactno
        {
            get { return contactno; }
            set { contactno = value; }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private string webid;

        public string Webid
        {
            get { return webid; }
            set { webid = value; }
        }
        private string cpnm;

        public string Cpnm
        {
            get { return cpnm; }
            set { cpnm = value; }
        }
        private string cpno;

        public string Cpno
        {
            get { return cpno; }
            set { cpno = value; }
        }
        private string remarks;

        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }
        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        //start my new source
        private string invoiceDtNo;

        public string InvoiceDtNo
        {
            get { return invoiceDtNo; }
            set { invoiceDtNo = value; }
        }
        private string invoicePinNo;

        public string InvoicePinNo
        {
            get { return invoicePinNo; }
            set { invoicePinNo = value; }
        }
        private string editNo;

        public string EditNo
        {
            get { return editNo; }
            set { editNo = value; }
        }

        private string to;

        public string To
        {
            get { return to; }
            set { to = value; }
        }
        private string toId;

        public string ToId
        {
            get { return toId; }
            set { toId = value; }
        }
        private string lcTp;

        public string LcTp
        {
            get { return lcTp; }
            set { lcTp = value; }
        }
        private string lcId;

        public string LcId
        {
            get { return lcId; }
            set { lcId = value; }
        }
        private DateTime lcDt;


        public DateTime LcDt
        {
            get { return lcDt; }
            set { lcDt = value; }
        }

        public DateTime TranDt  
        {
            get { return lcDt; }
            set { lcDt = value; }
        }

        private string psId;

        public string PsId
        {
            get { return psId; }
            set { psId = value; }
        }



        private Int64 transNo;

        public Int64 TransNo
        {
            get { return transNo; }
            set { transNo = value; }
        }
        private string monthYear;

        public string MonthYear
        {
            get { return monthYear; }
            set { monthYear = value; }
        }
        private DateTime trDt;

        public DateTime TrDt
        {
            get { return trDt; }
            set { trDt = value; }
        }


        public DateTime OrderDT { get; set; }
       

        private string invRefNo;

        public string InvRefNo
        {
            get { return invRefNo; }
            set { invRefNo = value; }
        }
        private string transTP;

        public string TransTP
        {
            get { return transTP; }
            set { transTP = value; }
        }
        private string storeFR;

        public string StoreFR
        {
            get { return storeFR; }
            set { storeFR = value; }
        }
        private string transSL;

        public string TransSL
        {
            get { return transSL; }
            set { transSL = value; }
        }
        private string catID;

        public string CatID
        {
            get { return catID; }
            set { catID = value; }
        }
        private string itemID;

        public string ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        private string unitTP;

        public string UnitTP
        {
            get { return unitTP; }
            set { unitTP = value; }
        }
        private Decimal cpQTY;

        public Decimal CpQTY
        {
            get { return cpQTY; }
            set { cpQTY = value; }
        }
        private Decimal cQTY;

        public Decimal CQTY
        {
            get { return cQTY; }
            set { cQTY = value; }
        }
        private Decimal pQTY;

        public Decimal PQTY
        {
            get { return pQTY; }
            set { pQTY = value; }
        }
        private Decimal qty;

        public Decimal Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        public decimal Orderqty { get; set; }

        private Decimal rate;

        public Decimal Rate
        {
            get { return rate; }
            set { rate = value; }
        }
        private Decimal amount;

        public Decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        private string userPC;

        public string UserPC
        {
            get { return userPC; }
            set { userPC = value; }
        }
        private string userID;

        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        private string ipaddress;

        public string Ipaddress
        {
            get { return ipaddress; }
            set { ipaddress = value; }
        }
        private DateTime iTime;

        public DateTime ITime
        {
            get { return iTime; }
            set { iTime = value; }
        }
        private DateTime updTime;

        public DateTime UpdTime
        {
            get { return updTime; }
            set { updTime = value; }
        }



        public string UPDUserPC { get; set; }

        public string UPDIpaddress { get; set; }

        public decimal disCountRT { get; set; }


        public decimal NetAmt { get; set; }

        public decimal TotAmnt { get; set; }
        public decimal TpCostAmt { get; set; }
        public decimal TotDedAmt { get; set; }
        public int ItemSerial { get; set; }

        public string vehicalsno { get; set; }
        public string driveerNm { get; set; }
        public string asstNm { get; set; }
        public string rettp { get; set; }


        public decimal RetRt { get; set; }

        public decimal netRt { get; set; }
        public decimal NetAmnt { get; set; }




        public string MstID { get; set; }

        public string MstNM { get; set; }

        public string UpdUserID { get; set; }

        public string SubNM { get; set; }

        public string SubID { get; set; }

        public string ItemCD { get; set; }

        public string ItemNM { get; set; }

        public string Brand { get; set; }

        public string BuyRT { get; set; }

        public string SaleRT { get; set; }

        public string MinStkQty { get; set; }

        public DateTime Efdt { get; set; }

        public DateTime Etdt { get; set; }





        private string uPDUserID;

        public string UPDUserID
        {
            get { return uPDUserID; }
            set { uPDUserID = value; }
        }



        private string cardHTP;

        public string CardHTP
        {
            get { return cardHTP; }
            set { cardHTP = value; }
        }

        private DateTime dOfBirth;

        public DateTime DOfBirth
        {
            get { return dOfBirth; }
            set { dOfBirth = value; }
        }

        private string cardHID;

        public string CardHID
        {
            get { return cardHID; }
            set { cardHID = value; }
        }

        private string cardHNM;

        public string CardHNM
        {
            get { return cardHNM; }
            set { cardHNM = value; }
        }

        private string fatherNM;

        public string FatherNM
        {
            get { return fatherNM; }
            set { fatherNM = value; }
        }

        private string motherNM;

        public string MotherNM
        {
            get { return motherNM; }
            set { motherNM = value; }
        }

        private string spouseNM;

        public string SpouseNM
        {
            get { return spouseNM; }
            set { spouseNM = value; }
        }

        private string prsntAdd;

        public string PrsntAdd
        {
            get { return prsntAdd; }
            set { prsntAdd = value; }
        }

        private string permntAdd;

        public string PermntAdd
        {
            get { return permntAdd; }
            set { permntAdd = value; }
        }

        private string telNo;

        public string TelNo
        {
            get { return telNo; }
            set { telNo = value; }
        }

        private string mobNo;

        public string MobNo
        {
            get { return mobNo; }
            set { mobNo = value; }
        }

        private string votrID;

        public string VotrID
        {
            get { return votrID; }
            set { votrID = value; }
        }

        private string emailID;

        public string EmailID
        {
            get { return emailID; }
            set { emailID = value; }
        }

        private string offNM;

        public string OffNM
        {
            get { return offNM; }
            set { offNM = value; }
        }

        private string offAdd;

        public string OffAdd
        {
            get { return offAdd; }
            set { offAdd = value; }
        }

        private string offTelNo;

        public string OffTelNo
        {
            get { return offTelNo; }
            set { offTelNo = value; }
        }

        private string offMblNo;

        public string OffMblNo
        {
            get { return offMblNo; }
            set { offMblNo = value; }
        }

        private string designation;

        public string Designation
        {
            get { return designation; }
            set { designation = value; }
        }

        private string incmPortl;

        public string IncmPortl
        {
            get { return incmPortl; }
            set { incmPortl = value; }
        }

        private decimal annualIncm;

        public decimal AnnualIncm
        {
            get { return annualIncm; }
            set { annualIncm = value; }
        }

        private string rfrCrdHID;

        public string RfrCrdHID
        {
            get { return rfrCrdHID; }
            set { rfrCrdHID = value; }
        }

        private string rfrCrdHNM;

        public string RfrCrdHNM
        {
            get { return rfrCrdHNM; }
            set { rfrCrdHNM = value; }
        }

        private string comments;

        public string Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        private DateTime submtDT;

        public DateTime SubmtDT
        {
            get { return submtDT; }
            set { submtDT = value; }
        }

        private DateTime issuDT;

        public DateTime IssuDT
        {
            get { return issuDT; }
            set { issuDT = value; }
        }

        private string issuFrm;

        public string IssuFrm
        {
            get { return issuFrm; }
            set { issuFrm = value; }
        }

        private string issuTO;

        public string IssuTO
        {
            get { return issuTO; }
            set { issuTO = value; }
        }

        private string rFrom;

        public string RFrom
        {
            get { return rFrom; }
            set { rFrom = value; }
        }

        //sale transaction//


        private string pcardHTP;

        public string PcardHTP
        {
            get { return pcardHTP; }
            set { pcardHTP = value; }
        }

        private string itemTP;

        public string ItemTP
        {
            get { return itemTP; }
            set { itemTP = value; }
        }

        private string discRT;

        public string DiscRT
        {
            get { return discRT; }
            set { discRT = value; }
        }

        private string discGP;

        public string DiscGP
        {
            get { return discGP; }
            set { discGP = value; }
        }

        private string discDP;

        public string DiscDP
        {
            get { return discDP; }
            set { discDP = value; }
        }

        public string TransNo2 { get; set; }

        public string RefferenceNo { get; set; }
        public string RefferenceType { get; set; }
        public string TranMode { get; set; }
        public string ItemFG { get; set; }
        public decimal ProductionQty { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }
        public string PublicationName { get; set; }
        public string ReturnType { get; set; }
        public decimal CommisionRate { get; set; }

        public object disAmount { get; set; }
        public string SubNMBan { get; set; }
        public string CompanyId { get; set; }
        public string ItemNMBangla { get; set; }
        public string ImagePath { get; set; }
        public string ApContactNo { get; set; }
        public string MobNo2 { get; set; }
        public string AddressBangla { get; set; }
        public string AddressEnglish { get; set; }
        public string PartyNameBangla { get; set; }
        public string PartyNameEnglish { get; set; }
        public string PartyId { get; set; }
        public string UserCode { get; set; }
        public string Password { get; set; }
        public string LoginId { get; set; }
        public DateTime BillDate { get; set; }
        public string PartySerial { get; set; }
        public string RemarksDetails { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string NoticeId { get; set; }
        public string NoticeType { get; set; }
        public string Notice { get; set; }
        public DateTime EffectFromDate { get; set; }
        public DateTime EffectToDate { get; set; }
        public string NoticeYear { get; set; }
    }
}
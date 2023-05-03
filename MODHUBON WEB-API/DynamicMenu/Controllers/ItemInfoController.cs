using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AlchemyAccounting;
using DynamicMenu.Functions;
using DynamicMenu.Models.Table;

namespace DynamicMenu.Controllers
{
    public class ItemInfoController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/ItemInfo/GetAllCategoryDetails")]
        public object GetAllCategoryDetails(string usercode, string token)
        {
            List<CategoryInterface> nulllist = new List<CategoryInterface>();

            if (Token.TokenCheck(usercode, token))
            {
                List<CategoryInterface> list = new List<CategoryInterface>();
                SqlConnection con = new SqlConnection(dbFunctions.connection);
                SqlCommand cmd = new SqlCommand("SELECT CATID, CATNM, CATNMB FROM STK_ITEMMST", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new CategoryInterface
                    {
                        CategoryId = dr[0].ToString(),
                        CategoryNameEng = dr[1].ToString(),
                        CategoryNameBan = dr[2].ToString()
                    });
                }
                dr.Close();
                con.Close();
                if (list.Count > 0)
                    return new
                    {
                        Data = list,
                        Success = true,
                        Message = ""
                    };
                else return new
                {
                    Data = nulllist,
                    Success = false,
                    Message = "No Data Found."
                };
            }
            return new
            {
                Data = nulllist,
                Success = false,
                Message = "Authorized not permitted."
            };
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/ItemInfo/GetAllItemDetails")]
        public object GetAllItemDetails(string usercode, string token)
        {
            List<CategoryInterface> nulllist = new List<CategoryInterface>();
            if (Token.TokenCheck(usercode, token))
            {
                List<ItemInterface> list = new List<ItemInterface>();
                SqlConnection con = new SqlConnection(dbFunctions.connection);
                SqlCommand cmd =
                    new SqlCommand(@"SELECT STK_ITEMMST.CATID, STK_ITEM.ITEMID, STK_ITEM.ITEMNM, STK_ITEM.ITEMNMB, 
            STK_ITEM.SALRT            FROM STK_ITEM INNER JOIN 
            STK_ITEMMST ON STK_ITEM.COMPID = STK_ITEMMST.COMPID AND STK_ITEM.CATID = STK_ITEMMST.CATID", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new ItemInterface
                    {
                        CategoryId = dr[0].ToString(),
                        ItemId = dr[1].ToString(),
                        ItemNameEng = dr[2].ToString(),
                        ItemNameBan = dr[3].ToString(),
                        SaleRate = dr[4].ToString()
                    });
                }
                dr.Close();
                con.Close();

                return new
                {
                    Data = list,
                    Success = true,
                    Message = ""
                };
            }
            return new
            {
                Data = nulllist,
                Success = false,
                Message = "Authorized not permitted."
            };
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ItemInfo/GetAllItemDetailsCategoryWise")]
        public object GetAllItemDetailsCategoryWise(string usercode, string token, string categoryid)
        {
            List<ItemInterface> nulllist = new List<ItemInterface>();
            if (Token.TokenCheck(usercode, token))
            {
                List<ItemInterface> list = new List<ItemInterface>();
                SqlConnection con = new SqlConnection(dbFunctions.connection);
                SqlCommand cmd =
                    new SqlCommand(@"SELECT STK_ITEMMST.CATID, STK_ITEM.ITEMID, STK_ITEM.ITEMNM, STK_ITEM.ITEMNMB, 
            STK_ITEM.SALRT            FROM STK_ITEM INNER JOIN 
            STK_ITEMMST ON STK_ITEM.COMPID = STK_ITEMMST.COMPID AND STK_ITEM.CATID = STK_ITEMMST.CATID
            WHERE STK_ITEMMST.CATID='" + categoryid + "'", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new ItemInterface
                    {
                        CategoryId = dr[0].ToString(),
                        ItemId = dr[1].ToString(),
                        ItemNameEng = dr[2].ToString(),
                        ItemNameBan = dr[3].ToString(),
                        SaleRate = dr[4].ToString()
                    });
                }
                dr.Close();
                con.Close();
                if (list.Count > 0)
                    return new
                    {
                        Data = list,
                        Success = true,
                        Message = ""
                    };
                else
                    return new
                    {
                        Data = nulllist,
                        Success = false,
                        Message = "item not found in this category."
                    };
            }
            return new
            {
                Data = nulllist,
                Success = false,
                Message = "Authorized not permitted."
            };
        }
    }
}

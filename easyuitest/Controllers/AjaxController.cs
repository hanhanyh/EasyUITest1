using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using easyuitest.Models;
using Oracle.DataAccess.Client;
using System.Data;
namespace easyuitest.Controllers
{
    public class AjaxController : Controller
    {
        private string connstr = "Data Source=127.0.0.0.1/orcl;User ID=test1;Password=123456";
        public ActionResult combox()
        {
            //List<SelectModel> lModels = new List<SelectModel>();
            //lModels.Add(new SelectModel() { id = 1, name = "name" });
            List<articletype> articletypes = getAllArticleType();
            return Json(articletypes, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Ajax获取所有文章
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxGetArticleList(int? page = null, string keyword = null, string typeid = null, string rows = null)
        {
            List<article> articles = GetAllArticles(page,keyword,typeid,rows);
            return Json(new { total = articles.Count, rows = articles }, JsonRequestBehavior.AllowGet);
        }
        #region 私有方法
        /// <summary>
        /// 获取所有文章类型返回List
        /// </summary>
        /// <returns></returns>
        private List<articletype> getAllArticleType()
        {
            List<articletype> ltype = new List<articletype>();
            using (OracleConnection conn=new OracleConnection(connstr))
            {
                conn.Open();
                DataSet ds = new DataSet();
                OracleDataAdapter adapter = new OracleDataAdapter("select * from articletype s order by s.id desc", conn);
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ltype.Add(new articletype() {
                        id = dt.Rows[i]["id"].ToString(),
                        name=dt.Rows[i]["name"].ToString()
                    });
                }
                return ltype;
            }
        }
        /// <summary>
        /// 获取所有Article
        /// </summary>
        /// <returns></returns>
         
        private List<article> GetAllArticles(int ? page=null,string  keyword=null,string typeid=null,string rows=null)
        {
            List<article> articles = new List<article>();
            using (OracleConnection conn=new OracleConnection(connstr))
            {
                conn.Open();
                DataSet ds = new DataSet();
                OracleDataAdapter dataAdapter = null;
                if (typeid == null)
                    dataAdapter = new OracleDataAdapter("select * from article a order by a.id desc", conn);
                else if (typeid != null && keyword.Trim() == "")
                {
                    dataAdapter = new OracleDataAdapter("select * from article a  where a.articletypeid=" + typeid + " order by a.id desc", conn);
                }
                else if (typeid != null && keyword.Trim() != "")
                {
                    dataAdapter = new OracleDataAdapter(" select * from article a  where a.articletypeid = "+typeid+" and a.name like '%"+keyword+"%' order by a.id desc", conn);
                }
                dataAdapter.Fill(ds);
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr  in dt.Rows)
                {
                    articles.Add(new article() {
                        id=dr["id"].ToString(),
                        name=dr["name"].ToString(),
                        detail=dr["detail"].ToString(),
                        articletypeid=dr["articletypeid"].ToString()
                    });
                }

                return articles;
            }
        }
        #endregion
    }
}
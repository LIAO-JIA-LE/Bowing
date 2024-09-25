using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data.SqlClient;
using s1411038021_NetFinal.Models;

namespace s1411038021_NetFinal.Services
{
    public class CarNewsDBService
    {
        private readonly static string cnstr = ConfigurationManager.ConnectionStrings["Bowling"].ConnectionString;
        private readonly SqlConnection conn = new SqlConnection(cnstr);
        public List<Carousel> GetCarList()
        {
            List<Carousel> CarList = new List<Carousel>();
            string sql = $@"SELECT * FROM Carousel;";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Carousel Data = new Carousel();
                    Data.cid = Convert.ToInt32(dr["cid"]);
                    Data.ctitle = dr["ctitle"].ToString();
                    Data.cfile = dr["cfile"].ToString();
                    CarList.Add(Data);
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return CarList;
        }

        internal void DeleteNew(int nid)
        {
            string sql = $@"DELETE FROM News WHERE nid = {nid}";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public News GetNewByid(string nid)
        {
            string sql = $@"SELECT * FROM News WHERE nid = '{nid}'";
            News Data = new News();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Data.nid = dr["nid"].ToString();
                Data.ntitle = dr["ntitle"].ToString();
                Data.ntime = Convert.ToDateTime(dr["ntime"]);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return Data;
        }
        public void EditNew(News UpdateData)
        {
            string sql = $@"UPDATE News SET ntitle = '{UpdateData.ntitle}',ntime = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE nid = '{UpdateData.nid}'";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        public List<News> GetNewsList()
        {
            List<News> NewsList = new List<News>();
            string sql = $@"SELECT * FROM News ORDER BY ntime DESC;";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    News Data = new News();
                    Data.nid = dr["nid"].ToString();
                    Data.ntitle = dr["ntitle"].ToString();
                    Data.ntime = Convert.ToDateTime(dr["ntime"]);
                    NewsList.Add(Data);
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return NewsList;
        }
        public void InsertNews(News newData)
        {
            //sql新增語法
            //設定新增時間為現在
            string sql = $@"
                            INSERT INTO News(ntitle,ntime)
                            VALUES('{newData.ntitle}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')";
            //確保城市不因錯誤而中斷
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
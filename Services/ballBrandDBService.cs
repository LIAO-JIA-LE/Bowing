using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data.SqlClient;
using s1411038021_NetFinal.ViewModels;
using s1411038021_NetFinal.Models;

namespace s1411038021_NetFinal.Services
{
    public class ballBrandDBService
    {
        private readonly static string cnstr = ConfigurationManager.ConnectionStrings["Bowling"].ConnectionString;
        private readonly SqlConnection conn = new SqlConnection(cnstr);
        public ballBrandViewModels GetAllBall(int brandid,int flareid,ForPaging Paging,string Search)
        {
            ballBrandViewModels AllBallList = new ballBrandViewModels();
            string sql = string.Empty;
            if (!string.IsNullOrWhiteSpace(Search))
            {
                AllBallList.Paging = SetMaxPage(brandid, flareid, Paging, Search);
                AllBallList.ballList = GetballList(brandid,flareid,Paging,Search);
            }
            else
            {
                AllBallList.Paging = SetMaxPage(brandid, flareid, Paging);
                AllBallList.ballList = GetballList(brandid, flareid, Paging);
            }
            return AllBallList;
        }

        private ForPaging SetMaxPage(int brandid, int flareid, ForPaging Paging, string Search)
        {
            string sql = string.Empty;
            int row = 0;
            if (brandid != 0 && flareid == 0)
            {
                sql = $@"SELECT * FROM Bowlingballs WHERE Brandid = {brandid} AND bname LIKE '%{Search}%';";
            }
            else if (brandid != 0 && flareid != 0)
            {
                sql = $@"SELECT * FROM Bowlingballs WHERE Brandid = {brandid} AND Flareid = {flareid}  AND bname LIKE '%{Search}%';";
            }
            else if (brandid == 0 && flareid != 0)
            {
                sql = $@"SELECT * FROM Bowlingballs WHERE Flareid = {flareid}  AND bname LIKE '%{Search}%';";
            }
            else
            {
                sql = $@"SELECT * FROM Bowlingballs WHERE bname LIKE '%{Search}%';";
            }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    row++;
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
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(row) / Paging.ItemNum));
            Paging.SetRightPage();
            return Paging;
        }

        private ForPaging SetMaxPage(int brandid, int flareid, ForPaging Paging)
        {
            string sql = string.Empty;
            int row = 0;
            if (brandid != 0 && flareid == 0)
            {
                sql = $@"SELECT * FROM Bowlingballs WHERE Brandid = {brandid};";
            }
            else if (brandid != 0 && flareid != 0)
            {
                sql = $@"SELECT * FROM Bowlingballs WHERE Brandid = {brandid} AND Flareid = {flareid};";
            }
            else if (brandid == 0 && flareid != 0)
            {
                sql = $@"SELECT * FROM Bowlingballs WHERE Flareid = {flareid};";
            }
            else
            {
                sql = $@"SELECT * FROM Bowlingballs;";
            }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    row++;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(row) / Paging.ItemNum));
            Paging.SetRightPage();
            return Paging;
        }
        public List<Bowlingballs> GetballList(int brandid, int flareid,ForPaging Paging,string Search)
        {
            string sql = "";
            List<Bowlingballs> ballList = new List<Bowlingballs>();
            if (brandid != 0 && flareid == 0)
            {
                sql = $@"SELECT * FROM(SELECT ROW_NUMBER() OVER(ORDER BY bprise DESC) as sort_id, * FROM Bowlingballs WHERE Brandid = {brandid} AND bname LIKE '%{Search}%')m WHERE m.sort_id BETWEEN {(Paging.NowPage - 1) * Paging.ItemNum + 1} AND {Paging.NowPage * Paging.ItemNum}";
            }
            else if (brandid != 0 && flareid != 0)
            {
                sql = $@"SELECT * FROM(SELECT ROW_NUMBER() OVER(ORDER BY bprise DESC) as sort_id, * FROM Bowlingballs WHERE Brandid = {brandid} AND Flareid = {flareid} AND bname LIKE '%{Search}%')m WHERE m.sort_id BETWEEN {(Paging.NowPage - 1) * Paging.ItemNum + 1} AND {Paging.NowPage * Paging.ItemNum}";
            }
            else if (brandid == 0 && flareid != 0)
            {
                sql = $@"SELECT * FROM(SELECT ROW_NUMBER() OVER(ORDER BY bprise DESC) as sort_id, * FROM Bowlingballs WHERE Flareid = {flareid} AND bname LIKE '%{Search}%')m WHERE m.sort_id BETWEEN {(Paging.NowPage - 1) * Paging.ItemNum + 1} AND {Paging.NowPage * Paging.ItemNum}";
            }
            else
            {
                sql = $@"SELECT * FROM(SELECT ROW_NUMBER() OVER(ORDER BY bprise DESC) as sort_id, * FROM Bowlingballs WHERE bname LIKE '%{Search}%')m WHERE m.sort_id BETWEEN {(Paging.NowPage - 1) * Paging.ItemNum + 1} AND {Paging.NowPage * Paging.ItemNum}";
            }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Bowlingballs Data = new Bowlingballs();
                    Data.bid = Convert.ToInt32(dr["bid"]);
                    Data.bprise = Convert.ToInt32(dr["bprise"]);
                    Data.bname = dr["bname"].ToString();
                    Data.bfile = dr["bfile"].ToString();
                    if (!dr["bcontent"].Equals(DBNull.Value))
                    {
                        Data.bcontent = dr["bcontent"].ToString();
                    }
                    Data.btime = Convert.ToDateTime(dr["btime"]);
                    Data.Brandid = Convert.ToInt32(dr["Brandid"]);
                    Data.Flareid = Convert.ToInt32(dr["Flareid"]);
                    ballList.Add(Data);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return ballList;
        }
        public List<Bowlingballs> GetballList(int brandid,int flareid,ForPaging Paging)
        {
            string sql = "";
            List<Bowlingballs> ballList = new List<Bowlingballs>();
            if (brandid != 0 && flareid == 0)
            {
                sql = $@"SELECT * FROM(SELECT ROW_NUMBER() OVER(ORDER BY bprise DESC) as sort_id, * FROM Bowlingballs WHERE Brandid = {brandid})m WHERE m.sort_id BETWEEN {(Paging.NowPage - 1) * Paging.ItemNum + 1} AND {Paging.NowPage * Paging.ItemNum}";
            }
            else if (brandid != 0 && flareid != 0)
            {
                sql = $@"SELECT * FROM(SELECT ROW_NUMBER() OVER(ORDER BY bprise DESC) as sort_id, * FROM Bowlingballs WHERE Brandid = {brandid} AND Flareid = {flareid})m WHERE m.sort_id BETWEEN {(Paging.NowPage - 1) * Paging.ItemNum + 1} AND {Paging.NowPage * Paging.ItemNum}";
            }
            else if (brandid == 0 && flareid != 0)
            {
                sql = $@"SELECT * FROM(SELECT ROW_NUMBER() OVER(ORDER BY bprise DESC) as sort_id, * FROM Bowlingballs WHERE Flareid = {flareid})m WHERE m.sort_id BETWEEN {(Paging.NowPage - 1) * Paging.ItemNum + 1} AND {Paging.NowPage * Paging.ItemNum}";
            }
            else
            {
                sql = $@"SELECT * FROM(SELECT ROW_NUMBER() OVER(ORDER BY bprise DESC) as sort_id, * FROM Bowlingballs)m WHERE m.sort_id BETWEEN {(Paging.NowPage - 1) * Paging.ItemNum + 1} AND {Paging.NowPage * Paging.ItemNum}";
            }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Bowlingballs Data = new Bowlingballs();
                    Data.bid = Convert.ToInt32(dr["bid"]);
                    Data.bprise = Convert.ToInt32(dr["bprise"]);
                    Data.bname = dr["bname"].ToString();
                    Data.bfile = dr["bfile"].ToString();
                    if (!dr["bcontent"].Equals(DBNull.Value))
                    {
                        Data.bcontent = dr["bcontent"].ToString();
                    }
                    Data.btime = Convert.ToDateTime(dr["btime"]);
                    Data.Brandid = Convert.ToInt32(dr["Brandid"]);
                    Data.Flareid = Convert.ToInt32(dr["Flareid"]);
                    ballList.Add(Data);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return ballList;
        }
        internal Flare GetFlareById(int flareid)
        {
            string sql = $@"SELECT * FROM Flare WHERE Flareid = {flareid};";
            Flare Data = new Flare();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Data.Flareid = Convert.ToInt32(dr["Flareid"]);
                Data.FlareLevel = dr["FlareLevel"].ToString();
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

        internal Brand GetBrandById(int brandid)
        {
            string sql = $@"SELECT * FROM Brand WHERE Brandid = {brandid};";
            Brand Data = new Brand();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Data.Brandid = Convert.ToInt32(dr["Brandid"]);
                Data.Brandname = dr["Brandname"].ToString();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return Data;
        }

        internal Bowlingballs GetBallByBid(int bid)
        {
            string sql = $@"SELECT * FROM Bowlingballs WHERE bid = {bid}";
            Bowlingballs Data = new Bowlingballs();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Data.bid = Convert.ToInt32(dr["bid"]);
                Data.bprise = Convert.ToInt32(dr["bprise"]);
                Data.bname = dr["bname"].ToString();
                Data.bfile = dr["bfile"].ToString();
                if (!dr["bcontent"].Equals(DBNull.Value))
                {
                    Data.bcontent = dr["bcontent"].ToString();
                }
                Data.btime = Convert.ToDateTime(dr["btime"]);
                Data.Brandid = Convert.ToInt32(dr["Brandid"]);
                Data.Flareid = Convert.ToInt32(dr["Flareid"]);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return Data;
        }
        public List<Flare> GetFlareList()
        {
            List<Flare> FlareList = new List<Flare>();
            string sql = $@"SELECT * FROM Flare;";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Flare Data = new Flare();
                    Data.Flareid = Convert.ToInt32(dr["Flareid"]);
                    Data.FlareLevel = dr["FlareLevel"].ToString();
                    FlareList.Add(Data);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return FlareList;
        }
        public List<Brand> GetBrandList()
        {
            List<Brand> BrandList = new List<Brand>();
            string sql = $@"SELECT * FROM Brand;";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Brand Data = new Brand();
                    Data.Brandid = Convert.ToInt32(dr["Brandid"]);
                    Data.Brandname = dr["Brandname"].ToString();
                    BrandList.Add(Data);
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
            return BrandList;
        }
        public void UpdateBall(Bowlingballs UpdateData)
        {
            string sql = string.Empty;
            if(UpdateData.bfile == null)
            {
                sql = $@"UPDATE Bowlingballs
                                SET bname = '{UpdateData.bname}',bprise = {UpdateData.bprise} 
                                , bcontent = '{UpdateData.bcontent}' , Brandid = {UpdateData.Brandid}
                                , Flareid = {UpdateData.Flareid} WHERE bid = {UpdateData.bid}";
            }
            else
            {
                sql = $@"UPDATE Bowlingballs
                                SET bname = '{UpdateData.bname}',bprise = {UpdateData.bprise} 
                                , bcontent = '{UpdateData.bcontent}',bfile = '{UpdateData.bfile}' , Brandid = {UpdateData.Brandid}
                                , Flareid = {UpdateData.Flareid} WHERE bid = {UpdateData.bid}";
            }
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
        public void CreateBall(Bowlingballs NewData)
        {
            string sql = $@"INSERT INTO Bowlingballs(bname,bprise,bfile,bcontent,btime,Brandid,Flareid)
                            VALUES('{NewData.bname}',{NewData.bprise},'{NewData.bfile}','{NewData.bcontent}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                            ,{NewData.Brandid},{NewData.Flareid})";
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
        public void Deleteball(int bid)
        {
            string sql = $@"DELETE FROM Bowlingballs WHERE bid = {bid}";
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
        public void CreateBrand(Brand NewData)
        {
            string sql = $@"INSERT INTO Brand(Brandid,BrandName) VALUES({NewData.Brandid},'{NewData.Brandname}')";
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
        public void UpdateBrand(Brand UpData)
        {
            string sql = $@"UPDATE Brand SET BrandName = '{UpData.Brandname}' WHERE Brandid = {UpData.Brandid}";
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
        public void DeleteBrand(int Brandid)
        {
            string sql = $@"DELETE FROM Brand WHERE Brandid={Brandid}";
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
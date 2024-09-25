using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data.SqlClient;
using s1411038021_NetFinal.Models;
using s1411038021_NetFinal.ViewModels;
using s1411038021_NetFinal.Services;

namespace s1411038021_NetFinal.Services
{
    public class CartDBService
    {
        private readonly static string cnstr = ConfigurationManager.ConnectionStrings["Bowling"].ConnectionString;
        private readonly SqlConnection conn = new SqlConnection(cnstr);
        public List<Cart> GetAllCart()
        {
            string sql = $@"SELECT c.*,b.bname,b.bprise,b.bfile,b.Brandname,b.FlareLevel,b.Flareid,b.Brandid
                            FROM Cart c 
                            LEFT JOIN (
　                            SELECT bo.bid,bo.bname,bo.bprise,bo.bfile,bo.FlareLevel,br.Brandname,bo.Flareid,bo.Brandid
                              FROM(
	                            SELECT B.*,F.FlareLevel 
	                            FROM Bowlingballs B
	                            JOIN Flare F
	                            ON B.Flareid = f.Flareid
                              ) bo
                              JOIN Brand br
                              on bo.Brandid = br.Brandid
                            ) b
                            ON c.bid = b.bid";
            List<Cart> DataList = new List<Cart>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    Cart Data = new Cart();
                    Data.CartId = Convert.ToInt32(dr["CartId"]);
                    Data.bid = Convert.ToInt32(dr["bid"]);
                    Data.amount = Convert.ToInt32(dr["amount"]);
                    Data.bprise = Convert.ToInt32(dr["bprise"]);
                    Data.bname = dr["bname"].ToString();
                    Data.bfile = dr["bfile"].ToString();
                    Data.BrandName = dr["BrandName"].ToString();
                    Data.Brandid = Convert.ToInt32(dr["Brandid"]);
                    Data.FlareLevel = dr["FlareLevel"].ToString();
                    Data.Flareid = Convert.ToInt32(dr["Flareid"]);
                    DataList.Add(Data);
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
            return DataList;
        }

        internal int GetSum()
        {
            string sql = $@"SELECT SUM(b.bprise*c.amount)AS Total_Prise FROM Cart C JOIN Bowlingballs B ON C.bid=B.bid ";
            int sum = 0;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                if (!dr["Total_Prise"].Equals(DBNull.Value))
                {
                    sum = Convert.ToInt32(dr["Total_Prise"]);
                }
                else
                {
                    sum = 0;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            return sum;
        }

        public void Delete(int cartId)
        {
            string sql = $@"DELETE FROM Cart WHERE CartId = {cartId}";
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

        public void InsertCart(int bid)
        {
            string sql = $@"INSERT INTO Cart(bid,amount) VALUES({bid},1)";
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
        public Cart GetCartByCartId(int CartId)
        {
            string sql = $@"SELECT c.*,b.bname,b.bprise,b.bfile,b.Brandname,b.FlareLevel,b.Flareid,b.Brandid
                            FROM Cart c 
                            LEFT JOIN (
　                            SELECT bo.bid,bo.bname,bo.bprise,bo.bfile,bo.FlareLevel,br.Brandname,bo.Flareid,bo.Brandid
                              FROM(
	                            SELECT B.*,F.FlareLevel 
	                            FROM Bowlingballs B
	                            JOIN Flare F
	                            ON B.Flareid = f.Flareid
                              ) bo
                              JOIN Brand br
                              on bo.Brandid = br.Brandid
                            ) b
                            ON c.bid = b.bid
                            WHERE CartId={CartId}";
            Cart Data = new Cart();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Data.CartId = Convert.ToInt32(dr["CartId"]);
                Data.bid = Convert.ToInt32(dr["bid"]);
                Data.amount = Convert.ToInt32(dr["amount"]);
                Data.bprise = Convert.ToInt32(dr["bprise"]);
                Data.bname = dr["bname"].ToString();
                Data.bfile = dr["bfile"].ToString();
                Data.BrandName = dr["BrandName"].ToString();
                Data.Brandid = Convert.ToInt32(dr["Brandid"]);
                Data.FlareLevel = dr["FlareLevel"].ToString();
                Data.Flareid = Convert.ToInt32(dr["Flareid"]);
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
        public void Updateamount(Cart Data)
        {
            string sql = $@"UPDATE Cart SET amount = {Data.amount} WHERE CartId = {Data.CartId}";
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
    }
}
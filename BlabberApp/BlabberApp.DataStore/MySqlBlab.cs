using System;
using System.Collections;
using System.Data;
using MySql.Data.MySqlClient;
using BlabberApp.Domain;

namespace BlabberApp.DataStore
{
    public class MySqlBlab : IBlabPlugin
    {
        MySqlConnection dcBlab;
        public MySqlBlab()
        {
            this.dcBlab = new MySqlConnection("server=142.93.114.73;database=JaredCHunter07;"
            + "user=JaredCHunter07;password=letmein");
            try
            {
                this.dcBlab.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public void Close()
        {
            this.dcBlab.Close();
        }
        public void Create(IEntity obj)
        {
            Blab blab = (Blab)obj;
            try
            {
                DateTime now = DateTime.Now;
                string sql = "INSERT INTO blabs (sys_id, message, dttm_created, user_id) VALUES ('"
                     + blab.Id + "', '"
                     + blab.Message.Replace("'", "''") + "', '"
                     + now.ToString("yyyy-MM-dd HH:mm:ss") + "', '"
                     + blab.User.Email + "')";
                MySqlCommand cmd = new MySqlCommand(sql, this.dcBlab);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public IEnumerable ReadAll()
        {
            try{
                string sql = "SELECT * FROM blabs";
                Console.WriteLine(sql);
                MySqlDataAdapter daBlab = new MySqlDataAdapter(sql, this.dcBlab);
                MySqlCommandBuilder dbBlab = new MySqlCommandBuilder(daBlab);
                DataSet dsBlabs = new DataSet();

                daBlab.Fill(dsBlabs);

                ArrayList myArrayList = new ArrayList();
                foreach(DataRow dtRow in dsBlabs.Tables[0].Rows)
                {
                    myArrayList.Add(DataRow2Blab(dtRow));
                }

                return myArrayList;
            }
            catch (Exception e) {
                throw new Exception(e.ToString());
            }
        }

        public IEntity ReadById(Guid Id)
        {
            try
            {
                string sql = "SELECT * FROM blabs WHERE blabs.sys_id = '" + Id.ToString() + "'";

                Console.WriteLine(sql);
                MySqlDataAdapter daBlab = new MySqlDataAdapter(sql, this.dcBlab); // To avoid SQL injection.
                MySqlCommandBuilder cbBlab = new MySqlCommandBuilder(daBlab);
                DataSet dsUser = new DataSet();

                daBlab.Fill(dsUser, "blabs");

                DataRow row = dsUser.Tables[0].Rows[0];
                Blab blab = new Blab();

                blab.Id = new Guid(row["sys_id"].ToString());

                return blab;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public IEnumerable ReadByUserId(string email)
        {
            try
            {
                string sql = "SELECT * FROM blabs WHERE blabs.user_id = '" + email.ToString() + "'";

                MySqlDataAdapter daBlabs = new MySqlDataAdapter(sql, this.dcBlab); // To avoid SQL injection.
                MySqlCommandBuilder cbBlabs = new MySqlCommandBuilder(daBlabs);
                DataSet dsBlabs = new DataSet();

                daBlabs.Fill(dsBlabs);

                ArrayList alBlabs = new ArrayList();

                foreach( DataRow dtRow in dsBlabs.Tables[0].Rows)
                {
                    alBlabs.Add(DataRow2Blab(dtRow));
                }
                
                return (IEnumerable) alBlabs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void Update(IEntity obj)
        {
            Blab blab = (Blab)obj;
            try
            {
                string sql = "UPDATE blabs SET message = '" + blab.Message.Replace("'", "''")
                    + "' WHERE sys_id = '"
                    + blab.Id + "'";
                MySqlCommand cmd = new MySqlCommand(sql, this.dcBlab);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void Delete(IEntity obj)
        {
            Blab blab = (Blab)obj;
            try{
                string sql = "DELETE FROM blabs WHERE blabs.sys_id='"+blab.Id+"'";
                MySqlCommand cmd = new MySqlCommand(sql, dcBlab);
                cmd.ExecuteNonQuery();
            } catch(Exception ex) {
                throw new Exception(ex.ToString());
            }
        }

        private Blab DataRow2Blab(DataRow row)
        {
            User user = new User();

            user.ChangeEmail(row["user_id"].ToString());

            Blab blab = new Blab(user);

            blab.Id = new Guid(row["sys_id"].ToString());
            blab.Message = row["message"].ToString();
            blab.DTTM = (DateTime)row["dttm_created"];

            return blab;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using System.IO;
using System.Data;

namespace BLL
{
    public class UserBLL
    {
        private string Encode(string Pass)
        {
            byte[] encData_byte = new byte[Pass.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(Pass);
            string encodeData = Convert.ToBase64String(encData_byte);
            return encodeData;
        }
        private string Decode(string EncodedPass)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(EncodedPass);
            int charCount = utf8Decode.GetCharCount(todecode_byte , 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new string(decoded_char);
            return result;
        }
        UserDAL dal = new UserDAL();

        public string Create(User u, UserGroup ug)
        {
            u.Password = Encode(u.Password);
            return dal.Create(u,ug);
        }
        public bool IsRegistered()
        {
            return dal.IsRegistered();
        }
        public DataTable Read()
        {
            return dal.Read();
        }
        public User Read(int id)
        {
            return dal.Read(id);
        }
        public User ReadU(string s)
        {
            return dal.ReadU(s);
        }
        public List<string> ReadUserNames()
        {
            return dal.ReadUserNames();
        }
        public string Update (User u ,UserGroup ug, int id)
        {
            u.Password = Encode(u.Password);
            return dal.Update(u,ug,id);
        }
        public string Delete (int id)
        {
            return dal.Delete(id);
        }
        public User Login(string u, string p)
        {
            return dal.Login(u,Encode(p));
        }
        public bool Access(User u, string s, int a)
        {
            return dal.Access(u, s, a);
        }
        public List<User> ReadInvoiceByUseer()
        {
            return dal.ReadInvoiceByUseer();
        }
    }
}

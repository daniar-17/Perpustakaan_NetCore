using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Models
{
    public class Anggota
    {
        public int id { get; set; }
        public string id_anggota { get; set; }

        [Required]
        public string nama { get; set; }
        public string jenis_kelamin { get; set; }

        [Required]
        public string alamat { get; set; }

        public int SaveDetails()
        {
            SqlConnection con = new SqlConnection(GetConString.ConString());
            string query = "INSERT INTO anggota(id_anggota, nama, jenis_kelamin, alamat) values ('" + id_anggota + "','" + nama + "','" + jenis_kelamin + "', '" +  alamat +"')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int EditDetails()
        {
            SqlConnection con = new SqlConnection(GetConString.ConString());
            string query = "UPDATE anggota SET id_anggota = '" + id_anggota + "', nama = '" + nama + "', jenis_kelamin = '" + jenis_kelamin + "', alamat = '" + alamat + "' where id = '" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }

        public int DeleteDetails()
        {
            SqlConnection con = new SqlConnection(GetConString.ConString());
            string query = "DELETE FROM Anggota WHERE id = '" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }

        //Last Line
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using siemens_pedigree_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;


namespace siemens_pedigree_api.Controllers
{
    [Route("api/struct")]
    [ApiController]
    public class JSONStructController : ControllerBase
    {

        //private JSONStructContext _context;
        private readonly SqlConnectionStringBuilder _builder;


        public JSONStructController(JSONStructContext context)
        {
            //this._context = context;

            _builder = new()
            {
                DataSource = "217.126.9.8, 1434",
                UserID = "Genetica",
                Password = "Servogen+",
                InitialCatalog = "GENETICA"
            };

        }

        private static JSONStruct GetStructFromReader(SqlDataReader reader)
        {
            Int32 ID = reader.GetInt32(0);
            DateTime date = reader.GetDateTime(1);
            string IdTree = reader.GetString(2);
            string JsonText = reader.GetString(3);
            return new JSONStruct(ID, date, IdTree, JsonText);
        }

        [HttpGet]
        public IEnumerable<JSONStruct> GetAllJSONStructs()
        {
            List<JSONStruct> _jsonEntries = new();

            using SqlConnection connection = new(_builder.ConnectionString);
            try
            {
                
                string sql = "SELECT ID, Fecha_Str, ID_Tree, Json_Struct FROM GENETICA.dbo.GE_JSON_STRUCT";

                using SqlCommand command = new(sql, connection);
                connection.Open();
                using SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        _jsonEntries.Add(GetStructFromReader(reader));
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return _jsonEntries;
            }
            finally
            {
                connection.Close();

            }

            return _jsonEntries;
        }

        [HttpGet("{id}", Name = "GetJSONStruct")]
        public async Task<ActionResult<JSONStruct>> GetJSONStruct(int id)
        {
            JSONStruct? _JSONStruct = null;

            using (SqlConnection connection = new(_builder.ConnectionString))
            {
                    String sql = $"SELECT ID, Fecha_Str, ID_Tree, Json_Struct FROM GENETICA.dbo.GE_JSON_STRUCT WHERE ID={id}";

                using SqlCommand command = new(sql, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            _JSONStruct = GetStructFromReader(reader);
                        }
                    }
                }
                connection.Close();

            }

            if (_JSONStruct == null)
            {
                return NotFound();
            }

            return _JSONStruct;
        }

        // POST: api/JSONStruct
        [HttpPost]
        public async Task<IActionResult> CreateJSONStruct(JSONStruct json)
        {
            using (SqlConnection connection = new(_builder.ConnectionString))
            {
                string sql = "INSERT INTO GENETICA.dbo.GE_JSON_STRUCT(Fecha_Str, ID_Tree, Json_Struct) " +
                    $"VALUES('', '{json.IdTree}', '{json.Content}'); ";
                using SqlCommand command = new(sql, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            return NoContent();

        }

        // PUT: api/JSONStruct/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJSONStruct(int id, JSONStruct json)
        {

            using (SqlConnection connection = new(_builder.ConnectionString))
            {
                string sql = "UPDATE GENETICA.dbo.GE_JSON_STRUCT" +
                    $" SET ID_Tree = '{json.IdTree}', Json_Struct = '{json.Content}' WHERE ID = {id}";
                using SqlCommand command = new(sql, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            return NoContent();

        }

        // DELETE: api/JSONStruct/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJSONStruct(int id)
        {


            using (SqlConnection connection = new(_builder.ConnectionString))
            {
                string sql = $"DELETE FROM GENETICA.dbo.GE_JSON_STRUCT WHERE ID = {id}";
                using SqlCommand command = new(sql, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            return NoContent();
        }
    }
}

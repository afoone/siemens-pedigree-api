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
    [Route("api/json")]
    [ApiController]
    public class JSONEntryController : ControllerBase
    {

        //private readonly JSONEntryContext _context;
        private readonly SqlConnectionStringBuilder _builder;


        public JSONEntryController(JSONEntryContext context)
        {
            //_context = context;

            SqlConnectionStringBuilder builder = new()
            {
                DataSource = "217.126.9.8, 1434",
                UserID = "Genetica",
                Password = "Servogen+",
                InitialCatalog = "GENETICA"
                
            };
            _builder = builder;

        }

        private JSONEntry GetEntryFromReader(SqlDataReader reader)
        {
            Int32 ID = reader.GetInt32(0);
            DateTime date = reader.GetDateTime(1);
            String IdTree = reader.GetString(2);
            String JsonText = reader.GetString(3);
            return new JSONEntry(ID, date, IdTree, JsonText);
        }

        [HttpGet]
        public IEnumerable<JSONEntry> GetAllJSON()
        {
            List<JSONEntry> _jsonEntries = new();


            using(SqlConnection connection = new(_builder.ConnectionString))
            {
                String sql = "SELECT ID, Fecha_Ent, ID_Tree, Json_Text FROM GENETICA.dbo.GE_JSON_ENTRY";

                using SqlCommand command = new(sql, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            _jsonEntries.Add(GetEntryFromReader(reader));
                        }
                    }
                }
                connection.Close();

            }

            return _jsonEntries;
        }

        [HttpGet("{id}", Name = "GetJSONEntry")]
        public async Task<ActionResult<JSONEntry>> GetJSON(int id)
        {
            JSONEntry? _jsonEntry = null;

            using (SqlConnection connection = new(_builder.ConnectionString))
            {
                String sql = $"SELECT ID, Fecha_Ent, ID_Tree, Json_Text FROM GENETICA.dbo.GE_JSON_ENTRY WHERE ID={id}";

                using SqlCommand command = new(sql, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            _jsonEntry = GetEntryFromReader(reader);
                        }
                    }
                }
                connection.Close();

            }

            if (_jsonEntry == null)
            {
                return NotFound();
            }

            return _jsonEntry;
        }

        // POST: api/json
        [HttpPost]
        public async Task<IActionResult> CreateJSON(JSONEntry json)
        {
            Console.WriteLine("adding json to json entry");

            using (SqlConnection connection = new(_builder.ConnectionString))
            {
                string JsonContent = json.Content;
                Console.WriteLine(JsonContent);
                string JsonSQL =
                    @$"INSERT INTO GENETICA.dbo.GE_JSON_ENTRY(Fecha_Ent, ID_Tree, Json_Text)
                    OUTPUT INSERTED.ID
                    VALUES('', @idtree, @jsonContent); ";
                string ImageSQL =
                    @$"INSERT INTO GENETICA.dbo.GE_TREE_IMAGEN(ID_Tree, TREE_IMAGEN)
                    OUTPUT INSERTED.ID
                    VALUES(@idtree, '{json.Image}')
                    ; ";

                connection.Open();
                using SqlCommand command = new(JsonSQL, connection);
                command.Parameters.AddWithValue("@idtree", json.IdTree);
                command.Parameters.AddWithValue("@jsonContent", json.Content.ToString());


                int insertedJson = (int)command.ExecuteScalar();
                using SqlCommand commandImage = new(ImageSQL, connection);
                commandImage.Parameters.AddWithValue("@idtree",json.IdTree);


                json.Id = insertedJson;


                connection.Close();
                return Ok(json);
            }

        }

        // PUT: api/JSONEntry/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJSON(int id, JSONEntry json)
        {

            using (SqlConnection connection = new(_builder.ConnectionString))
            {
                string sql = "UPDATE GENETICA.dbo.GE_JSON_ENTRY" +
                    $" SET ID_Tree = '{json.IdTree}', Json_Text = '{json.Content}' WHERE ID = {id}";
                using SqlCommand command = new(sql, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            return NoContent();

        }

        // DELETE: api/JSONEntry/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJSON(int id)
        {


            using (SqlConnection connection = new(_builder.ConnectionString))
            {
                string sql = $"DELETE FROM GENETICA.dbo.GE_JSON_ENTRY WHERE ID = {id}";
                using SqlCommand command = new(sql, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            return NoContent();
        }
    }
}

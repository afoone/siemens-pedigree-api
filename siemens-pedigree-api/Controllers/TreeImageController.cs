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
    [Route("api/image")]
    [ApiController]
    public class TreeImageController : ControllerBase
    {

        //private TreeImageContext _context;
        private readonly SqlConnectionStringBuilder _builder;
          //  private readonly IHostingEnvironment _hostingEnv;


        public TreeImageController()
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

        // private static TreeImage GetStructFromReader(SqlDataReader reader)
        // {
        //     Int32 ID = reader.GetInt32(0);
        //     DateTime date = reader.GetDateTime(1);
        //     string IdTree = reader.GetString(2);
        //     //string JsonText = reader.GetString(3);
        //     return new TreeImage(ID, date, IdTree, );
        // }

        // [HttpGet]
        // public IEnumerable<TreeImage> GetAllTreeImages()
        // {
        //     List<TreeImage> _jsonEntries = new();

        //     using SqlConnection connection = new(_builder.ConnectionString);
        //     try
        //     {
                
        //         string sql = "SELECT ID, Fecha_Str, ID_Tree, Json_Struct FROM GENETICA.dbo.GE_JSON_STRUCT";

        //         using SqlCommand command = new(sql, connection);
        //         connection.Open();
        //         using SqlDataReader reader = command.ExecuteReader();
        //         if (reader.HasRows)
        //         {
        //             while (reader.Read())
        //             {
        //                 _jsonEntries.Add(GetStructFromReader(reader));
        //             }
        //         }
        //     }
        //     catch(Exception e)
        //     {
        //         Console.WriteLine(e.Message);
        //         return _jsonEntries;
        //     }
        //     finally
        //     {
        //         connection.Close();

        //     }

        //     return _jsonEntries;
        // }

        // [HttpGet("{id}", Name = "GetTreeImage")]
        // public async Task<ActionResult<TreeImage>> GetTreeImage(int id)
        // {
        //     TreeImage? _TreeImage = null;

        //     using (SqlConnection connection = new(_builder.ConnectionString))
        //     {
        //             String sql = $"SELECT ID, Fecha_Str, ID_Tree, Json_Struct FROM GENETICA.dbo.GE_JSON_STRUCT WHERE ID={id}";

        //         using SqlCommand command = new(sql, connection);
        //         connection.Open();
        //         using (SqlDataReader reader = command.ExecuteReader())
        //         {
        //             if (reader.HasRows)
        //             {
        //                 while (reader.Read())
        //                 {
        //                     _TreeImage = GetStructFromReader(reader);
        //                 }
        //             }
        //         }
        //         connection.Close();

        //     }

        //     if (_TreeImage == null)
        //     {
        //         return NotFound();
        //     }

        //     return _TreeImage;
        // }

        // POST: api/TreeImage
        // [HttpPost]
        // public async Task<ActionResult> CreateTreeImage([FromForm] TreeImageViewModel  imageVM)
        // {
        //     if (imageVM == null)
        //     {
        //         return BadRequest();
        //     }


        //     var a = _hostingEnv.WebRootPath;
        //     var fileName = Path.GetFileName(imageVM.image.FileName);
        //     var filePath = Path.Combine(_hostingEnv.WebRootPath, "images\\treed", fileName);

        //     using (var fileSteam = new FileStream(filePath, FileMode.Create))
        //     {
        //         await imageVM.image.CopyToAsync(fileSteam);
        //     }

        //     TreeImage image = new TreeImage();
        //     image.Id = imageVM.Id;
        //     image.ImagePath = filePath;  //save the filePath to database ImagePath field.
        //     // _context.Add(car);
        //     // await _context.SaveChangesAsync();
        //     // return Ok();

        //     using (SqlConnection connection = new(_builder.ConnectionString))
        //     {
        //         string sql = "INSERT INTO GENETICA.dbo.GE_JSON_STRUCT(Fecha_Str, ID_Tree, Json_Struct) " +
        //             $"VALUES('', '${json.IdTree}', '${json.Content}'); ";
        //         using SqlCommand command = new(sql, connection);
        //         connection.Open();
        //         command.ExecuteNonQuery();
        //         connection.Close();
        //     }

        //     return NoContent();

        // }


[HttpPost]
[Route("upload")]
public async Task<IActionResult> UploadImageProfile([FromForm]TreeImageViewModel fileviewmodel)
{
    if (ModelState.IsValid)
    {
        using (var memoryStream = new MemoryStream())
        {
            await fileviewmodel.image.CopyToAsync(memoryStream);

            // Upload the file if less than 2 MB
            if (memoryStream.Length < 2097152)
            {
                        //create a AppFile mdoel and save the image into database.
                        var file = new TreeImage()
                        {
                            //Name = fileviewmodel.image.FileName,
                            // debería pedir que pusisesen el nombre del archivo?=?
                            Id = fileviewmodel.Id,
                            Image = memoryStream.ToArray().ToString()
                        };

                //INSERT [Thumbnail] ( Data )
                //SELECT * FROM OPENROWSET (BULK 'C:\Test\TestPic1.jpg', SINGLE_BLOB) AS X

                // _context.AppFiles.Add(file);
                // _context.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("File", "The file is too large.");
            }
        }

        // var returndata = _context.AppFiles
        //     .Where(c => c.Name == fileviewmodel.Name)
        //     .Select(c => new ReturnData()
        //     {
        //         Name = c.Name,
        //         ImageBase64 = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(c.Content))
        //     }).FirstOrDefault();
        //return Ok(returndata);
        return Ok();
    }
    return Ok("Invalid");
}

        // PUT: api/TreeImage/5
        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateTreeImage(int id, TreeImage json)
        // {

        //     using (SqlConnection connection = new(_builder.ConnectionString))
        //     {
        //         string sql = "UPDATE GENETICA.dbo.GE_JSON_STRUCT" +
        //             $" SET ID_Tree = '{json.IdTree}', Json_Struct = '{json.Content}' WHERE ID = {id}";
        //         using SqlCommand command = new(sql, connection);
        //         connection.Open();
        //         command.ExecuteNonQuery();
        //         connection.Close();
        //     }

        //     return NoContent();

        // }

        // // DELETE: api/TreeImage/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteTreeImage(int id)
        // {


        //     using (SqlConnection connection = new(_builder.ConnectionString))
        //     {
        //         string sql = $"DELETE FROM GENETICA.dbo.GE_JSON_STRUCT WHERE ID = {id}";
        //         using SqlCommand command = new(sql, connection);
        //         connection.Open();
        //         command.ExecuteNonQuery();
        //         connection.Close();
        //     }

        //     return NoContent();
        // }
    }
}

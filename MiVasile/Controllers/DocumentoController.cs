using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiVasile.Domain.Models;
using MiVasile.Infrastructure;
using MiVasile.Infrastructure.Business;


namespace MiVasile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        public static string PathSavesDocuments { get; set; }

        public DocumentoController()
        {
            PathSavesDocuments = Path.Combine(Directory.GetCurrentDirectory(), "Documents");
        }

        [HttpPost]
        [Route("Upload")]
        [DisableRequestSizeLimit, RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue, ValueLengthLimit = int.MaxValue)]
        public async Task<IActionResult> SubirDocumento([FromForm] Documento documento)// aca en realidad yo subiria un archivo al servidor y este me devolveria la ruta del mismo
        {
            try //ESTO ESTA RARO
            {
                string rutacompleta = Path.Combine(PathSavesDocuments, documento.Archivo.FileName);

                using (FileStream newfile = System.IO.File.Create(rutacompleta))
                {
                    await documento.Archivo.CopyToAsync(newfile);
                    await newfile.FlushAsync();
                }
                documento.Ruta = rutacompleta;

                Documento_Business business = new Documento_Business();

                if (await business.AddAsync(documento))
                    return StatusCode(StatusCodes.Status200OK, new { message = "Archivo subido exitosamente" });

                return StatusCode(StatusCodes.Status200OK, new { message = "Ocurrio un error al subir la ruta del documento a la base de datos, revisar acceso a la misma" });
            }
            catch (Exception)
            {
             

                return StatusCode(StatusCodes.Status200OK, new { message = "Archivo subido erroneamente, ocurrio un error en el controlador de documentos" });

            }


        }
    }
}

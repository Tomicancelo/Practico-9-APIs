using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practico_9_APIs.Modelos.Dto;
using Practico_9_APIs.Datos;
using Practico_9_APIs.Modelos;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;

namespace Practico_9_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EPController : ControllerBase
    {
        /********** Ejercicio 7 ******************/
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<EPDto>> GetEPDto()
        {
            return Ok(EPStore.EPList);
        }
        //Creo los Ends Points 
        [HttpGet("Id", Name = "GetEP")] //Retorna un objeto. Hay que diferencial los End Point.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EPDto> GetEPDto(int Id)
        {
            if (Id == 0)
            {
                return BadRequest();
            }

            var Ep = EPStore.EPList.FirstOrDefault(a => a.Id == Id);

            if (Ep == null)
            {
                return NotFound();//Responder con un codigo de estado 404
            }

            return Ok(Ep);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EPDto> CrearEPDto([FromBody] EPDto epdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (EPStore.EPList.FirstOrDefault(v => v.Nombre.ToLower() == epdto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExistente", "Ese nombre ya existe");
                return BadRequest(ModelState);
            }
            if (epdto == null)
            {
                return BadRequest(epdto);
            }
            if (epdto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            epdto.Id = EPStore.EPList.OrderByDescending(e => e.Id).FirstOrDefault().Id + 1;
            EPStore.EPList.Add(epdto);
            return CreatedAtRoute("GetEP", new { Id = epdto.Id }, epdto);

        }
        /********************* EJERCICIO 8 *******************************/
        [HttpDelete("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult DeleteEP(int Id)
        {
            if (Id == 0)
            {
                return BadRequest();
            }
            var ep = EPStore.EPList.FirstOrDefault(v => v.Id == Id);
            if (ep == null)
            {
                return NotFound();
            }
            EPStore.EPList.Remove(ep);
            return NoContent();
        }
        /********************* EJERCICIO 9 *******************************/
        [HttpPut("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdateEp(int Id, [FromBody] EPDto epdto)
        {
            if (epdto == null)
            {
                return BadRequest();
            }
            var ep = EPStore.EPList.FirstOrDefault(v => v.Id == Id);
            ep.Nombre = epdto.Nombre;
            ep.Opcupantes = epdto.Opcupantes;
            ep.MetrosCuadrados = epdto.MetrosCuadrados;
            return NoContent();
        }
        /************************ EJERCICIO 10 ***********************************/
        [HttpPatch("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdateEp(int Id, JsonPatchDocument<EPDto> patchdto)
        {
            if(patchdto == null || Id == 0)
            {
                return BadRequest();
            }
            var ep = EPStore.EPList.FirstOrDefault(v => v.Id == Id);
            patchdto.ApplyTo(ep, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }




    }
}
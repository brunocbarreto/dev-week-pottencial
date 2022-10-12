using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

using src.Models;
using src.Persistence;


namespace src.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase 
{
private DatabaseContext _context { get; set; }

public PersonController(DatabaseContext context)
{
  this._context = context;
}

[HttpGet] 
 public ActionResult<List<Person>> Get()
 {
   var result = _context.People.Include(p => p.contracts).ToList();

   if(!result.Any()) return NoContent();

   return Ok(result);  
 }

[HttpPost]
 public ActionResult<Person> Post([FromBody]Person person)
 {
  try
  {
    _context.People.Add(person);
    _context.SaveChanges();
  }
  catch (System.Exception)
  {
    return BadRequest();
  }
  

   return Created("Criado", person);
 }

[HttpPut("{id}")]
  public ActionResult<Object> Update(
    [FromRoute]int id, 
    [FromBody]Person person)
  {
    var result = _context.People.SingleOrDefault(e => e.Id == id);

    if(result is null){
      return NotFound(new {
        msg = "Registro não encontrado",
        status = HttpStatusCode.NotFound
      });
    }

    try
    {
      _context.People.Update(person);
      _context.SaveChanges();
    }
    catch (System.Exception)
    {
      return BadRequest(new {
        msg = "Houve um erro ao enviar a solicitação de atualização do " + id + " atualizados",
        status = HttpStatusCode.BadRequest
      });
    }
    

    return Ok(new {
      msg = $"Dados do id {id} atualizados",
      status = HttpStatusCode.OK  
    });
  }

  [HttpDelete("{id}")]
  public ActionResult<Object> Delete([FromRoute]int id){
    
    var result = _context.People.SingleOrDefault(e => e.Id == id);
    
    if(result is null){
      return BadRequest(new {
        msg = "Conteúdo Inexistente, solicitação inválida",
        status = HttpStatusCode.BadRequest
      });
    }

    _context.People.Remove(result);
    _context.SaveChanges();
 
    return Ok(new {
      msg = "Deletada pessoa de id: " + id,
      status = HttpStatusCode.OK
    });



 }

}



using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using final_mock.Data;
using System.Linq;
using final_mock.Models;

namespace FinalMock.Function
{
  public class HttpTrigger
  {
    private readonly ApplicationDbContext _context;
    public HttpTrigger(ApplicationDbContext context)
    {
      _context = context;
    }

    [FunctionName("GetPeople")]
    public IActionResult GetPeople(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "people")] HttpRequest req,
    ILogger log)
    {
      log.LogInformation("C# HTTP GET/posts trigger function processed a request in GetPeople().");

      var peoples = _context.People.ToArray();
      return new OkObjectResult(peoples);
    }

    [FunctionName("GetPerson")]
    public IActionResult GetPerson(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "people/{id}")] HttpRequest req,
    ILogger log, int id)
    {
      log.LogInformation("C# HTTP GET/posts trigger function processed a request.");
      if (id < 1)
      {
        return new NotFoundResult();
      }

      var people = _context.People.FindAsync(id).Result;
      if (people == null)
      {
        return new NotFoundResult();
      }
      log.LogInformation(people.Id.ToString());
      return new OkObjectResult(people);
    }

    [FunctionName("CreatePerson")]
    public async Task<IActionResult> CreatePerson(
    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "people")] HttpRequest req,
    ILogger log)
    {
      log.LogInformation("C# HTTP POST/posts trigger function processed a request.");
      string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      var input = JsonConvert.DeserializeObject<Person>(requestBody);

      var people = new Person()
      {
        FirstName = input.FirstName,
        LastName = input.LastName,
        Occupation = input.Occupation,
        Gender = input.Gender,
        PictureUrl = input.PictureUrl,
        Votes = input.Votes
      };

      _context.People.Add(people);
      await _context.SaveChangesAsync();
      log.LogInformation(requestBody);
      return new OkObjectResult(people);
    }

    [FunctionName("UpdatePerson")]
    public async Task<IActionResult> UpdatePerson(
    [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "people/{id}")]
        HttpRequest req,
    ILogger log, int id)
    {
      log.LogInformation("C# HTTP PUT/posts trigger function processed a request.");
      if (id < 1)
      {
        return new NotFoundResult();
      }

      var people = await _context.People.FindAsync(id);
      if (people == null)
      {
        return new NotFoundResult();
      }
      string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      var input = JsonConvert.DeserializeObject<Person>(requestBody);

      people.FirstName = input.FirstName;
      people.LastName = input.LastName;
      people.Occupation = input.Occupation;
      people.Gender = input.Gender;
      people.PictureUrl = input.PictureUrl;
      people.Votes = input.Votes;

      _context.Update(people);
      await _context.SaveChangesAsync();
      log.LogInformation(requestBody);
      return new OkObjectResult(people);
    }

    [FunctionName("DeletePerson")]
    public async Task<IActionResult> DeletePerson(
    [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "people/{id}")] HttpRequest req,
    ILogger log, int id)
    {
      log.LogInformation("C# HTTP DELETE/posts trigger function processed a request.");
      if (id < 1)
      {
        return new NotFoundResult();
      }

      var people = _context.People.FindAsync(id).Result;
      if (people == null)
      {
        return new NotFoundResult();
      }


      _context.Remove(people);
      await _context.SaveChangesAsync();
      return new OkResult();
    }
  }
}

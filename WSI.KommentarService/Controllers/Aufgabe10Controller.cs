using Microsoft.AspNetCore.Mvc;

namespace WSI.KommentarService.Controllers;

[ApiController]
[Route("v1.0/[controller]")]
public class Aufgabe10Controller : ControllerBase {
    private static readonly Dictionary<string, string> _tmpStorage = new();
    private const string _apiKey = "1234567890";

    [HttpGet]
    public ActionResult<Dictionary<string, string>> Get(string apiKey) {
        if (_apiKey != apiKey) return StatusCode(StatusCodes.Status405MethodNotAllowed);

        if (_tmpStorage.Any()) {
            return Ok(_tmpStorage);
        } else {
            return NoContent();
        }
    }

    [HttpGet("{key}")]
    public ActionResult<string> Get(string key, string apiKey) {
        if (_apiKey != apiKey) return StatusCode(StatusCodes.Status405MethodNotAllowed);

        if (_tmpStorage.ContainsKey(key)) {
            return Ok(_tmpStorage[key]);
        } else {
            return NoContent();
        }
    }

    [HttpPost]
    public ActionResult<string> Post(string key, string value, string apiKey) {
        if (_apiKey != apiKey) return StatusCode(StatusCodes.Status405MethodNotAllowed);

        if (_tmpStorage.ContainsKey(key)) {
            return Conflict();
        } else {
            _tmpStorage.Add(key, value);
            return CreatedAtAction(nameof(Get), value);
        }
    }

    [HttpPut]
    public IActionResult Put(string key, string value, string apiKey) {
        if (_apiKey != apiKey) return StatusCode(StatusCodes.Status405MethodNotAllowed);

        if (_tmpStorage.ContainsKey(key)) {
            _tmpStorage[key] = value;
            return NoContent();
        } else {
            return NotFound();
        }
    }

    [HttpDelete]
    public IActionResult Delete(string key, string apiKey) {
        if (_apiKey != apiKey) return StatusCode(StatusCodes.Status405MethodNotAllowed);

        if (_tmpStorage.ContainsKey(key)) {
            _tmpStorage.Remove(key);
            return NoContent();
        } else {
            return NotFound();
        }
    }
}

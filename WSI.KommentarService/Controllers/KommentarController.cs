using Microsoft.AspNetCore.Mvc;
using WSI.KommentarService.Data;
using WSI.KommentarService.Models;
using WSI.WortFilter;
using Microsoft.EntityFrameworkCore;

namespace WSI.KommentarService.Controllers;

[ApiController]
[Route("v1.0/Kommentar")]
public class KommentarController : ControllerBase {
    private readonly IWortFilterService _wortFilterService;
    private readonly DataContext _dataContext;
    public KommentarController(IWortFilterService wortFilterService, DataContext dataContext) {
        _wortFilterService = wortFilterService;
        _dataContext = dataContext;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(KommentarModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<KommentarModel>> Get(Guid id) {
        var kommentar = await _dataContext.Kommentare
            .Include(k => k.WeitereKommentare)!.ThenInclude(k => k.WeitereKommentare)!.ThenInclude(k => k.WeitereKommentare)
            .FirstOrDefaultAsync(f => f.Id == id);
        var kopf = kommentar?.KopfKommentar;

        if (kommentar == null) {
            return NoContent();
        } else {
            return Ok(kommentar);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(KommentarModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<KommentarModel>> Post([FromBody] KommentarPOSTModel kommentar) {
        var kommentarNeu = kommentar.ZuKommentar();

        if (_dataContext.Kommentare.Any(f => f.Id == kommentarNeu.Id)) {
            return Conflict();
        }

        if (kommentarNeu.ÜbergeordneterKommentarId != null) {
            var übergeordnetKommentar = await _dataContext.Kommentare
                .FirstOrDefaultAsync(f => f.Id == kommentarNeu.ÜbergeordneterKommentarId);

            if (übergeordnetKommentar == null)
                return BadRequest();
        
            kommentarNeu.KopfId = übergeordnetKommentar.KopfId;
        }

        kommentarNeu.Kommentar = _wortFilterService.FilterWörter(kommentarNeu.Kommentar);
        kommentarNeu.Title = _wortFilterService.FilterWörter(kommentarNeu.Title ?? "");

        await _dataContext.Kommentare.AddAsync(kommentarNeu);
        await _dataContext.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = kommentarNeu.Id}, kommentarNeu);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    public async Task<IActionResult> Put([FromBody] KommentarPUTModel kommentar) {
        var saved = await _dataContext.Kommentare
            .AsTracking()
            .FirstOrDefaultAsync(f => f.Id == kommentar.Id);

        if (saved == null) {
            return BadRequest();
        }

        if (saved.SchreibSchlüssel != kommentar.SchreibSchlüssel || saved.Gelöscht) {
            return StatusCode(405);
        }

        saved.Update(kommentar);

        saved.Kommentar = _wortFilterService.FilterWörter(saved.Kommentar);
        saved.Title = _wortFilterService.FilterWörter(saved.Title ?? "");

        await _dataContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    public async Task<IActionResult> Delete([FromBody] KommentarDELETEModel kommentar) {
        var saved = await _dataContext.Kommentare
            .AsTracking()
            .Include(i => i.WeitereKommentare)
            .FirstOrDefaultAsync(f => f.Id == kommentar.Id);

        if (saved == null) {
            return BadRequest();
        } else if (saved.SchreibSchlüssel != kommentar.SchreibSchlüssel) {
            return StatusCode(405);
        }

        if (saved.WeitereKommentare != null && saved.WeitereKommentare.Any()) {
            saved.Name = "deleted";
            saved.Kommentar = "deleted";
            saved.Title = "deleted";
            saved.Gelöscht = true;
        } else {
            _dataContext.Kommentare.Remove(saved);
        }

        await _dataContext.SaveChangesAsync();

        return NoContent();
    }
}

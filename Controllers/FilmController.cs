using Microsoft.AspNetCore.Http;
using filmapp.Models;
using filmapp.Services;
using Microsoft.AspNetCore.Mvc;

namespace filmapp.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class FilmController : ControllerBase
    {
        public FilmController()
        {
        }

        // GET all action
        [HttpGet]
        public ActionResult<List<Film>> GetAll() =>
        FilmService.GetAll();

        // GET by Id action
        [HttpGet("{id}")]
        public ActionResult<Film> Get(int id)
        {
            var film = FilmService.Get(id);

            if (film == null)
                return NotFound();

            return film;
        }
        // POST action
        [HttpPost]
        public IActionResult Create(Film film)
        {
            FilmService.Add(film);
            return CreatedAtAction(nameof(Get), new { id = film.Id }, film);
        }
        // PUT action
        [HttpPut("{id}")]
        public IActionResult Update(int id, Film film)
        {
            if (id != film.Id)
                return BadRequest();

            var existingFilm = FilmService.Get(id);
            if (existingFilm is null)
                return NotFound();

            FilmService.Update(film);

            return NoContent();
        }
        // DELETE action
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var film = FilmService.Get(id);

            if (film is null)
                return NotFound();

            FilmService.Delete(id);

            return NoContent();
        }
        [HttpOptions]
        
        public IActionResult HandleOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, OPTIONS");
            return Ok();
        }
        [HttpOptions("{id}")]
        public IActionResult Options(int id)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");

            return Ok(); // Válasz a CORS pre-flight kérésre
        }

    }
}

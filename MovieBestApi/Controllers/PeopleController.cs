using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieBestAPI.Dtos;
using MovieBestAPI.Interfaces;
using MovieBestAPI.Models;
using MovieBestAPI.Services;

namespace MovieBestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {

        
        private readonly IMapper _mapper;
        private readonly IPeopleService _peopleService;

        private new List<string> _allowedExtenstions = new List<string> { ".jpg", ".png", ".jpeg" };
        private long _maxAllowedPosterSize = 1048576;

        public PeopleController(IPeopleService peopleService, IMapper mapper)
        {

           _peopleService = peopleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var people = await _peopleService.GetAll();

            var data = _mapper.Map<IEnumerable<PeopleDetailsDto>>(people);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var people = await _peopleService.GetById(id);

            if (people == null)
                return NotFound();

            var dto = _mapper.Map<PeopleDetailsDto>(people);

            return Ok(dto);
        }

       

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] PeopleDto dto)
        {
            if (dto.Poster == null)
                return BadRequest("Poster is required!");

            if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only .png , .jpeg and .jpg  images are allowed!");

            if (dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max allowed size for poster is 1MB!");

          

            using var dataStream = new MemoryStream();

            await dto.Poster.CopyToAsync(dataStream);

            var people = _mapper.Map<People>(dto);
            people.Poster = dataStream.ToArray();

             _peopleService.Create(people);

            return Ok(people);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] PeopleDto dto)
        {
            var people = await _peopleService.GetById(id);

            if (people == null)
                return NotFound($"No people was found with ID {id}");


          

            if (dto.Poster != null)
            {
                if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png and .jpg images are allowed!");

                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Max allowed size for poster is 1MB!");

                using var dataStream = new MemoryStream();

                await dto.Poster.CopyToAsync(dataStream);

                people.Poster = dataStream.ToArray();
            }
            people.Name = dto.Name;
            people.Gender = dto.Gender;
            people.KnownCredits = dto.KnownCredits ;
            people.BirthDay = dto.BirthDay;
            people.PlaceOfBirth = dto.PlaceOfBirth;
            people.About = dto.About;



            _peopleService.Update(people);

            return Ok(people);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var people = await _peopleService.GetById(id);

            if (people == null)
                return NotFound($"No people was found with ID {id}");

            _peopleService.Delete(people);

            return Ok(people);
        }
    }
}

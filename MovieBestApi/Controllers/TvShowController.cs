using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieBestAPI.Dtos;
using MovieBestAPI.Interfaces;
using MovieBestAPI.Models;
using MovieBestAPI.Services;
using TvShow = MovieBestAPI.Models.TvShow;

namespace MovieBestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TvShowController : ControllerBase
    {
        #region ctor
        private readonly ITvShowService tvShowService;
        private readonly IMapper mapper;

        public TvShowController(ITvShowService tvShowService, IMapper mapper)
        {
            this.tvShowService = tvShowService;
            this.mapper = mapper;
        }
        #endregion

        private new List<string> _allowedExtenstions = new List<string> { ".jpg", ".png", ".jpeg" };
        private long _maxAllowedPosterSize = 1048576;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var people = await tvShowService.GetAll();

            var data = mapper.Map<IEnumerable<TvShowDetailsDto>>(people);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var tv = await tvShowService.GetById(id);

            if (tv == null)
                return NotFound();

            var dto = mapper.Map<TvShowDetailsDto>(tv);

            return Ok(dto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]  TvShowDto tv)
        {
            if (tv.Poster == null)
                return BadRequest("Poster is required!");

            if (!_allowedExtenstions.Contains(Path.GetExtension(tv.Poster.FileName).ToLower()))
                return BadRequest("Only .png , .jpeg and .jpg  images are allowed!");

            if (tv.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max allowed size for poster is 1MB!");



            using var dataStream = new MemoryStream();

            await tv.Poster.CopyToAsync(dataStream);

            var data = mapper.Map<TvShow>(tv);
            data.Poster = dataStream.ToArray();

            tvShowService.Create(data);
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] TvShowDto dto)
        {
            var tvShows = await tvShowService.GetById(id);

            if (tvShows == null)
                return NotFound($"No people was found with ID {id}");




            if (dto.Poster != null)
            {
                if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png and .jpg images are allowed!");

                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Max allowed size for poster is 1MB!");

                using var dataStream = new MemoryStream();

                await dto.Poster.CopyToAsync(dataStream);

                tvShows.Poster = dataStream.ToArray();
            }
           tvShows.Name = dto.Name;
           tvShows.Year = dto.Year;
           tvShows.Kind = dto.Kind;
           tvShows.Creator = dto.Creator;
           tvShows.Network = dto.Network;
           tvShows.Status = dto.Status;


            tvShowService.Update(tvShows);

            return Ok(tvShows);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var tvShows = await tvShowService.GetById(id);

            if (tvShows == null)
                return NotFound($"No people was found with ID {id}");

            tvShowService.Delete(tvShows);

            return Ok(tvShows);
        }
    }
}

using AutoMapper;
using MovieBestAPI.Dtos;
using MovieBestAPI.Models;

namespace MovieBestAPI.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDetailsDto>();
            CreateMap<MovieDto, Movie>().ForMember(src => src.Poster, opt => opt.Ignore());

            CreateMap<People, PeopleDetailsDto>();
            CreateMap<PeopleDto, People>().ForMember(src => src.Poster, opt => opt.Ignore());

            CreateMap<TvShow, TvShowDetailsDto>();
            CreateMap<TvShowDto, TvShow>().ForMember(src => src.Poster, opt => opt.Ignore());



        }
    }
}

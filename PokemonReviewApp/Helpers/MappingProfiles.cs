using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDTO>();
            CreateMap<PokemonInsertDTO, Pokemon>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<Country, CountryDTO>();
            CreateMap<CountryDTO, Country>();
            CreateMap<Owner, OwnerDTO>();
            CreateMap<OwnerDTO, Owner>();
            CreateMap<OwnerInsertDTO, Owner>();
            CreateMap<Review, ReviewDTO>();
            CreateMap<ReviewInsertDTO, Review>();
            CreateMap<Reviewer, ReviewerDTO>();
            CreateMap<ReviewerDTO, Reviewer>();
        }
    }
}

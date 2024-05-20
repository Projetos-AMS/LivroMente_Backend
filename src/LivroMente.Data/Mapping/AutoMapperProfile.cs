using AutoMapper;
using LivroMente.Domain.Models.CategoryBookModel;
using LivroMente.Domain.Requests;
using LivroMente.Domain.ViewModels;

namespace LivroMente.Data.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CategoryBook,CategoryBookRequest>().ReverseMap();
            CreateMap<CategoryBookRequest,CategoryBookViewModel>().ReverseMap();
           
        }
    }
}
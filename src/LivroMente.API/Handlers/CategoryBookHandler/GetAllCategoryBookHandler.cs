using AutoMapper;
using LivroMente.Domain.Models.CategoryBookModel;
using LivroMente.Domain.Requests;
using LivroMente.Domain.ViewModels;
using LivroMente.Service.Services;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LivroMente.API.Handlers.CategoryBookHandler
{
    public class GetAllCategoryBookHandler : IRequestHandler<GetAllCategoryBook, List<CategoryBookViewModel>>
    {
        private readonly BaseService<CategoryBook> _service;
        private readonly IMapper _mapper;

        public GetAllCategoryBookHandler(BaseService<CategoryBook> service,IMapper mapper)
        {
            
            _service = service;
            _mapper = mapper;
        }

        public async Task<List<CategoryBookViewModel>> Handle(GetAllCategoryBook request, CancellationToken cancellationToken)
        {

           var categories = await _service.GetAll(); // Corrigi o nome da variável para categories
            var result = _mapper.Map<List<CategoryBookViewModel>>(categories); // Mapear a lista de CategoryBook para List<CategoryBookViewModel>

            return result;
        }
    }
}
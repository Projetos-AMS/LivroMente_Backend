using AutoMapper;
using LivroMente.Domain.Models.CategoryBookModel;
using LivroMente.Domain.Requests;
using LivroMente.Domain.ViewModels;
using LivroMente.Service.Services;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LivroMente.API.Handlers.CategoryBookHandler
{
    public class CreateCategoryBookHandler : IRequestHandler<CategoryBookRequest, bool>
    {
        private readonly BaseService<CategoryBookViewModel> _service;
        private readonly IMapper _mapper;

        public CreateCategoryBookHandler(BaseService<CategoryBookViewModel> service,IMapper mapper)
        {
            
            _service = service;
            _mapper = mapper;
        }
        public  Task<CategoryBookViewModel> Handle(CategoryBookRequest request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<CategoryBookViewModel>(request);
             _service.Add(category);
            
           return Task.FromResult(category);
        }

        
    }
}
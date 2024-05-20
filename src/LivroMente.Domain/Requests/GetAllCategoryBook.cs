using LivroMente.Domain.ViewModels;
using MediatR;

namespace LivroMente.Domain.Requests
{
    public class GetAllCategoryBook : IRequest<List<CategoryBookViewModel>>
    {
        
    }
}
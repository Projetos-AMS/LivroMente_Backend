using LivroMente.Domain.ViewModels;
using MediatR;

namespace LivroMente.Domain.Requests
{
    public class CategoryBookRequest : IRequest<bool>
    {
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
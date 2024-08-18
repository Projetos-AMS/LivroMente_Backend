using MediatR;
using Microsoft.AspNetCore.Http;



namespace LivroMente.Domain.Commands.UploadCommands
{
    public class UploadAddCommand : IRequest<string>
    {
        public IFormFile File { get; }
        public UploadAddCommand(IFormFile file)
        {
            File = file;
        }
    }
}
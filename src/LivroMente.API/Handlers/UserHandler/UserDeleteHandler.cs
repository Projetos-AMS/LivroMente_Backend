// using LivroMente.Domain.Commands.UserCommands;
// using LivroMente.Domain.Models.IdentityEntities;
// using LivroMente.Service.Interfaces;
// using MediatR;

// namespace LivroMente.API.Handlers.UserHandler
// {
//     public class UserDeleteHandler : IRequestHandler<UserDeleteCommand, bool>
//     {
//         private readonly IUserService<User> _userService;
//         public UserDeleteHandler( IUserService<User> userService){_userService = userService;}
//         public async Task<bool> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
//         {
//             return await _userService.DeleteUserAsync(request.UserId);
//         }
//     }
// }
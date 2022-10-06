using RadioXXI.Models;
using RadioXXI.Models.Dtos;
using System.Collections.Generic;

namespace RadioXXI.Business.Interfaces
{
    public interface IUserBusiness
    {
        public UserDto getByEmail(string email);
        public UserDto login(UserLoginDto loginUser);
        public void newUser(Users user);
        public ICollection<UserDto> getAll();
        public string EncryptPass(string password);
    }
}

using RadioXXI.Models;
using RadioXXI.Models.Dtos;
using System.Collections.Generic;

namespace RadioXXI.Business.Interfaces
{
    public interface IUserBusiness
    {
        public Users getById(int id);
        public UserDto getByEmail(string email);
        public UserDto login(UserLoginDto loginUser);
        public void newUser(Users user);
        public ICollection<UserDto> getAll();
        public void update(Users update, int id);
        public void delete(int id);
        public string EncryptPass(string password);
    }
}

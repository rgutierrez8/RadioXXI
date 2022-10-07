using RadioXXI.Business.Interfaces;
using RadioXXI.Context;
using RadioXXI.Models;
using RadioXXI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RadioXXI.Business
{
    public class UserBusiness : RepositoryBase<Users>, IUserBusiness
    {
        public UserBusiness(RadioContext context): base(context)
        {

        }

        public Users getById(int id)
        {
            return FindByCondition(source => source.Id == id).FirstOrDefault();
        }

        public UserDto getByEmail(string email)
        {
            var data = FindByCondition(user => user.Email == email).FirstOrDefault();

            var user = new UserDto();

            try
            {
                user.Id = data.Id;
                user.Name = data.Name;
                user.LastName = data.LastName;
                user.Email = data.Email;
                user.Username = data.Username;
            }
            catch (Exception e)
            {
                return null;
            }

            return user;
        }

        public UserDto login(UserLoginDto loginUser)
        {
            var data = FindAll().Where(user => user.Username == loginUser.Username && user.Password == EncryptPass(loginUser.Password)).FirstOrDefault();
            var user = new UserDto();
            try
            {
                user.Id = data.Id;
                user.Name = data.Name;
                user.LastName = data.LastName;
                user.Email = data.Email;
                user.Username = data.Username;
            }
            catch(Exception e)
            {
                return null;
            }

            return user; 
        }

        public void newUser(Users newUser)
        {
            var user = new Users
            {
                Name = newUser.Name,
                LastName = newUser.LastName,
                Email = newUser.Email,
                Username = newUser.Username,
                Password = EncryptPass(newUser.Password)
            };

            Create(user);
            SaveChanges();
        }

        public void update(Users update, int id)
        {
            var user = getById(id);

            user.Name = update.Name;
            user.LastName = update.LastName;
            user.Email = update.Email;
            user.Username = update.Username;
            user.Password = EncryptPass(update.Password);

            Update(user);
            SaveChanges();
        }

        public ICollection<UserDto> getAll()
        {
            var users = new List<UserDto>();

            var dataUsers = FindAll().ToList();

            foreach(var data in dataUsers)
            {
                var user = new UserDto()
                {
                    Id = data.Id,
                    Name = data.Name,
                    LastName = data.LastName,
                    Email = data.Email,
                    Username = data.Username,
                };

                users.Add(user);
            }

            return users;
        }

        public void delete(int id)
        {
            Delete(getById(id));
            SaveChanges();
        }

        public string EncryptPass(string password)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(password));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}

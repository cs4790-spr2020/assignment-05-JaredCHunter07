using System;
using System.Collections;
using BlabberApp.Domain;
using BlabberApp.DataStore;

namespace BlabberApp.Services
{
    public class UserService : IUserService
    {
        private readonly UserAdapter _adapter;
        public UserService(UserAdapter adapter)
        {
            _adapter = adapter;
        }

        public IEnumerable GetAll()
        {
            return _adapter.GetAll();
        }

        public void AddNewUser(string email)
        {
            try
            {
                _adapter.Add(CreateUser(email));
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to add user");
            }
        }

        // public void AddNewUser(User user)
        // {
        //     try {
        //         _adapter.Add(user);
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception("Unable to add user");
        //     }
        // }

        public User CreateUser(string email)
        {
            return new User(email);
        }

        public User FindUser(string email)
        {
            return _adapter.GetByEmail(email);
        }

        public void RemoveUser(User user)
        {
            try
            {
                _adapter.Remove(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to remove user");
            }
        }

        public void UpdateUser(User user)
        {
            _adapter.Update(user);
        }
    }
}
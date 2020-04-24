using System;
using System.Collections;
using BlabberApp.Domain;
using BlabberApp.DataStore.Exceptions;

namespace BlabberApp.DataStore
{
    public class UserAdapter
    {
        private readonly IUserPlugin _plugin;

        public UserAdapter(IUserPlugin plugin)
        {
            _plugin = plugin;
        }

        public void Add(User user)
        {
            try
            {
                GetByEmail(user.Email.ToString());
            }
            catch (UserAdapterNotFoundException)
            {
                try
                {
                    _plugin.Create(user);
                    return;
                }
                catch (Exception ex)
                {
                    throw new UserAdapterException(ex.ToString());
                }
            }
            throw new UserAdapterDuplicateException("Email already exists.");
        }

        public void Remove(User user)
        {
            try
            {
                _plugin.Delete(user);
            }
            catch (Exception ex)
            {
                throw new UserAdapterException(ex.ToString());
            }
        }

        public void Update(User user)
        {
            try
            {
                GetByEmail(user.Email); //this makes sure user exists in database before proceeding
                _plugin.Update(user);
            }
            catch (Exception ex)
            {
                throw new UserAdapterException("Unable to update user");
            }
        }

        public IEnumerable GetAll()
        {
            try
            {
                return _plugin.ReadAll();
            }
            catch (Exception ex)
            {
                throw new UserAdapterException(ex.ToString());
            }
        }

        public User GetById(Guid Id)
        {
            try
            {
                User user = (User)_plugin.ReadById(Id);
                return user;
            }
            catch (Exception ex)
            {
                throw new UserAdapterNotFoundException(Id.ToString() + ": Not found.");
            }
        }

        public User GetByEmail(string email)
        {
            try
            {
                User user = (User)_plugin.ReadByUserEmail(email);
                return user;
            }
            catch (Exception ex)
            {
                throw new UserAdapterNotFoundException(email + " not found");
            }
        }
    }
}
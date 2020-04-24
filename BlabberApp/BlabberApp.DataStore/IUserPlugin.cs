using System;
using System.Collections;
using BlabberApp.Domain;

namespace BlabberApp.DataStore
{
    public interface IUserPlugin : IPlugin
    {
        IEntity ReadByUserEmail(string email);
    }
}
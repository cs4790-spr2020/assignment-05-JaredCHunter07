  
using System;

// An Entity is responsible for identity.
namespace BlabberApp.Domain
{
    // Use if single identity type is used across ALL entities.
    public interface IEntity
    {
        Guid Id { get; }
    }
}
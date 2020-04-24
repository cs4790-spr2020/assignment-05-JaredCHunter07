using System;
using System.Collections;
using BlabberApp.Domain;
using BlabberApp.DataStore.Exceptions;

namespace BlabberApp.DataStore
{
    public class BlabAdapter
    {
        private IBlabPlugin plugin;

        public BlabAdapter(IBlabPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Add(Blab blab)
        {
            this.plugin.Create(blab);
        }

        public void Remove(Blab blab)
        {
            this.plugin.Delete(blab);
        }

        public void Update(Blab blab)
        {
            this.plugin.Update(blab);
        }

        public IEnumerable GetAll()
        {
            return this.plugin.ReadAll();
        }

        public Blab GetById(Guid Id)
        {
            try
            {
                Blab blab = (Blab)this.plugin.ReadById(Id);
                return blab;
            }
            catch (Exception ex)
            {
                throw new BlabAdapterNotFoundException(Id.ToString() + ": Not found.");
            }
        }

        public IEnumerable GetByUserId(string Id)
        {
            return this.plugin.ReadByUserId(Id);
        }
    }
}
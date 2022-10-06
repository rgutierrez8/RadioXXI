using RadioXXI.Business.Interfaces;
using RadioXXI.Context;
using RadioXXI.Models;
using RadioXXI.Models.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace RadioXXI.Business
{
    public class CommsBusiness : RepositoryBase<Comms>, ICommsBusiness
    {
        public CommsBusiness(RadioContext context): base(context)
        {

        }

        public Comms findById(int id)
        {
            return FindByCondition(source => source.Id == id).FirstOrDefault();
           
        }

        public Comms findByTitle(string title)
        {
            return FindByCondition(source => source.Title == title).First();
        }

        public ICollection<Comms> findAll()
        {
            return FindAll().ToList();
        }

        public void insert(CommsDto newComms)
        {
            var comm = new Comms()
            {
                Title = newComms.Title,
                Body = newComms.Body
            };

            Create(comm);
            SaveChanges();
        }

        public void update(CommsDto update, int id)
        {
            var comm = findById(id);

            comm.Title = update.Title;
            comm.Body = update.Body;

            Update(comm);
            SaveChanges();
        }

        public void delete(int id)
        {
            var comm = findById(id);

            Delete(comm);
            SaveChanges();
        }
    }
}

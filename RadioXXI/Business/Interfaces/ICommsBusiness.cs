using RadioXXI.Models;
using RadioXXI.Models.Dtos;
using System.Collections.Generic;

namespace RadioXXI.Business.Interfaces
{
    public interface ICommsBusiness
    {
        public Comms findById(int id);
        public Comms findByTitle(string title);
        public ICollection<Comms> findAll();
        public void insert(CommsDto newComms);
        public void update(CommsDto update, int id);
        public void delete(int id);
    }
}

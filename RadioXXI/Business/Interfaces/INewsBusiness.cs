using RadioXXI.Models;
using RadioXXI.Models.Dtos;
using System.Collections.Generic;

namespace RadioXXI.Business.Interfaces
{
    public interface INewsBusiness
    {
        public News findByTitle(string title);
        public News findById(int id);
        public IEnumerable<News> findAll();
        public void insertNews(NewsDto newsInsert);
        public void update(NewsDto update, int id);
        public void delete(int id);
    }
}

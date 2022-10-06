using Microsoft.EntityFrameworkCore;
using RadioXXI.Business.Interfaces;
using RadioXXI.Context;
using RadioXXI.Models;
using RadioXXI.Models.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace RadioXXI.Business
{
    public class NewsBusiness : RepositoryBase<News>, INewsBusiness
    {
        public NewsBusiness(RadioContext repository) : base(repository)
        {
        }

        public News findByTitle(string title)
        {
            return FindByCondition(source => source.Title == title).First();
        }

        public News findById(int id)
        {
            var data = FindByCondition(source => source.Id == id).FirstOrDefault();

            return (News)data;
        }

        public IEnumerable<News> findAll()
        {
            return FindAll().ToList();
        }

        public void insertNews(NewsDto newsInsert)
        {
            var news = new News()
            {
                Title = newsInsert.Title,
                Body = newsInsert.Body,
                Banner = newsInsert.Banner,
            };

            Create(news);
            SaveChanges();
        }

        public void update(NewsDto update, int id)
        {
            var news = findById(id);

            news.Title = update.Title;
            news.Body = update.Body;
            news.Banner = update.Banner;

            Update(news);
            SaveChanges();
        }

        public void delete(int id)
        {
            Delete(findById(id));
            SaveChanges();
        }
    }
}

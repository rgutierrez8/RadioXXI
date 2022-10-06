using RadioXXI.Business.Interfaces;
using RadioXXI.Context;
using RadioXXI.Models;
using RadioXXI.Models.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace RadioXXI.Business
{
    public class PhotoBusiness: RepositoryBase<Photos>, IPhotoBusiness
    {
        public PhotoBusiness(RadioContext repository) : base(repository)
        {

        }


        public Photos findById(int id)
        {
            var photo = FindByCondition(source => source.Id == id).First();

            return photo;
        }
        public IEnumerable<Photos> getPhotos(int type, int id)
        {
            IEnumerable<Photos>? data = null;

            if(type == 1)
            {
                data = FindAll().Where(source => (source.NewsId == id)).ToList();
            }
            else if(type == 2)
            {
                data = FindAll().Where(source => (source.CommsId == id)).ToList();
            }

            return data;
        }

        /*public IEnumerable<Photos> getCommsPhotos(int id)
        {
            var data = FindAll().Where(source => (source.CommsId == id)).ToList();

            return data;
        }*/

        public void insertPhotos(int type, PhotoDto photos, int id)
        {
            int? newsId = null;
            int? commsId = null;

            if(type == 1)
            {
                newsId = id;
            }
            else if(type == 2)
            {
                commsId = id;
            }


            var photo = new Photos()
            {
                Url = photos.Url,
                Description = photos.Description,
                NewsId = newsId,
                CommsId = commsId,
            };

            Create(photo);
            SaveChanges();
        }

        public void delete(int id)
        {
            Delete(findById(id));
            SaveChanges();
        }
    }
}

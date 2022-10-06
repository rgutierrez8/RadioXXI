using RadioXXI.Models;
using RadioXXI.Models.Dtos;
using System.Collections.Generic;

namespace RadioXXI.Business.Interfaces
{
    public interface IPhotoBusiness
    {
        public IEnumerable<Photos> getPhotos(int type, int id);
        //public IEnumerable<Photos> getCommsPhotos(int id);
        public void insertPhotos(int type, PhotoDto photos, int id);
        public void delete(int id);
    }
}

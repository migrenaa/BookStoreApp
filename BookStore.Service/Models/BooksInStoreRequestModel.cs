using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BookStore.Service.Mapping;
using AutoMapper;

namespace BookStore.Service.Models
{
    public class BooksInStoreRequestModel : IMapFrom<BooksInStores>, IMapTo<BooksInStores>
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public int StoreId { get; set; }
        
            

       /* public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<BooksInStoreRequestModel, BooksInStores>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Price, opt => opt.MapFrom(x => x.Price));
        }
        */
    }
}

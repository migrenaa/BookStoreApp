using BookStore.DATA.ADO;
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
    public class BooksInStoresViewModel : IMapFrom<BooksInStore>, IMapTo<BooksInStores>, IHaveCustomMappings
    {
        [Required]
        public string Book { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Store { get; set; }
        

        public void CreateMappings(IMapperConfiguration configuration)
        {
            return;
        }
        
        //?
        /* public void CreateMappings(IMapperConfiguration configuration)
         {
             configuration.CreateMap<BooksInStore, BooksInStoresViewModel>()
                 .ForMember(x => x.Book , opt => opt.MapFrom(x => x.))
                 .ForMember(x => x.Price, opt => opt.MapFrom(x => x.Price));
         }*/
    }
}

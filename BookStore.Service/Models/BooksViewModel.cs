using BookStore.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BookStore.Service.Mapping;
using BookStore.DATA.ADO;
using BookStore.Service.Models;
using AutoMapper;

namespace BookStore.Service
{
    public class BooksViewModel : IMapFrom<Book>, IMapTo<Book>
    {
        public string Name { get; set; }

        [Range(1, 100)]
        public decimal Price { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Book, BooksViewModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Price, opt => opt.MapFrom(x => x.Price));
        }

    }
}

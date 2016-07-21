using BookStore.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BookStore.DATA.ADO;
using BookStore.Service.Models;
using BookStore.Service.Mapping;
using AutoMapper;

namespace BookStore.Service
{
    public class BooksRequestModel : IMapFrom<Book>, IMapTo<Book>, IHaveCustomMappings
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Range(1, 100)]
        public decimal Price { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<BooksRequestModel, Book>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Author, opt => opt.MapFrom(x => x.Author))
                .ForMember(x => x.Price, opt => opt.MapFrom(x => x.Price));

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BookStore.DATA.ADO;
using BookStore.Service.Mapping;
using AutoMapper;

namespace BookStore.Service.Models
{
    public class StoreViewModel : IMapFrom<Store>, IMapTo<Store>
    {
        [Required]
        public string Name { get; set; }

    }
}

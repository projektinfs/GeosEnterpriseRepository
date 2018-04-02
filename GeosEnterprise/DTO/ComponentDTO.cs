using GeosEnterprise.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.DTO
{
    public class ComponentDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ComponentType Type { get; set; }
        public string Description { get; set; }
        public string Producer { get; set; }
        //public List<Photo> Photos { get; set; }
        public decimal? ShoppingPrice { get; set; }
        public decimal? RepairPrice { get; set; }

        public static ComponentDTO ToDTO(DBO.Component entity)
        {
            return new ComponentDTO
            {
                Description = entity.Description,
                ID = entity.ID,
                Name = entity.Name,
                Producer = entity.Producer,
                RepairPrice = entity.RepairPrice,
                ShoppingPrice = entity.ShoppingPrice,
                Type = entity.Type
            };
        }
    }
}

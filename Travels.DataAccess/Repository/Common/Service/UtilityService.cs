using Microsoft.AspNetCore.Mvc.Rendering;
using Travels.DataAccess.Repository.Common.Interface;

namespace Travels.DataAccess.Repository.Common.Service
{
    public class UtilityService : IUtilityService
    {
        public List<SelectListItem> products =
        [
            new SelectListItem() { Text = "Hotel", Value = "1" },
            new SelectListItem() { Text = "Tours", Value = "2" },
            new SelectListItem() { Text = "Transfers", Value = "3" },
            new SelectListItem() { Text = "Attractions", Value = "4" },
            new SelectListItem() { Text = "Packages", Value = "5" },
            new SelectListItem() { Text = "Visa", Value = "6" },
            new SelectListItem() { Text = "Cruise", Value = "7" },
            new SelectListItem() { Text = "Others ", Value = "8" }
        ];

        public async Task<List<SelectListItem>> GetMembercount(bool isAdult)
        {
            List<SelectListItem> item = new();
            for (int i = 0; i < 11; i++)
            {
                if (isAdult && i == 0)
                    continue;
                item.Add(new SelectListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                    Selected = false
                });

            }
            return item;
        }

        public async Task<List<SelectListItem>> GetProductSupplies()
        {
            return products;
        }

        public string GetProductSuppliesName(int Id)
        {
            return products.Where(x => x.Value == Id.ToString()).FirstOrDefault().Text;
        }

        public async Task<List<SelectListItem>> GetSalutation()
        {
            List<SelectListItem> item = new();
            item.Add(new SelectListItem() { Text = "Mr", Value = "Mr" });
            item.Add(new SelectListItem() { Text = "Miss", Value = "Miss" });
            item.Add(new SelectListItem() { Text = "Mrs", Value = "Mrs" });
            return item;
        }
    }
}

using _01_MROQuery.Contracts.Station;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace ServiceHost.Areas.Administration.Pages.Lookups
{
    public class IndexModel : PageModel
    {
        private readonly IStationQuery _stationQuery;

        public IndexModel(IStationQuery stationQuery)
        {
            _stationQuery = stationQuery;
        }

        public void OnGet() { }

        public JsonResult OnGetStations(string term, int take = 30)
        {
            var list = _stationQuery.Search(term, take) ?? new();
            return new JsonResult(list.Select(x => new
            {
                id = x.Id,
                text = x.Text
            }));
        }
    }
}
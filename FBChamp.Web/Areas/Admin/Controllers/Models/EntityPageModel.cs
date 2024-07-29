using FBChamp.Common.Paging;
using FBChamp.Core.DALModels;

namespace FBChamp.Web.Areas.Admin.Controllers.Models
{
    public class EntityPageModel<T>: EntityModel where T: EntityModel
    {
        public PagedList<T> List { get; }
        public string Filter { get; }
        public Guid AssignedToId { get; set; }

        public EntityPageModel(PagedList<T> list, string filter)
        {
            ArgumentNullException.ThrowIfNull(list);
            List = list;
            Filter = filter;
        }

        public EntityPageModel(PagedList<T> list, string filter, Guid currentTeamId = default)
        {
            ArgumentNullException.ThrowIfNull(list);
            List = list;
            Filter = filter;
            AssignedToId = currentTeamId;
        }

        public EntityPageModel()
        {
            List = new PagedList<T>(new List<T>(), 0);
            Filter = "";
            AssignedToId = default;
        }

    }
}

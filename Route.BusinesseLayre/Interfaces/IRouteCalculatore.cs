using System.Threading.Tasks;

namespace Route.BusinesseLayre.Interfaces
{
    public interface IRouteCalculatore
    {
        Task GenneradeRoute(DataBaseLayre.Models.Route route);
    }
}
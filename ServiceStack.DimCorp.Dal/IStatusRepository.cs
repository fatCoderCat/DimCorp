using ServiceStack.DimCorp.Core;

namespace ServiceStack.DimCorp.Dal
{
    public interface IStatusRepository
    {
        Status GetById(int statusId);
    }
}
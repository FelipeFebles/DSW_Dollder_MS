namespace DSW_ApiNoConformidades_Dollder_MS.Core.Database
{
    public interface IDbContextTransactionProxy : IDisposable
    {
        void Commit();
        void Rollback();
    }
}

using TestexErcise.Data;

namespace TestExercise.Data.Repositories.Interfaces
{
    public interface IRepository
    {
        public bool CheckConnectDatabase(ApplicationDbContext context);
    }
}

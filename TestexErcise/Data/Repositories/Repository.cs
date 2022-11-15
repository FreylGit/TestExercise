using TestexErcise.Data;
using TestExercise.Data.Repositories.Interfaces;

namespace TestExercise.Data.Repositories
{
    public class Repository : IRepository
    {
        public bool CheckConnectDatabase(ApplicationDbContext context)
        {
            if (context.Database.CanConnect())
            {
                return true;
            }
            return false;   
        }
    }
}

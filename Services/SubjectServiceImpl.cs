using LMSOnline.Models;

namespace LMSOnline.Services
{
    public class SubjectServiceImpl : SubjectService
    {
        private readonly DatabaseContext databaseContext;
        public SubjectServiceImpl(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public dynamic GetSubject()
        {
            throw new NotImplementedException();
        }
    }
}

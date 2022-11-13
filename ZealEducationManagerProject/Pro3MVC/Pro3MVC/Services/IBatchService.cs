using Pro3MVC.Entities;
using Pro3MVC.Models;

namespace Pro3MVC.Services
{
    public interface IBatchService
    {
        public List<BatchEntity> findAll();
        public Task Add(Batch batchM );
        public Task Update(Batch batchM);
        public Task<BatchEntity> Find(int id);
        public Task<Batch> FindById(int id);
        public Task<bool> Delete(int id);
        public Task<int> AddSubject(BatchSubject batchsubM);
        public List<BatchSubjectEntity> listSubject(int id);

    }
}

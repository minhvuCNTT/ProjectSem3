using Microsoft.EntityFrameworkCore;
using Pro3MVC.Entities;
using Pro3MVC.Models;

namespace Pro3MVC.Services
{
    public class BatchServiceImplement : IBatchService
    {
        private DatabaseContext _db;

        public BatchServiceImplement(DatabaseContext db)
        {
            _db = db;
        }

        public async Task Add(Batch batchM)
        {
            await _db.Batches.AddAsync(batchM);
            await _db.SaveChangesAsync();
        }

        public async Task<int> AddSubject(BatchSubject batchsubM)
        {
            await _db.BatchSubjects.AddAsync(batchsubM);
            await _db.SaveChangesAsync();
            return (int) batchsubM.BatchId;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                _db.Batches.Remove(await FindById(id));
                await _db.SaveChangesAsync();
                return true;
            } catch
            {
                return false;
            }
        }

        public async Task<BatchEntity> Find(int id)
        {
            var batches = await _db.Batches.FindAsync(id);
            return new BatchEntity
            {
                Id = batches.Id,
                Name = batches.Name,
                courseName = batches.Course.Name,
                Status = (bool)batches.Status
            };
        }

        public List<BatchEntity> findAll()
        {
            var batches = _db.Batches.Select(x => new BatchEntity
            {
                Id = x.Id,
                Name = x.Name,
                courseName = x.Course.Name,
                Status = (bool)x.Status
            }).ToList();
            return batches;
        }

        public async Task<Batch> FindById(int id)
        {
            return await _db.Batches.FindAsync(id);
            
        }

        public List<BatchSubjectEntity> listSubject(int id)
        {
            var sub = from bs in _db.BatchSubjects
                      where
                      bs.BatchId == id
                      select new BatchSubjectEntity
                      {
                          Id = bs.Id,
                          BatchId = bs.BatchId,
                          subjectName = bs.Subject.Name,
                          Sessions = bs.Subject.Sessions
                      };
            var list = sub.ToList();
            return list;
        }

        public async Task Update(Batch batchM)
        {
            _db.Entry(batchM).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}

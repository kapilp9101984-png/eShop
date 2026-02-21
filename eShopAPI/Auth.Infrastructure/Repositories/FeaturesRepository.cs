using Auth.Domain.Entity;
using Auth.Domain.Interface;
using Auth.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories
{
    public class FeaturesRepository : IFeatures
    {
        private readonly AuthDbContext _db;

        public FeaturesRepository(AuthDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateFeature(Features feature)
        {
            _db.Features.Add(feature);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFeature(int ID)
        {
            var entity = await _db.Features.FindAsync(ID);
            if(entity == null) return false;
            _db.Features.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Features>> GetAllFeatures()
        {
            return await _db.Features.ToListAsync();
        }

        public async Task<Features> GetFeature(int ID)
        {
            return await _db.Features.FindAsync(ID);
        }

        public async Task<Features> UpdateFeature(Features feature)
        {
            _db.Features.Update(feature);
            await _db.SaveChangesAsync();
            return feature;
        }
    }
}

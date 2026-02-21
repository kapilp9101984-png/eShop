using Auth.Domain.Entity;

namespace Auth.Domain.Interface
{
    public interface IFeatures
    {
        Task<List<Features>> GetAllFeatures();
        Task<Features> GetFeature(int ID);
        Task<bool> CreateFeature(Features feature);
        Task<Features> UpdateFeature(Features feature);
        Task<bool> DeleteFeature(int ID);
    }
}

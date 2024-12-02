using CandidateManagementAPI.Model;
using CandidateManagementAPI.NewFolder;

namespace CandidateManagementAPI.IServices
{
    public interface ICandidateRepository
    {
        Task<int> AddCandidateAsync(CandidateDto candidate);
        Task<int> UpdateCandidateAsync(int id, CandidateDto candidate);
        Task<CandidateDto> GetCandidateByIdAsync(int id);
    }
}

using CandidateManagementAPI.IServices;
using CandidateManagementAPI.Model;
using CandidateManagementAPI.NewFolder;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CandidateManagementAPI.Service
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly CandidateDbContext _context;

        public CandidateRepository(CandidateDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddCandidateAsync(CandidateDto candidate)
        {
           
            
                if (candidate != null)
                {
                    Candidate candidateToAdd = new Candidate()
                    {

                        FirstName = candidate.FirstName,
                        LastName = candidate.LastName,
                        PhoneNumber = candidate.PhoneNumber,
                        Email = candidate.Email,
                        CallInterval = candidate.CallInterval,
                        LinkedInProfile = candidate.LinkedInProfile,
                        GitHubProfile = candidate.GitHubProfile,
                        Comments = candidate.Comments,
                        CreatedAt = DateTime.UtcNow


                    };
                    await _context.Candidates.AddAsync(candidateToAdd);
                    await _context.SaveChangesAsync();

                    return candidateToAdd.Id;
                }



            return 0;


        }

        public async Task<int> UpdateCandidateAsync(int id, CandidateDto candidate)
        {
            
                var existingCandidate = await _context.Candidates.FindAsync(id);
                if (existingCandidate == null)
                    return 0;
                // Update properties
                if (candidate is not null)
                {


                    existingCandidate.FirstName = candidate.FirstName;
                    existingCandidate.LastName = candidate.LastName;
                    existingCandidate.PhoneNumber = candidate.PhoneNumber;
                    existingCandidate.Email = candidate.Email;
                    existingCandidate.CallInterval = candidate.CallInterval;
                    existingCandidate.LinkedInProfile = candidate.LinkedInProfile;
                    existingCandidate.GitHubProfile = candidate.GitHubProfile;
                    existingCandidate.Comments = candidate.Comments;
                    existingCandidate.UpdatedAt = DateTime.UtcNow;

                    _context.Candidates.Update(existingCandidate);
                    await _context.SaveChangesAsync();
                    return existingCandidate.Id;
                }

            return 0;

            }

        public async Task<CandidateDto> GetCandidateByIdAsync(int id)
        {
            Candidate candidate = await _context.Candidates.FindAsync(id);
            if (candidate is not null)
            {
                CandidateDto candidateDto = new CandidateDto()
                {

                    FirstName = candidate.FirstName,
                    LastName = candidate.LastName,
                    PhoneNumber = candidate.PhoneNumber,
                    Email = candidate.Email,
                    CallInterval = candidate.CallInterval,
                    LinkedInProfile = candidate.LinkedInProfile,
                    GitHubProfile = candidate.GitHubProfile,
                    Comments = candidate.Comments,
                    

                };
                return candidateDto; 

            }
            return null; 
        }
    }
}

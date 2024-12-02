using CandidateManagementAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CandidateManagementAPI
{

    public class CandidateDbContext : DbContext
    {
        public CandidateDbContext(DbContextOptions<CandidateDbContext> options) : base(options) { }

        public DbSet<Candidate> Candidates { get; set; }
    }

}

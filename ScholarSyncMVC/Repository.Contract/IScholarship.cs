﻿using ScholarSyncMVC.Models;

namespace ScholarSyncMVC.Repository.Contract
{
    public interface IScholarship:IGenericRepository<Scholarship>
    {
        Task<IEnumerable<Scholarship>> GetAllWithTables();
        Task<Scholarship?> GetByIdInclude(int id);
    }
}
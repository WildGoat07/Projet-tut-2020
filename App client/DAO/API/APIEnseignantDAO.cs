﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAO.API
{
    public class APIEnseignantDAO : IEnseignantDAO
    {
        internal APIEnseignantDAO(HttpClient client)
        {
            Client = client;
        }

        private HttpClient Client { get; }

        public Task<Enseignant[]> CreateAsync(IEnumerable<Enseignant> values)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IEnumerable<Enseignant> values)
        {
            throw new NotImplementedException();
        }

        public Task<Enseignant[]> GetAllAsync(int maxCount, int page)
        {
            throw new NotImplementedException();
        }

        public Task<Enseignant[]> GetByIdAsync(IEnumerable<string> id)
        {
            throw new NotImplementedException();
        }

        public Task<Enseignant[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, string? search = null, IEnumerable<string>? function = null, IEnumerable<int>? comp = null, IEnumerable<char>? CRCT = null, IEnumerable<char>? PesPedr = null, (float?, float?)? forcedHours = null, (float?, float?)? maxHours = null)
        {
            throw new NotImplementedException();
        }

        public Task<Enseignant[]> UpdateAsync(IEnumerable<(Enseignant, Enseignant)> values)
        {
            throw new NotImplementedException();
        }
    }
}
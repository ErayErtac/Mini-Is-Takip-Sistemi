using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MiniIsTakip.Core.Entities;
using MiniIsTakip.Core.Interfaces;

namespace MiniIsTakip.Application.Services
{
    public class PersonService : IPersonService
    {
        // Bu örnek için geçici bir liste kullanıyoruz (veritabanı yok)
        private readonly List<Person> _persons = new();

        public async Task<List<Person>> GetAllAsync()
        {
            return await Task.FromResult(_persons);
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            var person = _persons.FirstOrDefault(p => p.Id == id);
            return await Task.FromResult(person);
        }

        public async Task AddAsync(Person person)
        {
            _persons.Add(person);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Person person)
        {
            var existing = _persons.FirstOrDefault(p => p.Id == person.Id);
            if (existing != null)
            {
                existing.FullName = person.FullName;
                existing.Email = person.Email;
                existing.IsActive = person.IsActive;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var person = _persons.FirstOrDefault(p => p.Id == id);
            if (person != null)
            {
                _persons.Remove(person);
            }
            await Task.CompletedTask;
        }
    }
}

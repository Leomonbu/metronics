using Microsoft.AspNetCore.Identity;
using WSRegistro.DTOs;
using WSRegistro.Models;
using WSRegistro.Repositories;

namespace WSRegistro.Services
{
    public class ProfesorService
    {
        private readonly IProfesorRepositoñry _repo;

        public ProfesorService(IProfesorRepositoñry repo)
        {
            _repo = repo;
        }

        public async Task<List<Profesor>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }
    }
}

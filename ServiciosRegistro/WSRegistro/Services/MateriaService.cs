using WSRegistro.Models;
using WSRegistro.Repositories;

namespace WSRegistro.Services
{
    public class MateriaService
    {
        private readonly IMateriaRepository _repo;

        public MateriaService(IMateriaRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Materia>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }
    }
}

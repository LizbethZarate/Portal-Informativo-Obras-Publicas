using PortalInforObrasPublicas.Data;
using PortalInforObrasPublicas.Interfaces;
using PortalInforObrasPublicas.Models;

namespace PortalInforObrasPublicas.Repositories
{
    public class ObraRepository : IObraRepository
    {
        private readonly AppDbContext _context;

        public ObraRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Obra> ObtenerTodas()
        {
            return _context.Obras.ToList();
        }

        public Obra ObtenerPorId(int id)
        {
            return _context.Obras.Find(id);
        }

        public void Crear(Obra obra)
        {
            _context.Obras.Add(obra);
            _context.SaveChanges();
        }

        public void Actualizar(Obra obra)
        {
            _context.Obras.Update(obra);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var obra = _context.Obras.Find(id);

            if (obra != null)
            {
                _context.Obras.Remove(obra);
                _context.SaveChanges();
            }
        }

        public bool ExisteObra(string nombre)
        {
            return _context.Obras.Any(o => o.Nombre == nombre);
        }
    }
}
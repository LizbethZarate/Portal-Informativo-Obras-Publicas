using PortalInforObrasPublicas.Interfaces;
using PortalInforObrasPublicas.Models;

namespace PortalInforObrasPublicas.Services
{
    public class ObraService
    {
        private readonly IObraRepository _repo;

        public ObraService(IObraRepository repo)
        {
            _repo = repo;
        }

        public List<Obra> ObtenerTodas()
        {
            return _repo.ObtenerTodas();
        }

        public string CrearObra(Obra obra)
        {
            if (_repo.ExisteObra(obra.Nombre))
                return "La obra ya existe.";

            if (obra.Presupuesto < 0)
                return "El presupuesto no puede ser negativo.";

            if (obra.FechaFin < obra.FechaInicio)
                return "La fecha fin no puede ser menor.";

            var estadosValidos = new[]
            {
                "Pendiente",
                "En ejecución",
                "Finalizada"
            };

            if (!estadosValidos.Contains(obra.Estado))
                return "Estado inválido.";

            _repo.Crear(obra);

            return "";
        }

        public Obra ObtenerPorId(int id)
        {
            return _repo.ObtenerPorId(id);
        }

        public string Actualizar(Obra obra)
        {
            if (obra.Presupuesto < 0)
                return "El presupuesto no puede ser negativo.";

            if (obra.FechaFin < obra.FechaInicio)
                return "La fecha fin no puede ser menor.";

            var estadosValidos = new[] { "Pendiente", "En ejecución", "Finalizada" };
            if (!estadosValidos.Contains(obra.Estado))
                return "Estado inválido.";

            _repo.Actualizar(obra);
            return "";
        }

        public void Eliminar(int id)
        {
            _repo.Eliminar(id);
        }

        public string ValidarObra(Obra obra)
        {
            if (obra.Presupuesto < 0)
                return "El presupuesto no puede ser negativo.";

            if (obra.FechaFin < obra.FechaInicio)
                return "La fecha fin no puede ser menor.";

            var estadosValidos = new[]
            {
        "Pendiente",
        "En ejecución",
        "Finalizada"
    };

            if (!estadosValidos.Contains(obra.Estado))
                return "Estado inválido.";

            return "";
        }

        public void Crear(Obra obra)
        {
            _repo.Crear(obra);
        }
    }
}
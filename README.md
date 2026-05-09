# SITOP - Sistema de Información y Transparencia de Obras Públicas

Aplicación web desarrollada en **ASP.NET Core Razor Pages (.NET 8)** para la gestión, visualización y reporte de obras públicas del distrito.

---

## Estructura del Proyecto

- **Controllers/**  
  Controladores MVC que gestionan la lógica de las rutas, acciones y vistas para cada entidad principal (usuarios, obras, reportes).

- **Data/**  
  Contiene el contexto de base de datos (`AppDbContext.cs`) usado por Entity Framework Core para el acceso a datos.

- **Interfaces/**  
  Define las interfaces para los repositorios, promoviendo la inyección de dependencias y el desacoplamiento.

- **Migrations/**  
  Archivos generados por Entity Framework Core para la gestión y versionado del esquema de la base de datos.

- **Models/**  
  Clases que representan las entidades del dominio: Usuario, Obra, Reporte, etc.

- **Repositories/**  
  Implementaciones concretas de los repositorios para el acceso a datos de cada entidad.

- **Services/**  
  Lógica de negocio y servicios de aplicación, como validaciones, reglas y operaciones complejas.

- **Views/**  
  Vistas Razor Pages organizadas por entidad y propósito.  
  - **Shared/**: Layouts y vistas compartidas.
  - **Account/**: Login y registro de usuarios.
  - **Home/**: Página principal, privacidad, historial.
  - **Obra/**, **Reporte/**, **Usuario/**: CRUD y visualización de cada entidad.

- **wwwroot/**  
  Archivos estáticos públicos: hojas de estilo, imágenes, librerías JS/CSS.

- **appsettings.json**  
  Configuración general de la aplicación (cadenas de conexión, parámetros globales, etc).

- **Program.cs**  
  Punto de entrada de la aplicación, configuración de servicios, middlewares y rutas.

---

## Notas de Seguridad

- Las contraseñas se almacenan de forma segura (hasheadas).
- El rol de administrador solo puede ser asignado internamente.

---

## Cómo ejecutar el proyecto

1. Restaura los paquetes y dependencias.
2. Configura la cadena de conexión en `appsettings.json`.
3. Ejecuta las migraciones (`update-database`).
4. Ejecuta el proyecto (`dotnet run` o desde Visual Studio).

---

## Contacto

Proyecto académico desarrollado para la gestión y transparencia de obras públicas.

---
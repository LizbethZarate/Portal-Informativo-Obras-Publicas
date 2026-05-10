document.addEventListener('DOMContentLoaded', () => {
    console.log("Vista de detalle de obra cargada.");

    const btnReportar = document.querySelector('.btn-dark');

    if (btnReportar) {
        btnReportar.addEventListener('click', () => {
            // Aquí puedes redirigir a un formulario o abrir un modal
            alert("Redirigiendo al formulario de reporte de incidencias...");
        });
    }
    
    // Ejemplo por si quieres animar la barra de progreso al cargar
    const progressBar = document.getElementById('progress-bar');
    if (progressBar) {
        // La barra ya tiene el ancho por defecto en el HTML, 
        // pero aquí podrías actualizarla dinámicamente con datos reales.
    }
});
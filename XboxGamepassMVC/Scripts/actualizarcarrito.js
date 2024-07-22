document.addEventListener('DOMContentLoaded', () => {
    const cartCountElements = document.querySelectorAll('#cart-count');

    // Verifica si el localStorage está disponible
    if (typeof(Storage) !== "undefined") {
        // Obtén el valor del contador del carrito desde localStorage y conviértelo a un número
        let cartItemCount = Number(localStorage.getItem('contadorCarro')) || 0;

        // Actualiza el contador del carrito en todas las páginas
        cartCountElements.forEach((element) => {
            element.textContent = cartItemCount;
        });
    } else {
        console.log('Lo siento, tu navegador no soporta Web Storage...');
    }
});

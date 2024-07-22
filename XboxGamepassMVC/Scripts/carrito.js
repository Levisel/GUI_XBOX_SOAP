document.addEventListener('DOMContentLoaded', () => {
    const itemsContainer = document.getElementById('items');
    const contadorCarro = document.getElementById('cart-count'); // Elemento del carrito

    if (itemsContainer === null) {
        console.error("El contenedor de los elementos del carrito no se encontró en el DOM.");
        return;
    }
    // Obtén el valor actual del contador del carrito desde localStorage
    let cartItemCount = parseInt(localStorage.getItem('contadorCarro')) || 0;

    // Verifica si el contador del carrito es negativo y corrige si es necesario
    if (cartItemCount < 0) {
        cartItemCount = 0;
        localStorage.setItem('contadorCarro', cartItemCount);
    }

    // Actualiza el elemento HTML con el valor del contador del carrito
    contadorCarro.textContent = cartItemCount;

    // Lógica para determinar si se ha agregado un tipo de producto al carrito
    let tipoProductoActual = null;
    let idProductoActual = null;

    // Función para mostrar los productos en el carrito
    function mostrarProductos(data) {
        data.forEach((producto) => {
            const productElement = document.createElement('div');
            productElement.classList.add('product');

            const productInfoElement = document.createElement('div');
            productInfoElement.classList.add('product-info');

            const titleElement = document.createElement('h4');
            titleElement.textContent = producto.prd_Nombre;

            const durationElement = document.createElement('h5');
            durationElement.textContent = producto.prd_Duracion;

            const priceElement = document.createElement('p');
            priceElement.classList.add('product-price');
            priceElement.textContent = `$${producto.prd_Precio}`;

            const btnElement = document.createElement('a');
            btnElement.classList.add('product-btn');
            btnElement.classList.add('comprar-button');
            btnElement.textContent = 'Comprar';

            btnElement.addEventListener('click', () => {
                // Verificar si el contador del carrito se ha vuelto negativo y corregir si es necesario
                if (cartItemCount < 0) {
                    cartItemCount = 0;
                    localStorage.setItem('contadorCarro', cartItemCount);
                }

                const carrito = JSON.parse(localStorage.getItem('carrito')) || [];

               // Verificar si el carrito ya tiene un elemento
               if (carrito.length > 0 && !carrito.find(item => item.id === producto.prd_Codigo)) {
                const alertDiv = document.createElement('div');
                alertDiv.classList.add('alert', 'alert-warning', 'alert-dismissible', 'fade', 'show', 'position-fixed', 'bottom-0', 'end-0');
                alertDiv.setAttribute('role', 'alert');
                alertDiv.style.zIndex = 1050; // Ajustar el z-index para que aparezca sobre otros elementos

                alertDiv.innerHTML = `
                    <strong>¡Atención!</strong> Ya tienes un plan en tu carrito. No puedes agregar un plan diferente.
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                `;

                itemsContainer.appendChild(alertDiv);

                // Desaparece después de 3 segundos
                setTimeout(() => {
                    alertDiv.classList.remove('show');
                    setTimeout(() => {
                        alertDiv.remove();
                    }, 1000); // Retraso adicional para la animación de desaparición
                }, 3000);

                return;
            }

                // Verificar si el producto es del mismo tipo y del mismo ID que el último producto agregado al carrito
                if ((tipoProductoActual === null && idProductoActual === null) || (tipoProductoActual === producto.tipo && idProductoActual === producto.prd_Codigo)) {
                    cartItemCount++;
                    contadorCarro.textContent = cartItemCount;

                    const existingItem = carrito.find(existing => existing.id === producto.prd_Codigo);

                    if (existingItem) {
                        existingItem.quantity = (existingItem.quantity || 1) + 1;
                    } else {
                        carrito.push({
                            id: producto.prd_Codigo,
                            title: producto.prd_Nombre,
                            price: producto.prd_Precio,
                            duration: producto.prd_Duracion,
                            quantity: 1,
                            tipo: producto.tipo
                        });
                    }

                    localStorage.setItem('carrito', JSON.stringify(carrito));
                    tipoProductoActual = producto.tipo;
                    idProductoActual = producto.prd_Codigo;

                    // Almacena el nuevo valor en localStorage
                    localStorage.setItem('contadorCarro', cartItemCount);
                    // Emitir un evento personalizado para notificar que el contador del carrito ha cambiado
                    const cartUpdateEvent = new Event('cartUpdated');
                    window.dispatchEvent(cartUpdateEvent);

                } else {
                    alert("Solo puedes agregar un tipo de producto al carrito a la vez.");
                }
            });

            productInfoElement.appendChild(titleElement);
            productInfoElement.appendChild(durationElement);
            productInfoElement.appendChild(priceElement);
            productInfoElement.appendChild(btnElement);

            productElement.appendChild(productInfoElement);

            itemsContainer.appendChild(productElement);
        });
    }

    // Hacer una solicitud AJAX al controlador GUIPlanes para obtener los productos
    fetch('/GUIPlanes/ObtenerProductos')
        .then(response => {
            if (!response.ok) {
                throw new Error('Error al obtener los productos');
            }
            return response.json();
        })
        .then(data => mostrarProductos(data))
        .catch(error => console.error(error.message));
});

document.addEventListener('DOMContentLoaded', () => {
    const cartItemsContainer = document.getElementById('carrito-lista');
    const totalPriceElement = document.getElementById('precio');
    const cartCount = document.getElementById('cart-count'); // Elemento del carrito


    // Función para mostrar un mensaje si el carrito está vacío
    function mostrarMensajeCarritoVacio() {
        if (cartItemsContainer.childElementCount === 0) {
            const emptyCartMessage = document.createElement('p');
            emptyCartMessage.textContent = 'El carrito está vacío... ¡Puedes comprar una suscripción en planes!';
            cartItemsContainer.appendChild(emptyCartMessage);
        }
    }

    // Llama a la función para mostrar el mensaje de carrito vacío después de un breve retraso
    setTimeout(mostrarMensajeCarritoVacio, 10);

    // Cargar y mostrar elementos del carrito desde localStorage
    function mostrarElementosCarrito() {
        const carrito = JSON.parse(localStorage.getItem('carrito')) || [];

        let totalPrice = 0;
        cartItemsContainer.innerHTML = ''; // Limpia los elementos previos del carrito

        carrito.forEach((item, index) => {
            const listItem = document.createElement('li');
            const itemName = document.createElement('span');
            const itemDuration = document.createElement('span');
            const itemPrice = document.createElement('span');
            const itemQuantity = document.createElement('span'); // Agrega este elemento para mostrar la cantidad
            const increaseButton = document.createElement('button');
            const decreaseButton = document.createElement('button');
            const deleteButton = document.createElement('button');
            itemName.textContent = item.title;
            itemDuration.textContent = item.duration;
            itemPrice.textContent = `$${(item.price * (item.quantity || 1)).toFixed(2)}`; // Calcula el precio total
            itemQuantity.textContent = `x ${item.quantity || 1}`; // Muestra la cantidad
            increaseButton.textContent = '+';
            decreaseButton.textContent = '-';
            //Aplicación de estilos
            listItem.classList.add('product-item');
            itemName.classList.add('item-name');
            itemDuration.classList.add('item-name');
            itemPrice.classList.add('item-price');
            itemQuantity.classList.add('item-quantity');
            increaseButton.classList.add('boton-aumentar');
            decreaseButton.classList.add('boton-disminuir');
            deleteButton.classList.add('delete-button');
            // Crea un elemento <i> para el ícono de basurero (Font Awesome)
            const trashIcon = document.createElement('i');
            trashIcon.classList.add('fas', 'fa-trash'); // Clases de Font Awesome

            // Agrega el ícono de basurero al botón de eliminar
            deleteButton.appendChild(trashIcon);

            listItem.appendChild(itemName);
            listItem.appendChild(itemDuration);
            listItem.appendChild(itemPrice);
            listItem.appendChild(itemQuantity); // Agrega la cantidad al elemento del carrito
            listItem.appendChild(increaseButton);
            listItem.appendChild(decreaseButton);
            listItem.appendChild(deleteButton);
            cartItemsContainer.appendChild(listItem);

            increaseButton.addEventListener('click', () => {
                // Asegura que cartItemCount sea un número entero y, si no, inicialízalo en 0
                let cartItemCount = parseInt(localStorage.getItem('contadorCarro'), 10) || 0;

                // Aumentar la cantidad de productos en el carrito
                cartItemCount++;
                cartCount.textContent = cartItemCount;
                // Almacena el nuevo valor en localStorage
                localStorage.setItem('contadorCarro', cartItemCount);
                // Emitir un evento personalizado para notificar que el contador del carrito ha cambiado
                const cartUpdateEvent = new Event('cartUpdated');
                window.dispatchEvent(cartUpdateEvent);

                // Verifica si el producto ya existe en el carrito
                const existingItem = carrito.find(existing => existing.title === item.title);

                if (existingItem) {
                    // Si el producto ya existe, aumenta su cantidad
                    existingItem.quantity = (existingItem.quantity || 1) + 1;
                }

                // Actualiza el carrito en el almacenamiento local
                localStorage.setItem('carrito', JSON.stringify(carrito));

                // Llama a la función para volver a mostrar los elementos del carrito
                mostrarElementosCarrito();
            });

            decreaseButton.addEventListener('click', () => {
                // Disminuye la cantidad del producto en el carrito
                let cartItemCount = parseInt(localStorage.getItem('contadorCarro'), 10) || 0;
                // Aumentar la cantidad de productos en el carrito
                cartItemCount--;
                // Si el contador del carrito es menor que cero, establecerlo en cero
                if (cartItemCount < 0) {
                    cartItemCount = 0;
                }
                cartCount.textContent = cartItemCount;
                // Almacena el nuevo valor en localStorage
                localStorage.setItem('contadorCarro', cartItemCount);
                // Emitir un evento personalizado para notificar que el contador del carrito ha cambiado
                const cartUpdateEvent = new Event('cartUpdated');
                window.dispatchEvent(cartUpdateEvent);

                if (item.quantity && item.quantity > 1) {
                    item.quantity -= 1;
                } else {
                    // Si la cantidad es 1 o menos, elimina el producto del carrito
                    carrito.splice(index, 1);
                    // Crear alerta de Bootstrap
                    const alertDiv = document.createElement('div');
                    alertDiv.classList.add('alert', 'alert-info', 'alert-dismissible', 'fade', 'position-fixed', 'bottom-0', 'end-0', 'show'); // Añadir la clase 'show' para mostrar la alerta
                    alertDiv.setAttribute('role', 'alert');
                    alertDiv.style.zIndex = 1050; // Ajustar el z-index para que aparezca sobre otros elementos
                
                    alertDiv.innerHTML = `
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                        El producto ha sido eliminado exitosamente..
                    `;
                
                    document.body.appendChild(alertDiv);
                
                    // Desaparece después de 3 segundos
                    setTimeout(() => {
                        alertDiv.remove();
                    }, 3000);
                }

                // Actualiza el carrito en el almacenamiento local
                localStorage.setItem('carrito', JSON.stringify(carrito));

                // Llama a la función para volver a mostrar los elementos del carrito
                mostrarElementosCarrito();
                mostrarMensajeCarritoVacio();
            });

            deleteButton.addEventListener('click', () => {
                // Obtiene la cantidad del producto en el carrito
                const productQuantity = item.quantity || 1;
            
                // Disminuye el contador del carrito por la cantidad de productos eliminados
                let cartItemCount = parseInt(localStorage.getItem('contadorCarro'), 10) || 0;
                cartItemCount -= productQuantity;
                // Si el contador del carrito es menor que cero, establecerlo en cero
                if (cartItemCount < 0) {
                    cartItemCount = 0;
                }
                // Actualiza el contador del carrito y el localStorage
                cartCount.textContent = cartItemCount;
                localStorage.setItem('contadorCarro', cartItemCount);
            
                // Elimina el producto del carrito
                carrito.splice(index, 1);
            
                // Actualiza el carrito en el almacenamiento local
                localStorage.setItem('carrito', JSON.stringify(carrito));
            
                // Emitir un evento personalizado para notificar que el contador del carrito ha cambiado
                const cartUpdateEvent = new Event('cartUpdated');
                window.dispatchEvent(cartUpdateEvent);
            
                // Llama a la función para volver a mostrar los elementos del carrito
                mostrarElementosCarrito();
                mostrarMensajeCarritoVacio();
            
                // Crear alerta de Bootstrap
                const alertDiv = document.createElement('div');
                alertDiv.classList.add('alert', 'alert-info', 'alert-dismissible', 'fade', 'position-fixed', 'bottom-0', 'end-0', 'show'); // Añadir la clase 'show' para mostrar la alerta
                alertDiv.setAttribute('role', 'alert');
                alertDiv.style.zIndex = 1050; // Ajustar el z-index para que aparezca sobre otros elementos
            
                alertDiv.innerHTML = `
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    El producto ha sido eliminado exitosamente..
                `;
            
                document.body.appendChild(alertDiv);
            
                // Desaparece después de 3 segundos
                setTimeout(() => {
                    alertDiv.remove();
                }, 3000);
            });
            

            totalPrice += item.price * (item.quantity || 1);
        });

        // Actualiza el precio total en la interfaz
        totalPriceElement.textContent = `Total: $${totalPrice.toFixed(2)}`;

    }

    mostrarElementosCarrito();
    // Escucha el evento personalizado para actualizar el carrito
    window.addEventListener('cartUpdated', mostrarElementosCarrito);
});

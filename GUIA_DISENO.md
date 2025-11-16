# ?? Guía de Diseño - Food Truck System

## Paleta de Colores

La aplicación utiliza una paleta de colores cálida y acogedora que refleja la naturaleza amigable de un Food Truck.

### Colores Principales

| Color | Código HEX | Uso Principal | Vista Previa |
|-------|-----------|---------------|--------------|
| **Terracota** | `#B96F64` | Botones principales, títulos | ![#B96F64](https://via.placeholder.com/50x30/B96F64/B96F64) |
| **Verde Agua** | `#96AFAA` | Navbar, botones secundarios | ![#96AFAA](https://via.placeholder.com/50x30/96AFAA/96AFAA) |
| **Beige Cálido** | `#DAB89F` | Acentos, botones de advertencia | ![#DAB89F](https://via.placeholder.com/50x30/DAB89F/DAB89F) |
| **Gris Cálido** | `#8D8280` | Elementos neutros, footer | ![#8D8280](https://via.placeholder.com/50x30/8D8280/8D8280) |
| **Lavanda** | `#B3BED5` | Información, acentos suaves | ![#B3BED5](https://via.placeholder.com/50x30/B3BED5/B3BED5) |
| **Fondo** | `#f8f5f2` | Fondo general de la página | ![#f8f5f2](https://via.placeholder.com/50x30/f8f5f2/f8f5f2) |

---

## Variables CSS

Todas las variables de color están definidas en `site.css`:

```css
:root {
  --color-principal: #B96F64;      /* Terracota cálido */
  --color-secundario: #96AFAA;     /* Verde agua suave */
  --color-acento: #DAB89F;         /* Beige cálido */
  --color-neutro: #8D8280;         /* Gris cálido */
  --color-claro: #B3BED5;          /* Lavanda suave */
  --color-fondo: #f8f5f2;          /* Beige muy claro */
  --color-texto: #2d2d2d;          /* Gris oscuro para texto */
}
```

---

## Aplicación de Colores

### Navegación (Navbar)
- **Fondo**: Gradiente de `#B96F64` a `#96AFAA`
- **Texto**: Blanco con opacidad
- **Hover**: Fondo blanco semitransparente

### Botones

| Tipo | Colores | Uso |
|------|---------|-----|
| **Primary** | `#B96F64` (Terracota) | Acciones principales |
| **Success** | `#96AFAA` (Verde agua) | Crear, confirmar |
| **Warning** | `#DAB89F` (Beige) | Editar, modificar |
| **Secondary** | `#8D8280` (Gris) | Cancelar, volver |
| **Info** | `#B3BED5` (Lavanda) | Información, detalles |

### Tarjetas (Cards)
- **Fondo**: Blanco
- **Header**: Gradiente de `#96AFAA` a `#B3BED5`
- **Sombra**: Gris cálido con transparencia
- **Hover**: Elevación con sombra más pronunciada

### Tablas
- **Header**: Gradiente de `#8D8280` a `#B96F64`
- **Filas Alternas**: `#96AFAA` al 5% de opacidad
- **Hover**: `#96AFAA` al 10% de opacidad

### Alertas

| Tipo | Color Base | Uso |
|------|-----------|-----|
| **Success** | Verde agua `#96AFAA` | Operaciones exitosas |
| **Danger** | Rojo Bootstrap | Errores |
| **Info** | Lavanda `#B3BED5` | Información general |
| **Warning** | Beige `#DAB89F` | Advertencias |

### Badges (Estados de Pedido)

```css
.badge.bg-warning   /* Pendiente - Amarillo */
.badge.bg-info      /* En Preparación - Azul */
.badge.bg-primary   /* Listo para Recoger - Azul oscuro */
.badge.bg-success   /* Entregado - Verde */
.badge.bg-danger    /* Cancelado - Rojo */
```

---

## Efectos y Animaciones

### Transiciones
- **Botones**: Transform + Box-shadow (0.3s)
- **Tarjetas**: Transform translateY (0.3s)
- **Enlaces**: Color + background (0.3s)

### Hover Effects
```css
/* Botones */
transform: translateY(-2px);
box-shadow: 0 4px 12px rgba(185, 111, 100, 0.4);

/* Tarjetas */
transform: translateY(-5px);
box-shadow: 0 8px 20px rgba(0,0,0,0.12);

/* Iconos */
transform: scale(1.1);
```

### Animación de Entrada
```css
@keyframes fadeIn {
  from {
    opacity: 0;
    transform: translateY(20px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}
```

---

## Tipografía

### Fuente Principal
```css
font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
```

### Tamaños
- **Títulos principales**: `display-4` (personalizado con color terracota)
- **Subtítulos**: `lead` (color gris cálido)
- **Texto normal**: 14px base, 16px en pantallas grandes

### Pesos
- **Negrita**: Headers de tarjetas, labels, botones (600)
- **Normal**: Texto general (400)

---

## Iconos

### Librería
Bootstrap Icons v1.11.0

### Iconos Principales Usados
```html
<i class="bi bi-truck"></i>          <!-- Logo -->
<i class="bi bi-house-door"></i>     <!-- Inicio -->
<i class="bi bi-menu-button-wide"></i> <!-- Menú -->
<i class="bi bi-cart-plus"></i>      <!-- Nuevo Pedido -->
<i class="bi bi-list-check"></i>     <!-- Pedidos -->
<i class="bi bi-heart-fill"></i>     <!-- Footer -->
```

---

## Diseño Responsivo

### Breakpoints
```css
/* Móvil */
@media (max-width: 768px) {
  .card { margin-bottom: 20px; }
  .btn { width: 100%; margin-bottom: 10px; }
}

/* Tablet y Desktop */
@media (min-width: 768px) {
  html { font-size: 16px; }
}
```

---

## Sombras y Profundidad

### Niveles de Elevación
```css
/* Bajo */
box-shadow: 0 2px 8px rgba(0,0,0,0.08);

/* Medio */
box-shadow: 0 4px 12px rgba(0,0,0,0.08);

/* Alto (Hover) */
box-shadow: 0 8px 20px rgba(0,0,0,0.12);

/* Botones con color */
box-shadow: 0 4px 12px rgba(185, 111, 100, 0.4);
```

---

## Gradientes

### Navbar y Footer
```css
background: linear-gradient(90deg, #B96F64 0%, #96AFAA 100%);
```

### Header de Tarjetas
```css
background: linear-gradient(135deg, #96AFAA 0%, #B3BED5 100%);
```

### Botones Primary
```css
background: linear-gradient(135deg, #B96F64 0%, #c77f73 100%);
```

### Fondo de Página
```css
background: linear-gradient(135deg, #f8f5f2 0%, #ffffff 100%);
```

---

## Accesibilidad

### Contraste de Texto
- Texto principal: `#2d2d2d` sobre fondos claros
- Texto en botones oscuros: `#ffffff`
- Links: Color base con hover más brillante

### Focus States
```css
box-shadow: 0 0 0 0.1rem white, 0 0 0 0.25rem #96AFAA;
```

---

## Cómo Usar los Colores

### En HTML
```html
<div style="color: var(--color-principal);">Texto terracota</div>
<div style="background: var(--color-secundario);">Fondo verde agua</div>
```

### En CSS
```css
.mi-elemento {
  background-color: var(--color-principal);
  border: 2px solid var(--color-secundario);
}
```

---

## Personalización Futura

Para cambiar la paleta de colores completa, solo modifica las variables en `site.css`:

```css
:root {
  --color-principal: #TU_COLOR_AQUI;
  --color-secundario: #TU_COLOR_AQUI;
  /* etc... */
}
```

Todos los componentes se actualizarán automáticamente.

---

**Diseño creado para el Sistema Food Truck - 2025**

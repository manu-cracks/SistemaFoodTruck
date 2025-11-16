# ?? Actualización de Moneda a Soles Peruanos (S/)

## ? Cambios Realizados

El sistema ha sido actualizado para mostrar todos los precios en **Soles Peruanos (S/)** en lugar de dólares.

---

## ?? Actualizar Base de Datos Existente

Si ya tienes la base de datos creada con los precios anteriores, sigue estos pasos:

### Opción 1: Crear Nueva Migración (Recomendado)

En **Package Manager Console** (con Infraestructura seleccionado):

```powershell
# Crear migración para actualizar precios
Add-Migration ActualizarPreciosASoles -Context FoodTruckDbContext

# Aplicar la migración
Update-Database -Context FoodTruckDbContext
```

### Opción 2: Actualizar Manualmente con SQL

Si prefieres, puedes ejecutar este script SQL directamente:

```sql
-- Actualizar precios de productos a Soles Peruanos
UPDATE Productos SET Precio = 25.00 WHERE Id = 1;  -- Hamburguesa Clásica
UPDATE Productos SET Precio = 18.00 WHERE Id = 2;  -- Hot Dog Premium
UPDATE Productos SET Precio = 10.00 WHERE Id = 3;  -- Papas Fritas
UPDATE Productos SET Precio = 5.00  WHERE Id = 4;  -- Refresco
UPDATE Productos SET Precio = 12.00 WHERE Id = 5;  -- Taco Mexicano
```

**Cómo ejecutar el script:**
1. Abrir **SQL Server Object Explorer** en Visual Studio
2. Clic derecho en `FoodTruckDb` ? **New Query**
3. Pegar el script SQL
4. Ejecutar (Ctrl+Shift+E)

### Opción 3: Recrear la Base de Datos

Si quieres empezar de cero con los nuevos precios:

```powershell
# Eliminar la base de datos actual
Drop-Database -Context FoodTruckDbContext

# Volver a crear con los nuevos precios
Update-Database -Context FoodTruckDbContext
```

---

## ?? Nuevos Precios en Soles (S/)

### Conversión Aproximada (1 USD ? 3.70 PEN)

| Producto | Precio Anterior | Precio Nuevo | Diferencia |
|----------|----------------|--------------|------------|
| Hamburguesa Clásica | $8.99 | **S/ 25.00** | ~S/ 33 PEN |
| Hot Dog Premium | $5.99 | **S/ 18.00** | ~S/ 22 PEN |
| Papas Fritas | $3.50 | **S/ 10.00** | ~S/ 13 PEN |
| Refresco | $2.00 | **S/ 5.00** | ~S/ 7.50 PEN |
| Taco Mexicano | $3.99 | **S/ 12.00** | ~S/ 15 PEN |

> **Nota:** Los precios han sido redondeados a valores más comerciales típicos en Perú.

---

## ?? Cambios en la Interfaz

Todos los lugares donde se muestran precios ahora usan **S/** en lugar de **$**:

### Vistas Actualizadas
- ? `Productos/Index.cshtml` - Lista de productos
- ? `Productos/Delete.cshtml` - Confirmación de eliminación
- ? `Pedidos/Create.cshtml` - Crear pedido
- ? `Pedidos/Pendientes.cshtml` - Lista de pedidos
- ? `Pedidos/Details.cshtml` - Detalles del pedido

### Controladores Actualizados
- ? `PedidosController.cs` - Mensaje de confirmación

### Datos Semilla (Seed Data)
- ? `FoodTruckDbContext.cs` - Precios iniciales actualizados

---

## ?? Verificar los Cambios

### 1. Verificar en la Base de Datos

```sql
SELECT Id, Nombre, Precio, Categoria FROM Productos;
```

Deberías ver:
```
1 | Hamburguesa Clásica | 25.00 | Hamburguesas
2 | Hot Dog Premium     | 18.00 | Hot Dogs
3 | Papas Fritas        | 10.00 | Acompañamientos
4 | Refresco            |  5.00 | Bebidas
5 | Taco Mexicano       | 12.00 | Tacos
```

### 2. Verificar en la Aplicación Web

1. Ejecutar la aplicación (F5)
2. Ir a **Gestión de Menú**
3. Verificar que los precios muestren **S/** en lugar de **$**

### 3. Crear un Pedido de Prueba

1. Ir a **Nuevo Pedido**
2. Seleccionar productos
3. Verificar que el total se calcule en Soles
4. Confirmar que el mensaje diga: "Total: S/ XX.XX"

---

## ?? Formato de Precios

El formato utilizado es:
```csharp
S/ @precio.ToString("F2")
```

Esto muestra:
- **S/** como símbolo de moneda
- Dos decimales siempre (ejemplo: S/ 25.00)
- Separador de miles para cantidades grandes

### Ejemplos:
```
S/ 5.00
S/ 25.00
S/ 125.50
S/ 1,250.00
```

---

## ?? Cálculo de Totales

Los totales de pedidos se calculan automáticamente:

```csharp
// Ejemplo de pedido:
Hamburguesa Clásica (2) = S/ 25.00 × 2 = S/ 50.00
Papas Fritas (1)        = S/ 10.00 × 1 = S/ 10.00
Refresco (2)            = S/  5.00 × 2 = S/ 10.00
                                Total = S/ 70.00
```

---

## ?? Personalizar Precios

Si quieres ajustar los precios a otros valores:

### Desde la Aplicación Web
1. Ir a **Gestión de Menú**
2. Clic en **Editar** del producto
3. Cambiar el precio
4. Guardar cambios

### Desde la Base de Datos
```sql
UPDATE Productos 
SET Precio = 30.00 
WHERE Id = 1;
```

### Desde el Código (Seed Data)
Editar `FoodTruckDbContext.cs`:
```csharp
new Producto { 
    Id = 1, 
    Nombre = "Hamburguesa Clásica", 
    Precio = 30.00m,  // ? Cambiar aquí
    Categoria = "Hamburguesas" 
}
```

---

## ?? Soporte Multi-Moneda (Futuro)

Si en el futuro necesitas soportar múltiples monedas:

1. Agregar campo `Moneda` a la entidad `Producto`
2. Crear tabla de tipos de cambio
3. Implementar conversión automática
4. Permitir al usuario seleccionar su moneda preferida

---

## ? Verificación Final

Antes de ejecutar en producción, verifica:

- [ ] Migración ejecutada correctamente
- [ ] Precios actualizados en la BD
- [ ] Interfaz muestra **S/** correctamente
- [ ] Cálculos de totales son correctos
- [ ] Mensajes de confirmación usan S/
- [ ] Formularios muestran precios en Soles

---

## ?? Ejecutar Actualización Completa

```powershell
# 1. Limpiar y reconstruir
dotnet clean
dotnet build

# 2. Crear nueva migración
Add-Migration ActualizarPreciosASoles -Context FoodTruckDbContext

# 3. Aplicar cambios
Update-Database -Context FoodTruckDbContext

# 4. Ejecutar aplicación
dotnet run --project Presentacion
```

---

**¡Actualización a Soles Peruanos completada!** ??????

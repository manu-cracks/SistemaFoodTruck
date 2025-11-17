#  Sistema de Gestión Food Truck

Sistema de gestión de pedidos y reservas en línea para un negocio Food Truck, desarrollado con **Clean Architecture** en **.NET 9** y **ASP.NET Core MVC**.

## ?? Descripción del Proyecto

Este proyecto implementa un sistema completo de gestión para un Food Truck que permite:
- **Gestión de Menú**: Crear, editar y eliminar productos
- **Procesamiento de Pedidos**: Los clientes pueden realizar pedidos en línea
- **Gestión de Estados**: Actualizar el estado de los pedidos (Pendiente, En Preparación, Listo para Recoger, Entregado, Cancelado)
- **Visualización**: Interfaz administrativa para gestionar pedidos pendientes

## ??? Arquitectura del Proyecto

El proyecto sigue los principios de **Clean Architecture** dividido en 4 capas:

```
SistemasResevaComida/

 Dominio/                    # Capa de Dominio (Entidades y Reglas de Negocio)
    Entidades/
        Producto.cs
        Cliente.cs
       Pedido.cs
       DetallePedido.cs
       PedidoEstado.cs

 Aplicacion/                 # Capa de Aplicación (Casos de Uso - CQRS)
    Contratos/
       Persistencia/       # Interfaces de Repositorios
    DTOs/                   # Data Transfer Objects
    Features/               # Casos de Uso (Commands y Queries)
        Productos/
           Commands/
           Queries/
       Pedidos/
            Commands/
           Queries/

 Infraestructura/            # Capa de Infraestructura (Implementaciones)
    Persistencia/
      FoodTruckDbContext.cs
    Repositorios/
       ProductoRepository.cs
      ClienteRepository.cs
       PedidoRepository.cs
       UnitOfWork.cs

 Presentacion/               # Capa de Presentación (ASP.NET Core MVC)
     Controllers/
       ProductosController.cs
       PedidosController.cs
     Views/
        ??? Productos/
        ??? Pedidos/
```

## ?? Principios de Clean Architecture Aplicados

### Reglas de Dependencia
-  **Dominio**: No tiene dependencias de ninguna otra capa
-  **Aplicacion**: Solo depende de **Dominio**
-  **Infraestructura**: Depende de **Aplicacion** (implementa las interfaces)
-  **Presentacion**: Depende de **Aplicacion** e **Infraestructura**

### Patrones Implementados
- **CQRS** (Command Query Responsibility Segregation) con **MediatR**
- **Repository Pattern** para abstracción de acceso a datos
- **Unit of Work** para manejo de transacciones
- **Dependency Injection** para inversión de control
- **DTOs** para transferencia de datos entre capas

## ??? Tecnologías Utilizadas

- **.NET 9**
- **ASP.NET Core MVC 9**
- **Entity Framework Core 9.0.11**
- **SQL Server Express / LocalDB**
- **MediatR 9.0.0** (Patrón CQRS)
- **FluentValidation 12.1.0** (Validación de Comandos)
- **Bootstrap 5** (Interfaz de Usuario)

## ?? Paquetes NuGet Instalados

### Aplicacion
```xml
<PackageReference Include="MediatR.Extensions.Microsoft.DependencyInjection" Version="9.0.0" />
<PackageReference Include="FluentValidation" Version="12.1.0" />
<PackageReference Include="FluentValidation.DependencyInjectionExtensions" Version="12.1.0" />
```

### Infraestructura
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.11" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.11" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.11" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="9.0.11" />
```

### Presentacion
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="9.0.11" />
```

## ?? Instrucciones de Instalación y Ejecución

### Prerequisitos
- Visual Studio 2022 o superior
- .NET 9 SDK
- SQL Server Express o SQL Server LocalDB

### Paso 1: Clonar o Descargar el Proyecto
```bash
# Si está en un repositorio Git
git clone <url-del-repositorio>
cd SistemasResevaComida
```

### Paso 2: Restaurar Paquetes NuGet
Abrir la solución en Visual Studio y ejecutar:
```bash
dotnet restore
```

### Paso 3: Configurar la Cadena de Conexión
El archivo `Presentacion/appsettings.json` ya está configurado con:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=FoodTruckDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

Si usas SQL Server Express, modifica la cadena a:
```json
"DefaultConnection": "Server=.\\SQLEXPRESS;Database=FoodTruckDb;Trusted_Connection=true;MultipleActiveResultSets=true"
```

### Paso 4: Crear la Base de Datos (MIGRACIONES)

#### Opción A: Usando Package Manager Console (Recomendado)
1. En Visual Studio, ir a **Tools ? NuGet Package Manager ? Package Manager Console**
2. Seleccionar el proyecto **Infraestructura** en el dropdown "Default project"
3. Ejecutar los siguientes comandos:

```powershell
Add-Migration InitialCreate -Context FoodTruckDbContext
Update-Database -Context FoodTruckDbContext
```

#### Opción B: Usando .NET CLI
Desde la carpeta raíz del proyecto:
```bash
dotnet ef migrations add InitialCreate --project Infraestructura --startup-project Presentacion --context FoodTruckDbContext
dotnet ef database update --project Infraestructura --startup-project Presentacion --context FoodTruckDbContext
```

### Paso 5: Ejecutar la Aplicación
1. Configurar **Presentacion** como proyecto de inicio
2. Presionar **F5** o hacer clic en el botón de ejecución
3. La aplicación se abrirá en el navegador (por defecto en `https://localhost:xxxxx`)

## Datos de Prueba (Seed Data)

La base de datos se crea con datos iniciales:

**Productos:**
- Hamburguesa Clásica - s/8.99
- Hot Dog Premium - s/5.99
- Papas Fritas - $3.50
- Refresco - s/2.00
- Taco Mexicano - s/3.99

**Cliente de Ejemplo:**
- Nombre: Cliente Demo
- Email: demo@foodtruck.com
- Teléfono: 555-1234

##  Uso del Sistema

### Para Clientes (Realizar Pedidos)
1. Ir a **"Realizar Pedido"** desde la página principal
2. Seleccionar un cliente
3. Elegir productos y cantidades
4. Hacer clic en **"Crear Pedido"**

### Para Administradores

#### Gestión de Productos
1. Ir a **"Gestión de Menú"**
2. Opciones disponibles:
   - Crear nuevo producto
   - Editar producto existente
   - Eliminar producto
   - Ver detalles del producto

#### Gestión de Pedidos
1. Ir a **"Pedidos Pendientes"**
2. Ver lista de pedidos activos
3. Actualizar estado de pedidos:
   - Pendiente
   - En Preparación
   - Listo para Recoger
   - Entregado
   - Cancelado

##  Estructura de Casos de Uso (CQRS)

### Commands (Modifican el Estado)
- `CrearProductoCommand` - Crear nuevo producto
- `ActualizarProductoCommand` - Actualizar producto existente
- `CrearPedidoCommand` - Crear pedido con transacción
- `ActualizarEstadoPedidoCommand` - Cambiar estado del pedido

### Queries (Solo Lectura)
- `ObtenerProductoPorIdQuery` - Obtener producto específico
- `ObtenerPedidosPendientesQuery` - Listar pedidos pendientes

##  Validaciones Implementadas

### Producto
- Nombre: Obligatorio, máximo 100 caracteres
- Precio: Mayor a 0
- Descripción: Máximo 500 caracteres (opcional)
- Categoría: Máximo 50 caracteres (opcional)

### Pedido
- ClienteId: Debe ser válido y existir
- Detalles: Al menos un producto
- Cantidad: Mayor a 0 por producto

##  Solución de Problemas

### Error: "No se puede conectar a la base de datos"
- Verificar que SQL Server LocalDB o Express esté instalado
- Verificar la cadena de conexión en `appsettings.json`
- Ejecutar `sqllocaldb info` para verificar instancias disponibles

### Error: "Tabla no existe"
- Asegurarse de haber ejecutado las migraciones: `Update-Database`

### Error: "No se puede resolver el servicio..."
- Verificar que todos los servicios estén registrados en `Program.cs`
- Verificar referencias de proyecto

##  Notas Adicionales

- La aplicación utiliza **SQL Server LocalDB** por defecto (incluido con Visual Studio)
- Los pedidos se crean con transacciones para garantizar integridad de datos
- La interfaz está diseñada con **Bootstrap 5** para ser responsiva
- Todas las operaciones de base de datos son **asíncronas**

##  Autor

Sistema desarrollado siguiendo principios de Clean Architecture y mejores prácticas de desarrollo en .NET.

##  Licencia

Este proyecto es de uso educativo y demostrativo.

---

**¡Gracias por usar el Sistema de Gestión Food Truck!** 

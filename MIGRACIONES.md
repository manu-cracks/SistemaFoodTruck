# ??? Guía de Migraciones de Base de Datos

## Ejecutar Migraciones desde Visual Studio (Package Manager Console)

### Paso 1: Abrir Package Manager Console
1. En Visual Studio, ir a **Tools** ? **NuGet Package Manager** ? **Package Manager Console**
2. La consola se abrirá en la parte inferior de Visual Studio

### Paso 2: Seleccionar el Proyecto Infraestructura
En el dropdown **"Default project"** de la consola, seleccionar: **Infraestructura**

![Package Manager Console](https://docs.microsoft.com/en-us/ef/core/cli/powershell-console.png)

### Paso 3: Crear la Migración Inicial
Ejecutar el siguiente comando en la Package Manager Console:

```powershell
Add-Migration InitialCreate -Context FoodTruckDbContext
```

**Resultado esperado:**
```
Build started...
Build succeeded.
To undo this action, use Remove-Migration.
```

Esto creará una carpeta `Migrations` en el proyecto **Infraestructura** con archivos de migración.

### Paso 4: Aplicar la Migración a la Base de Datos
Ejecutar el siguiente comando:

```powershell
Update-Database -Context FoodTruckDbContext
```

**Resultado esperado:**
```
Build started...
Build succeeded.
Applying migration '20250114xxxxx_InitialCreate'.
Done.
```

### Paso 5: Verificar la Base de Datos
1. Abrir **SQL Server Object Explorer** en Visual Studio (View ? SQL Server Object Explorer)
2. Expandir **(localdb)\MSSQLLocalDB** ? **Databases** ? **FoodTruckDb**
3. Verificar que las tablas se hayan creado:
   - Clientes
   - Productos
   - Pedidos
   - DetallesPedido

## Comandos Útiles de Migraciones

### Ver lista de migraciones aplicadas
```powershell
Get-Migration
```

### Revertir la última migración
```powershell
Update-Database -Migration 0
```

### Eliminar la última migración (antes de aplicarla)
```powershell
Remove-Migration
```

### Generar script SQL de migración
```powershell
Script-Migration -From 0 -To InitialCreate
```

## Alternativa: Usar .NET CLI (Si dotnet-ef está instalado)

Si tienes instalado `dotnet-ef`, puedes usar estos comandos desde la línea de comandos en la raíz del proyecto:

```bash
# Crear migración
dotnet ef migrations add InitialCreate --project Infraestructura --startup-project Presentacion --context FoodTruckDbContext

# Aplicar migración
dotnet ef database update --project Infraestructura --startup-project Presentacion --context FoodTruckDbContext

# Listar migraciones
dotnet ef migrations list --project Infraestructura --startup-project Presentacion

# Eliminar base de datos
dotnet ef database drop --project Infraestructura --startup-project Presentacion --force
```

### Instalar dotnet-ef (si es necesario)
```bash
dotnet tool install --global dotnet-ef --version 9.0.0
```

## Solución de Problemas

### Error: "Build failed"
- Asegúrate de que el proyecto compile correctamente: Build ? Build Solution (Ctrl+Shift+B)
- Verifica que todas las referencias de NuGet estén restauradas

### Error: "Unable to create an object of type 'FoodTruckDbContext'"
- Verifica que la cadena de conexión esté correctamente configurada en `appsettings.json`
- Asegúrate de que el proyecto **Presentacion** sea el proyecto de inicio

### Error: "A network-related or instance-specific error occurred"
- Verifica que SQL Server LocalDB esté instalado
- Ejecuta `sqllocaldb info` para ver instancias disponibles
- Ejecuta `sqllocaldb start mssqllocaldb` si la instancia no está iniciada

### Error: "Cannot open database ... requested by the login"
- Verifica que la cadena de conexión tenga permisos correctos
- Intenta usar una instancia diferente de SQL Server

## Cadenas de Conexión Alternativas

### Para SQL Server Express
```json
"DefaultConnection": "Server=.\\SQLEXPRESS;Database=FoodTruckDb;Trusted_Connection=true;MultipleActiveResultSets=true"
```

### Para SQL Server Local con autenticación
```json
"DefaultConnection": "Server=localhost;Database=FoodTruckDb;User Id=tu_usuario;Password=tu_password;MultipleActiveResultSets=true;TrustServerCertificate=true"
```

### Para SQL Server en Azure
```json
"DefaultConnection": "Server=tcp:tu-servidor.database.windows.net,1433;Database=FoodTruckDb;User ID=tu_usuario;Password=tu_password;Encrypt=true;TrustServerCertificate=false;Connection Timeout=30;"
```

## Datos de Prueba (Seed Data)

La migración incluye datos iniciales automáticos:

### Productos (Precios en Soles Peruanos - S/)
- Hamburguesa Clásica (S/ 25.00)
- Hot Dog Premium (S/ 18.00)
- Papas Fritas (S/ 10.00)
- Refresco (S/ 5.00)
- Taco Mexicano (S/ 12.00)

### Cliente
- Cliente Demo (demo@foodtruck.com)

Estos datos se insertan automáticamente al ejecutar `Update-Database`.

---

**¡Después de ejecutar las migraciones, el sistema estará listo para usar!** ??

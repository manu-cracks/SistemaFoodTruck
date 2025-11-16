# ?? Comandos de Inicio Rápido

## Para el Usuario: Ejecuta estos comandos en orden

### 1?? Abrir Package Manager Console
En Visual Studio: **Tools** ? **NuGet Package Manager** ? **Package Manager Console**

### 2?? Seleccionar Proyecto
En el dropdown "Default project", seleccionar: **Infraestructura**

### 3?? Crear Migración
```powershell
Add-Migration InitialCreate -Context FoodTruckDbContext
```

### 4?? Crear Base de Datos
```powershell
Update-Database -Context FoodTruckDbContext
```

### 5?? Ejecutar Aplicación
Presionar **F5** o hacer clic en el botón ? de Visual Studio

---

## ?? ¡Listo!

El sistema está funcionando en: `https://localhost:xxxxx`

### Primeros Pasos:
1. Ver productos iniciales: **Gestión de Menú**
2. Crear un pedido: **Realizar Pedido**
3. Gestionar pedidos: **Pedidos Pendientes**

---

## ?? Más Ayuda

- **Problemas con migraciones:** Ver `MIGRACIONES.md`
- **Guía completa:** Ver `README.md`
- **Verificar completitud:** Ver `CHECKLIST.md`
- **Resumen del proyecto:** Ver `RESUMEN_EJECUTIVO.md`

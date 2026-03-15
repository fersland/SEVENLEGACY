===========================================================
PROYECTO: SEVENLEGACY
DESARROLLADOR: Fernando Reyes
TECNOLOGÍAS: ASP.NET WebForms (.NET 4.7.2), SQL Server, C#, jQuery
===========================================================

Siga estos pasos para ejecutar la aplicación correctamente:

1. CONFIGURACIÓN DE LA BASE DE DATOS
------------------------------------
- Abra el archivo "script_PruebaSeven.sql" en SSMS.
- Ejecutar el script completo.
- Este script creará automáticamente la base de datos 'PruebaSeven', 
  las tablas necesarias, datos para las pruebas y store procdure.

2. CONFIGURACIÓN DE LA CONEXIÓN
-------------------------------
- Abra la solución (.sln) en Visual Studio.
- Diríjase al archivo "Web.config" del proyecto SEVENLEGACY.UI.
- En la sección <connectionStrings>, verifique que el 'Data Source' 
  coincida con el nombre de su instancia local de SQL Server.
  (Ej: Data Source=TU_SERVIDOR\SQLEXPRESS)

3. EJECUCIÓN DEL PROYECTO
-------------------------
- Establezca el proyecto 'SEVENLEGACY.UI' como proyecto de inicio.
- Presione F5 para ejecutar el sistema.

4. CREDENCIALES DE ACCESO
-------------------------
Para ingresar al sistema, puede utilizar:
- Usuario: admin    | Clave: 1234
- Usuario: fernando | Clave: seven2026

===========================================================

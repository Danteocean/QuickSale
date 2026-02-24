Este documento detalla la colaboración técnica entre el desarrollador y la IA durante la construcción del ecosistema QuickSale, destacando la resolución de errores de compatibilidad y la definición de la arquitectura del sistema.

🛠️ Herramientas Usadas
Frontend: Angular 6 (Legacy Framework).

Backend: ASP.NET Web API (.NET Framework 4.7).

Base de Datos: SQL Server (Arquitectura Relacional).

Estilos: Bootstrap 4, FontAwesome 5 y CSS3 Personalizado.

Runtime: Node.js (Configurado con OpenSSL Legacy Provider).

💡 Prompts Profesionales (Resolución de Consultas y Errores)
Optimización de UI/UX: "Reestructurar el componente de registro de ventas para implementar un diseño de doble columna (Side-by-Side), manteniendo la integridad de las variables de plantilla actuales pero mejorando la experiencia visual del usuario."

Lógica de Navegación Post-Acción: "Implementar la redirección programática tras la persistencia exitosa de la venta, utilizando el ID devuelto por el servicio para transicionar el estado de la aplicación hacia la vista de detalle."

Depuración de Entorno de Compilación: "Resolver el error de ejecución ERR_OSSL_EVP_UNSUPPORTED derivado de la incompatibilidad entre el motor de Webpack de Angular 6 y las versiones recientes de Node.js."

Diagnóstico de Renderizado de Estilos: "Analizar la jerarquía de carga de estilos para identificar por qué las utilidades de Flexbox (Bootstrap) no están siendo interpretadas correctamente en el DOM principal."

Modelado de Persistencia: "Diseñar la estructura del objeto de transferencia de datos (DTO) para la creación de ventas, asegurando que la jerarquía de 'Venta-Detalle' se mapee correctamente hacia las tablas relacionales en SQL Server para mantener la integridad referencial."

⚖️ Decisiones de Arquitectura y Correcciones Críticas
Durante el desarrollo, se tomaron decisiones determinantes para estabilizar la solución frente a sugerencias de la IA que no se alineaban con el stack tecnológico:

1. Persistencia y Arquitectura del Backend (.NET 4.7)
Contexto: Se discutió la forma de retornar la información tras la creación de una venta para mejorar la fluidez de la app.

Decisión del Desarrollador: Se rechazó el uso de retornos genéricos de éxito (HTTP 200). Se decidió estructurar el Backend para que, tras ejecutar el INSERT y obtener el SCOPE_IDENTITY() en SQL Server, el API devuelva un objeto con el saleId real. Esto permite que el Frontend realice una redirección dinámica inmediata, optimizando el flujo de trabajo del usuario sin recargas innecesarias.

2. Control de Versiones y Stack Legacy (Angular 6)
Contexto: La IA sugirió utilizar inject() para la gestión de dependencias, una característica de versiones modernas de Angular (v14+).

Decisión del Desarrollador: El usuario rechazó tajantemente la sugerencia y exigió mantener la arquitectura de Inyección de Dependencias vía Constructor. Esta decisión fue fundamental para evitar errores de compilación y mantener la coherencia con la versión 6 del framework, demostrando dominio sobre el ciclo de vida de aplicaciones legacy.

3. Integración de Base de Datos y Seguridad de Node
Contexto: El proyecto presentó un bloqueo crítico en el servidor de desarrollo (10% building modules) debido a la actualización de los algoritmos de cifrado en las versiones nuevas de Node.js.

Decisión del Desarrollador: En lugar de degradar la versión de Node del sistema o reinstalar el entorno, el desarrollador instruyó configurar la bandera --openssl-legacy-provider. Esta decisión técnica permitió mantener el entorno de desarrollo actualizado sin sacrificar la compatibilidad con el motor de Webpack de Angular 6.
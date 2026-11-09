# Universidad Tecnológica de Panamá

# Facultad de Ingeniería de Sistemas Computacionales

**Campus Víctor Levis Sasso**

---

# Lab MVP - Parcial Práctico

Aplicación de escritorio en C# con Windows Forms que implementa el patrón de diseño **MVP (Modelo - Vista - Presentador)** sobre una arquitectura en capas. Incluye inicio de sesión con contraseñas protegidas mediante hash, una ventana principal MDI con menú lateral, un juego de Craps y un módulo CRUD de productos con manejo de imágenes.

| | |
| :--- | :--- |
| **Asignatura** | Herramientas de la Programación Aplicada III (.Net) |
| **Grupo** | 1IL133 |
| **Instructora** | Ing. Irina Fong |
| **Estudiante** | Kankibe González |
| **Fecha** | 9 de noviembre de 2026 |
---

## Tabla de contenido

1. [Objetivos](#objetivos)
2. [Introducción](#introducción)
3. [Tecnologías y versiones](#tecnologías-y-versiones)
4. [Arquitectura del proyecto](#arquitectura-del-proyecto)
5. [Patrón MVP](#patrón-mvp)
6. [Seguridad: hash y verificación de contraseñas](#seguridad-hash-y-verificación-de-contraseñas)
7. [Base de datos y conexión con privilegios mínimos](#base-de-datos-y-conexión-con-privilegios-mínimos)
8. [Resultados: ventana principal MDI](#resultados-ventana-principal-mdi)
9. [Resultados: juego de Craps](#resultados-juego-de-craps)
10. [Módulo de productos (CRUD)](#módulo-de-productos-crud)
11. [Resultados: validaciones](#resultados-validaciones)
12. [Controles utilizados](#controles-utilizados)
13. [Guía de instalación y ejecución](#guía-de-instalación-y-ejecución)
14. [Documentación del código](#documentación-del-código)
15. [Información del estudiante](#información-del-estudiante)

---

## Objetivos

- Implementar el patrón de diseño **MVP** separando la interfaz (Vista), la lógica de presentación (Presentador) y la lógica de negocio y datos (Modelo).
- Crear **dos contratos independientes** (interfaces) en la capa de Negocio: uno para generar el hash de una contraseña y otro para verificarla.
- Establecer una **conexión específica a la base de datos con privilegios mínimos**, de modo que la aplicación no use el usuario administrador (`root`).
- Construir una ventana principal **MDI con menú lateral izquierdo**, que muestre la hora actual y, en la parte inferior, el usuario que inició sesión.
- Validar los datos del usuario antes de enviarlos a la base de datos.

## Introducción

El proyecto parte de un inicio de sesión (login) con registro de usuarios. Las contraseñas nunca se guardan en texto plano: se almacena un **salt** aleatorio por usuario y el **hash SHA-256** de la combinación `salt + contraseña`. Al iniciar sesión, la capa de Negocio vuelve a calcular el hash con el salt guardado y lo compara con el almacenado.

Una vez dentro, el usuario accede a una ventana principal MDI con un menú lateral desde el cual puede abrir el **juego de Craps**, el **módulo de productos**, organizar las ventanas abiertas (cascada o mosaico) y cerrar la sesión. El menú muestra un reloj en tiempo real y, abajo, el nombre y el correo del usuario con la sesión activa.

Toda la comunicación con la base de datos se hace mediante **procedimientos almacenados**, y el usuario de la aplicación solo tiene permiso para ejecutarlos; no puede leer ni modificar las tablas directamente.

## Tecnologías y versiones

| Tecnología | Versión | Uso |
| :--- | :--- | :--- |
| C# | 7.3 | Lenguaje de programación |
| .NET Framework | 4.7.2 | Plataforma de ejecución (Windows Forms) |
| Windows Forms | .NET Framework 4.7.2 | Interfaz gráfica |
| Visual Studio | 2026 | Entorno de desarrollo |
| MySQL Server | 8.0.46 | Motor de base de datos |
| MySQL Workbench | 8.0 | Administración y ejecución de scripts SQL |
| MySql.Data (NuGet) | 26.10.0 | Conector de C# para MySQL |
| SHA-256 (`System.Security.Cryptography`) | Incluido en .NET Framework | Generación del hash de contraseñas |
| Git / GitHub | - | Control de versiones y repositorio |

## Arquitectura del proyecto

La solución `LabMVP` está dividida en cuatro proyectos (capas):

```
LabMVP/
├── Entidades/                    Clases que representan los datos
│   ├── E_Usuario.cs
│   └── E_Producto.cs
│
├── Datos/                        Acceso a MySQL (solo procedimientos almacenados)
│   ├── Conexion.cs               Conexión con el usuario de privilegios mínimos
│   ├── D_Usuario.cs
│   └── D_Producto.cs
│
├── Negocio/                      Reglas de negocio y contratos
│   ├── Interfaces/
│   │   ├── IHashService.cs               Contrato 1: generar el hash
│   │   └── IVerificarHashService.cs      Contrato 2: verificar el hash
│   ├── Servicios/
│   │   ├── HashService.cs                
│   │   └── VerificarHashService.cs
│   ├── Validaciones/
│   │   ├── IValidadorCampo.cs
│   │   └── Validadores.cs                ValidadorTexto / Decimal / Entero
│   ├── N_Usuario.cs              Login y registro
│   ├── N_Producto.cs             CRUD y validaciones de productos
│   ├── N_Craps.cs                Reglas del juego de Craps
│   ├── ErroresProducto.cs
│   └── Sesion.cs                 Usuario que inició sesión
│
└── Presentacion/                 Windows Forms (MVP)
    ├── Interfaces/               Contratos de cada Vista
    │   ├── ILoginView.cs
    │   ├── IPrincipalView.cs
    │   ├── ICrapsView.cs
    │   └── IProductosView.cs
    ├── Presentadores/            Un presentador por cada Vista
    │   ├── LoginPresenter.cs
    │   ├── PrincipalPresenter.cs
    │   ├── CrapsPresenter.cs
    │   └── ProductosPresenter.cs
    ├── Vistas/                   Formularios (solo controles)
    │   ├── FrmLogin.cs
    │   ├── FrmPrincipal.cs       Contenedor MDI
    │   ├── FrmCraps.cs
    │   └── FrmProductos.cs
    ├── ImagenHelper.cs           Imagen <-> bytes
    └── Program.cs
```

Dependencias entre capas: `Presentacion` usa `Negocio` y `Entidades`; `Negocio` usa `Datos` y `Entidades`; `Datos` usa `Entidades`. La Vista nunca accede a la base de datos.

## Patrón MVP

| Componente | En este proyecto | Responsabilidad |
| :--- | :--- | :--- |
| **Modelo** | `Entidades`, `Datos` y `Negocio` | Datos, reglas de negocio y acceso a la base de datos. |
| **Vista** | `Frm*.cs` (implementan `I*View`) | Mostrar controles y avisar al Presentador cuando el usuario hace algo. No valida, no usa SQL ni conoce el hash. |
| **Presentador** | `*Presenter.cs` | Recibe las acciones de la Vista, le pide el trabajo al Negocio y le ordena a la Vista qué mostrar. |

Flujo de una acción (ejemplo: iniciar sesión):

```
FrmLogin (Vista)  --evento IngresarClick-->  LoginPresenter
LoginPresenter    --IniciarSesion()------->  N_Usuario (Negocio)
N_Usuario         --ObtenerPorCorreo()---->  D_Usuario (Datos) --> sp_obtener_usuario (MySQL)
N_Usuario         --Verificar()----------->  IVerificarHashService
LoginPresenter    --AbrirPrincipal() o MostrarMensaje()--> FrmLogin
```

Cada Vista implementa una **interfaz** (`ILoginView`, `ICrapsView`, etc.), por lo que el Presentador depende de un contrato y no de un formulario concreto. Esto permite probar los presentadores sin abrir ventanas.

## Seguridad: hash y verificación de contraseñas

En la capa de Negocio se crearon **dos contratos independientes**, cada uno con una única responsabilidad:

```csharp
// Contrato 1: solo genera el hash
public interface IHashService
{
    string GenerarHash(string textoPlano, string salt);
}

// Contrato 2: solo verifica una contraseña contra el hash guardado
public interface IVerificarHashService
{
    bool Verificar(string textoPlano, string salt, string hashAlmacenado);
}
```

- `HashService` implementa `IHashService` con **SHA-256**: calcula el hash de `salt + contraseña` y lo devuelve en hexadecimal minúscula (64 caracteres).
- `VerificarHashService` implementa `IVerificarHashService` y recibe un `IHashService` por el constructor: vuelve a generar el hash con el mismo salt y lo compara con el almacenado.
- `N_Usuario` genera un **salt aleatorio de 32 caracteres hexadecimales** (con `RNGCryptoServiceProvider`) cada vez que se registra un usuario.

Ventajas de este diseño:

- **Desacoplamiento:** si mañana se cambia SHA-256 por BCrypt o Argon2, solo cambia la clase que implementa la interfaz; no se tocan la Vista, el Presentador ni la base de datos.
- **Responsabilidad única:** generar un hash y comprobarlo son acciones separadas, cada una con su contrato.
- **Pruebas:** `N_Usuario` tiene un segundo constructor que recibe ambos contratos, por lo que se pueden reemplazar por versiones de prueba.
- Al iniciar sesión, el mensaje de error es el mismo si el correo no existe o si la contraseña es incorrecta, para no revelar cuáles correos están registrados.

## Base de datos y conexión con privilegios mínimos

### Estructura de datos

**Tabla `usuarios`**

| Campo | Tipo | Nulo | Clave | Descripción |
| :--- | :--- | :--- | :--- | :--- |
| `id` | `INT` | NO | PRI (`AUTO_INCREMENT`) | Identificador del usuario |
| `nombre` | `VARCHAR(100)` | NO | | Nombre del usuario |
| `correo` | `VARCHAR(100)` | NO | UNI | Correo (se usa para iniciar sesión) |
| `salt` | `VARCHAR(50)` | NO | | Texto aleatorio que se mezcla con la contraseña |
| `password_hash` | `CHAR(64)` | NO | | Hash SHA-256 en hexadecimal |
| `activo` | `TINYINT(1)` | NO | | 1 = activo, 0 = deshabilitado |
| `fecha_creacion` | `DATETIME` | NO | | Fecha de registro |

**Tabla `productos`**

| Campo | Tipo | Nulo | Clave | Descripción |
| :--- | :--- | :--- | :--- | :--- |
| `id` | `INT` | NO | PRI (`AUTO_INCREMENT`) | Identificador único del producto |
| `nombre` | `VARCHAR(100)` | NO | | Nombre o descripción del producto |
| `precio` | `DECIMAL(10,2)` | NO | | Precio unitario (mayor o igual a 0) |
| `cantidad` | `INT` | NO | | Unidades en inventario (mayor o igual a 0) |
| `imagen` | `LONGBLOB` | SÍ | | Imagen en formato binario (PNG) |

### Procedimientos almacenados

| Procedimiento | Uso |
| :--- | :--- |
| `sp_obtener_usuario(p_correo)` | Devuelve el usuario activo con ese correo (la app compara el hash en C#). |
| `sp_insertar_usuario(...)` | Registra un usuario nuevo con su salt y hash. |
| `sp_listar_productos(p_filtro)` | Lista los productos; con filtro busca en id, nombre, precio y cantidad. |
| `sp_insertar_producto(...)` | Guarda un producto nuevo. |
| `sp_actualizar_producto(...)` | Actualiza un producto existente. |
| `sp_eliminar_producto(p_id)` | Borra un producto. |

### Usuario de la aplicación (privilegios mínimos)

La aplicación **no usa `root`**. Se conecta con un usuario propio, `app_lab`, que solo puede ejecutar los procedimientos almacenados de la base `lab_mvp`:

```sql
CREATE USER 'app_lab'@'localhost' IDENTIFIED BY 'Lab_2026*';
GRANT EXECUTE ON lab_mvp.* TO 'app_lab'@'localhost';
```

La conexión está centralizada en `Datos/Conexion.cs`, que construye la cadena con ese usuario. Con este esquema, si la aplicación fuera comprometida, el atacante no podría leer ni modificar las tablas directamente. Se puede comprobar con una conexión de `app_lab`:

```sql
CALL lab_mvp.sp_obtener_usuario('admin@lab.com');   -- funciona
SELECT * FROM lab_mvp.usuarios;                     -- error: permiso denegado
```

## Resultados: ventana principal MDI

La ventana principal es un contenedor **MDI**. Tiene un **menú lateral izquierdo** con:

- El nombre de la aplicación y un **reloj en tiempo real** (hora y fecha en español).
- Botón **Juego de Craps**: abre el juego dentro del MDI.
- Botón **Productos**: abre el CRUD de productos dentro del MDI (si ya está abierto, lo trae al frente).
- Botón **Organizar ventanas**: alterna entre cascada y mosaico.
- Botón **Cerrar sesión**: borra la sesión y vuelve al login.
- En la **parte inferior**, el usuario que inició sesión (nombre y correo).

**Captura 1 - Inicio de sesión (login)**

<img width="472" height="617" alt="image" src="https://github.com/user-attachments/assets/7becfc4e-24ca-46a1-b164-086816658b67" />


**Captura 2 - Ventana principal MDI con el menú lateral**

<img width="1246" height="843" alt="image" src="https://github.com/user-attachments/assets/34826c81-0933-4764-8119-739b97696d51" />


**Captura 3 - Varias ventanas hijas abiertas dentro del MDI**

<img width="1251" height="844" alt="image" src="https://github.com/user-attachments/assets/8e482826-ce6f-479d-9f9c-7e15f211fbf5" />


## Módulo de productos (CRUD)

Módulo integrado al MVP que permite **crear, leer, actualizar y borrar** productos con su nombre, precio, cantidad e imagen.

- **Guardar:** valida los campos y registra el producto nuevo.
- **Listar y buscar:** el grid muestra todos los productos; al escribir en el campo de búsqueda (o pulsar la lupa) se filtra por id, nombre, precio o cantidad.
- **Seleccionar:** al elegir una fila, sus datos pasan a los campos del formulario para editarlos.
- **Modificar:** actualiza el producto seleccionado.
- **Borrar:** pide confirmación y elimina el producto seleccionado.
- **Limpiar:** vacía el formulario y vuelve al modo de creación.
- **Imágenes:** al hacer clic en el cuadro de imagen se abre un selector de archivos (jpg, png, bmp). La clase `ImagenHelper` convierte la imagen a `byte[]` (PNG) para guardarla en la columna `LONGBLOB` y la reconstruye al leerla. En el grid se muestran miniaturas de 80 x 60 píxeles. Si el producto no tiene foto, se guarda sin imagen.

Los eventos de los botones los recibe el `ProductosPresenter`, que usa a `N_Producto` para validar y guardar, y le ordena a `FrmProductos` qué mostrar.


### Login y registro (`N_Usuario`)

- El correo no puede estar vacío y debe tener un formato válido (`algo@algo.algo`).
- La contraseña no puede estar vacía; para registrarse debe tener **al menos 6 caracteres**.
- El nombre es obligatorio al registrarse.
- No se permiten dos usuarios con el mismo correo.
- Si el correo o la contraseña son incorrectos se muestra el mismo mensaje: "Correo o contraseña incorrectos."

### Productos (`N_Producto`)

Las validaciones usan una interfaz que actúa como contrato:

```csharp
public interface IValidadorCampo
{
    bool EsValido(string valor);
    string MensajeError { get; }
}
```

Cada tipo de dato tiene su propia clase que la implementa:

- **ValidadorTexto:** el nombre no puede estar vacío ni pasar de 100 caracteres.
- **ValidadorDecimal:** el precio debe ser un número decimal válido, no negativo y que quepa en `DECIMAL(10,2)`. Acepta punto o coma decimal.
- **ValidadorEntero:** la cantidad debe ser un número entero válido y no negativo.

Si algún campo falla, el Presentador le ordena a la Vista mostrar un icono de error junto al campo (componente `ErrorProvider`) con su mensaje, y no se guarda nada. Cuando el campo se corrige, el icono desaparece.

## Controles utilizados

| Control | Dónde se usa |
| :--- | :--- |
| `Form` (con `IsMdiContainer = true`) | `FrmPrincipal`, contenedor MDI de las demás ventanas |
| `Panel` | Menú lateral, zona de usuario en la parte inferior y panel de búsqueda |
| `Label` | Textos, reloj, fecha, usuario, dados, marcador y mensajes |
| `Button` | Ingresar, Registrarme, menú lateral, Tirar dados, Nuevo juego, Guardar, Modificar, Limpiar y Borrar |
| `TextBox` | Correo, contraseña, nombre, id, precio, cantidad y búsqueda |
| `Timer` | Actualiza el reloj del menú lateral cada segundo |
| `DataGridView` (con columnas de texto e imagen) | Lista de productos con miniaturas |
| `PictureBox` | Imagen del producto y lupa de búsqueda |
| `ImageList` | Íconos de los botones del CRUD |
| `ErrorProvider` | Iconos de error de las validaciones |
| `OpenFileDialog` | Elegir la imagen del producto |
| `MessageBox` | Mensajes y confirmaciones |

## Guía de instalación y ejecución

### Requisitos previos

- Windows con **Visual Studio 2026** (carga de trabajo de escritorio con .NET) y **.NET Framework 4.7.2**.
- **MySQL Server 8.0** y **MySQL Workbench**.
- Conexión a internet la primera vez, para que Visual Studio restaure el paquete NuGet `MySql.Data`.

### Pasos

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/Kankibe/Parcial_practico.git
   ```
2. **Crear la base de datos:** abrir **MySQL Workbench**, conectarse con un usuario administrador (`root`) y ejecutar el archivo `Parcial_Practico.sql` (menú **File**, **Open SQL Script**, y luego el botón del rayo para ejecutarlo). Este único script, exportado con *Dump Structure and Data*, crea:
   - La base de datos `lab_mvp`.
   - Las tablas `usuarios` y `productos`, con datos de ejemplo.
   - Los seis procedimientos almacenados (`sp_obtener_usuario`, `sp_insertar_usuario`, `sp_listar_productos`, `sp_insertar_producto`, `sp_actualizar_producto` y `sp_eliminar_producto`).
   - El usuario `app_lab` con privilegios mínimos (solo ejecutar procedimientos).
3. **Revisar la conexión:** abrir `Datos/Conexion.cs` y comprobar que el servidor, el puerto, el usuario y la contraseña coincidan con los del script:
   ```csharp
   private const string Servidor = "localhost";
   private const string Puerto = "3306";
   private const string BaseDatos = "lab_mvp";
   private const string Usuario = "app_lab";
   private const string Clave = "Lab_2026*";
   ```
4. **Abrir la solución:** abrir `LabMVP.slnx` en Visual Studio y esperar a que se restauren los paquetes NuGet.
5. **Marcar el proyecto de inicio:** clic derecho sobre el proyecto `Presentacion`, **Establecer como proyecto de inicio**.
6. **Ejecutar:** presionar `F5` o el botón **Iniciar**.

### Usuario de prueba

| Campo | Valor |
| :--- | :--- |
| Correo | `admin@lab.com` |
| Contraseña | `Admin123*` |

También se pueden crear más usuarios desde la misma pantalla de login con el botón **Registrarme**.

## Documentación del código

## Información del estudiante


**Nombre:** Kankibe González

**Curso:** Herramientas de la Programación Aplicada III (.Net)

**Grupo:** 1IL133

**Instructora:** Ing. Irina Fong

**Fecha:** 9 de noviembre de 2026

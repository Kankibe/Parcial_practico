# Universidad Tecnológica de Panamá

# Facultad de Ingeniería de Sistemas Computacionales

**Campus Víctor Levis Sasso**

---

# Lab MVP - Parcial Práctico

Aplicación de escritorio en C# con Windows Forms que implementa el patrón de diseño **MVP (Modelo - Vista - Presentador)** sobre una arquitectura en capas. Incluye inicio de sesión con contraseñas protegidas mediante hash, **roles de usuario** (lector, editor y administrador), una ventana principal MDI con menú lateral, un juego de Craps, un módulo CRUD de productos con manejo de imágenes y una ventana de gestión de usuarios. La base de datos se usa con **dos cuentas de MySQL**: un superusuario (`Kanki`) y un usuario de privilegios mínimos (`Ibe`).

| | |
| :--- | :--- |
| **Asignatura** | Herramientas de la Programación Aplicada III (.Net) |
| **Grupo** | 1IL133 |
| **Instructora** | Ing. Irina Fong |
| **Estudiante** | Kankibe González |
| **Fecha** | 23 de noviembre de 2026 |
---


## Objetivos

- Implementar el patrón de diseño **MVP** separando la interfaz (Vista), la lógica de presentación (Presentador) y la lógica de negocio y datos (Modelo).
- Crear **dos contratos independientes** (interfaces) en la capa de Negocio: uno para generar el hash de una contraseña y otro para verificarla.
- Establecer una **conexión a la base de datos con privilegios mínimos** (usuario `Ibe`), de modo que la aplicación no use el usuario administrador (`root`), y un **superusuario** (`Kanki`) que solo se usa cuando entra un administrador.
- Construir una ventana principal **MDI con menú lateral izquierdo**, que muestre la hora actual y, en la parte inferior, el usuario que inició sesión.
- Validar los datos del usuario antes de enviarlos a la base de datos.

## Introducción

El proyecto parte de un inicio de sesión (login) con registro de usuarios. Las contraseñas nunca se guardan en texto plano: se almacena un **salt** aleatorio por usuario y el **hash SHA-256** de la combinación `salt + contraseña`. Al iniciar sesión, la capa de Negocio vuelve a calcular el hash con el salt guardado y lo compara con el almacenado.

Una vez dentro, el usuario accede a una ventana principal MDI con un menú lateral desde el cual puede abrir el **juego de Craps**, el **módulo de productos**, la **gestión de usuarios** (solo el administrador), organizar las ventanas abiertas (cascada o mosaico) y cerrar la sesión. El menú muestra un reloj en tiempo real y, abajo, el nombre, el rol, la cuenta de MySQL en uso y el correo del usuario con la sesión activa.

Toda la comunicación con la base de datos se hace mediante **procedimientos almacenados**. La cuenta de privilegios mínimos solo tiene permiso para ejecutar unos pocos de ellos; no puede leer ni modificar las tablas directamente.

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
│   ├── E_Producto.cs
│   └── Roles.cs                  Los tres roles: lector, editor y admin
│
├── Datos/                        Acceso a MySQL (solo procedimientos almacenados)
│   ├── Conexion.cs               Conexión con Ibe (mínimos) o Kanki (superusuario)
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
│   ├── N_Usuario.cs              Login, registro y gestión de usuarios
│   ├── N_Producto.cs             CRUD y validaciones de productos
│   ├── N_Craps.cs                Reglas del juego de Craps
│   ├── ErroresProducto.cs
│   └── Sesion.cs                 Usuario que inició sesión y cuenta de MySQL en uso
│
└── Presentacion/                 Windows Forms (MVP)
    ├── Interfaces/               Contratos de cada Vista
    │   ├── ILoginView.cs
    │   ├── IPrincipalView.cs
    │   ├── ICrapsView.cs
    │   ├── IProductosView.cs
    │   └── IUsuariosView.cs
    ├── Presentadores/            Un presentador por cada Vista
    │   ├── LoginPresenter.cs
    │   ├── PrincipalPresenter.cs
    │   ├── CrapsPresenter.cs
    │   ├── ProductosPresenter.cs
    │   └── UsuariosPresenter.cs
    ├── Vistas/                   Formularios (solo controles)
    │   ├── FrmLogin.cs
    │   ├── FrmPrincipal.cs       Contenedor MDI
    │   ├── FrmCraps.cs
    │   ├── FrmProductos.cs
    │   └── FrmUsuarios.cs        Gestión de usuarios (solo admin)
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
| `rol` | `VARCHAR(20)` | NO | | `lector` (por defecto), `editor` o `admin` (con `CHECK`) |

**Tabla `productos`**

| Campo | Tipo | Nulo | Clave | Descripción |
| :--- | :--- | :--- | :--- | :--- |
| `id` | `INT` | NO | PRI (`AUTO_INCREMENT`) | Identificador único del producto |
| `nombre` | `VARCHAR(100)` | NO | | Nombre o descripción del producto |
| `precio` | `DECIMAL(10,2)` | NO | | Precio unitario (mayor o igual a 0) |
| `cantidad` | `INT` | NO | | Unidades en inventario (mayor o igual a 0) |
| `imagen` | `LONGBLOB` | SÍ | | Imagen en formato binario (PNG) |

### Procedimientos almacenados

| Procedimiento | Uso | Ibe | Kanki |
| :--- | :--- | :---: | :---: |
| `sp_obtener_usuario(p_correo)` | Devuelve el usuario con ese correo (la app compara el hash en C#). | Sí | Sí |
| `sp_insertar_usuario(...)` | Registra un usuario nuevo con su salt y hash (rol `lector`). | Sí | Sí |
| `sp_listar_productos(p_filtro)` | Lista los productos; con filtro busca en id, nombre, precio y cantidad. | Sí | Sí |
| `sp_insertar_producto(...)` | Guarda un producto nuevo. | Sí | Sí |
| `sp_actualizar_producto(...)` | Actualiza un producto existente. | Sí | Sí |
| `sp_eliminar_producto(p_id)` | Borra un producto. | No | Sí |
| `sp_insertar_usuario_con_rol(...)` | Crea un usuario con el rol que elige el admin. | No | Sí |
| `sp_listar_usuarios()` | Lista los usuarios para la ventana de gestión. | No | Sí |
| `sp_cambiar_rol_usuario(p_id, p_rol)` | Cambia el rol de un usuario. | No | Sí |
| `sp_cambiar_estado_usuario(p_id, p_activo)` | Activa o desactiva una cuenta. | No | Sí |

Los procedimientos que cambian el rol o el estado también protegen los datos desde la base: nunca permiten dejar el sistema sin al menos **un administrador activo**.

### Usuarios de MySQL: Kanki e Ibe

La aplicación **no usa `root`**. Se crearon dos cuentas de MySQL con permisos muy distintos:

| Cuenta | Permisos | Para qué se usa |
| :--- | :--- | :--- |
| `Kanki` | `ALL PRIVILEGES` sobre todo el servidor (superusuario) | Administrar la base y atender a los usuarios con rol `admin` |
| `Ibe` | Solo `EXECUTE` sobre 5 procedimientos de `lab_mvp` (privilegios mínimos) | Login, registro y todos los usuarios que no son admin |

```sql
-- Kanki: superusuario
CREATE USER IF NOT EXISTS 'Kanki'@'localhost' IDENTIFIED BY 'Kanki_2026*';
GRANT ALL PRIVILEGES ON *.* TO 'Kanki'@'localhost' WITH GRANT OPTION;

-- Ibe: privilegios minimos (solo ejecutar procedimientos)
CREATE USER IF NOT EXISTS 'Ibe'@'localhost' IDENTIFIED BY 'Ibe_2026*';
GRANT EXECUTE ON PROCEDURE lab_mvp.sp_obtener_usuario TO 'Ibe'@'localhost';
GRANT EXECUTE ON PROCEDURE lab_mvp.sp_insertar_usuario TO 'Ibe'@'localhost';
GRANT EXECUTE ON PROCEDURE lab_mvp.sp_listar_productos TO 'Ibe'@'localhost';
GRANT EXECUTE ON PROCEDURE lab_mvp.sp_insertar_producto TO 'Ibe'@'localhost';
GRANT EXECUTE ON PROCEDURE lab_mvp.sp_actualizar_producto TO 'Ibe'@'localhost';
```

**Cómo elige la aplicación la cuenta.** La conexión está centralizada en `Datos/Conexion.cs`:

1. El login y el registro siempre arrancan con `Ibe`.
2. Cuando el usuario inicia sesión correctamente, `N_Usuario` mira su rol: si es `admin`, la aplicación cambia a `Kanki`; si es `editor` o `lector`, sigue con `Ibe`.
3. Al cerrar sesión, la conexión vuelve a `Ibe`.

El menú lateral muestra la cuenta en uso (por ejemplo `BD: Kanki | kanki@lab.com`), para comprobarlo a simple vista.

**Dónde ver las cuentas en MySQL.** Las cuentas de MySQL no están dentro de `lab_mvp`, sino en la base del sistema `mysql`. En Workbench se ven en el menú **Server, Users and Privileges**, o con:

```sql
SELECT user, host FROM mysql.user WHERE user IN ('Kanki', 'Ibe');
SHOW GRANTS FOR 'Ibe'@'localhost';
SHOW GRANTS FOR 'Kanki'@'localhost';
```

**Comprobación del privilegio mínimo** (conectado como `Ibe` en Workbench):

```sql
CALL lab_mvp.sp_listar_productos('');   -- funciona
SELECT * FROM lab_mvp.usuarios;         -- error: SELECT command denied
CALL lab_mvp.sp_eliminar_producto(1);   -- error: EXECUTE command denied
```

Gracias a este esquema, si la aplicación fuera comprometida, el atacante con la cuenta `Ibe` no podría leer ni modificar las tablas directamente, ni borrar productos, ni gestionar usuarios. Además, todos los datos viajan como **parámetros** de los procedimientos y nunca se arman consultas concatenando texto, lo que evita la inyección SQL; el privilegio mínimo reduce el daño en caso de que algo fallara.

> **Nota:** las contraseñas de `Kanki` e `Ibe` que aparecen en `Conexion.cs` y en el script son **credenciales de prueba** para este laboratorio. Solo funcionan en el MySQL local (las cuentas están creadas con host `localhost`).

## Roles de la aplicación

Hay **dos niveles de permisos** que no se deben confundir:

- **Nivel base de datos:** las cuentas de MySQL `Kanki` e `Ibe` (sección anterior). Deciden qué puede ejecutar la aplicación en el servidor.
- **Nivel aplicación:** el rol del usuario que inicia sesión (columna `rol` de la tabla `usuarios`). Decide qué botones y acciones puede usar cada persona dentro del programa.

Los usuarios de la tabla `usuarios` **no son cuentas de MySQL**: son datos de la aplicación. Todos se conectan a MySQL mediante `Ibe`, excepto los administradores, que usan `Kanki`.

| Acción | Lector | Editor | Admin |
| :--- | :---: | :---: | :---: |
| Ver y buscar productos | Sí | Sí | Sí |
| Jugar Craps | Sí | Sí | Sí |
| Crear y modificar productos | No | Sí | Sí |
| Borrar productos | No | No | Sí |
| Gestionar usuarios (botón **Usuarios y roles**) | No | No | Sí |
| Cuenta de MySQL que usa | `Ibe` | `Ibe` | `Kanki` |

Reglas de la gestión de usuarios:

- Quien se registra desde el login queda siempre con el rol **lector**. Solo el admin puede darle otro rol.
- El admin puede crear usuarios con el rol que elija, cambiar el rol de un usuario y activar o desactivar cuentas.
- Una cuenta desactivada se conserva en la base de datos, pero ya no puede iniciar sesión.
- El cambio de rol se aplica la próxima vez que esa persona inicie sesión.
- El admin no puede cambiarse el rol ni desactivarse a sí mismo, y siempre debe quedar al menos un administrador activo.
- Aunque un botón estuviera visible, el Negocio vuelve a comprobar el rol antes de crear, modificar o borrar, y MySQL limita lo que cada cuenta puede ejecutar.

## Resultados: ventana principal MDI

La ventana principal es un contenedor **MDI**. Tiene un **menú lateral izquierdo** con:

- El nombre de la aplicación y un **reloj en tiempo real** (hora y fecha en español).
- Botón **Juego de Craps**: abre el juego dentro del MDI.
- Botón **Productos**: abre el CRUD de productos dentro del MDI (si ya está abierto, lo trae al frente).
- Botón **Usuarios y roles**: abre la gestión de usuarios. **Solo lo ve el administrador.**
- Botón **Organizar ventanas**: alterna entre cascada y mosaico.
- Botón **Cerrar sesión**: borra la sesión y vuelve al login.
- En la **parte inferior**, el usuario que inició sesión: nombre con su rol, la cuenta de MySQL en uso y el correo.

**Captura 1 - Inicio de sesión (login)**

<img width="472" height="617" alt="image" src="https://github.com/user-attachments/assets/7becfc4e-24ca-46a1-b164-086816658b67" />


**Captura 2 - Ventana principal MDI con el menú lateral**

<img width="1246" height="843" alt="image" src="https://github.com/user-attachments/assets/34826c81-0933-4764-8119-739b97696d51" />


**Captura 3 - Varias ventanas hijas abiertas dentro del MDI**

<img width="1251" height="844" alt="image" src="https://github.com/user-attachments/assets/8e482826-ce6f-479d-9f9c-7e15f211fbf5" />


## Módulo de productos (CRUD)

Módulo integrado al MVP que permite **crear, leer, actualizar y borrar** productos con su nombre, precio, cantidad e imagen. Las acciones disponibles dependen del rol del usuario (ver la tabla de roles).

- **Guardar:** valida los campos y registra el producto nuevo (editor y admin).
- **Listar y buscar:** el grid muestra todos los productos; al escribir en el campo de búsqueda (o pulsar la lupa) se filtra por id, nombre, precio o cantidad.
- **Seleccionar:** al elegir una fila, sus datos pasan a los campos del formulario para editarlos.
- **Modificar:** actualiza el producto seleccionado (editor y admin).
- **Borrar:** pide confirmación y elimina el producto seleccionado (solo admin).
- **Limpiar:** vacía el formulario y vuelve al modo de creación.
- **Imágenes:** al hacer clic en el cuadro de imagen se abre un selector de archivos (jpg, png, bmp). La clase `ImagenHelper` convierte la imagen a `byte[]` (PNG) para guardarla en la columna `LONGBLOB` y la reconstruye al leerla. En el grid se muestran miniaturas de 80 x 60 píxeles. Si el producto no tiene foto, se guarda sin imagen.

Los eventos de los botones los recibe el `ProductosPresenter`, que usa a `N_Producto` para validar y guardar, y le ordena a `FrmProductos` qué mostrar.

## Gestión de usuarios (solo administrador)

La ventana **Usuarios y roles** (`FrmUsuarios`, con su `UsuariosPresenter` y su contrato `IUsuariosView`) sigue el mismo patrón MVP. Muestra en un grid el nombre, el correo, el rol y el estado de cada usuario, y permite:

- **Cambiar el rol** de un usuario con un `ComboBox` y el botón **Guardar rol**.
- **Activar o desactivar** la cuenta elegida.
- **Crear un usuario nuevo** con nombre, correo, contraseña y rol (la contraseña se guarda con salt y hash, igual que en el registro).

### Login y registro (`N_Usuario`)

- El correo no puede estar vacío y debe tener un formato válido (`algo@algo.algo`).
- La contraseña no puede estar vacía; para registrarse debe tener **al menos 6 caracteres**.
- El nombre es obligatorio al registrarse.
- No se permiten dos usuarios con el mismo correo.
- Una cuenta desactivada por el admin no puede iniciar sesión.
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
| `GroupBox` | Formulario "Crear usuario nuevo" en la gestión de usuarios |
| `Label` | Textos, reloj, fecha, usuario, dados, marcador y mensajes |
| `Button` | Ingresar, Registrarme, menú lateral, Tirar dados, Nuevo juego, Guardar, Modificar, Limpiar, Borrar, Guardar rol y Crear usuario |
| `TextBox` | Correo, contraseña, nombre, id, precio, cantidad y búsqueda |
| `ComboBox` | Elegir el rol de un usuario existente y el rol de un usuario nuevo |
| `Timer` | Actualiza el reloj del menú lateral cada segundo |
| `DataGridView` (con columnas de texto e imagen) | Lista de productos con miniaturas y lista de usuarios |
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
2. **Crear la base de datos:** abrir **MySQL Workbench**, conectarse con un usuario administrador (`root`) y ejecutar el archivo `Parcial_Practico.sql` (menú **File**, **Open SQL Script**, y luego el botón del rayo para ejecutarlo). Hay que usar `root` porque el script crea usuarios y asigna permisos. Este único script, exportado con *Dump Structure and Data*, crea:
   - La base de datos `lab_mvp`.
   - Las tablas `usuarios` y `productos`, con datos de ejemplo.
   - Los diez procedimientos almacenados (`sp_obtener_usuario`, `sp_insertar_usuario`, `sp_insertar_usuario_con_rol`, `sp_listar_usuarios`, `sp_cambiar_rol_usuario`, `sp_cambiar_estado_usuario`, `sp_listar_productos`, `sp_insertar_producto`, `sp_actualizar_producto` y `sp_eliminar_producto`).
   - Las cuentas de MySQL `Kanki` (superusuario) e `Ibe` (privilegios mínimos).
3. **Revisar la conexión:** abrir `Datos/Conexion.cs` y comprobar que el servidor, el puerto, el usuario y la contraseña de `Ibe` y `Kanki` coincidan con los del script.
4. **Abrir la solución:** abrir `LabMVP.slnx` en Visual Studio y esperar a que se restauren los paquetes NuGet.
5. **Marcar el proyecto de inicio:** clic derecho sobre el proyecto `Presentacion`, **Establecer como proyecto de inicio**.
6. **Ejecutar:** presionar `F5` o el botón **Iniciar**.

### Usuarios de prueba

Son las cuentas de la aplicación (se escriben en el login):

| Correo | Contraseña | Rol | Cuenta de MySQL que usa |
| :--- | :--- | :--- | :--- |
| `admin@lab.com` | `Admin123*` | admin | `Kanki` |
| `kanki@lab.com` | `Kanki_2026*` | admin | `Kanki` |
| `ibe@lab.com` | `Ibe_2026*` | lector | `Ibe` |
| `usuario@lab.com` | `Usuario123*` | lector | `Ibe` |


## Documentación del código

## Información del estudiante


**Nombre:** Kankibe González

**Curso:** Herramientas de la Programación Aplicada III (.Net)

**Grupo:** 1IL133

**Instructora:** Ing. Irina Fong

**Fecha:** 23 de noviembre de 2026

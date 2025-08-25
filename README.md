# BusinessManager

## 📖 Propósito del Proyecto

BusinessManager es una aplicación completa de gestión empresarial desarrollada con arquitectura Clean Architecture y tecnologías modernas. El sistema está diseñado para gestionar clientes, usuarios, roles, permisos y órdenes de trabajo de manera eficiente y escalable.

### Funcionalidades Principales

- **Gestión de Usuarios**: Creación, autenticación y autorización de usuarios con JWT
- **Sistema de Roles y Permisos**: Control de acceso basado en roles dinámicos
- **Gestión de Clientes**: CRUD completo para manejo de información de clientes
- **Órdenes de Trabajo**: Creación y asignación de órdenes a técnicos
- **Autenticación Segura**: Sistema de login/logout con tokens JWT y cookies HttpOnly
- **API RESTful**: Endpoints bien documentados con Swagger/OpenAPI

## 🏗️ Arquitectura y Diseño

El proyecto implementa **Clean Architecture** con separación clara de responsabilidades y principios SOLID:

### Estructura del Proyecto

```
BusinessManager/
├── src/
│   ├── BusinessManager.Domain/          # 🏛️ Capa de Dominio
│   │   ├── Entities/                    # Entidades de negocio
│   │   ├── ValueObjects/                # Objetos de valor
│   │   ├── Enums/                       # Enumeraciones
│   │   └── Primitives/                  # Tipos base
│   │
│   ├── BusinessManager.Application/     # 🧠 Capa de Aplicación
│   │   ├── UseCases/                    # Casos de uso (CQRS)
│   │   ├── Interfaces/                  # Contratos
│   │   └── DTOs/                        # Objetos de transferencia
│   │
│   ├── BusinessManager.Infrastructure/  # 🔧 Capa de Infraestructura
│   │   ├── Context/                     # DbContext de Entity Framework
│   │   ├── Auth/                        # Autenticación y autorización
│   │   └── Repositories/                # Implementaciones de repositorios
│   │
│   ├── BusinessManager.Api/             # 🌐 API Web
│   │   ├── Program.cs                   # Configuración y endpoints
│   │   └── Properties/                  # Configuraciones
│   │
│   └── businessmanager.web/             # 🎨 Frontend React
│       ├── src/                         # Código fuente React
│       ├── components/                  # Componentes reutilizables
│       └── pages/                       # Páginas de la aplicación
```

### Patrones de Diseño Implementados

- **Clean Architecture**: Separación en capas con dependencias hacia adentro
- **CQRS (Command Query Responsibility Segregation)**: Separación de comandos y consultas
- **Mediator Pattern**: Desacoplamiento entre controladores y lógica de negocio
- **Repository Pattern**: Abstracción del acceso a datos
- **Dependency Injection**: Inversión de control y inyección de dependencias
- **Value Objects**: Encapsulación de lógica de validación (Email, NIF, Address)

### Tecnologías Utilizadas

#### Backend (.NET 9)
- **ASP.NET Core**: Framework web
- **Entity Framework Core**: ORM para acceso a datos
- **JWT Authentication**: Autenticación basada en tokens
- **Swagger/OpenAPI**: Documentación automática de API

#### Frontend (React + TypeScript)
- **React 19**: Biblioteca de UI
- **TypeScript**: Tipado estático
- **Material-UI**: Componentes de interfaz
- **React Router**: Enrutamiento
- **Vite**: Herramienta de construcción
- **TailwindCSS**: Framework de estilos

### Entidades del Dominio

#### 👤 **User (Usuario)**
```csharp
- Id: Guid
- Name: string
- Surname: string
- NIF: ValueObject
- Email: ValueObject
- Password: string (hasheada)
- Roles: Collection<Role>
```

#### 👑 **Role (Rol)**
```csharp
- Id: Guid
- Name: string
- Description: string
```

#### 👥 **Customer (Cliente)**
```csharp
- Id: Guid
- Name: string
- Surname: string
- Email: ValueObject
- PhoneNumber: string
- NIF: ValueObject
- Address: ValueObject
```

#### 📋 **Order (Orden)**
```csharp
- Id: Guid
- CustomerId: Guid
- Description: string
- Status: OrderStatus (enum)
- CreatedAt: DateTime
- UpdatedAt: DateTime
- Technicians: Collection<User>
```

## 🚀 API Documentation

### Base URL
```
https://localhost:7000/api
```

### Autenticación
La API utiliza JWT Bearer tokens. Para acceder a endpoints protegidos, incluye el header:
```
Authorization: Bearer <token>
```

### Endpoints Principales

#### 🔐 **Autenticación**

**POST** `/user/login`
```json
{
  "email": "user@example.com",
  "password": "password123"
}
```

**POST** `/user/logout`
- Requiere autenticación
- Invalida el token JWT

**POST** `/user/me`
- Valida el token actual y devuelve información del usuario

#### 👤 **Gestión de Usuarios**

**GET** `/users`
- Obtiene todos los usuarios
- Requiere autenticación

**POST** `/user`
```json
{
  "name": "Juan",
  "surname": "Pérez",
  "nif": "12345678A",
  "email": "juan@example.com",
  "password": "securePassword123"
}
```

**PUT** `/user/{id}/password`
```json
{
  "password": "newSecurePassword456"
}
```

**PUT** `/user/roles`
```json
{
  "userId": "guid",
  "roleIds": ["guid1", "guid2"]
}
```

#### 👑 **Gestión de Roles**

**GET** `/roles`
- Lista todos los roles disponibles

**POST** `/role`
```json
{
  "name": "Técnico",
  "description": "Personal técnico especializado"
}
```

#### 👥 **Gestión de Clientes**

**GET** `/customers`
- Lista todos los clientes

**POST** `/customer`
```json
{
  "name": "María",
  "surname": "González",
  "email": "maria@example.com",
  "phoneNumber": "+34123456789",
  "nif": "87654321B",
  "address": {
    "street": "Calle Principal 123",
    "city": "Madrid",
    "state": "Madrid",
    "zipCode": "28001",
    "country": "España"
  }
}
```

#### 📋 **Gestión de Órdenes**

**POST** `/order/{customerId}`
```json
{
  "description": "Reparación de equipo informático"
}
```

**POST** `/order/{orderId}/assign`
```json
{
  "technicianId": "guid-del-tecnico"
}
```

#### 🔒 **Permisos**

**GET** `/permissions?roleId={guid}`
- Obtiene permisos para un rol específico

**GET** `/endpoints`
- Lista todos los endpoints disponibles

### Códigos de Respuesta

- `200 OK`: Operación exitosa
- `401 Unauthorized`: Token inválido o ausente
- `403 Forbidden`: Sin permisos suficientes
- `404 Not Found`: Recurso no encontrado
- `500 Internal Server Error`: Error interno del servidor

## 🎨 Frontend

### Tecnologías y Librerías

- **React 19** con TypeScript
- **Material-UI (MUI)** para componentes de interfaz
- **React Router** para navegación
- **MUI Charts** para visualización de datos
- **TailwindCSS** para estilos personalizados
- **Vite** como bundler y servidor de desarrollo

### Estructura del Frontend

```
src/
├── components/          # Componentes reutilizables
├── pages/              # Páginas principales
├── hooks/              # Hooks personalizados
├── assets/             # Recursos estáticos
└── types/              # Definiciones de TypeScript
```

### Características del Frontend

- **Interfaz Responsiva**: Diseño adaptable a diferentes dispositivos
- **Componentes Reutilizables**: Biblioteca de componentes con Material-UI
- **Gestión de Estado**: Context API y hooks personalizados
- **Autenticación**: Manejo automático de tokens y redirección
- **Formularios Validados**: Validación en tiempo real
- **Dashboards**: Visualización de datos con gráficos

### Scripts Disponibles

```bash
# Desarrollo
npm run dev

# Construcción
npm run build

# Linting
npm run lint

# Vista previa
npm run preview
```

## 🛠️ Instalación y Configuración

### Requisitos Previos

- **.NET 9 SDK**
- **Node.js 18+**
- **SQL Server** (LocalDB o instancia completa)
- **Visual Studio 2022** o **VS Code**

### Configuración del Backend

1. **Clona el repositorio**
```bash
git clone https://github.com/adesduende/BusinessManager.git
cd BusinessManager
```

2. **Configura la base de datos**
(_En produccion usa variables de entorno_)
```bash
dotnet user-secrets --project src/BusinessManager.Api set "ConnectionStrings:BusinessDbSettings" "Server=(localdb)\\mssqllocaldb;Database=BusinessManagerDb;Trusted_Connection=True;"
```
3. **Configura JWT Settings**
(_En produccion usa variables de entorno_)
```bash
dotnet user-secrets --project src/BusinessManager.Api set "JwtSettings:SecretKey" "TuClaveSecretaMuySegura"
}
```


3. **Ejecuta las migraciones**
```bash
dotnet ef database update
```

4. **Inicia la API**
```bash
cd src/BusinessManager.Api
dotnet run
```

### Configuración del Frontend

1. **Instala dependencias**
```bash
cd src/businessmanager.web
npm install
```

2. **Configura la URL de la API**
```typescript
// config.ts
export const API_BASE_URL = 'https://localhost:7000';
```

3. **Inicia el servidor de desarrollo**
```bash
npm run dev
```

### URLs de Acceso

- **API**: https://localhost:7000
- **Swagger**: https://localhost:7000/swagger
- **Frontend**: https://localhost:59559

## 🔒 Seguridad

- **JWT Tokens**: Autenticación stateless con expiración
- **HttpOnly Cookies**: Almacenamiento seguro de tokens
- **CORS Configurado**: Políticas de origen cruzado
- **Autorización Dinámica**: Control de acceso basado en roles
- **Validación de Entrada**: Sanitización de datos de entrada
- **HTTPS Obligatorio**: Comunicación encriptada

## 📝 Contribución

1. Fork del proyecto
2. Crea una rama para tu feature (`git checkout -b feature/nueva-funcionalidad`)
3. Commit de cambios (`git commit -m 'Añadir nueva funcionalidad'`)
4. Push a la rama (`git push origin feature/nueva-funcionalidad`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE.txt` para más detalles.

---

**Desarrollado con ❤️ usando Clean Architecture y tecnologías modernas**
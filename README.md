# ***Dealer App*** 

## *Tabla de contenido* 📄

- [Descripcion general](https://github.com/Elian-Developer/DealerMarket/tree/master#descripción-general-)
- [Demostraciones del proyecto]( https://github.com/Elian-Developer/DealerMarket/tree/master#demostraciones-del-proyecto-)
- [Construido con](https://github.com/Elian-Developer/DealerMarket/tree/master#construido-con-%EF%B8%8F)
- [Autor](https://github.com/Elian-Developer/DealerMarket/tree/master#autor-%EF%B8%8F)

## *Descripción General* 📋

Este proyecto consiste en un ***Dealer App*** la cual está destinada a usuarios que deseen comprar o vender vehículos, la app cuenta con un sistema 
de autenticación, la cual te permite crear un usuario en la aplicación, el mismo debe ser confirmado por el usuario mediante un correo que le envía 
la aplicación inmediatamente el usuario se registra.

Una vez registrado y confirmado su cuenta mediante el correo, la app le permitirá iniciar sesión y acceder a la página principal donde allí podrá 
visualizar todos los anuncios publicados por otros usuarios. Destacando que los anuncios del usuario activo solo serán vistos en el apartado de 
***"Mis anuncios"***.

Dicha aplicación cuneta con permisos y roles, dentro de los roles está el usuario básico y el Admin, el usuario básico dolo tendrá acceso a la 
página principal y a sus anuncios. Mientras que el usuario administrador tendrá acceso a todos los módulos de la app.

Dealer App también cuenta con un apartado para ***"Forgot Password"*** en caso de haber olvidado su contraseña. Para poder recuperarla la aplicación 
le enviara por correo un link el cual usted debe utilizar para restablecer su contraseña e iniciar sesión nuevamente.

En la sección "mis anuncios" si el usuario activo tiene algún anuncio publicado se lo mostrará en ese apartado y del mismo modo le permitirá realizar 
las operaciones CRUD, en caso de no tener ningún anuncio publicado le mostrar un mensaje indicándoselo.

La aplicación cuenta con una sección de categorías (Esta sección solo esta visible para los Admin), en esta sección se encuentran las categorías a 
seleccionar al momento de publicar algún anuncio, destacando que en la misma también se pueden realizar las operaciones CRUD.

Esta aplicación fue realizada implementando el enfoque ***Code First*** con ***EntityFramework*** en ***ASP.Net Core*** con ***MVC*** y la 
arquitectura ***Onion*** con repositorios y servicios genéricos, Además se implementó ***Identity*** para el manejo de la autenticación y autorización 
y ***MimeKit*** para el manejo de envío de correos desde la app 

## ***Demostraciones del Proyecto*** 📋

- ***Vista del register para crear un usuario en la app***

![image](https://github.com/Elian-Developer/DealerMarket/assets/107364306/72573070-5516-45c8-92e6-e2f63ab3a057)

- ***Ejemplo de una cuenta creada y confirmada con exito en la app***

![image](https://github.com/Elian-Developer/DealerMarket/assets/107364306/7266b685-ef2e-4954-b924-f91076c412bb)

![image](https://github.com/Elian-Developer/DealerMarket/assets/107364306/d3219c91-49f7-4bf7-a0ce-aeb5085ed49a)

- ***Vista del login para iniciar sesion luego de haber confirmado su cuenta***

![image](https://github.com/Elian-Developer/DealerMarket/assets/107364306/040a2184-8c5d-4a44-9fad-55fd2746ec9b)

- ***Vista de "Olvidé mi contraseña."***

![image](https://github.com/Elian-Developer/DealerMarket/assets/107364306/4b0bb801-c88c-417e-b28e-18a9724b2ce3)

- ***Vista para restablecer su contraseña olvidada***

![image](https://github.com/Elian-Developer/DealerMarket/assets/107364306/377f9903-bfbd-490c-b875-68fc674cd430)

![image](https://github.com/Elian-Developer/DealerMarket/assets/107364306/c41f6ba5-17cf-4713-999d-b3a1e9c18999)

- ***Vista de la pagina principal de la app***

![image](https://github.com/Elian-Developer/DealerMarket/assets/107364306/66ced8eb-ebf1-481e-866c-a75a3d554d5d)

- ***Vista de datalles al hacer clic en un anuncio***

![image](https://github.com/Elian-Developer/DealerMarket/assets/107364306/b7773961-fb9e-4c9d-8592-427b5f0f74d5)

- ***Vista de Mis anuncios***

![image](https://github.com/Elian-Developer/DealerMarket/assets/107364306/1b089f89-1666-4515-8f87-c33b64afb260)



## *Construido con* 🛠️

Este proyecto fue construido haciendo uso de lo siguiente: 
- [ASP.NET Core](https://learn.microsoft.com/es-es/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-7.0)
- [EntityFramework](https://learn.microsoft.com/en-us/ef/)
- [SQL server](https://learn.microsoft.com/en-us/sql/sql-server/?view=sql-server-ver16)
- [Arquitectura Onion]()
- [Bootstrap](https://getbootstrap.com)
- [MVC](https://learn.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/getting-started)
- [Razor](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-7.0)
- [Identity]()
- [MimeKit]()

## Autor ✒️

_El autor de este proyecto es:_

***Elian Báez*** - ***Trabajo Completo*** - [Elian-Developer](https://github.com/Elian-Developer)

---

⌨️ con ❤️ por [Elian-Developer]((https://github.com/Elian-Developer)) 😊

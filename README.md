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

![image - 1](https://github.com/Elian-Developer/DealerMarket/assets/107364306/bdbf0bc9-56c9-4bef-8217-d03898640442)

- ***Ejemplo de una cuenta creada y confirmada con exito en la app***

![image](https://github.com/Elian-Developer/DealerMarket/assets/107364306/6f4e1fbf-dff8-43f1-83ee-b41dbeeebfdc)

![image - 2](https://github.com/Elian-Developer/DealerMarket/assets/107364306/27c6a71a-97c6-413f-94a6-5cc99c68968c)

- ***Vista del login para iniciar sesion luego de haber confirmado su cuenta***

![image - 3](https://github.com/Elian-Developer/DealerMarket/assets/107364306/71a7b0c2-b49d-40e1-aadc-6abe69d3fff6)

- ***Vista de "Olvidé mi contraseña."***

![image - 4](https://github.com/Elian-Developer/DealerMarket/assets/107364306/ab10a13f-308d-4c65-9e4d-b6d587603254)

- ***Vista para restablecer su contraseña olvidada***

![image](https://github.com/Elian-Developer/DealerMarket/assets/107364306/e17e7ece-50a9-46b5-8e8a-6614a512181b)

![image](https://github.com/Elian-Developer/DealerMarket/assets/107364306/a2f409db-6b54-4353-9311-f700a760ae07)

- ***Vista de la pagina principal de la app***

![image - 6](https://github.com/Elian-Developer/DealerMarket/assets/107364306/a4799063-d358-49a8-871d-e248fab109a0)

- ***Vista de datalles al hacer clic en un anuncio***

![image - 7](https://github.com/Elian-Developer/DealerMarket/assets/107364306/7ecc3697-669b-4b36-b517-807f1f559559)

- ***Vista de Mis anuncios***

![image - 8](https://github.com/Elian-Developer/DealerMarket/assets/107364306/b0fb16c4-f96a-408b-b2a8-c9154dee5c16)


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

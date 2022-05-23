# HTTP Generic Service Client (.Net Core)

## Descripción general
==================================================================

Proyecto de ejemplo con una clase `HttpService` genérico para ser reutilizado por otros servicios, con el fin de realizar requests http.

En este caso se hace uso de la clase `ProductsService`, utilizando una [Dummy API](https://dummyjson.com/) para facilitar las pruebas.

## Unit tests (xUnit)
==================================================================

Los tests se pueden encontrar en carpeta **test** para verificar los resultados devueltos por la API.

Los tests se pueden correr con el siguiente comando:
```
dotnet test
```
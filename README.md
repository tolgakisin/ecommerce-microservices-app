# ecommerce-microservices-app

## ApiGateway 
Ocelot: Gateway

Consul: Service Discovery

## EventBus
Services communicate with events via RabbitMQ. BuildingBlocks/EventBus is a common project for services and it can be published as a package on Nuget.

## Orchestrator
Middle Event Communicator for services using RabbitMQ. Can be used for distributed transactions. 
In case of error on services(Business,Network etc.), Orchestrator logs the exceptions, call Reverse functions and provide Consistency on system.

Orchestrator is an intelligent communicator that manages a chain of events. Orchestrator knows the next event to be called if next event is existed and should be called.

## Services
### 1. ProductService
Product Service provides to create, delete and list products. 

Used CQRS, Mediatr, RabbitMQ, MassTransit in this service and used MsSQL to store product data.

### 2. BasketService
Basket Service provides to add products to basket, delete products from basket, list products and checkout basket to start an order process.

Used Redis to store basket.

### 3. IdentityService
Identity Service provides register and login process.

Used MsSQL to store user data.

### 4. OrderService
Order Service provides to list orders, manage Order process with events.

Used DDD in this service and used MsSQL to store order data.

### 5. PaymentService
Payment Service provides to manage payment process with dummy service.

### 6. NotificationService
This service runs as a console application. Listens events and prints notification messages to console.

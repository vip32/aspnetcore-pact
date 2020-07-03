# aspnetcore-pact
api contract testing for consumers/providers with aspnet core

### Layers & Dependencies

``` 

                                                                    - Services, Jobs, Validators
                                                                    - Commands/Query + Handlers
                                                .----------------.  - Messages/Queues + Handlers
   - WebApi/Mvc/                            .-->| Application    |  - Adapter Interfaces, Exceptions
     SPA/Console program host              /    `----------------`  - View Models(DTO) + Mappings
                                          /        |        ^
  .--------------.                       /         |        |
  .              |     .--------------. /          V        |  - Events, Aggregates, Services
  | Presentation |     |              |/        .--------.  |  - Entities, ValueObjects
  | .Web|Tool    |---->| Presentation |-------->| Domain |  |  - Repository interfaces
  |  Service|*   |     |              |\        `--------`  |  - Specifications, Rules
  |              |     `--------------` \          ^        |                                         
  `--------------`                       \         |        |                                    
                       - Composition Root \        |        |     
                       - Controllers       \    .----------------.  - Interface Implementierungen (Adapters/Repositories)  
                       - Razor Pages        `-->| Infrastructure |  - DbContext
                       - Hosted Services        `----------------`  - Data Entities + Mappings   

```


## Orders
- provides of orders + items
- consumer of products: when a product is added to an order the products service (api) is called for product details
- [pact](./pacts/orders-products.json)

##### [QUERY] GetOrder(orderId)
- endpoint > GET /orders/{{order_id}}
##### [QUERY] GetOrders()
- endpoint > GET /orders/
##### [COMMAND] CreateOrder(order)
- endpoint > POST /orders/
##### [COMMAND] AddProduct(Guid orderId, Guid productId, int quantity)
- endpoint > PUT /orders/{{order_id}}/products/{{product_id}}?quantity=1
- lookup product in Products Service (http GET) > ProductDto::[-price, -name]
- add new item to order, take values from ProductDto+Command into Item::[-price, -name, -quantity]

## Products
- provider of products

##### [QUERY] GetProduct(productId)
- endpoint > GET /products/{{product_id}}
##### [QUERY] GetProducts()
- endpoint > GET /products/

see [REST.http](REST.http) for request details
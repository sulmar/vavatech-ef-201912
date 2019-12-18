# ASP.NET WebAPI 2

## Protokól HTTP

Zapytanie:
~~~
GET /docs/index.html HTTP/1.1
Host: www.mydomain.com
(blank line)
~~~

Odpowiedź:
~~~
HTTP/1.1 200 OK
Content-Type: text/html

<html><body><h1>Hello World!</h1></body></html>
~~~


### cURL

- Zapytanie
~~~ bash
curl -v http://localhost:51666/api/customers"
~~~

~~~ bash
curl -v http://localhost:51666/api/customers -H "Accept:application/xml"
~~~

- Wyświetlenie szczegółowych informacji
~~~ bash
curl -v --request GET http://localhost:51666/api/customers -H "Accept:application/json"
~~~


### POST
~~~ bash
curl -d "{\"id\":9,\"name\":\"asp.net core book\"}" -H "Content-Type: application/json" http://localhost:51666/api/products
~~~

## REST API

| Akcja  | Opis                  |
|--------|-----------------------|
| GET    | Pobierz               |
| POST   | Utwórz                |
| PUT    | Zamień                |
| DELETE | Usuń                  |
| PATCH  | Zmodyfikuj częściowo  |
| HEAD   | Czy istnieje          |

## Uruchomienie usługi na kilku portach
~~~ csharp
static void Main()
{
    StartOptions options = new StartOptions();
    options.Urls.Add("http://localhost:5000");
    options.Urls.Add("http://localhost:5010/");

    // Start OWIN host 
    using (WebApp.Start<Startup>(options))
    {
        Console.ReadLine();
    }
}
~~~

## Kontroler


### Ustawienie trasy

~~~ csharp
[RoutePrefix("api/zamowienia")]
public class OrdersController : ApiController
{
}

~~~


## Akcje

### Reguły

~~~ csharp

    public class OrdersController : ApiController
    {
        [Route()]
        public string Get()
        {
            Console.WriteLine(this.Request);

            return "Hello Web Api";
        }

        [Route("{id}")]
        public string Get(string id)
        {
            return $"Hello string {id}";
        }

        [Route("{id:int}")]
        public string Get(int id)
        {
            return $"Hello Number {id}";
        }

        //[Route(@"{id:range(1,3)}")]
        //public IHttpActionResult GetByNumber(string id)
        //{
        //    return Ok($"Hello Range {id}");
        //}


        [Route(@"{id:regex(^ABC\d+$)}")]
        public IHttpActionResult GetRegex(string id)
        {
            return Ok($"Hello Regex {id}");


        }

        [Route("{orderdate:DateTime}")]
        public string Get(DateTime orderdate)
        {
            return $"Hello {orderdate.ToShortDateString()}";
        }

        [HttpGet]
        [Route("{code:regex(^C[0-9]{3}$)}")]
        public IHttpActionResult Get(string code)
        {
            var customer = customersService.Get(code);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
~~~

### Własne reguły

- Definicja
~~~ csharp
public class NonZeroConstraint : IHttpRouteConstraint
{
    public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, 
        IDictionary<string, object> values, HttpRouteDirection routeDirection)
    {
        object value;
        if (values.TryGetValue(parameterName, out value) && value != null)
        {
            long longValue;
            if (value is long)
            {
                longValue = (long)value;
                return longValue != 0;
            }

            string valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
            if (Int64.TryParse(valueString, NumberStyles.Integer, 
                CultureInfo.InvariantCulture, out longValue))
            {
                return longValue != 0;
            }
        }
        return false;
    }
}
~~~

- Rejestracja

~~~ csharp
public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        var constraintResolver = new DefaultInlineConstraintResolver();
        constraintResolver.ConstraintMap.Add("nonzero", typeof(NonZeroConstraint));

        config.MapHttpAttributeRoutes(constraintResolver);
    }
}
~~~

- Użycie
~~~ csharp
[Route("{id:nonzero}")]
public HttpResponseMessage GetNonZero(int id) { ... }

~~~


### Złożone obiekty jako parametry

~~~ csharp
public class GeoPoint
{
    public double Latitude { get; set; } 
    public double Longitude { get; set; }
}

public ValuesController : ApiController
{
    public IHttpActionResult Get([FromUri] GeoPoint location) { ... }
}

~~~

http://localhost/api/values/?Latitude=52.678558&Longitude=28.130989


~~~ csharp

public IHttpActionResult Get([FromUri] OrderSearchCriteria criteria)
{
    var orders = OrderRepository.Get(criteria);


    return Ok(loads);
}

~~~


### GET


~~~ csharp
public IHttpActionResult Get(int id)
{
    var customer = CustomerRepository.Get(id);

    if (customer == null)
    {
        return NotFound();
    }
    
    return Ok(customer);
}

~~~
   
   
~~~ csharp
[Route("api/customers/{customerId}/orders")]
public async Task<IHttpActionResult> GetByCustomer(int customerId)
{
    var orders = await CustomerRepository.GetAsync(customerId);

    return Ok(orders);
}
~~~

### HEAD



### Mieszane

~~~ csharp
[AcceptVerbs("GET", "HEAD")]
public IHttpActionResult Get(int id)
{
    var customer = CustomerRepository.Get(id);

    if (customer == null)
    {
        return NotFound();
    }

    if (this.Request.Method == HttpMethod.Head)
    {
        return Ok();
    }
    else
    {

        return Ok(customer);
    }
}

~~~
        

### POST

~~~ csharp
public async Task<IHttpActionResult> Post(Customer customer)
{
    if (!ModelState.IsValid)
    {
        return BadRequest();
    }

    await CustomerRepository.AddAsync(customer);

    // return Created($"http://localhost:9000/api/customers/{customer.Id}", customer);

    return CreatedAtRoute("DefaultApi", new { id = customer.Id }, customer);
}
~~~

### PUT

~~~ csharp

[Route("{id}")]
public async Task<IHttpActionResult> Put(int id, [FromBody] Customer customer)
{
    if (id != customer.Id)
    {
        return BadRequest();
    }

    await CustomerRepository.UpdateAsync(customer);

    return Ok();
}

~~~

### DELETE

~~~ csharp

public IHttpActionResult Delete(int id)
{
    CustomerRepository.Remove(id);

    return Ok();
}

~~~

## Generyczny kontroler

~~~ csharp
public abstract class BaseController<TEntity> : ApiController
        where TEntity : BaseEntity
    {
        protected readonly IEntityRepository<TEntity> entityRepository;

        public BaseController(IEntityRepository<TEntity> entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        public virtual async Task<IHttpActionResult> Get()
        {
            var entities = await entityRepository.GetAsync();

            return Ok(entities);
        }

        public virtual async Task<IHttpActionResult> Get(TKey id)
        {
            TEntity entity = await entityRepository.GetAsync(id);
            
            if (entity == null)
               return NotFound();
        
            return Ok();
        }

        [Route("api/{controller}/{id}")]
        public virtual async Task<IHttpActionResult> Put(TKey id, TEntity entity)
        {
            await entityRepository.UpdateAsync(entity);

            return Ok();
        }

        public virtual async Task<IHttpActionResult> Post(TEntity entity)
        {
            await entityRepository.AddAsync(entity);

            return CreatedAtRoute("DefaultApi", new { id = entity.Id }, entity);
        }

        public virtual async Task<IHttpActionResult> Delete(int id)
        {
            await entityRepository.DeleteAsync(id);

            return Ok();
        }

    }
~~~

## ngrok

- Wystawienie usługi hostowanej w IIS Express:
~~~ bash
ngrok http -host-header="localhost:[port]" [port]
~~~


## Usługa jako Windows Service

Instalacja
~~~ powershell
Install-Package TopShelf
~~~

Instalacja usługi wraz z uruchomieniem
~~~ bash
ApiService install start
~~~

Odinstalowanie usługi
~~~ bash
ApiService uninstall
~~~

## Pobranie strumienia danych

~~~ csharp
public class StreamActionResult : IHttpActionResult
{
    private readonly Stream Stream;

    public StreamActionResult(Stream stream)
    {
        this.Stream = stream;
    }

    public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    {
        var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

        response.Content = new StreamContent(Stream);
        response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");

        return Task.FromResult(response);
    }
}
~~~

### Przykład użycia Crystal Reports
~~~ csharp
 public class ReportsController : ApiController
    {
        public IHttpActionResult Get()
        {
            var reports = Directory.GetFiles(@"Reports\", "*.rpt")
                .Select(f => Path.GetFileNameWithoutExtension(f));

            return Ok(reports);
        }

        public IHttpActionResult Get(string id)
        {
            var parameters = this.Request.GetQueryNameValuePairs();

            string filename = $@"Reports\{id}.rpt";

            if (!File.Exists(filename))
            {
                return NotFound();
            }

            var rpt = new ReportDocument();
            rpt.Load(filename);

            foreach (var parameter in parameters)
            {
                rpt.SetParameterValue(parameter.Key, parameter.Value);
            }

            var stream = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            return new StreamActionResult(stream);

        }
    }
~~~


## Upload pliku

~~~ csharp

public class FilesController : ApiController
{
    public async Task<IHttpActionResult> Post()
    {
        var provider = new MultipartMemoryStreamProvider();

        await this.Request.Content.ReadAsMultipartAsync(provider);

        foreach (var file in provider.Contents)
        {
            var filename = file.Headers.ContentDisposition.FileName;

            var buffer = await file.ReadAsByteArrayAsync();

            File.WriteAllBytes(filename, buffer);
        }

        return Ok();
    }
}
    
 ~~~ 


## Filtry

### Filtr akcji

Czas wykonania
~~~ csharp
 public class ExecutionTimeFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            actionContext.Request.Properties[actionContext.ActionDescriptor.ActionName] = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);

            var stopwatch = (Stopwatch) actionExecutedContext.Request.Properties[actionExecutedContext.ActionContext.ActionDescriptor.ActionName];

            var actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;

            Trace.WriteLine($"{actionName} - elapsed - {stopwatch.Elapsed}");
        }
    }
    
~~~

Rejestracja filtrów

~~~ csharp
 public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            

            config.Filters.Add(new ExecutionTimeFilterAttribute());
            config.Filters.Add(new ValidationModelStateAttribute());
        }
            
}

~~~

Walidacja

~~~ csharp
public class ValidationModelStateAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(HttpActionContext actionContext)
    {
        if (!actionContext.ModelState.IsValid)
            actionContext.Response =
                actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);

        base.OnActionExecuting(actionContext);
    }
}
~~~

## Wstrzykiwanie zależności


### Unity

- Instalacja biblioteki

~~~ powershell
Install-Package Unity 
~~~

- Implementacja

~~~ csharp

public class UnityDependencyResolver : IDependencyResolver
{
    private readonly IUnityContainer container;

    public UnityDependencyResolver(IUnityContainer container)
    {
        this.container = container;
    }

    public IDependencyScope BeginScope()
    {
        var child = container.CreateChildContainer();
        return new UnityDependencyResolver(child);
    }

    public void Dispose()
    {
        container.Dispose();
    }

    public object GetService(Type serviceType)
    {
        try
        {
            return container.Resolve(serviceType);
        }
        catch(ResolutionFailedException)
        {
            return null;
        }

    }

    public IEnumerable<object> GetServices(Type serviceType)
    {
        try
        {
            return container.ResolveAll(serviceType);
        }
        catch (ResolutionFailedException)
        {
            return Enumerable.Empty<object>();
        }
    }
}
    
~~~

- Rejestracja

~~~ csharp
public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        // Web API configuration and services

        IUnityContainer container = new UnityContainer();
        container.RegisterType<ICustomerRepository, FakeCustomerRepository>();
        container.RegisterType<IProductRepository, FakeProductRepository>();

        container.RegisterInstance(new CustomerFaker());
        container.RegisterInstance(new ProductFaker());


        config.DependencyResolver = new UnityDependencyResolver(container);
    }
}
~~~




# Instalacja biblioteki

~~~ bash
PM> Install-Package EntityFramework
~~~

# Utworzenie kontekstu

~~~ csharp
public class MyContext : DbContext
{
    public MyContext()
      :base("MyDbConnection")
    { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
}
~~~

# Połączenie do bazy danych

~~~ xaml
 <connectionStrings>
    <add name="MyConnection" providerName="System.Data.SqlClient" connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MyDb;Integrated Security=True" />
  </connectionStrings>
~~~

## Algorytm wyszukiwania bazy danych

1. Jeśli w konstruktorze DbContext podano nazwę połączenia to szuka jej w pliku konfiguracynym app.config w sekcji _connectionStrings_ 

2. Jeśli użyto domyślnego konstruktora DbContext to szuka pliku konfiguracynym app.config nazwy klasy DbContext w sekcji _connectionStrings_  

3. Szuka instancji SQL Express 

4. Szuka bazy danych LocalDb o adresie (localdb)\mssqllocaldb

# Catalog API Tests .NET Core

Esse projeto tem como intuito criar uma API simples de catálogo de produtos com testes de **Integração** e **Unidade**.

Asp .net Core 2.2

### Por padrão a API está configurada com banco de dados em memória
```UseInMemoryDatabase("Catalog_Database")```

### Caso necessário altere a string de conexão no arquivo appsettings.json
```"DefaultConnection": "Server=.,1433;Database=Catalog;User ID=SA;Password=12345"```

### E remova o comentário no arquivo Startup.cs
```//services.AddDbContext<StoreDataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));```

### Execute o comando a seguir no package manager console

```Update-Database```

### Run Application 

## Resources

Lista de resources disponíveis na API:

- Categories ```http://localhost:5000/api/v1/categories```
- Products ```http://localhost:5000/api/v1/products```

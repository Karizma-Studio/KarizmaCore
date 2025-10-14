# Repository Core Library

This project provides **core repository utilities** and helpers for working with EF Core and domain models. It is intended to be used as a **shared library** across microservices or applications.

---

## Features

- **Repository Interfaces**  
  Predefined interfaces for repositories (`IRepository<TEntity>`) to standardize CRUD operations.

- **Base Repository**  
  Abstract base classes with common functionality for adding, updating, deleting, and querying entities.
    - Supports **soft deletes**.
    - Supports **tracking `UpdatedDate`** without committing directly to the database (compatible with UnitOfWork).

- **Model Builders & Helpers**
    - Classes and helpers to **initialize and construct domain models** easily.
    - Extension methods for **converting objects to JSON** for logging, caching, or debugging.

- **Converters & Comparers**
    - Utilities to **compare entities** or **convert types** in EF Core queries.
    - Supports equality checks and transformations for repository operations.

---

## Installation

Install via [NuGet](https://www.nuget.org/packages/KarizmaCore)

```bash
dotnet add package KarizmaCore --version 2.0.0
```

### Using Abstract Model Classes

```csharp
[Table("test")]
public class Test : BaseEntity {
    
  //id, created, updated, deleted are added automatically
    
  [Column("a_list"), Required] public required List<A> AList { get; init; }
}
```

### Using Repository Classes

```csharp
public interface ITestRepository : IRepository<Test>
{
   //add, update, delete, soft delete,find by id, get all, get all not deleted added automatically 
}

public class TestRepository(CoinDatabase database) : BaseRepository<Test>(database), ITestRepository
{
   //override default functions or implement new functions
}

//using in code
ITestRepository testRepository;
var test = await testRepository.FindById(34)
test.value = 123;
await testRepository.Update(test);
dbContext.SaveChangesAsync();
```

### Converting Objects to JSON

```csharp
var test = new Test();
JsonDocument json = test.ToJsonDocument();
string jsonStr = test.ToJsonString();
```

### Converter and Comparer

```csharp
modelBuilder.Entity<Test>()
  .Property(t => t.AList)
  .HasConversion(new ListJsonConverter<A>())
  .HasColumnType("jsonb").Metadata
  .SetValueComparer(new JsonListComparer<A>());
```
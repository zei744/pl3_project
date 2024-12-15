
open Microsoft.EntityFrameworkCore
open Microsoft.Extensions.DependencyInjection
open System.ComponentModel.DataAnnotations
open System.ComponentModel.DataAnnotations.Schema
open System.Linq
open System

[<Table("User")>]
type User() =
    [<Key>]
    [<Column("Id")>]
    member val Id: int = 0 with get, set

    [<Column("Name")>]
    member val Name: string = "" with get, set

    [<Column("PhoneNumber")>]
    member val PhoneNumber: int = 0 with get, set
    
    [<Column("Username")>]
    member val Username: string = "" with get, set

    [<Column("Password")>]
    member val Password: string = "" with get, set

    
[<Table("Movie")>]
type Movie() =
    [<Key>]
    [<Column("Id")>]
    member val Id: int = 0 with get, set

    [<Column("MovieName")>]
    member val MovieName: string = "" with get, set

    [<Column("Showtime")>]
    member val Showtime: DateOnly = DateOnly.MinValue with get, set
    

[<Table("Seat")>]
type Seat() =
    [<Key>]
    [<Column("Id")>]
    member val Id: int = 0 with get, set

    [<Column("Row")>]
    member val Row: int = 0 with get, set

    [<Column("Column")>]
    member val Column: int = 0 with get, set
    
    [<Column("isAvailable")>]
    member val isAvailable: bool = true with get, set

    [<Column("MovieId")>]
    member val MovieId: int = 0 with get, set

    
[<Table("Ticket")>]
type Ticket() =
    [<Key>]
    [<Column("Id")>]
    member val Id: int = 0 with get, set

    [<Column("CustomerName")>]
    member val CustomerName: string = "" with get, set

    [<Column("MovieName")>]
    member val MovieName: string = "" with get, set
    
    [<Column("SeatId")>]
    member val SeatId: int = 0 with get, set

    [<Column("Showtime")>]
    member val Showtime: DateOnly = DateOnly.MinValue with get, set




type AppDbContext(options: DbContextOptions<AppDbContext>) =
    inherit DbContext(options)

    override this.OnModelCreating(modelBuilder: ModelBuilder) =
        modelBuilder.Entity<User>() |> ignore
        modelBuilder.Entity<Movie>() |> ignore
        modelBuilder.Entity<Seat>() |> ignore
        modelBuilder.Entity<Ticket>() |> ignore
        
type UserService(dbContext: AppDbContext) =

    member this.AddUser(name: string, phoneNumber: int, username: string, password: string) =
        let user = User(Name = name, PhoneNumber = phoneNumber, Password = password, Username = username)
        dbContext.Add(user) |> ignore
        dbContext.SaveChanges() |> ignore
        "User added successfully!"

    member this.GetAllUsers()=
        dbContext.Set<User>().ToList()|> List.ofSeq

    member this.AddMovie(moviename: string,showtime: DateOnly) =
        let movie =Movie(MovieName=moviename,Showtime=showtime)
        dbContext.Add(movie) |> ignore
        dbContext.SaveChanges() |> ignore
        printfn "Movie added successfully!"




let optionsBuilder = DbContextOptionsBuilder<AppDbContext>()
optionsBuilder.UseSqlServer("Data Source=ZENOO;Initial Catalog=PL3_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;
                             TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False") |> ignore
use dbContext = new AppDbContext(optionsBuilder.Options)
let userService = UserService(dbContext)



// Add a new user
//userService.AddUser("Alice", 1234567890, "password123", "alice")
////userService.AddMovie("awlad_rezk", DateOnly(2024, 12, 10))
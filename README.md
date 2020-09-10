# ERD
![.NET Core](https://github.com/ArikMackenburg/ERD/workflows/.NET%20Core/badge.svg?branch=master)

# The Problem Domain
Now that you have a solid understanding of your database schema for your hotel management system, today you will build off of your initial web application from lab 11 and integrate into it our database tables from our ERD.

This lab will be a “tutorial” style, meaning it is encouraged that you follow along with the steps and notice how the data evolves as you move through the instructions. A few things for you to notice:

+ The creation of the migration scripts and how they are applied to the database
+ The simple models and how they define the shape of each database table
+ The creation of the controllers and how they control the routing of the api
Don’t forget to use the class demo code as an example of how to structure specific code or syntax.

Let’s begin…

## Application Specifications
### Startup File
+ Create a new Empty .NET Core Web Application to and implement the basic setup to create your API server:
  + Add explicit routing of Controllers in your ‘Configure’ method
  + Enable the use of MVC controllers in your ConfigureServices method
  + DBContext registered in ConfigureServices
+ Simple Models & The Database
Create a new Models folder that will contain your basic entities from your ERD
  + Create a Hotels model that contains the same propertied defined in your ERD
  + Don’t worry about adding the Navigation properties just yet. We will add those in later.
+ After your first simple model is created, Create a new Data folder and add a new AsyncInnDbContext file. Make your new class derive from the DbContext class, as well as creating the constructor with the proper parameters. Use the demo code as an example.

+ Register your DbContext in your startup file. Configure your appsettings.json file to include your connection string.

+ Go back to your AsyncInnDbContext file, and add a new property to include a new table into your database. public DbSet<Hotel> Hotels {get; set;}. Be sure to include the Models namespace into our current cs file.

+ Now that you have your database registered, and a single table property inside of your dbContext file, create a new migration to see the script that creates and adds that table to the databse: add-migration initial

+ Once you create the migration, run the update-database script and watch the script get run against your database.

+ Confirm in your local database that the Hotels table has been added.

+ You just successfully created and added your first table to your local DB! Now, let’s add the other two tables, except this time, we can just add the tables at the same time and have the script include both of them when adding to the database

+ Go back to your Models folder and add two new class files; Room and Amenity.

+ Populate each of these classes with the same properties that you have defined inside of your ERD. Don’t worry about adding the Navigation properties yet. We will add those later.

+ Go back into your AsyncInnDbContext file and add the two additional properties to represent the Room and the Amenity models.
  + public DbSet<Room> Rooms {get; set;}
  + public DbSet<Amenity> Amenities {get; set;}
+ Create a new migration to include the creation of these two new tables within your Package Manager Console: add-migration addingRoomAndAmenity

+ Finally, run update-database and watch those two new tables get added to the database. Confirm locally that the tables exist.
## Seeding data
Let’s add some default data into our tables on it’s initial launch.

+ Within your AsyncInnDbContext add a new override method for the OnModelCreating method under your constructor.
+ Seed in some default data for all three of your simple models
  + 3 hotels
  + 3 rooms
  + 3 amenities
Here is an example of adding a single default item to a table: (source HERE

```modelBuilder.Entity<Blog>().HasData(new Blog {BlogId = 1, Url = "http://sample.com"});```

After creating the seeded data, you will now want to create a new migration so that the seeded data can get added to the databse tables

+ Within package manager console, create a new migration add-migration addSeededData

+ Notice how the migration scripts created include the default data for all 3 tables.

+ Run update-database so that the data gets added to the table

+ Confirm that the data was added to your local database for all 3 tables.

## Controllers
Now that we have completed our “Code First Migrations” in the directions above. Let’s add some routes so that we can access the data through an API.

+ Create a new folder named Controllers in your project.
+ Right click on the folder, and choose Add » Controller
+ Choose the Entity Framework Scaffold for API option
+ Select the Hotels Entity for your model
+ Select your AsyncInnDbContext as your DbContext
+ After it’s been scaffolded, confirm through POSTMAN that your can do basic CRUD operations on the Hotels route

+ Follow the instructions above to scaffold out the Room and Amenity Controllers.
Once you have all 3 controllers created, and have manually tested the CRUD operations within Postman, your lab is completed. We will continue to build off of this lab over the next few days.
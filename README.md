# ERD Lab12
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

# Lab13

## Application Specifications
+ Using Dependency Injection, refactor your Hotels, Rooms, and Amenities Controllers to depend on an interface rather than the dbcontext.

+ Build an interface for each of the controllers that contain the required method signatures to all for CRUD operations to the database directly

+ Update each of the controllers to inject the interface rather than the DBContext
+ Create a service for each of the controllers that implement the appropriate interface. Build out the logic to satisfy the interface by making the appropriate calls to the db for each action.

+ Update your Controller to use the appropraite methosd from the interface rather than the DBContext direclty.

+ Confirm in POSTMAN that your controllers are returning the same logic as they did in Lab 12.

# Lab 14

## RoomAmenities
+ Add onto your RoomsController the ability to add and remove amenities to a specific room
  + Routes: POST/DELETE: [Route("{roomId}/Amenity/{amenityId}")]
  + Add to your IRoom Interface the method signatures to AddAmenityToRoom(int roomId, int amenityId) and RemoveAmentityFromRoom(int roomId, int amenityId)
  + Add the logic for the above methods into your RoomRepository.cs Service.
+ Add to your Room.cs, Amenity.cs, and RoomAmenity.cs file the navigation properties that we defined in your ERD.
+ On the Get() based call in your RoomRepository.cs and your ‘AmenityRepository.cs file, use the Include()` to populate the navigation property details within the return object.
## HotelRoom
+ Create a new interface named IHotelRoom that contains basic CRUD operations for manipulating a HotelRoom.
+ Create a service named HotelRoomRepository that implements the IHotelRoom interface. Add the logic for each of the methods to satisfy the CRUD operations on a HotelRoom.
+ Scaffold out a new HotelRoomController that will inject the IHotelRoomInterface. Update/customize the logic to use the interface instead of the DBContext
+ Modify the routes of this controller for the following:
  + GET all the rooms for a hotel: /api/Hotels/{hotelId}/Rooms
  + POST to add a room to a hotel: /api/Hotels/{hotelId}/Rooms
  + GET all room details for a specific room: /api/Hotels/{hotelId}/Rooms/{roomNumber}
  + PUT update the details of a specific room: /api/Hotels/{hotelId}/Rooms/{roomNumber}
  + DELETE a specific room from a hotel: /api/Hotels/{hotelId}/Rooms/{roomNumber}
## Misc
+ Be sure that all navigation properties are present in all models.
+ Be sure that when you query a room, you get all the amenities attached to it
+ Be sure that when you query an Amenity, you get all the rooms associated with it.
+ Be sure that when you query a HotelRoom, you get all the Rooms that are associated with it(which should also have all the amenitites)
+ Be sure that when you query a Hotel, you get all the Rooms (which should contain all the information from the option above)
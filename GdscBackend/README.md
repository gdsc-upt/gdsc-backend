# GDSC Backend

### Requirements

* having .net 5.0 installed on your machine

### Setup

* Open [how-to-database] and learn how to connect to our database

* Next, here in Rider, copy-paste `appsettings.Development.json` to `appsettings.Local.json` (make sure you DO NOT
  delete `appsettings.Development.json`)

* Good. Now we should tell our application to which database to connect. Open [appsettings.Local.json]
  and change the following values from the default connection string:
    * `myDataBase` with your database name(in our case it's `bill-gates-gdsc`)
    * `myPassword` with `someparolă`

* To be able to work with the database, we need to install .NET Entity Framework Core Tools by running in the terminal
  the next command: `dotnet tool install --global dotnet-ef`

* Great! Next we should create database tables by running `dotnet ef database update`

* We're good to run our application! Type the following command: `dotnet watch run`

# Important notes!

* After you modify a model class (ExampleModel, ContactModel, etc) you must
  run `dotnet ef migrations add <insert some  name here>` and then `dotnet ef database update`

[how-to-database]: https://youtrack.dscupt.tech/youtrack/articles/GDSCA-A-6/How-to-connect-to-Databases-and-steal-our-data

[appsettings.Local.json]: appsettings.Local.json

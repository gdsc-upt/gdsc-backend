# GDSC Backend

### Requirements

* having .net 5.0 installed on your machine

### Setup

* Open [pga.gdscupt.tech] and log in with `admin@example.com` and `parola01`
  > This is our development database. You should create your own database here

* Now open `Servers` > `Remote` > `Databases`
  and right-click on `Databases` and go to `Create` > `Database`, fill in the `Database` field with a name for your
  database (let's say we name it `bill-gates-gdsc`) and click `Save`

* Next, here in Rider, copy-paste `appsettings.Development.json` to `appsettings.Local.json` (make sure you DO NOT
  delete `appsettings.Development.json`)

* Good. Now we should tell our application to which database to connect. Open [appsettings.Local.json]
  and change the following values from the default connection string:
    * `myDataBase` with your database name(in our case it's `bill-gates-gdsc`)
    * `myPassword` with `parola01`

* To be able to work with the database, we need to install .NET Entity Framework Core Tools by running in the terminal
  the next command: `dotnet tool install --global dotnet-ef`

* Great! Next we should create database tables by running `dotnet ef database update`

* We're good to run our application! Type the following command: `dotnet watch run`

# Important notes!

* After you modify a model class (ExampleModel, ContactModel, etc) you must
  run `dotnet ef migrations add <insert some  name here>` and then `dotnet ef database update`

[pga.gdscupt.tech]: https://pga.gdscupt.tech

[appsettings.Local.json]: appsettings.Local.json

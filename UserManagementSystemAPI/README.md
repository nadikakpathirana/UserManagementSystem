## Steps for setup the Local Environment.
1. First you have to Change the server part of the Connection String in the [appsettings.json](./appsettings.json)
2. After that you can run "Update-database" command in the Nuget Package Manager console to run migration scripts.
3. Now you need to execute [scripts.sql](./script.sql) file.
4. In that sql file i have added 3 types of users
    * Nadika Koshala is a Admin
      * Username - nadika
      * Password - admin
    * Yohan Sandun is a Staff Member
      * Username - yohan
      * Password - admin
    * Harshna Perera is a Normal user
      * Username - harshana
      * Password - admin
5. Im done this because, every time it need to be have a Admin, Otherwise you have to changes user type using the database.
6. Finally you can run the API using "http" Profile(That port is use in the react app)
# TV Show Reminder
A simple web application to remind one person about his or her TV shows.

It uses the TVRage API:s to retrieve information about TV shows and episodes.

It will run on Microsoft Azure.

The UI culture and copy is in Swedish.

###Dependencies
* .NET Framework 4.5
* MSSQL Server

###Set up

Replace the MasterPassword app setting with your prefered password:

    <appSettings>
    .......
      <add key="MasterPassword" value="SuperHardToGuessPassword" />
    </appSettings> 

Replace the connection string with your connection string:

    <connectionStrings>
      <add name="DefaultConnection" providerName="System.Data.SqlClient" connectionString="Data Source=.\SQLExpress;Initial Catalog=tvshowreminder_db;Integrated Security=SSPI" />
    </connectionStrings>

Deploy!

Database tables will be set up automatically.


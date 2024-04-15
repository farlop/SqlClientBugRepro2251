# Repro for SqlClient issue 2251

## Description
This Xamarin application has a 'plugin' architecture based on assembly loading files. The solution contains the typical Shared and Android projects but it also includes the following additional:
 * Workers.Abstractions: which includes just one `IWorker` interface.
 * Workers.SqlClient: which has one implementation targeting Microsoft.Data.SqlClient version 5.1.0 which is the last version that works.
 * Workers.SqlClientNewer: just the same as above, but targeting 5.1.1 or 5.2.0 versions.

The Workers are distributed by using Publish profiles which are also included. These profiles have an additional `RuntimeIdentifier` property with the `Android` value that ensures that the right Microsoft.Data.SqlClient.dll assembly is distributed along with the others assemblies.

The artifacts for the Workers are then copied manually to the device inside an specific  folder structure  starting at `Android.OS.Environment.DirectoryDocuments`.

The Workers implementation just performs a connection to an SqlServer database and executes a simple query.

## Steps for reproducing the issue

- Build and launch the application once to have all the required folder structure created. Note that the error 'Assembly '_(path)_' not found' is fine because we haven't deployed any worker yet.
- Update the `Worker.SqlClient` implementation by changing the connection string and the query as desired to have valid values (double check that database exists and the query can be performed).
- Publish the project and copy all files into the device at the target folder: _Android/data/com.companyname.sqlclientbugrepro2251/files/Documents/workers/sqlclient_
- Now update the `Worker.SqlClientNewer` implementation with the same connection string and query.
- Publish the project and copy all files into the device at the target folder: _Android/data/com.companyname.sqlclientbugrepro2251/files/Documents/workers/sqlclientnewer_
- Run the application again and check that the connection is done successfully and the query is performed, as the first worker is used.
![image](https://github.com/farlop/SqlClientBugRepro2251/assets/2676254/a23ea128-b7d9-4b23-8ae6-a4cea455dbe0)

- In `SqlClientBugRepro2251` project comment line 29 and uncomment line 30 inf `MainPage.xaml.cs`. That will cause to use the worker that uses a newer version of the library.
![image](https://github.com/farlop/SqlClientBugRepro2251/assets/2676254/63471972-be57-48d6-8044-def2f4a16245)

- Run the application one more time and see how the error is shown in the message:
![image](https://github.com/farlop/SqlClientBugRepro2251/assets/2676254/1498df54-21d9-4058-83fe-f36991f5e7f8)

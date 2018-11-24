# OkooneBlogger
## Using ASP.NET Core MVC & Web API
### Okoone Blogger Assessment for Interview
---
## Requirements & Includes
* Dotnet Core 2.1
* Visual Studio 2017 (For Deploy codes)
* Telerik 2018.3 R1 (KendoUI + Bootstrap 3+)
* EF Core (Latest)
* Newtonsoft (Latest)
* Session
---
## Installation
    git clone https://github.com/SomboChea/OkooneBlogger.git
<br />

    dotnet restore
    dotnet run
---
## Functional
* Articles
    * List
    * Create
    * Delete
    * Update
    * Read
* Users
    * List
    * Create
    * Delete
* Web Api
    * List all Articles
    * List all Articles with Authors
    * Inferior by date
    * Get by specific article
    * List all Users
    * Get by specific user
---
## Web API
### Articles
    List All Articles: /api/articles

    List Article by Id : /api/articles/1

    List All Articles with Authors : /api/articles?with=authors

    Inferior Articles Date : /api/articles?date=2018-11-24

    Inferior Articles Date using find: /api/articles/2018-11-24

### Users

    List All Users : /api/users

    List User by Id : /api/users/1

    List All Users with Articles: /api/users?with=articles
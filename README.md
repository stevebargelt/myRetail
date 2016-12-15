# myRetail

Features:
* ASP.Net Core (will run on Mac OS, Linux, Windows)
* MongoDB
* Full Docker-ized CI/CD pipeline [LINK]
* Swagger API Documentation 


## Build and Run

These instruction have only been tested on a Macbook running MacOS. You can be on Linux or Windows as a client, though some of the client-side tooling changes around a bit.

### Docker (preferred method!)

Prerequisites: [Docker](http://www.docker.com), [dotnet core](https://www.microsoft.com/net/core)

At a shell prompt:

~~~~
buildme.sh
~~~~

Browse to: [http://localhost:8001/swagger/ui](http://localhost:8001/swagger/ui)

### Locally
Prerequisites: [MongoDB](), [dotnet core](https://www.microsoft.com/net/core)

Create and seed MongoDB:

~~~~
mongo
use myRetail
db.createCollection('Products')
exit

mongoimport --db myretail --collection Products --drop --file ./mongo-seed/primer-target.json
~~~~

In myretail/src/myretail/Models/DataAccess.cs change
~~~~
_client = new MongoClient("mongodb://mongo:27017");
~~~~
to
~~~~
_client = new MongoClient("mongodb://127.0.0.1:27017");
~~~~

Back at the shell:
~~~~
cd src
cd myRetail
dotnet restore
dotnet run
~~~~

Browse to: [http://localhost:5000/swagger/ui](http://localhost:5000/swagger/ui)


## Test Data

### Stored without price
13860428
16696652

### Stored Locally with Price
50567541
50859085
11302975

### Prices without info from redsky
14628678
50437535
15416243
50064723
16271062

### Test Price Updates
#### Update a product without price (null)
{"targetid": 13860428,"name": "The Big Lebowski (Blu-ray)", "current_price": {"value": 13.99,"currency_code": "USD"}}

#### Update a product with price
{"targetid": 50567541,"name": "LEGO&#174; Star Wars&#153; AT-ST&#153; Walker 75153","current_price": {"value": 89.99,"currency_code": "USD"}}
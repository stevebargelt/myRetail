# myRetail




## MongoDB

docker run -it --link mongo:mongo --rm mongo sh -c 'exec mongo "$MONGO_PORT_27017_TCP_ADDR:$MONGO_PORT_27017_TCP_PORT/test"'

use myRetail
db.createCollection('Products')

db.Products.insert({'targetid':13860428,'name':'The Big Lebowski (Blu-ray) (Widescreen)','current_price':{'value': 13.49,'currency_code':'USD'}})

## Test Data

### Stored without price
13860428
16696652

### Stored Locally with Price
50567541
50859085
11302975

### Prices without info from redsky
51848010
14628678
50437535
15416243
50064723
16271062
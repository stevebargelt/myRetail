# myRetail




## MongoDB

docker run -it --link mongo:mongo --rm mongo sh -c 'exec mongo "$MONGO_PORT_27017_TCP_ADDR:$MONGO_PORT_27017_TCP_PORT/test"'

use myRetail
db.createCollection('Products')

db.Products.insert({'targetid':13860428,'name':'The Big Lebowski (Blu-ray) (Widescreen)','current_price':{'value': 13.49,'currency_code':'USD'}})
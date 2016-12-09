FROM mongo

COPY primer-target.json /primer-target.json
CMD mongoimport --host mongo --db myretail --collection Products --drop --file /primer-target.json
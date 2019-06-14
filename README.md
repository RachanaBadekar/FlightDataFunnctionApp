This is a near Real - Time Flight Tracking App using several Azure technologies which tracks all the flights over United States.

I have used Azure Maps to draw the Map Canvas.
In order to get the real time flight information I have created a timer triggered function which pulls the latest flight data from OpenSky
Network Public API. The flight data is then stored in Cosmos Database.

Later a Azure function listens to the Cosmos DB change feed and updates an Azure SignalR hub with all the changes.

I have added some additional functionality where if you hover over a particular flight a pop-up displays all the flight related information
acquired from the OpenSky API in readable format.

In order to run the app clone this repo in after creating a visual studio web project. Once you have the VS project running just run the
index.html file in a browser to see the magic!


References: 
https://www.youtube.com/watch?v=w_G3S6q02JE&t=6s
https://davetheunissen.io/real-time-flight-map-w-azure-functions-cosmosdb-signalr/

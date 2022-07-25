How to Run the solution
-------------------------
You will need to run docker compose, in order to load up an event store instance

In the solution's directory, please run  docker-compose -up

The API is listening in http://localhost:5051/swagger/index.html

We can monitor events in http://localhost:2113/web/index.html#/dashboard

Click on the 'Stream Browser' and we should be able to monitor the events that have been submitted.

Tip: Guid Generator https://www.guidgenerator.com/online-guid-generator.aspx

i.e Successful request
-----------------------------------
```json
{
"bookingId": "c8c80b34-5d1f-4c9b-adaa-11a7b0a25d1c",
"propertyId": "c8c80b34-5d1f-4c9b-adaa-11a7b0a25d1d",
"agentId": "ef3ad68f-b568-41fc-8813-b958a114bffb",
"from": "2022-07-19T12:57:19.906Z",
"to": "2022-07-19T14:57:19.906Z",
"day": "2022-07-19T23:57:19.906Z"
}`

Response

`{ 
 "state":
 {
"agentId": "string",
"propertyId": "c8c80b34-5d1f-4c9b-adaa-11a7b0a25d1d",
"id": {}
},
"success": true,
"changes": [
{
"event": {
"bookingId": "c8c80b34-5d1f-4c9b-adaa-11a7b0a25d1c",
"agentId": "string",
"propertyId": "c8c80b34-5d1f-4c9b-adaa-11a7b0a25d1d",
"fromHour": "2022-07-19T12:57:19.906",
"toHour": "2022-07-19T14:57:19.906",
"submittedBy": "User",
"submittedAt": "0001-01-01T00:00:00"
},
"eventType": "V1.BookingSubmitted"
}
]
}```

 A request that fails the 2 hour Validation Rule

`{
"bookingId": "c7c80b34-5d1f-4c9b-adaa-11a7b0a25d1d",
"propertyId": "c3c80b34-5d1f-4c9b-adaa-11a7b0a25d1e",
"agentId": "ef3ad68f-b568-41fc-8813-b958a114bffb",
"from": "2022-07-19T19:57:19.906Z",
"to": "2022-07-19T21:57:19.906Z",
"day": "2022-07-19T23:57:19.906Z"
}`

Response

`{
"errorMessage": "Booking can't be made after 18:00",
"message": "Error handling command BookAppointment",
"state": null,
"success": false,
"changes": null
}`

Send Grid
----------
There is an 'Infrastructure' folder. I have added an event Handler `NotificationsHandler`.
The idea is that once we subscribe to the events, we can enable any follow up activity via projections. 
We can push events in a NoSql or we can send out notifications.

Commands
--------
In a real scenario, we could enhance commands for validating requests

Mocked Services
---------------
The intent of the solution is to show an end to end flow for a successful submission, or a submission that fails business rules.
I didn't go "very far" with them as they are only mocked services. 

Application services vs Domain Services
--------------
There is a fine line between them. The idea is that the requests will be forwarded to the domain model.
We use Func delegates as we don't want to pollute the aggregate with external dependencies. Domain service will verify if those commands can be
processed or else throw a domain exception.

Projections Using MongoDB
-------------------------

At the moment the Query loads our bookings from the aggregate store. Although, there is no rule that prohibits Queries against the streams,very often
we prefer to send projections to the reporting models. We could point our query to the MongoDB.

As the events are emitted from the aggergates, we project them via the subscription services and we store them in MongoDB.
This is happening regardless as we have already subscribed al all streams 

      services.AddSubscription<AllStreamSubscription, AllStreamSubscriptionOptions>(
                "BookingsProjections",
                builder => builder
                    .Configure(cfg => cfg.ConcurrencyLimit = 2)
                    .UseCheckpointStore<MongoCheckpointStore>()
                    .AddEventHandler<BookingStateProjection>()
                    .WithPartitioningByStream(2));

Docker compose has also created a mongo Instance. To visualize it, you can download Mongo DB Compass and use the following connection string:
mongodb://mongoadmin:secret@localhost:27017

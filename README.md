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
`{
"bookingId": "c8c80b34-5d1f-4c9b-adaa-11a7b0a25d1c",
"propertyId": "c8c80b34-5d1f-4c9b-adaa-11a7b0a25d1d",
"agentId": "string",
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
}`

A request that fails the 2 hour Validation Rule

`{
"bookingId": "c7c80b34-5d1f-4c9b-adaa-11a7b0a25d1d",
"propertyId": "c3c80b34-5d1f-4c9b-adaa-11a7b0a25d1e",
"agentId": "string",
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
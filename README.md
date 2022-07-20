How to Run the solution
-------------------------
You will need to run docker compose, in order to load up an event store instance

In the solution's directory, please run  docker-compose -up

The API is listening in http://localhost:5051/swagger/index.html

An example of a successful request
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
---------
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

We can monitor events in http://localhost:2113/web/index.html#/dashboard
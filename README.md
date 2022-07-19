Savills tech test

The problem
Your task is to design a system that allows users of the Savills website to book property viewings. While on a property page, the user will be able to choose an available time slot from a calendar. Once they choose a slot and put in their personal details, they click a button and they'll send a request to the system to book an appointment. Some notes about the system:

each property has a single agent for which you can access their (the agent's) availability calendar
each property has an availability calendar that contains bookings for the property. However, other agencies also use this calendar, so there might be times when there is no Savills agent booked, but the timeslot is still not available.
The user should be able to select any timeslot that is free on both calendars. You should update both calendars with the time picked by the user - both calendars will return errors if you try to book in a slot that is not available. The following restrictions apply:

the user cannot make bookings for before 8AM or after 6PM, even if both calendars have availability at that time
no booking can be longer than 2 hours
Please implement the back-end component responsible for booking a viewing. The request will come as an HTTP request and will have in its payload:

user details (including email)
time slot required by the user (start and end time)
the property ID (a string)
the agent ID (a string)
There are 2 possible results of the user's action:

The appointment was successful - the API returns a success message and both calendars are updated. The user should get a confirmation email.
The appointment was not succesfull - the API returns an error and both calendars contain their original values
The calendar API contracts are as follows:

Agent calendar
Guid CreateApointment(string agentId, DateTime startTime, DateTime endTime)
void DeleteApointment(string agentId, Guid appointmentId)
List<DateTime,DateTime> GetAppointments(string agentId, DateTime startTime, DateTime endTime)
The agent calendar API returns a list of current appointments in the form of a list of start and end dates. The agent API will throw exceptions if the appointment wasn't successfully created or deleted.

Property calendar
bool CreateApointment(string propertyId, DateTime startTime, DateTime endTime)
List<DateTime,DateTime> GetAvailabilities(string propertyId, DateTime day)
The property calendar API returns a list of contigous availabilities. If there's already a 12-13 and 15-16 booking, the system would return [0-12, 13-15, 16-24] (in DateTime format). This API will return null/false in case of errors. It will ignore any time information in day and return the availabilities for the whole day.

Tips for a succesful submission
You can ignore the front end implementation
Make sure to provide tests for your implementation
You can mock out the external components (sending emails and the 2 calendar APIs)
You can ignore time zones
Do regular commits in the repository
Update this readme file to contain information on how to run/test your project
Think about how you'd handle errors when updating the 2 calendars
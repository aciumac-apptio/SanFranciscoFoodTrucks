
##### HOW TO RUN THE APPLICATION
1. Install .NET Core 2.2 Runtime on Linux Ubuntu 18.04 x64 | .NET: 
	Go to this link and follow the instructions on how to install runtime to run the application 
	(https://dotnet.microsoft.com/download/linux-package-manager/ubuntu18-04/runtime-2.2.3)
	P.S: You can choose another Linux distribution if necessary    	

2. In unzipped folder, navigate to cd SanFranciscoFoodTrucks/FoodTrucks directory (where .csproj file lives)
3. Run the application
	Type dotnet run 

P.S: Project uses Newtonsoft.Json library to deserialize JSON objects
	
= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 

I have attached an image of a web application structure for reference ( WebApplicationDiagram.png ).

##### Overall Data Flow and System Design
User makes a request through web browser, then request gets routed to the backend web server by the load balancer, 
and web server serves user's request and sends information back to user.
Web server hosts the web application and runs a background job to pull data from the San Francisco government API and save data in the local cache.

##### Other Considerations
Number of trucks in San Francisco is not on the order of millions or even 100,000 thus having full data replica on 
each backend server should be an acceptable approach in this case.

Given that food truck is not expected to change location very often (every second), it is fine to have the background service pull data once a minute.

By the nature of the FoodTrack application, we expect that 95% of load is going to happen between 9 AM and 1 PM.
A group of backend servers and load balancer are introduced in the proposed design, to address peak load and efficiently distribute incoming network traffic across backend servers.
Load balancer performs following functions:
   * Distributes client requests (network load) efficiently across multiple servers
   * Ensures high availability and reliability by sending requests only to servers that are online
   * Provides the flexibility to add or subtract servers as demand dictates without affecting the application users

= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
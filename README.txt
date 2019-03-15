
##### HOW TO RUN THE APPLICATION
1. Install .NET Core 2.2 Runtime on Linux Ubuntu 18.04 x64 | .NET: 
	Go to this link and follow the instructions on how to install runtime to run the application 
	(https://dotnet.microsoft.com/download/linux-package-manager/ubuntu18-04/runtime-2.2.3)
	P.S: You can choose another Linux distribution if necessary    	

2. In unzipped folder, navigate to cd SanFranciscoFoodTrucks/FoodTrucks directory (where .csproj file lives)
3. Run the application
	Type dotnet run 

P.S: Project uses Newtonsoft.Json library to deserialize JSON objects
	
##### Web Application Design
I have attached an image of a web application structure for reference.
Client makes request through web browser, then request gets routed to the web server by the load balancer.
Web servers host the website and run a background service to pull data from the San Francisco government API.
Given that food truck data is not expected to change every second, it is fine to have the background service
pull data once a minute.

##### Other Considerations
Load balancer will reduce latency experienced by the end user.
Multiple web servers will give us a more redundant system that will reduce downtime in case one of the links fails.
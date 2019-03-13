
	##### HOW TO RUN THE APPLICATION
    1. Install .NET Core 2.2 Runtime on Linux Ubuntu 18.04 x64 | .NET: 
		Go to this link and follow the instructions on how to install runtime to run the application 
		(https://dotnet.microsoft.com/download/linux-package-manager/ubuntu18-04/runtime-2.2.3)
		P.S You can choose another Linux distribution if necessary    	
    
    2. In unzipped folder, navigate to cd SanFranciscoFoodTrucks/FoodTrucks directory (where .csproj file lives)
    3. Run the application
		Type dotnet run 
		
		
	##### Web Application Design
	If this were to be a full scale web application, whose purpose is to display the food trucks in SF open at a given time
	I would build the following structure:
	-Backend application server that will pull data from the San Francisco government's API.
	-Web Server hosts the web application that will request data from the backend application server.
	-Client will interact with the the web application hosted on the server.
	Given that the food trucks don't move every second, backend application server can pull data from the
	API every minute. Given that the number of food trucks in San Francisco is not on the order of millions
	it is fine to store that data in memory on the server without the need for a database.
	Web Application will continuously request data from the backend server more frequently giving the client the most up to date information.
	
	##### Other Considerations
	Given that then number of clients can be large it may be wise to add a load balancer between the client and multiple web servers to reduce latency.
	We can consider adding another load balancer between the web servers and the backend servers for the same purpose.
	Multiple web and backend servers will give us a more redundant system that will reduce downtime in case one of the links fails.
	
	


OR Installing from a ZIP File
Download and Extract:
Download the ZIP file that contains the solution.
Extract the ZIP file to a folder of your choice on your computer.

OR Installing from Git
Clone the Repository:
Open a terminal or Git Bash.
Clone the repository by running:bash Copy git clone <repository-url>  Replace <repository-url> with the actual URL of your private or public Git repository.



Open the Solution:
Open your IDE (for example, Visual Studio).
In Visual Studio, go to File > Open > Project/Solution and select the solution file (e.g., DiscussionApiSolution.sln) from the extracted folder.
Alternatively, open the folder in Visual Studio Code.
Restore NuGet Packages:
Open a terminal/command prompt in the solution’s root folder.
Run the following command:bash Copy dotnet restore  
Build the Solution:
From the terminal, run:bash Copy dotnet build 
Or use your IDE’s build functionality.
Run the API:
To start the Web API, run:bash Copy dotnet run --project Discussion.API 
The API should start and typically listen on a local port (e.g., http://localhost:5000).
Test the API:
Open a browser and navigate to the Swagger UI at:bash Copy http://localhost:5000/swagger
Alternatively, use Postman or another API client to test the endpoints.
Run Unit Tests (Optional):
In the solution root, run:bash Copy dotnet test
This will execute all the unit tests (which use xUnit).




END POINTS

Create a New Discussion
URL: POST /api/discussions
Request Body: json Copy   {
  "username": "professor1",
  "content": "How is your day going so far?"
}
  
Response: json Copy   {
  "discussionId": 1
}
  
2. Create a Reply (Comment)
URL: POST /api/discussions/comments
Request Body: json Copy   {
  "username": "student1",
  "parentId": 1,
  "content": "It is going great, thanks!"
}
  
Response: json Copy   {
  "commentId": 2
}
  
3. Retrieve All Comments for a Discussion
URL: GET /api/discussions/{discussionId}/comments
Response Example: json Copy   [
  {
    "id": 1,
    "parentId": null,
    "username": "professor1",
    "content": "How is your day going so far?",
    "createdAt": "2025-02-08T12:34:56Z"
  },
  {
    "id": 2,
    "parentId": 1,
    "username": "student1",
    "content": "It is going great, thanks!",
    "createdAt": "2025-02-08T12:35:30Z"
  }
]
  

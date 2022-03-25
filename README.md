# CSharpExercises
A repo to use as a playground for C# .NET Core Console Apps / .NET Core Web Apps
 
 
1. Create a new .NET Core Web API Service to handle retrieving information about players for your upcoming VR game.
2. Be sure to keep the project namespace within the StampinUp.Service.*
3. DATA should all be retrieved in JSON format
4. Player will have: id, email, name, country
5. Player will also be in multiple platforms so consider having: device id, device name, device purchased date
6. Data we would be interested in getting:
   - An endpoint to retrieve all players
   - An endpoint to retrieve players by id/email
   - An endpoint to insert a new player into a memory collection
   - An endpoint to update an existing players
7. All of the data can be kept in memory, but create a class to hold the player's information and inject it by using dependency injection.
8. How can we structure the player's data in a way that a player can have multiple platforms "VR Platform", "PS5 Platform", and still be able to function for all of those platforms.
9. Some of the users have a third party service, how can we get user data from https://gorest.co.in/ and integrate it into our VR game player's data?
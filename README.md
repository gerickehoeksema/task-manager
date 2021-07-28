# task-manager
Task manager microservice

## How to run API
- Set TaskManager.API as startup project
- Test API with Swagger by executing login endpoint on AuthCotroller
	- username: admin@user
	- password: Password1!
- Get token and copy and paste it to 'Authorize' endpoints

## Obstacles
- had some issues mocking User- and RoleManager

## Resources
References to architecture
https://github.com/jasontaylordev/CleanArchitecture

Unit Testing with a UserManager
https://www.daimto.com/unit-testing-with-a-usermanager/

## Improvements
- would like to improve User- and RoleManager to not have that clutter in the API
- in the near future I would like to have IdentityServer managing all my users
- OData might also be a nice improvement

## Time breakdown
- Day 1: About 2 hours to get basic structure of project
- Day 2: Bit more than 2 hours to start getting db and auth up
- Day 3: +- 4 hours spending on unit tests, commands.queries and api for task

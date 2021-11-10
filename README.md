# Infos Release

## Initial Setup
- Clone this repository > git clone https://github.com/ivamcruz/aspnetcore5-api-unittest-sqlserver
- Exec file RUN.sh
- The tests will be run in the (RUN.sh) file
- Docker build will automatically download images and dependencies
- The browser will be launched

## API Authentication & Authorization
- Auth0 is the name of the technology used for authentication and authorization.
    > https://auth0.com/
- Endpoint to create user
    > url_base/signup
- Endpoint to login user
    > url_base/signin

## API Documentation
- Documentation for endpoint detailing will be provided by swagger
- Endpoint to access swagger
    > url_base/swagger
- Click on each endpoint to read details
- To use endpoints, you must first get the authentication token from endpoint 
    > /signin
- When getting the token, paste it in the upper right corner after clicking the authorize button.
- Use the 'Try it' button and check the CURL version of the call

## Technologies used
- Microsoft SQL Express run on linux with container
    > image for container : https://hub.docker.com/repository/docker/ivamneres/portal-db
- ASP NET CORE 5 run on Windows, with container.
- Unit Test using xUnit and the EntityFramework using InMemoryDatabase
- Entity Framework Core 5 for ORM Database

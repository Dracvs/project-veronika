# Project Veronika
Hahn Application process.

## Abstract

This application is constructed as follows. It composes an API in C# .NET 5.0 with three layers (Data, Domain and the API itself). The API is described in Swagger, and it contains plenty of failovers and error catching. The API also uses an InMemoryDatabase.

The frontend is in Aurelia.IO. This is a single page application. The application is missing a couple of functions due to my lack of knowledge in the framework. When I started this project I didn't even know what Aurelia was. Now I have some more knowledge and I want to learn more, with more depth and breath.

## Docker instructions

- For the Web Api, from the root where the solution file (.sln) is contained:

```dockerfile
# make sure the project compiles
dotnet run

# create a Docker temporal image
docker build -t veronika .

# now we make this run on port 5000 for the HTTP api
docker run -it --rm -p 5000:80 --name veronika-api veronika
```

- Now for the Front end in Aurelia

```dockerfile
# We make sure the project runs properly
npm start

# then we create a temporal image.
docker build -t veronika-aurelia:1 .

# after we run the aurelia web page and it should communicate with the prior container
docker run -it --rm -p 8080:8080 veronika-aurelia:1
```

- We point the browser to http://localhost:8080 or the IP of the server with the container. It should load with three items in the InMemoryDB.

## A note about me

I will continue uploading changes to this repository until I feel is feature complete. You can see the progress in the GitFlow and the branches. Each branch is a functionality.

This will be forked into an API template that will have plenty of things already done to save time on new projects or in new microservices.

I will add as well another layer of abstraction for services, and other for infraestructure eventually.

Both projects run inside a docker container, one for each, you could run them following the instructions in the previous section.

I truly wish I had more knowledge about Aurelia to do more grandiouse things. But learning as I went was a challenge, that I welcome. And that gave my brain a workout. It has been sometime since I've got such mental workout.

I do leave this repo as your consideration.
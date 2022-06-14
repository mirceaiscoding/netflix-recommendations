

# Movie4u

### App used for finding Netflix titles available in your country 

Mobile application written in flutter, with a music player like interface where you can like/dislike movies to get more/less movies like them or add them to your watchlist.

# Team members


- [Mircea Bina](https://github.com/mirceaiscoding)
- [Stefan Grigorescu](https://github.com/StefanGrigorescu)
- [Denis Troaca](https://github.com/TroacaDenis)


# App description

After logging in, the user is shown a list of different movies based on previously liked movies. He can then navigate between them by swiping left or right or by pressing one of the bottom arrows. Each movie can be added to the watchlist and liked or disliked using the buttons under the movie. The user can access this watchlist and lists for liked and disliked movies by using the menu at the top right corner of the screen.


# User stories

1.  As a user I would like to be able to log in or out and create an account
2. As a user I would like to be able to like/dislike titles and this to affect future recommendations 
3. As a user I would like to only get titles available in my country  
4. As a user I would like to add or remove titles from a watchlist
5. As a user I would like to select between tv shows and movies
6. As a user I would like to be able to select a broad genre (drama, comedy, horror, etc) that I'm looking for.
7. As a user I would like my latest ratings to have more effect on the new recommendations 
8. As a user I would like to mostly be recommended highly rated movies
9. As a user I would like to only be recommended movies I haven't rated before
10. As a user I would like to know the imdb rating and description of titles
11. As a user I would like to be able to swipe through recommendations (or press a button)


# Backlog

<img width="1440" alt="photo1" src="https://user-images.githubusercontent.com/79210519/173343556-ee7f5f34-d067-419b-87a6-fc49dd2f316f.png">

https://github.com/mirceaiscoding/netflix-recommendations/projects/1


# UML diagram


![UML](https://user-images.githubusercontent.com/79210519/173590391-a84b0d40-c23c-4780-b0bd-ed0d4575ce56.png)


# Source control


Used Sourcetree for branch management and Github for hosting.

<img width="1440" alt="photo2" src="https://user-images.githubusercontent.com/79210519/173346665-62e6cbfc-aad3-4bfd-9fc2-c445c8c7164e.png">

Branches: https://github.com/mirceaiscoding/netflix-recommendations/branches


# Bug reporting


Used Github issues for bug reporting.

<img width="1440" alt="photo3" src="https://user-images.githubusercontent.com/79210519/173348163-56fe7a9a-52af-4935-b029-4b262f0acc68.png">

https://github.com/mirceaiscoding/netflix-recommendations/issues?q=is%3Aissue+is%3Aclosed+label%3Abug


# Build tools


For the frontend: our project uses Visual Studio Code for writing the flutter program and Xcode for simulating a mobile interface (iphone) for the app.

The backend is written in .NET with Visual Studio as the IDE. It Connects to a cloud database (Microsoft Azure SQL Database)  populated with movies from a public Netflix API ([uNoGS](https://rapidapi.com/unogs/api/unogs/)).


# Refactoring


<img width="1440" alt="photo4" src="https://user-images.githubusercontent.com/79210519/173361343-f117137c-8ca0-40dc-b5f9-2195826e0979.png">

Multiple issues that require refactoring: https://github.com/mirceaiscoding/netflix-recommendations/issues?q=is%3Aissue+label%3Arefactoring+is%3Aclosed


# Design patterns

### Controller - Model - Entity in .NET

Controllers give access to the backend from the frontend. In them we defined endpoints that have CRUD (Create Read Update Delete) functionality. In addition some endpoints have more complex logic hidden and can do Batch operations.

Models are used in the upper layers of the backend and can be converted to and from entities.

Entities follow the design of the database and may have virtual fields (that are populated using joins). Since entity framework is code-first the database is automatically modelled using migrations (the entity lists are transformed in tables).

### Generic Repository in .NET

As most repositories need the basic methods like `GetById`, `Delete`, etc we created a `GenericRepository<Entity>` that has most of the methods needed.

### Singleton service in Flutter

Used a singleton service for the titles of a session. It holds the list of titles loaded, it can load more from the database and update the preferences based on new interactions with the titles (like/dislike).

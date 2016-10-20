# Full Security Login System with Spring Boot

This project contains all the configuration about an authentication and authorization system. 
We are using spring boot framework, spring security, spring oauth token and mysql. Cause we want ours projects be scalable as 
they can we are using the JWT token key for authentication with oauth2, this means that we don’t need sessions and  session 
storage system so our application will work faster and better even in cloud server without any configuration.

### Features
User Auhtentication

User Authorazation

Account Registration

Forgot Password (Not Ready yet)

### The concept of OAuth2 authentication and JWT
**OAuth1**
---
OAuth1 has 3 steps and one loop. Check the diagram bellow and you will understand what we define as step and loop.
![alt tag](http://s12.postimg.org/mnjxjivrx/Screen_Shot_2016_02_13_at_8_27_17_PM.png)

**loop**: We can say that a loop is a request between client and server

**step**: As a step we define the blocks of algorithm “mythology”

In login system client and server have to follow those steps

- Sent the username and password to server via post request
- server will send to the client the access token
- client now have to store the access token into cookies or local storage

When client now want to get all products (for example)

- Client should send the access token as header parameter
- server parse all request the check if the header access token field is correct
- then return all products

**OAuth2**
---
OAuth2  is more complex that OAuth, it has 3 loops and 3 steps in each loop.
![alt tag](http://s12.postimg.org/t7rb3lz6l/Screen_Shot_2016_02_13_at_8_05_56_PM.png)

**In the first loop**

- client send the username and the password throw get request
- server return the refresh token. Refresh token is something that give the ability to user to request for re access token. The main difference
between them are that the refresh token exist for a lot longer and but the access token is the only one that give to you the ability to have real
access to the application.
- client get the refresh token and have to store it to cookies or local storage

**The second loop**

- client is ready to request for an access token, so it sends to server a get request with the refresh token to get the access token.
- server check for the refresh token and if this is correct then
- return to client the access token

**The third loop**

- Now user have to send the access token via get or as header parameter
- Server analyze the access token and if this is correct then
- return the request to user

### Database
As database we are using the **MySql**, if you want to run the project you want edit the **resource/config/application.properties** file and add your database settings. After that import the databse.sql file that exists in root directory

### How to run it
- First **Download** the project
- Install all **Maven Dependencies**
- Edit the **resource/config/application.properties** file and add your properties (Mysql Database, Gmail - Email Sender)
- After that import the **Databse.sql** file that exists in root directory
- Run the project by **spring-boot:run** agian

### How to use it
To run this example you must import the databse file that exists in root directory

-First ask for a access token  **curl -X POST -vu clientapp:123456 http://localhost:8080/oauth/token -H "Accept: application/json" -d "password=papidakos123&username=papidakos&grant_type=password&scope=read%20write&client_secret=123456&client_id=clientapp"**

-Now Try with postman get data from secure api uri. **Don't forget to replace the headers token with your access token**
![alt tag](http://s23.postimg.org/xmuqxrzsb/Screen_Shot_2016_02_29_at_4_00_24_PM.png)




*The project has been created with Intelli IDEA IDE*

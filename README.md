# AkuznetsovReddit
Simple project like Reddit created with .NET MVC4, Entity Framework 6, Autofac, AutoMapper. 
In project implemented custom login system. Two types of users (Admin and User). Passwords salted, has 3 failed attempts and 30 min blocks.  
#For Admin ability to:
  
  1. CRUD on Topics. (Deletions are all logical, keeps topic in db, make them inactive).
  2. Restore to Active Topic.
  3. CRUD on all Posts (Articles) Deletions again logical.
  
#For User ability to:
  1. CRUD on Posts (Articles). Deletions again logical.
  2. CRUD only on there own Posts.
  
  Project Also contain unit tests. using xUnit and Moq.

# Grapefruit.VuCore | [中文](/README_zh-CN.md "中文")
Grapefruit.VuCore is a front-background template project which built by ASP.NET Core and Vue.js. 

The goal of this project is want to demonstrate the entire process of implementation about how to use ASP.NET Core Web Api and Vue.js to build a front-background project.

I will show you a series of articles about my coding journey for building this project, if you are interseted in it, you can follow me in the following websites.

- [https://yuiter.com/2018/08/15/ASP-NET-Core-on-Linux-Overview/](https://yuiter.com/2018/08/15/ASP-NET-Core-on-Linux-Overview/ "yuiter.com") (This is my personal site, I will post article first, hope more attention about it! ​)
- [https://www.cnblogs.com/danvic712/p/10124831.html](https://www.cnblogs.com/danvic712/p/10124831.html "www.cnblogs.com")
- [https://juejin.im/user/5bd93a936fb9a0224268c11b](https://juejin.im/user/5bd93a936fb9a0224268c11b "juejin.im")

PS: Currently only Chinese posts version, sorry for it.



#  :star: Give a Star!  :star: 

If you like it or this project helped you, I hope you can give a star for it. Thanks =^_^=



# Environment Requirements

- IDE
  - Visual Studio 2017: For ASP.NET Core Web API development, you can use Visual Studio Code to replace it.
  - Visual Studio Code: For front project development
- Development  Environment
  - .NET Core SDK: This project is built by .NET Core SDK version 2.1, make sure your development machine has installed .NET Core SDK which higher than 2.1 version.
  - MySQL Server/SQL Server: This project use MySQL Server 8.0 or SQL Server 2012  as project's database, you can chose one of it.
  - MongoDB: In this project I will store logs info in MongoDB.
  - Redis: In this project I use redis as a distributed caching tool.
  - Node.js: In this project I use Vue-CLI to build my front project and in front project developing we will use npm or yarn as our packages management tool, so you shoud make sure your development machine has installed Node.js version 8.9 or above.
  - Git: In this project I use git as a version control tool, if you do not want use it, you can not install it.



# Technologies(To be determined)

- Background Project Framework: ASP.NET Core 2.1 Web API
- Front Project Framework: Vue.js(Created by Vue CLI)
- ORM: Dapper
- Object-Object Mapper: AutoMapper
- Logging:  Use NLog to store logs in MongoDB
- Permission: Use Jwt token and claims-based authorization



# License
The Grapefruit.VuCore was developed by [Lanesra712](https://github.com/Lanesra712 "Lanesra712") and under the [MIT License](/LICENSE "MIT License").

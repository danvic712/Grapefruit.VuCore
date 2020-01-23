## 注意：

这个项目是当时开始学习 ASP.NET Core Web API 时创建的，经过自己一年多的练手以及在工作中的使用经验，很多东西已经不太适合了，因此，这个项目将不再更新任何的代码，后续会将所有的代码移动到 [ingos-server](https://github.com/Lanesra712/ingos-server "") 这个基于领域驱动思想构建的项目中，感谢大家的支持~~~

# Grapefruit.VuCore | [English](/README.md "English")

Grapefruit.VuCore 是一个使用 ASP.NET Core 和 Vue.js 构建的前后端分离模板项目。

这个项目的目的是为了展示我是如何使用 ASP.NET Core Web Api 和 Vue.js 进行构建这个项目的全过程。

我会通过一系列的文章来向你展示构建这个项目的全过程，如果你对此有兴趣的话，你可以在下列的网站中去关注我，去查看这些文章。

- [https://yuiter.com/2018/08/15/ASP-NET-Core-on-Linux-Overview/](https://yuiter.com/2018/08/15/ASP-NET-Core-on-Linux-Overview/ "yuiter.com") (我的个人网站，我会第一时间发布文章到这个站点上，欢迎多多关注啊! )
- [https://www.cnblogs.com/danvic712/p/10124831.html](https://www.cnblogs.com/danvic712/p/10124831.html "www.cnblogs.com")
- [https://juejin.im/user/5bd93a936fb9a0224268c11b](https://juejin.im/user/5bd93a936fb9a0224268c11b "juejin.im")



#  :star: 给个小星星!  :star: 

如果你喜欢这个项目，或是这个项目有帮到你，我希望你可以给这个项目个小星星，谢谢啊 =^_^=



# 开发环境要求

- IDE
  - Visual Studio 2017: 用来开发 Web API 项目，当然，你也可以使用 Visual Studio Code 进行开发。
  - Visual Studio Code: 用来开发调试前端的 Vue 项目
- Development  Environment
  - .NET Core SDK: 这个项目是基于 .NET Core  SDK 2.1 版本进行构建的，你需要确保你的开发机有安装 .NET Core SDK 并且版本不低于 2.1。
  - MySQL Server/SQL Server: 这个项目可以使用  MySQL Server 8.0 或是 SQL Server 2012  作为项目的数据库使用，你可以其中的一个使用。
  - MongoDB: 在这个项目中，我使用 MongoDB 存储我们的日志信息。
  - Redis: 在这个项目中，我使用 Redis 作为我们的分布式缓存工具。
  - Node.js: 在这个项目中，我是通过 Vue-CLI 构建的 Vue 项目，同时，在前端开发中，我们是使用 npm 或是 yarn 作为我们的包管理工具，因此，你需要在你的开发机上安装 Node.js，同时，确保版本高于 8.9。
  - Git: 在这个项目中，我是使用的 Git 作为项目的版本控制工具，如果你不需要的话，可以不安装这个。



# 使用到的技术(待定)

- 后端框架: ASP.NET Core 2.1 Web API
- 前端框架: 通过 Vue-CLI 创建的 Vue 项目
- ORM: Dapper
- Object-Object Mapper: AutoMapper
- 日志记录:  使用 NLog 将日志信息记录到 MongoDB 中
- 权限管理: 使用 Jwt token 授权辅助基于声明的验证（claims-based authorization）进行项目的权限验证



# License

Grapefruit.VuCore 是由 [Lanesra712](https://github.com/Lanesra712 "Lanesra712") 开发的，同时采用 [MIT License](/LICENSE "MIT License") 协议.
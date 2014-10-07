
SET IDENTITY_INSERT [restorder].[dbo].[tblSubject] ON

INSERT INTO [restorder].[dbo].[tblSubject]
           (
			SubjectId
           ,[MasterSubjectId]
           ,[SubjectType]
           ,[Description]
           ,[TableName]
           ,[ManageUrl]
           ,[EditUrl]
           ,[ListUrl]
           ,[SubjectLabel]
           ,[AllowListFiltering]
           ,[IsAddInGrid]
           ,[IsGridInFormEdit]
           ,[AllowListAdd]
           ,[AllowListEdit]
           ,[AllowListDelete]
           ,[SubjectIdField]
           ,[MasterSubjectIdField]
           )
     VALUES
           (
           29,
           NULL,
           'Post',
           NULL,
           'tblPost',
           '/webPages/Post/PostManager.aspx?PostId={0}',
           '/WebPages/Post/PostEdit.aspx?PostId={0}',
           '/WebPages/Post/PostMgt.aspx',
           'Post',
           1,
           0,
           0,
           1,
           1,
           1,
           'PostId',
           'TopicId'
           )
           
SET IDENTITY_INSERT [restorder].[dbo].[tblSubject] OFF
           
GO


SET IDENTITY_INSERT [restorder].[dbo].[tblMainMenu] ON

INSERT INTO [restorder].[dbo].[tblMainMenu]
           (
           MainMenuId
           ,[Name]
           ,[MenuText]
           ,[Tooltip]
           ,[NavigateUrl]
           ,[MenuTypeId]
           ,[Sort]
           ,[ImageUrl])
     VALUES
           (
           18,
           'News',
           'News Manage',
           NULL,
           '/WebPages/News/NewsMgt.aspx',
           NULL,
           24,
           '/Images/News.png'
           )

INSERT INTO [restorder].[dbo].[tblMainMenu]
           (
           MainMenuId
           ,[Name]
           ,[MenuText]
           ,[Tooltip]
           ,[NavigateUrl]
           ,[MenuTypeId]
           ,[Sort]
           ,[ImageUrl])
     VALUES
           (
           19,
           'Topic',
           'Topic Manage',
           NULL,
           '/WebPages/Topic/TopicMgt.aspx',
           NULL,
           25,
           '/Images/Topic.png'
           )

INSERT INTO [restorder].[dbo].[tblMainMenu]
           (
           MainMenuId
           ,[Name]
           ,[MenuText]
           ,[Tooltip]
           ,[NavigateUrl]
           ,[MenuTypeId]
           ,[Sort]
           ,[ImageUrl])
     VALUES
           (
           20,
           'NewsList',
           'News List',
           NULL,
           '/Shop/NewsList.aspx',
           NULL,
           26,
           '/Images/NewsList.png'
           )

INSERT INTO [restorder].[dbo].[tblMainMenu]
           (
           MainMenuId
           ,[Name]
           ,[MenuText]
           ,[Tooltip]
           ,[NavigateUrl]
           ,[MenuTypeId]
           ,[Sort]
           ,[ImageUrl])
     VALUES
           (
           21,
           'Forum',
           'Forum',
           NULL,
           '/Social/Forum.aspx',
           NULL,
           27,
           '/Images/Forum.png'
           )

SET IDENTITY_INSERT [restorder].[dbo].[tblMainMenu] OFF

SET IDENTITY_INSERT [restorder].[dbo].[tblMainMenu] ON
INSERT INTO [restorder].[dbo].[tblMainMenu]
           (
           MainMenuId
           ,[Name]
           ,[MenuText]
           ,[Tooltip]
           ,[NavigateUrl]
           ,[MenuTypeId]
           ,[Sort]
           ,[ImageUrl])
     VALUES
           (
           22,
           'OrderTracking',
           'Order Tracking',
           NULL,
           '/Shop/OrderTracking.aspx',
           NULL,
           28,
           '/Images/OrderTracking.png'
           )

SET IDENTITY_INSERT [restorder].[dbo].[tblMainMenu] OFF

GO

--INSERT INTO [restorder].[dbo].[tblEntity]
--           ([Code]
--           ,[Description]
--           ,[EntityTypeId]
--           ,[IsBuiltIn]
--           ,[AllowAddItem]
--           ,[AllowEditItem]
--           ,[AllowDeleteItem]
--           ,[EntityKey])
--     VALUES
--           (<Code, nvarchar(100),>
--           ,<Description, nvarchar(100),>
--           ,<EntityTypeId, int,>
--           ,<IsBuiltIn, bit,>
--           ,<AllowAddItem, bit,>
--           ,<AllowEditItem, bit,>
--           ,<AllowDeleteItem, bit,>
--           ,<EntityKey, nvarchar(50),>)
--GO

--INSERT INTO [restorder].[dbo].[tblEntityItem]
--           ([EntityId]
--           ,[Text]
--           ,[Value])
--     VALUES
--           (<EntityId, int,>
--           ,<Text, nvarchar(100),>
--           ,<Value, int,>)
--GO





USE
[UserManagementSystem]
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Username], [Id], [FName], [LName], [Email], [Role], [PasswordHash], [CreatedDate]) VALUES (N''harshana'', 3, N''Harshan'', N''Perera'', N''harshana@gmail.com'', N''User'', N''$2a$11$RjvgBeynPvEYQZTjDD7YD.oq1SQW2SKCUs8Ib/7XVAqwRzYq5oyk.'', CAST(N''2023-06-26T17:55:16.5211350'' AS DateTime2))
INSERT [dbo].[Users] ([Username], [Id], [FName], [LName], [Email], [Role], [PasswordHash], [CreatedDate]) VALUES (N''nadika'', 1, N''Nadika'', N''Koshala'', N''nadika@gmail.com'', N''Admin'', N''$2a$11$eZFD/r.aYx8AnFVsqAquHubImT.GIPDejYYUp/pWkU31y8V86SSTO'', CAST(N''2023-06-26T17:47:39.4807918'' AS DateTime2))
INSERT [dbo].[Users] ([Username], [Id], [FName], [LName], [Email], [Role], [PasswordHash], [CreatedDate]) VALUES (N''yohan'', 2, N''Yohan'', N''Sandun'', N''yohan@gmail.com'', N''Staff'', N''$2a$11$lHLmyDqwefUJBke3wqXW4O23ulGR5z9QwzZMb5/68pDuyJqoD1Sg.'', CAST(N''2023-06-26T17:54:31.3209891'' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO

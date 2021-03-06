USE [ApplicationVersions]
GO
/****** Object:  Table [dbo].[Application]    Script Date: 7/26/2018 10:53:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Application](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[ProjectID] [bigint] NOT NULL,
	[StatusTypeID] [int] NOT NULL,
	[ActiveTFRelease] [nvarchar](50) NULL,
	[DisplayName] [nvarchar](256) NULL,
 CONSTRAINT [PK_Application] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EnvironmentType]    Script Date: 7/26/2018 10:53:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EnvironmentType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](60) NOT NULL,
 CONSTRAINT [PK_EnvironmentType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 7/26/2018 10:53:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[EventTypeID] [int] NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[ServerID] [int] NOT NULL,
	[msiFile] [nvarchar](250) NULL,
	[Version] [nvarchar](30) NOT NULL,
	[EnvironmentTypeID] [int] NOT NULL,
	[EventDate] [datetime] NOT NULL,
	[Installer] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventType]    Script Date: 7/26/2018 10:53:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](60) NOT NULL,
 CONSTRAINT [PK_EventType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 7/26/2018 10:53:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[TFprojID] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Server]    Script Date: 7/26/2018 10:53:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Server](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[StatusTypeID] [int] NOT NULL,
 CONSTRAINT [PK_Server] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StatusType]    Script Date: 7/26/2018 10:53:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](60) NULL,
 CONSTRAINT [PK_StatusType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[EnvironmentType] ON 

INSERT [dbo].[EnvironmentType] ([ID], [Description]) VALUES (1, N'Development')
INSERT [dbo].[EnvironmentType] ([ID], [Description]) VALUES (2, N'Test')
INSERT [dbo].[EnvironmentType] ([ID], [Description]) VALUES (3, N'Production')
INSERT [dbo].[EnvironmentType] ([ID], [Description]) VALUES (4, N'Training')
SET IDENTITY_INSERT [dbo].[EnvironmentType] OFF

SET IDENTITY_INSERT [dbo].[EventType] ON 

INSERT [dbo].[EventType] ([ID], [Description]) VALUES (1, N'Install')
INSERT [dbo].[EventType] ([ID], [Description]) VALUES (2, N'Uninstall')
INSERT [dbo].[EventType] ([ID], [Description]) VALUES (3, N'Upgrade')
INSERT [dbo].[EventType] ([ID], [Description]) VALUES (4, N'Other')
SET IDENTITY_INSERT [dbo].[EventType] OFF

SET IDENTITY_INSERT [dbo].[StatusType] ON 

INSERT [dbo].[StatusType] ([ID], [Description]) VALUES (1, N'Active')
INSERT [dbo].[StatusType] ([ID], [Description]) VALUES (2, N'Inactive')
SET IDENTITY_INSERT [dbo].[StatusType] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [U_Name]    Script Date: 7/26/2018 10:54:00 AM ******/
ALTER TABLE [dbo].[Application] ADD  CONSTRAINT [U_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [U_Project_Name]    Script Date: 7/26/2018 10:54:00 AM ******/
ALTER TABLE [dbo].[Project] ADD  CONSTRAINT [U_Project_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [U_Server_Name]    Script Date: 7/26/2018 10:54:00 AM ******/
ALTER TABLE [dbo].[Server] ADD  CONSTRAINT [U_Server_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Event] ADD  CONSTRAINT [DF_Event_EventDate]  DEFAULT (getdate()) FOR [EventDate]
GO
ALTER TABLE [dbo].[Project] ADD  CONSTRAINT [DF_Project_TFprojID]  DEFAULT (N'tbd') FOR [TFprojID]
GO
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_Application_Project] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ID])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_Application_Project]
GO
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_Application_StatusType] FOREIGN KEY([StatusTypeID])
REFERENCES [dbo].[StatusType] ([ID])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_Application_StatusType]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Application] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Application] ([ID])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Application]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_EnvironmentType] FOREIGN KEY([EnvironmentTypeID])
REFERENCES [dbo].[EnvironmentType] ([ID])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_EnvironmentType]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_EventType] FOREIGN KEY([EventTypeID])
REFERENCES [dbo].[EventType] ([ID])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_EventType]
GO
ALTER TABLE [dbo].[Server]  WITH CHECK ADD  CONSTRAINT [FK_Server_StatusType] FOREIGN KEY([StatusTypeID])
REFERENCES [dbo].[StatusType] ([ID])
GO
ALTER TABLE [dbo].[Server] CHECK CONSTRAINT [FK_Server_StatusType]
GO

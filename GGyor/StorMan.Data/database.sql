USE [StorMan]
GO

/****** Object:  Table [dbo].[ConvertedDataSet]    Script Date: 06/27/2013 18:23:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ConvertedDataSet](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[SourceXmlPath] [varchar](500) NOT NULL,
 CONSTRAINT [PK_ConvertedDataSet] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


USE [StorMan]
GO

/****** Object:  Table [dbo].[ConvertedDataSetHistory]    Script Date: 06/27/2013 18:24:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ConvertedDataSetHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ConvertedDataSetID] [int] NOT NULL,
	[SourceXmlPath] [varchar](500) NULL,
	[XmlString] [ntext] NULL,
 CONSTRAINT [PK_ConvertedDataSetHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ConvertedDataSetHistory]  WITH CHECK ADD  CONSTRAINT [FK_ConvertedDataSetHistory_ConvertedDataSet] FOREIGN KEY([ConvertedDataSetID])
REFERENCES [dbo].[ConvertedDataSet] ([ID])
GO

ALTER TABLE [dbo].[ConvertedDataSetHistory] CHECK CONSTRAINT [FK_ConvertedDataSetHistory_ConvertedDataSet]
GO



USE [StorMan]
GO

/****** Object:  Table [dbo].[Transform]    Script Date: 06/27/2013 18:24:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Transform](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ConvertedDataSetID] [int] NOT NULL,
	[FieldName] [varchar](50) NULL,
	[Expression] [varchar](200) NULL,
	[Value] [varchar](200) NULL,
 CONSTRAINT [PK_Transform] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Transform]  WITH CHECK ADD  CONSTRAINT [FK_Transform_ConvertedDataSet] FOREIGN KEY([ConvertedDataSetID])
REFERENCES [dbo].[ConvertedDataSet] ([ID])
GO

ALTER TABLE [dbo].[Transform] CHECK CONSTRAINT [FK_Transform_ConvertedDataSet]
GO



USE [StorMan]
GO

/****** Object:  Table [dbo].[Filter]    Script Date: 06/27/2013 18:24:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Filter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TransformID] [int] NOT NULL,
	[FieldName] [varchar](50) NOT NULL,
	[FilterType] [int] NULL,
	[Value] [varchar](100) NULL,
 CONSTRAINT [PK_Filter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Filter]  WITH CHECK ADD  CONSTRAINT [FK_Filter_Transform] FOREIGN KEY([TransformID])
REFERENCES [dbo].[Transform] ([ID])
GO

ALTER TABLE [dbo].[Filter] CHECK CONSTRAINT [FK_Filter_Transform]
GO



USE [StorMan]
GO

/****** Object:  Table [dbo].[Operation]    Script Date: 06/27/2013 18:24:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Operation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TransformID] [int] NOT NULL,
	[FieldName] [varchar](50) NULL,
	[OperationType] [int] NULL,
	[Value] [varchar](500) NULL,
 CONSTRAINT [PK_Operation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Operation]  WITH CHECK ADD  CONSTRAINT [FK_Operation_Transform] FOREIGN KEY([TransformID])
REFERENCES [dbo].[Transform] ([ID])
GO

ALTER TABLE [dbo].[Operation] CHECK CONSTRAINT [FK_Operation_Transform]
GO




alter table Transform drop column FieldName;
alter table Transform drop column Expression;
alter table Transform drop column Value;

alter table Transform add Name varchar(50);

USE [CurrencyExchangeDB]
GO

/****** Object:  Table [dbo].[Purchase]    Script Date: 26/11/2020 3:18:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Purchase](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[AmountLC] [decimal](18, 2) NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[PurchaseAmount] [decimal](18, 2) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Purchase] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



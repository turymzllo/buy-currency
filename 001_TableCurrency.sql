USE [CurrencyExchangeDB]
GO

/****** Object:  Table [dbo].[Currency]    Script Date: 26/11/2020 3:16:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Currency](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ISOCode] [nvarchar](3) NOT NULL,
	[PurchaseLimit] [decimal](18, 2) NOT NULL,
	[ExchangeRateProvider] [nvarchar](400) NULL,
	[UseUSDFactor] [bit] NOT NULL,
	[USDFactor] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



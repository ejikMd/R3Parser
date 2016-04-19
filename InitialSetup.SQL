
CREATE TABLE MainListing(
	Id [int] IDENTITY(1,1) NOT NULL,
	dateTaken dateTime NOT NULL DEFAULT GETDATE(),
	mlsId [varchar](10) NOT NULL,
	mlsNumber [varchar](10) NOT NULL,
	price [varchar](10) NOT NULL,
	yearBuild [varchar](5) NOT NULL,
	neighbourhoodName [varchar](100) NOT NULL,
	bedrooms int NOT NULL,
	bathrooms int  NOT NULL,
	type [varchar](10) NOT NULL,
	parkingType [varchar](100) NULL,
	addressText [varchar](100) NULL,
	publicRemarks [varchar](4000) NOT NULL,
	relativeDetailsURL [varchar](1000) NOT NULL,
	alternateURL [varchar](1000)  NULL,
	priceCoefficient [int] NOT NULL DEFAULT (0)
 CONSTRAINT [PK_MainListing] PRIMARY KEY CLUSTERED 
(
	Id ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO


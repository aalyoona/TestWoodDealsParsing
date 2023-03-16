CREATE TABLE [dbo].[WoodDeals] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [SellerName]       NVARCHAR (MAX) NOT NULL,
    [SellerInn]        NVARCHAR (12)  NULL,
    [BuyerName]        NVARCHAR (MAX) NOT NULL,
    [BuyerInn]         NVARCHAR (12)  NULL,
    [WoodVolumeBuyer]  FLOAT (53)     NOT NULL,
    [WoodVolumeSeller] FLOAT (53)     NOT NULL,
    [DealDate]         NVARCHAR(10)       NOT NULL,
    [DealNumber]       NVARCHAR (28)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


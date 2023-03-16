create procedure AddWoodDeal
@SellerName nvarchar(max),
@SellerInn nvarchar(12),
@BuyerName nvarchar(max),
@BuyerInn nvarchar(12),
@WoodVolumeBuyer float,
@WoodVolumeSeller float,
@DealDate nvarchar(10),
@DealNumber nvarchar(28)
AS
insert into dbo.WoodDeals
values (@SellerName, @SellerInn, @BuyerName, @BuyerInn, @WoodVolumeBuyer, @WoodVolumeSeller, @DealDate, @DealNumber)
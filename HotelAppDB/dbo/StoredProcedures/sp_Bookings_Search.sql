CREATE PROCEDURE [dbo].[sp_Bookings_Search]
	@mobileno varchar(15),
	@startDate date
AS
begin

	set nocount on;
	select b.*,r.*,g.*,rt.*
	from dbo.Bookings b
	inner join dbo.Guests g 
	on b.GuestId = g.Id
	inner join dbo.Rooms r 
	on b.RoomId = r.Id
	inner join dbo.RoomTypes rt 
	on r.RoomTypeId = rt.Id
	where b.StartDate = @startDate and g.MobileNo = @mobileno;
end

CREATE PROCEDURE [dbo].[sp_Bookings_Cancel]
	@bookingId int
AS
begin
	set nocount on;
	delete from dbo.Bookings
	where Id = @bookingId;
end


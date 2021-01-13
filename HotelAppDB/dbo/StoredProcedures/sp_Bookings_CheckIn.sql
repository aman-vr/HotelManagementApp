CREATE PROCEDURE [dbo].[sp_Bookings_CheckIn]
	@roomId int,
	@startDate datetime
AS
begin
	set nocount on;

	update dbo.Bookings
	set CheckedIn = 1
	where RoomId = @roomId
	and StartDate = @startDate;

end


CREATE PROCEDURE [dbo].[sp_Guests_Insert]
	@firstName nvarchar(50),
	@lastName nvarchar(50),
	@mobileno varchar(15)
AS
begin
	set nocount on;
	if not exists(select 1 from dbo.Guests where FirstName = @firstName and LastName = @lastName and MobileNo = @mobileno)
	begin
		insert into dbo.Guests(FirstName, LastName, MobileNo)
		values(@firstName, @lastName, @mobileno);
	end

	select top 1 [Id],[FirstName],[LastName],[MobileNo]
	from dbo.Guests
	where FirstName = @firstName and LastName = @lastName and MobileNo = @mobileno;
end

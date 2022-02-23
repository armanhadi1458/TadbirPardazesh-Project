CREATE PROC Person_Update
	@Id UNIQUEIDENTIFIER,
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@Age INT
AS
BEGIN
	UPDATE People 
	SET FirstName = @FirstName,
		LastName = @LastName,
		Age = @Age
	WHERE Id = @Id

	SELECT * FROM People WHERE Id = @Id
END
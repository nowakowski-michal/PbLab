CREATE PROCEDURE [dbo].[ProcedureAddNewProduct]
    @ProductName NVARCHAR(100),
    @Price DECIMAL(18,2),
	@Image NVARCHAR(100),
    @GroupID INT

AS
BEGIN
    SET NOCOUNT ON;

    -- Dodajemy nowy produkt
    INSERT INTO Products (Name, Price,Image, GroupID, IsActive)
    VALUES (@ProductName, @Price, @Image, @GroupID, 1) -- Zakładamy, że nowo dodany produkt jest aktywny

    -- Pobieramy ID nowo dodanego produktu
    DECLARE @NewProductID INT
    SELECT @NewProductID = SCOPE_IDENTITY()

END;
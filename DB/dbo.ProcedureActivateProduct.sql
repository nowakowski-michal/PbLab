CREATE PROCEDURE [dbo].[ProcedureActivateProduct]
@ProductID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Sprawdzamy, czy produkt o podanym ID istnieje
    IF EXISTS (SELECT 1 FROM Products WHERE ID = @ProductID)
    BEGIN
        -- Aktywujemy produkt
        UPDATE Products
        SET IsActive = 1
        WHERE ID = @ProductID

        PRINT 'Produkt został aktywowany.'
    END
    ELSE
    BEGIN
        -- Jeśli produkt nie istnieje, wyświetlamy komunikat
        PRINT 'Produkt o podanym ID nie istnieje.'
    END
END;
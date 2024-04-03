
CREATE PROCEDURE [dbo].[ProcedureAddProductToBasket]
    @ProductID INT,
    @UserID INT,
    @Amount INT,
    @Price DECIMAL(18, 2)
AS
BEGIN
    SET NOCOUNT ON;

    -- Sprawdzamy, czy produkt istnieje i czy jest aktywny
    IF EXISTS (SELECT 1 FROM Products WHERE ID = @ProductID AND IsActive = 1)
    BEGIN
        -- Dodajemy nowy produkt do koszyka
        INSERT INTO BasketPositions (ProductID, Amount, Price, UserID)
        VALUES (@ProductID, @Amount, @Price, @UserID);
        
        PRINT 'Produkt został dodany do koszyka.';
    END
    ELSE
    BEGIN
        -- Jeśli produkt nie istnieje lub nie jest aktywny, wyświetlamy komunikat
        PRINT 'Nie można dodać produktu do koszyka. Produkt nie istnieje lub nie jest aktywny.';
    END;
END;
CREATE PROCEDURE [dbo].[ProcedureRemoveProductFromBasket]
    @BasketPositionID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Sprawdzamy, czy istnieje pozycja w koszyku o podanym ID
    IF EXISTS (SELECT 1 FROM BasketPositions WHERE ID = @BasketPositionID)
    BEGIN
        -- Usuwamy produkt z koszyka
        DELETE FROM BasketPositions
        WHERE ID = @BasketPositionID
        
        PRINT 'Produkt został usunięty z koszyka.'
    END
    ELSE
    BEGIN
        -- Jeśli pozycja w koszyku o podanym ID nie istnieje, wyświetlamy komunikat
        PRINT 'Nie można usunąć produktu z koszyka. Pozycja w koszyku nie istnieje.'
    END
END;
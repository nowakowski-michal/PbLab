CREATE PROCEDURE [dbo].[ProcedureChangeAmount]
    @BasketPositionID INT,
    @NewAmount INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Sprawdzamy, czy istnieje pozycja w koszyku o podanym ID
    IF EXISTS (SELECT 1 FROM BasketPositions WHERE ID = @BasketPositionID)
    BEGIN
        -- Aktualizujemy ilość produktu w koszyku
        UPDATE BasketPositions
        SET Amount = @NewAmount
        WHERE ID = @BasketPositionID
        
        PRINT 'Ilość produktu w koszyku została zaktualizowana.'
    END
    ELSE
    BEGIN
        -- Jeśli pozycja w koszyku o podanym ID nie istnieje, wyświetlamy komunikat
        PRINT 'Nie można zmienić ilości produktu w koszyku. Pozycja w koszyku nie istnieje.'
    END
END;
CREATE TRIGGER trg_RemoveProductFromBasket
ON BasketPositions
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM deleted)
    BEGIN
        -- Jeśli usunięte rekordy istnieją, wykonaj operacje usuwania produktów z koszyka
        DELETE FROM BasketPositions
        WHERE ID IN (SELECT ID FROM deleted);

        PRINT 'Produkty zostały usunięte z koszyka.';
    END
    ELSE
    BEGIN
        -- Jeśli nie ma usuniętych rekordów, wyświetl komunikat
        PRINT 'Nie znaleziono pozycji w koszyku do usunięcia.';
    END
END;
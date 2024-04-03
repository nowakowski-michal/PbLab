CREATE PROCEDURE [dbo].[ProcedureGenerateOrder]
     @UserID INT,
    @OrderDate DATETIME = NULL -- Domyślnie używamy wartości NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Jeśli nie podano daty zamówienia, użyj bieżącej daty
    IF @OrderDate IS NULL
        SET @OrderDate = GETDATE();

    -- Sprawdzamy, czy użytkownik ma jakieś produkty w koszyku
    IF EXISTS (SELECT 1 FROM BasketPositions WHERE UserID = @UserID)
    BEGIN
        -- Tworzymy nowe zamówienie dla użytkownika
        INSERT INTO [Orders] (UserID, Date, isPayed)
        VALUES (@UserID, @OrderDate, 0)

        -- Pobieramy ID nowo utworzonego zamówienia
        DECLARE @OrderID INT
        SELECT @OrderID = SCOPE_IDENTITY()

        -- Przenosimy produkty z koszyka do nowego zamówienia
        INSERT INTO OrderPositions (OrderID, ProductID, Amount, Price)
        SELECT @OrderID, ProductID, Amount, Price
        FROM BasketPositions
        WHERE UserID = @UserID

        -- Usuwamy produkty z koszyka
        DELETE FROM BasketPositions WHERE UserID = @UserID

        PRINT 'Zamówienie zostało wygenerowane.'
    END
    ELSE
    BEGIN
        -- Jeśli użytkownik nie ma produktów w koszyku, wyświetlamy komunikat
        PRINT 'Nie można wygenerować zamówienia. Koszyk użytkownika jest pusty.'
    END
END;
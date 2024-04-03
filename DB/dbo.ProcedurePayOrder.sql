CREATE PROCEDURE [dbo].[ProcedurePayOrder]
    @UserID INT,
    @PaidAmount DECIMAL(10, 2)
AS
BEGIN
    SET NOCOUNT ON;

    -- Sprawdzamy, czy użytkownik o podanym ID ma nieopłacone zamówienia
    IF EXISTS (SELECT 1 FROM [Orders] WHERE UserID = @UserID AND isPayed = 0)
    BEGIN
        -- Pobieramy sumę kwoty zamówień dla użytkownika
        DECLARE @TotalOrderAmount DECIMAL(10, 2)
        SELECT @TotalOrderAmount = SUM(Price * Amount)
        FROM OrderPositions OP
        INNER JOIN [Orders] O ON OP.OrderID = O.ID
        WHERE O.UserID = @UserID AND O.isPayed = 0

        -- Sprawdzamy, czy podana kwota jest wystarczająca do opłacenia zamówień użytkownika
        IF @PaidAmount >= @TotalOrderAmount
        BEGIN
            -- Aktualizujemy wszystkie nieopłacone zamówienia użytkownika jako opłacone
            UPDATE [Orders]
            SET isPayed = 1
            WHERE UserID = @UserID AND isPayed = 0

            PRINT 'Zamówienia zostały opłacone.'
        END
        ELSE
        BEGIN
            -- Jeśli podana kwota jest mniejsza niż kwota zamówień użytkownika, wyświetlamy odpowiedni komunikat
            PRINT 'Podana kwota jest niewystarczająca do opłacenia zamówień.'
        END
    END
    ELSE
    BEGIN
        -- Jeśli użytkownik nie ma nieopłaconych zamówień, wyświetlamy odpowiedni komunikat
        PRINT 'Użytkownik nie ma nieopłaconych zamówień.'
    END
END;
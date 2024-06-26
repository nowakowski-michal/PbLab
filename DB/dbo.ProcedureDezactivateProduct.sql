﻿CREATE PROCEDURE [dbo].[ProcedureDezactivateProduct]
    @ProductID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Sprawdzamy, czy produkt istnieje
    IF EXISTS (SELECT 1 FROM Products WHERE ID = @ProductID)
    BEGIN
        -- Dezaktywujemy produkt
        UPDATE Products
        SET IsActive = 0
        WHERE ID = @ProductID

        -- Sprawdzamy, czy produkt był powiązany z koszykiem
        IF EXISTS (SELECT 1 FROM BasketPositions WHERE ProductID = @ProductID)
        BEGIN
            -- Nie możemy usunąć produktu, który był w koszyku, więc go jedynie dezaktywujemy
            PRINT 'Nie można usunąć produktu, który był w koszyku. Produkt został jedynie dezaktywowany.'
        END
        ELSE
        BEGIN
            -- Usuwamy produkt, jeśli nie był powiązany z koszykiem
            DELETE FROM Products WHERE ID = @ProductID
            PRINT 'Produkt został usunięty.'
        END
    END
    ELSE
    BEGIN
        -- Jeśli produkt nie istnieje, wyświetlamy komunikat
        PRINT 'Produkt o podanym ID nie istnieje.'
    END
END;
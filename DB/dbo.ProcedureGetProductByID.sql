CREATE PROCEDURE [dbo].[ProcedureGetProductByID]
    @ProductID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        P.ID,
        P.Name,
        P.Price,
        P.Image,
        P.IsActive,
        P.GroupID,
        PG.Name AS GroupName
    FROM
        Products AS P
    LEFT JOIN
        ProductGroups AS PG ON P.GroupID = PG.ID
    WHERE
        P.ID = @ProductID;
END;
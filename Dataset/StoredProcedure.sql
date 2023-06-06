CREATE PROCEDURE dbo.CancelTodayNotVisitedReservations
AS
BEGIN
    UPDATE Reservations
    SET status = '3'
    WHERE status = '1' AND CAST(DateTime AS DATE) = CAST(GETDATE() AS DATE);
END
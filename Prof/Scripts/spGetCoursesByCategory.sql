
CREATE OR ALTER PROCEDURE [spGetCoursesByCategory] 
@CategoryId uniqueidentifier
AS 
SELECT * FROM Course WHERE CategoryId =@CategoryId
SELECT TOP (1000) [BatchId]
      ,[BatchName]
      ,[StartDate]
      ,[EndDate]
      ,[IsActive]
      ,[CourseId]
      ,[TrainerId]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[ModifiedBy]
      ,[ModifiedOn]
      ,[CourseId1]
      ,[CourseId2]
  FROM [Course1Db].[dbo].[Batches]

DECLARE @CourseId INT;
DECLARE @TrainerId INT;



SELECT TOP 1 @CourseId = CourseId FROM [dbo].[Courses] WHERE CourseName = 'Angular Basics';
SELECT TOP 1 @TrainerId = TrainerId FROM [dbo].[Trainers] WHERE TrainerName = 'John Doe';


INSERT INTO [dbo].[Batches] 
    ([BatchName], [StartDate], [EndDate], [IsActive], [CourseId], [TrainerId], [CreatedBy], [CreatedOn])
VALUES
    ('Batch 1 - Angular', '2025-08-25', '2025-09-24', 1, @CourseId, @TrainerId, 1, GETDATE());


    select * from Batches;
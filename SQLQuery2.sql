SELECT TOP (1000) [CourseId]
      ,[CourseName]
      ,[Description]
      ,[Duration]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[ModifiedBy]
      ,[ModifiedOn]
      ,[IsActive]
  FROM [Course1Db].[dbo].[Courses]

  select * from Courses;

  INSERT INTO [dbo].[Courses] 
    ([CourseName], [Description], [Duration], [CreatedBy], [CreatedOn], [IsActive])
VALUES
    ('Angular Basics', 'Introduction to Angular framework', 30, 1, GETDATE(), 1);

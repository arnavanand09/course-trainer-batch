SELECT TOP (1000) [TrainerId]
      ,[TrainerName]
      ,[Expertise]
      ,[Email]
      ,[Phone]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[ModifiedBy]
      ,[ModifiedOn]
      ,[IsActive]
  FROM [Course1Db].[dbo].[Trainers]

  INSERT INTO [dbo].[Trainers] 
    ([TrainerName], [Expertise], [Email], [Phone], [CreatedBy], [CreatedOn], [IsActive])
VALUES
    ('John Doe', 'Frontend Development', 'john.doe@example.com', '1234567890', 1, GETDATE(), 1);


    select * from Trainers;

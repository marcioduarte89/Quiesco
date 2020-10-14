IF NOT EXISTS (SELECT * FROM master.dbo.syslogins WHERE loginname = 'DockerUser' AND dbname = 'Products')
	CREATE LOGIN DockerUser WITH PASSWORD = 's[qfBKS/r((2Cg+m' ,
		CHECK_EXPIRATION = OFF,
		DEFAULT_DATABASE = [Products]  

CREATE USER [DockerUser] FOR LOGIN [DockerUser];
GO


ALTER ROLE [db_datawriter] ADD MEMBER [DockerUser];
GO


ALTER ROLE [db_datareader] ADD MEMBER [DockerUser];
GO

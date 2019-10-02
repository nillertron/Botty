use Botty;
GO

DROP TABLE Commands;
GO

create table Commands (
    id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    CommandName NVARCHAR(15) NOT NULL,
    CommandDescription NVARCHAR(50) NOT NULL
);

GO
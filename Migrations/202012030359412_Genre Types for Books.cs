namespace OnlineBookStore1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenreTypesforBooks : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id, Name) VALUES (1, 'Economics')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (2, 'Engineering')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (3, 'Craftsman')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (4, 'Construction')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (5, 'Science')");
        }
        
        public override void Down()
        {
        }
    }
}

namespace Computer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Deletion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Computers", "ComputerTypeId", "dbo.ComputerTypes");
            DropForeignKey("dbo.Computers", "DeparmentTypeId", "dbo.DeparmentTypes");
            DropForeignKey("dbo.Computers", "ProducerTypeId", "dbo.ProducerTypes");
            DropForeignKey("dbo.ComputerUsingHistories", "UserId", "dbo.AppUsers");
            DropForeignKey("dbo.ComputerUsingHistories", "ComputerId", "dbo.Computers");
            AddForeignKey("dbo.Computers", "ComputerTypeId", "dbo.ComputerTypes", "ComputerTypeId");
            AddForeignKey("dbo.Computers", "DeparmentTypeId", "dbo.DeparmentTypes", "DeparmentTypeId");
            AddForeignKey("dbo.Computers", "ProducerTypeId", "dbo.ProducerTypes", "ProducerTypeId");
            AddForeignKey("dbo.ComputerUsingHistories", "UserId", "dbo.AppUsers", "Id");
            AddForeignKey("dbo.ComputerUsingHistories", "ComputerId", "dbo.Computers", "ComputerId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComputerUsingHistories", "ComputerId", "dbo.Computers");
            DropForeignKey("dbo.ComputerUsingHistories", "UserId", "dbo.AppUsers");
            DropForeignKey("dbo.Computers", "ProducerTypeId", "dbo.ProducerTypes");
            DropForeignKey("dbo.Computers", "DeparmentTypeId", "dbo.DeparmentTypes");
            DropForeignKey("dbo.Computers", "ComputerTypeId", "dbo.ComputerTypes");
            AddForeignKey("dbo.ComputerUsingHistories", "ComputerId", "dbo.Computers", "ComputerId", cascadeDelete: true);
            AddForeignKey("dbo.ComputerUsingHistories", "UserId", "dbo.AppUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Computers", "ProducerTypeId", "dbo.ProducerTypes", "ProducerTypeId", cascadeDelete: true);
            AddForeignKey("dbo.Computers", "DeparmentTypeId", "dbo.DeparmentTypes", "DeparmentTypeId", cascadeDelete: true);
            AddForeignKey("dbo.Computers", "ComputerTypeId", "dbo.ComputerTypes", "ComputerTypeId", cascadeDelete: true);
        }
    }
}

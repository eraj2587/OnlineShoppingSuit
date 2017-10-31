namespace ECommerce.Storage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LatestChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employee", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.EmployeeDetail", "Employee_ID", "dbo.Employee");
            DropIndex("dbo.Employee", new[] { "DepartmentID" });
            DropIndex("dbo.EmployeeDetail", new[] { "Employee_ID" });
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdatedBy = c.String(),
                        CustomerDetail_CustomerDetailId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.CustomerDetail", t => t.CustomerDetail_CustomerDetailId)
                .Index(t => t.CustomerDetail_CustomerDetailId);
            
            CreateTable(
                "dbo.CustomerDetail",
                c => new
                    {
                        CustomerDetailId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerDetailId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductCode = c.String(),
                        ProductName = c.String(),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Department");
            DropTable("dbo.Employee");
            DropTable("dbo.EmployeeDetail");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EmployeeDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(),
                        Email = c.String(maxLength: 50),
                        MobileNumber = c.String(),
                        PhotoSource = c.String(),
                        BloodGroup = c.String(),
                        Gender = c.String(),
                        MaritalStaus = c.String(),
                        LocalAddress_Address1 = c.String(),
                        LocalAddress_Address2 = c.String(),
                        LocalAddress_Address3 = c.String(),
                        LocalAddress_City = c.String(),
                        LocalAddress_State = c.String(),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdatedBy = c.String(),
                        Employee_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        EmployeeAddress = c.String(maxLength: 250),
                        Salary = c.Int(),
                        DepartmentID = c.Int(nullable: false),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdatedBy = c.String(),
                        DepartmentHead = c.Int(),
                        DepartmentCode = c.String(),
                        Location = c.String(),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.Order", "CustomerDetail_CustomerDetailId", "dbo.CustomerDetail");
            DropIndex("dbo.Order", new[] { "CustomerDetail_CustomerDetailId" });
            DropTable("dbo.Product");
            DropTable("dbo.CustomerDetail");
            DropTable("dbo.Order");
            CreateIndex("dbo.EmployeeDetail", "Employee_ID");
            CreateIndex("dbo.Employee", "DepartmentID");
            AddForeignKey("dbo.EmployeeDetail", "Employee_ID", "dbo.Employee", "ID");
            AddForeignKey("dbo.Employee", "DepartmentID", "dbo.Department", "ID", cascadeDelete: true);
        }
    }
}

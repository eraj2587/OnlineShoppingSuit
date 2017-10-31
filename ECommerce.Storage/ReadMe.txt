REFER BELOW LINKS

https://msdn.microsoft.com/en-us/data/jj591621.aspx
http://www.entityframeworktutorial.net/code-first/code-based-migration-in-code-first.aspx

First, open the package manager console from Tools → Library Package Manager → Package Manager Console and then run "enable-migrations –EnableAutomaticMigration:$true" command
Ensure construtor of dbcontect class should containt connectionstringname else it will not work
//TO ADD SCAFFOLD FILE
Add-Migration AddEmployeeSalary

//TO UPDATE DATABASE
Update-Database

//TO ROLLBACK TO ANY PREVIOUS STATE
update-database -TargetMigration:"AddEmployeeSalary"

//TO CHECK DATABASE QUERIES WHILE UPDATING DATABASE
update-database -verbose

//TO CREATE SCRIPT FILE OF SPECIFID CHANGES
Update-Database -Script -SourceMigration: $InitialDatabase -TargetMigration: AddEmployeeSalary

//Update to Specific migration
update-database -TargetMigration MyTuesdayMigration
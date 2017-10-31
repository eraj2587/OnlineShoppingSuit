
REFER BELOW LINKS

https://msdn.microsoft.com/en-us/data/jj591621.aspx
http://www.entityframeworktutorial.net/code-first/code-based-migration-in-code-first.aspx

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
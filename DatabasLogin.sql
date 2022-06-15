
use CRM
create table Signup
(  
   id int primary key identity (1,1),
   UserId int ,
   Username nvarchar(50),
   Password nvarchar(50),
   Createddate date,
   RoleName varchar(10)
   foreign key (UserId) references AdminEmployee(EmployeeId), 
)

create table Signupcustomer
(  
   id int primary key identity (1,1),
   CustomerId int ,
   Username nvarchar(50),
   Password nvarchar(50),
   Createddate date,
   RoleName varchar(10)
   foreign key (CustomerId) references AdminCustomer(CustomerId), 
)


Create table employees(
	Employee_Id int primary key ,
	Firstname varchar(30),
	Lastname varchar(30),	
	MailId varchar(30),
	Gender nvarchar(10) ,
	DateofBirth date,
	Username nvarchar(50),
	foreign key (Employee_Id) references AdminEmployee(EmployeeId),  
)
create table Admin
(  id int primary key identity(1,1),
   Username nvarchar(50),
   Password nvarchar(50),
)  
insert into Admin values ('Admin','Admin@CRM596')
UPDATE Signupcustomer SET RoleName = 'Customer' WHERE CustomerId = 0;
create table AdminEmployee
(
	EmployeeId int primary key,
	ClassType varchar(10) Not null,	
	Department nvarchar(20)
)

create table AdminCustomer
(
	CustomerId int primary key,	
	EmployeeId nvarchar(20)
)


Drop table Signup
truncate table employees
Drop table AdminEmployee
Drop table employees

create table Roletype 
(  
   RoleId int primary key identity(1,1),  
   RoleName nvarchar(20)  
) 
drop table Roletype
insert into roletype values ('Employee'),('Manager')

alter table AdminEmployee Add Department nvarchar(20)
select*from AdminEmployee

select*from employees

insert into employees values(0,'sas','sada','male','05/02/2000','ancksncah','sadasdasda')






--PAYROLL
----------------------------------------------------------------------------------
create table salary 
(
salaryId int identity(1,1),
Class Varchar(20) primary key ,
amount int,
annual int,
)

insert into salary values ('Class1',33333,400000),
('Class2',50000,600000),
('Class3',66666,800000),
('Class4',83333,1000000),
('Class5',100000,1200000)


create table PayRoll( 
PayrollId int Primary Key identity(1,1),
Emp_Id int, 
ClassType varchar(20),
Totalsalary int,
Foreign key (Emp_Id) references employees(Employee_Id),
Foreign key (ClassType) references salary(Class)
)

select*from PayRoll
drop table salary
drop table PayRoll



create table queries(
Queryno int identity primary key,
custId int not null,
custname varchar(30) not null,
custEmail varchar(20)not null,
EmpId int not null, 
Qsubject varchar(50),
Qmessage varchar(250) not null,
foreign key(custId) references customers(customerId)	
)




create table Request(
Requestno int identity primary key,
custId int not null,
custname varchar(30) not null,
custEmail varchar(20)not null,
EmpId int not null, 
subject varchar(50),
message varchar(250) not null,
 foreign key(custId) references customers(customerId)
)









----drop table Department,salary

----insert into employees values(2,'sathiya','moorthi','male','2001-09-05','Developer','12345','2021-06-12') 
----insert into Department values('DOT NET Developer',400000),('Android Developer',400000),('SalseForce',400000),('Admin',350000)
----insert into salary values('12345',22000)

----select*from Department
----select*from employees


----CREATE TABLE customers(
----customerId int  not null,
----customerName varchar(50) not null unique,
----phone varchar(15) not null unique,
----email varchar(50) not null unique,
----contactaddress varchar(100) not null,
----EmployeeId int not null,
----primary key(customerId),
------foreign key(EmployeeId) references employees(EmployeeId)
----)

Insert into customers values (9,'Shivanias',7854123692,'Shivanisd@gmail.com','GOA',1001),(1,'Sathiya',852369741,'Sathiyasri@gmail.com','Salem',21),(2,'Sri',258741369,'sri@gmail.com','Salem',21),
(3,'Arunshiva',785236419,'Arun@gmail.com','Kaniyakumari',21),(4,'Muthu',564123789,'muthu@gmail.com','Thirunalveli',29),(5,'Ravi',123456789,'Ravi@gmail.com','Chennai',24),(6,'jhon',789456123,'jhon@gmail.com','Namakkal',24),
(7,'Andrew',1597634628,'Andrew@gmail.com','Salem',29),(8,'Shivani',785412369,'Shivani@gmail.com','GOA',29)

select*from Admin
select*from customers
select*from employees
select *from Signup
select*from salary;
select*from AdminCustomer
select*from AdminEmployee
select*from Payroll
select*from Roletype
select*from Signupcustomer
truncate table payroll
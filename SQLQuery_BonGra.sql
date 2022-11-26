--
Create table AccManager (
   ID Int Not null Identity(1,1),
   Name Nvarchar(150) Not null,
   Password Varchar(10) Not null,
   Email Varchar(200) Null,
   Phone Varchar(12) Null,
   Address Nvarchar(250) Not null,
   Type int Null, 

   PRIMARY KEY (ID)
);

Create table AccCustomer (
   ID Int Not null Identity(1,1),
   Name Nvarchar(150) Not null,
   Password Varchar(10) Not null,
   Email Varchar(200) Null,
   Phone Varchar(12) Null,
   Address Nvarchar(250) Not null,

   PRIMARY KEY (ID)
);

Create table Categogy (
   ID Int Not null Identity(1,1),
   Name Nvarchar(250) Null,

   PRIMARY KEY (ID),
);

Create table Type (
   ID Int Not null Identity(1,1),
   Name Nvarchar(250) Null,

   PRIMARY KEY (ID),
);

Create table Product (
   ID Int Not null Identity(1,1),
   Name Nvarchar(150) Not null,
   Info Nvarchar(max) Null,
   Place Nvarchar(12) Null,
   Address Nvarchar(250) Not null,
   TypeID Int not Null,
   CategoryID Int Not null,
   Amount TINYINT Null,

   PRIMARY KEY (ID),
   FOREIGN KEY (CategoryID) References Categogy(ID),
   FOREIGN KEY (TypeID) References Type(ID)
);

Create table Picture (
   ID Int Not null Identity(1,1),
   ProductID Int Not null,
   Path Varchar(2048),
   TypeID int Null,

   PRIMARY KEY (ID),
   FOREIGN KEY (ProductID) References Product(ID)
);


Create table Cart (
   ID Int Not null Identity(1,1),
   ProductID Int Null,
   Amount Int Null,
   AccCustomerID Int Not null,

   PRIMARY KEY (ID),
   FOREIGN KEY (ProductID) References Product(ID),
   FOREIGN KEY (AccCustomerID) References AccCustomer(ID)
);


Create table CustomerOrder (
   ID Int Not null Identity(1,1),
   AccCustomerID Int Not null,
   CreateAtTime datetime Null,

   PRIMARY KEY (ID),
   FOREIGN KEY (AccCustomerID) References AccCustomer(ID)
);

Create table OrderDetail (
   ID Int Not null Identity(1,1),
   CustomerOrderID Int Not null,
   Amount Int Null,
   ProductID Int Null,
   Note Nvarchar(max) Null,

   PRIMARY KEY (ID),
   FOREIGN KEY (ProductID) References Product(ID),
   FOREIGN KEY (CustomerOrderID) References CustomerOrder(ID)
);

Create table CommentProduct (
   ID Int Not null Identity(1,1),
   Content Nvarchar(max) Not null,
   AccManagerID Int Null,
   Answer Nvarchar(max) Not null, 
   ProductID Int Null,
   AccCustomerID Int Not null,

   PRIMARY KEY (ID),
   FOREIGN KEY (ProductID) References Product(ID),
   FOREIGN KEY (AccManagerID) References AccManager(ID),
   FOREIGN KEY (AccCustomerID) References AccCustomer(ID)
);

Create table Feedback (
   ID Int Not null Identity(1,1),
   Content Nvarchar(max) Not null,
   AccManagerID Int Null,
   Answer Nvarchar(max) Not null, 
   AccCustomerID Int Not null,

   PRIMARY KEY (ID),
   FOREIGN KEY (AccManagerID) References AccManager(ID),
   FOREIGN KEY (AccCustomerID) References AccCustomer(ID)
);
--
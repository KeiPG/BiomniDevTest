# BiomniDevTest                                                                                                                                                                        
BooksApi:                                                                                                                                            
To run this you will need to add a DB to the project. for this I used the built in SQL Server on Visual Studio.                                                                                                        
to create your own please open the project in VS and click View->SQL Server Object Explorer then Right click on the Databases and select "Add New Database".                                                                                        
In the pop-up please make the Database name Books and the location "BiomniDevTest\BooksApi\BooksApi\DB\"(Equivelent).                                                                    
once created please open up the Database right click tables and create new table see below for table.                                                                                
CREATE TABLE [dbo].[Book] (                                                    
    [Id]              INT           NOT NULL,                        
    [Title]           VARCHAR (255) NOT NULL,                                
    [Author]          VARCHAR (255) NOT NULL,                                
    [PublicationDate] DATE          NOT NULL,                                
    [EditionNumber]   INT           NULL,                                                                    
    [ISBN]            VARCHAR (50)  NOT NULL,                                                                        
    PRIMARY KEY CLUSTERED ([Id] ASC)                                                                                        
);                                                                                
After Creating new table please ensure that BooksContext(Located in Models) has the correct string on line 22 to line up to your mdf file.                                                                        
you should be able to run the project after the DB is set up.                                                    
if there are any other problems please dont hesitate to contact me.                                                                                                    
                                                                                                                                                                                        
A note for the UI you might need to change const API_URL on line 55 in index.html to line up with where your api is running.                                                                                            


#Enhancements:
here is a quick list of things i would do to make this project better.
1. I would apply unit testing to the API and possibly Integration testing for the UI.
2. I would make the a microservice and create a docker image of the project to help with making it scalable.
3. I would add more validation into the UI and API to ensure that a injection attack couldnt happen.
4. make the UI more User Friendly and responsive.
5. host it in the cloud to make it more secure(Add tokens to the API).

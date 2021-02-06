create database spacex;

CREATE TABLE Satellites (
    SatelliteID int NOT NULL IDENTITY,
    SatelliteName varchar(300),
    Longitude float,
    Latitude float,
	Elevation float,
    HealthStatus varchar(300)
);


CREATE TABLE Status(
	ID int NOT NULL IDENTITY,
	HealthStatus varchar(300)
);

INSERT INTO Status(HealthStatus) Values('Good Condition'),('Avarage Condition'),('Poor Condition')

GO  
/****** Object:  StoredProcedure [dbo].[spacex]    Script Date: 07/01/2021 ******/  
SET ANSI_NULLS ON  
GO  
SET QUOTED_IDENTIFIER ON  
GO  
-- =============================================  
-- Author:      <Indrajith Ekanayake>   
-- Description: <Perform crud operation on Satellites table>  
-- =============================================  
CREATE PROCEDURE spacex_pro 
    -- Add the parameters for the stored procedure here  
    @SatelliteID int,  
    @SatelliteName varchar(300),  
    @Longitude float,  
    @Latitude float,  
    @Elevation float,
	@HealthStatus varchar(300),
	@OperationType int 
    --================================================  
    --operation types   
    -- 1) Insert  
    -- 2) Update  
    -- 3) Delete  
    -- 4) Select Perticular Record  
    -- 5) Selec All  
AS  
BEGIN  
    -- SET NOCOUNT ON added to prevent extra result sets from  
    -- interfering with SELECT statements.  
    SET NOCOUNT ON;  
      
    --select operation  
    IF @OperationType=1  
    BEGIN  
        INSERT INTO Satellites VALUES (@SatelliteName,@Longitude,@Latitude,@Elevation,@HealthStatus)  
    END  
    ELSE IF @OperationType=2  
    BEGIN  
        UPDATE Satellites SET SatelliteName=@SatelliteName , Longitude=@Longitude ,Latitude=@Latitude, Elevation=@Elevation, HealthStatus=@HealthStatus WHERE SatelliteID=@SatelliteID  
    END  
    ELSE IF @OperationType=3  
    BEGIN  
        DELETE FROM Satellites WHERE SatelliteID=@SatelliteID  
    END  
    ELSE IF @OperationType=4  
    BEGIN  
        SELECT * FROM Satellites WHERE SatelliteID=@SatelliteID  
    END  
    ELSE   
    BEGIN  
        SELECT * FROM Satellites   
    END  
       
END  

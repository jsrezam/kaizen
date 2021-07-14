SELECT *
FROM Orders

SELECT *
FROM OrderDetails
WHERE OrderId = 1025



SELECT COUNT(1)
FROM CampaignDetails CD
WHERE CD.[State] NOT IN ('Earned')

SELECT MIN(Id)
FROM CampaignDetails CD
WHERE CD.[State] NOT IN ('Earned')
--1595

SELECT MAX(Id)
FROM CampaignDetails CD
WHERE CD.[State] NOT IN ('Earned')
--35645

SELECT MIN(Id)
FROM Products
--2
SELECT MAX(Id)
FROM Products
--9947


SELECT *
FROM Products
WHERE Id = 2


SELECT COUNT(1)
FROM CampaignDetails CD WITH (NOLOCK)
WHERE CD.[State] NOT IN ('Earned')--34039

SELECT COUNT(1)
FROM CampaignDetails CD WITH (NOLOCK)
WHERE CD.[State] IN ('Earned')--34039

SELECT *
FROM OrderDetails

SELECT *
FROM Orders

/****************************************************************/

/*
DELETE FROM OrderDetails
DELETE FROM Orders
DELETE FROM CampaignDetails
DELETE FROM Campaigns
DELETE FROM Customers
DELETE FROM Products
--DELETE FROM Categories
*/
SELECT COUNT(1)
FROM OrderDetails
SELECT COUNT(1)
FROM Orders
SELECT COUNT(1)
FROM CampaignDetails
SELECT COUNT(1)
FROM Campaigns
SELECT COUNT(1)
FROM Customers
SELECT COUNT(1)
FROM Products
--DELETE FROM Categories

SELECT MIN(Id), MAX(Id)
FROM CampaignDetails
SELECT MIN(Id), MAX(Id)
FROM Products
SELECT *
FROM Products
WHERE Id in (61891,62889)

SELECT cus.Id, cus.CellPhone, cd.*--COUNT(1)
FROM CampaignDetails cd
    INNER JOIN Customers cus on cus.Id = cd.CustomerId
where cd.[State] in ('UnCalled')

SELECT *
FROM Customers
WHERE Id = '84050'
SELECT *
FROM CampaignDetails
WHERE Id = '83346'

--76680







SELECT *
FROM AspNetUsers
DELETE FROM AspNetUsers 
WHERE Id = '0204fb96-4ea9-4988-8a9e-aca75107c0cc'

INSERT INTO CUSTOMERS
VALUES
    ('Padilla', 'Leonardo', 'Av. 1234 MSt strett', 'Quito', 'Pichincha', 'Ecuador', '2798371', '0987451236', '1234w1', 'lpadilla@gmail.com', '1715987453', 1)
INSERT INTO CUSTOMERS
VALUES
    ('Lopez', 'Andre', 'Av. Mariscal 123 y Av.Roges', 'Milagro', 'Guayas', 'Ecuador', '2365147', '0987456321', '128745', 'alopez@gmail.com', '1748785919', 1)
INSERT INTO CUSTOMERS
VALUES
    ('Alarcon', 'Fabian', 'Av. New York y Av. Nefistofeles', 'Rumiñahui', 'Pichincha', 'Ecuador', '2365214', '0977777771', 'A234R8', 'falarcon@gmail.com', '1798647875', 1)
INSERT INTO CUSTOMERS
VALUES
    ('Gallegos', 'Felipe', 'Av. Artigas y Av. de los piñeiros', 'Quito', 'Pichincha', 'Ecuador', '2587456', '0987896541', '125d11', 'fgallegos@outlook.com', '1720258743', 1)
INSERT INTO CUSTOMERS
VALUES
    ('Cervantez', 'Juan', 'Av. Falsa 123', 'Quito', 'Pichincha', 'Ecuador', '2798654', '0988851247', '125d15', 'jcervantez@outlook.com', '1789878391', 1)
INSERT INTO CUSTOMERS
VALUES
    ('Havelinez', 'Julio', 'Av. Falsa 123546987', 'Balao', 'Guayas', 'Ecuador', '2789634', '0987845120', '123481', 'jhavelino@gmail.con', '1723626598', 1)
INSERT INTO CUSTOMERS
VALUES
    ('Kamado', 'Nezuko', 'Av. Nepii  y Av. de los alfileres 12365', 'Quito', 'Pichincha', 'Ecuador', '2326598', '0996325874', '170524', 'nkamado@outlook.com', '1715995112', 1)
INSERT INTO CUSTOMERS
VALUES
    ('Onofre', 'David', 'Av. Iñaquito 1234 ST y Av. Falsa 897', 'Pedro Moncayo', 'Pichincha', 'Ecuador', '2798366', '0985236541', '170524', 'donofre@gmail.com', '1725362514', 1)

use kaizen
SELECT *
FROM Campaigns
where id = '1068'
SELECT *
FROM CampaignDetails

SELECT *
--update c set c.FinishDate = DATEADD(DAY,-30,c.FinishDate)
FROM Campaigns c
where c.id = '1068'

use kaizen
SELECT MIN(Id), MAX(Id)
FROM Categories




DECLARE @__agentId_0 nvarchar(450) = N'eb6dd55a-90eb-4845-93a6-c2f9d05c08cb';
SELECT [c].[Id], [c].[CustomerId], [c].[CampaignId], [c1].[CellPhone], [c].[LastCallDate], [c].[LastCallDuration], [c].[LastValidCallDate], [c].[LastValidCallDuration], [c].[TotalCallsNumber], [c].[State]
FROM [CampaignDetails] AS [c]
    INNER JOIN [Campaigns] AS [c0] ON [c].[CampaignId] = [c0].[Id]
    INNER JOIN [Customers] AS [c1] ON [c].[CustomerId] = [c1].[Id]
WHERE [c0].[UserId] = @__agentId_0



SELECT *
UPDATE C SET C.[State] = 1 
FROM Categories C
WHERE C.Name = 'cpuS'


select *
from Orders o

select *
from OrderDetails od
where od.OrderId = 110496














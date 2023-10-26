CREATE TABLE [dbo].[Weather] (
    [Location]           NVARCHAR (50) NOT NULL,
    [CurrentTemperature] FLOAT (53)    NULL,
    [MaxTemperature]     FLOAT (53)    NULL,
    [MinTemperature]     FLOAT (53)    NULL,
    [WeatherCondition]   FLOAT (53)    NULL,
    [Description]        NCHAR (10)    NULL,
    [Icon]               NCHAR (10)    NULL,
    PRIMARY KEY CLUSTERED ([Location] ASC)
);
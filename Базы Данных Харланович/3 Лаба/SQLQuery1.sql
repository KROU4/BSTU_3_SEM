USE master

CREATE database B_MyBase

on primary
( name = N'B_MyBase_mdf', filename = N'C:\BD\B_MyBase_mdf.mdf', 
   size = 10240Kb, maxsize=UNLIMITED, filegrowth=1024Kb),
( name = N'B_MyBase_ndf', filename = N'C:\BD\B_MyBase_ndf.ndf', 
   size = 10240KB, maxsize=1Gb, filegrowth=25%),

filegroup FG1
( name = N'B_MyBase_fg1_1', filename = N'C:\BD\B_MyBase_fgq-1.ndf', 
   size = 10240Kb, maxsize=1Gb, filegrowth=25%),
( name = N'B_MyBase_fg1_2', filename = N'C:\BD\B_MyBase_fgq-2.ndf', 
   size = 10240Kb, maxsize=1Gb, filegrowth=25%),

filegroup FG2
( name = N'B_MyBase_fg2_1', filename = N'C:\BD\B_MyBase_fgq-3.ndf', 
   size = 10240Kb, maxsize=1Gb, filegrowth=25%),
( name = N'B_MyBase_fg2_2', filename = N'C:\BD\B_MyBase_fgq-4.ndf', 
   size = 10240Kb, maxsize=1Gb, filegrowth=25%),

   filegroup FG3
( name = N'B_MyBase_fg3_1', filename = N'C:\BD\B_MyBase_fgq-5.ndf', 
   size = 10240Kb, maxsize=1Gb, filegrowth=25%),
( name = N'B_MyBase_fg3_2', filename = N'C:\BD\B_MyBase_fgq-6.ndf', 
   size = 10240Kb, maxsize=1Gb, filegrowth=25%)

log on
( name = N'B_MyBase_log', filename=N'C:\BD\B_MyBase_log.ldf',       
   size=10240Kb,  maxsize=2048Gb, filegrowth=10%)
 

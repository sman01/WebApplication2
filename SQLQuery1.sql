select * from credentials
select * from users

--delete from credentials where uname in ('abtkr','g');
--insert into credentials (uname,pass,access) values ('abtkr','abtkr',0);
GO
CREATE PROCEDURE dlcust @uname varchar(100)
AS
BEGIN
delete  from users where uname=@uname
delete  from credentials where uname=@uname
END
GO;

exec dlcust @uname='q'
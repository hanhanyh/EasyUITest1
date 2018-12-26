select * from dba_data_files;--查询所有表空间
create tablespace TEST1 datafile 'C:/APP/ADMINISTRATOR/ORADATA/ORCL/TEST1.DBF' size 100m;--创建表空间
create user test1 identified by 123;--创建用户 test1 设置密码
alter user test1 default tablespace test1;--设置默认表空间
grant dba to test1 ;--设置权限

--创建表
CREATE TABLE USERTABLE
(
  id int primary key,
  name varchar(20) not null
)
--创建自增序列
create sequence test1_id_seq minvalue 1 nomaxvalue increment by 1 start with 1 nocache; 
--创建 insert触发器
create or replace trigger test1table_INS_TRG
before insert on USERTABLE
for each row
begin 
select TEST1_ID_SEQ.NEXTVAL into :new.ID from dual;
end;

insert into USERTABLE(name)values('name21212');


select table_name 表名 ,tablespace_name 所使用表空间 from user_tables;--查询表所在空间
select * from USERTABLE;


select count(*) from USERTABLE;

----


-----
create table article
(
  id  varchar(255) primary key,
  name varchar(50) not null,
  detail varchar(999) not null,
  articletypeid varchar(255) not null
)

create table articletype
(
   id varchar(255) primary key,
   name varchar(255) not null
)
--查询类型
select * from articletype s order by s.id desc 
--查询所有文章
select * from article a order by a.id desc


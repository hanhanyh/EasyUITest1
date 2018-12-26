select * from dba_data_files;--��ѯ���б�ռ�
create tablespace TEST1 datafile 'C:/APP/ADMINISTRATOR/ORADATA/ORCL/TEST1.DBF' size 100m;--������ռ�
create user test1 identified by 123;--�����û� test1 ��������
alter user test1 default tablespace test1;--����Ĭ�ϱ�ռ�
grant dba to test1 ;--����Ȩ��

--������
CREATE TABLE USERTABLE
(
  id int primary key,
  name varchar(20) not null
)
--������������
create sequence test1_id_seq minvalue 1 nomaxvalue increment by 1 start with 1 nocache; 
--���� insert������
create or replace trigger test1table_INS_TRG
before insert on USERTABLE
for each row
begin 
select TEST1_ID_SEQ.NEXTVAL into :new.ID from dual;
end;

insert into USERTABLE(name)values('name21212');


select table_name ���� ,tablespace_name ��ʹ�ñ�ռ� from user_tables;--��ѯ�����ڿռ�
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
--��ѯ����
select * from articletype s order by s.id desc 
--��ѯ��������
select * from article a order by a.id desc


----- Использование XML  -----

---- 1 задание ---

use UNIVER;

select trim(t.TEACHER) 'Сокращенное_имя', trim(t.TEACHER_NAME) 'Полное_имя', trim(t.PULPIT) 'Кафедра', t.GENDER 'Пол' 
	from TEACHER t
		where t.PULPIT like 'ИСиТ' for xml path('Преподаватель'),
		root('Список_преподавателей'), elements;

---- 2 задание ----

select trim(AUDITORIUM) 'Наименование_аудитории', trim(AUDITORIUM_TYPENAME) 'Наименование_типа_аудитории', 
		AUDITORIUM_CAPACITY 'Вместимость'
	from AUDITORIUM[Аудитория] inner join AUDITORIUM_TYPE[Тип_аудитории]
		on [Аудитория].AUDITORIUM_TYPE = [Тип_аудитории].AUDITORIUM_TYPE
	where [Аудитория].AUDITORIUM_TYPE like '%ЛК%' for xml AUTO,
	root('Список_аудиторий'), elements;

--- 3 задание ----

declare @h int = 0;
declare @xmlDoc varchar(500) = 
'<?xml version="1.0" encoding="windows-1251" ?>
 <дисциплины>
 <дисциплина кратко="КЯР" название="Компьютерные языки разметки" кафедра="ИСиТ"/>
 <дисциплина кратко="ТРПИ" название="Технологии разработки пользовательских интерфейсов" кафедра="ИСиТ"/>
 <дисциплина кратко="КСиС" название="Компьютерные системы и сети" кафедра="ИСиТ"/>
 </дисциплины>';
 exec sp_xml_preparedocument @h output, @xmlDoc;
 select * from openxml(@h, '/дисциплины/дисциплина', 0)
 with([кратко] char(10), [название] varchar(100), [кафедра] char(20))

 insert SUBJECT select [кратко], [название], [кафедра]
						from openxml(@h, '/дисциплины/дисциплина', 0)
							 with([кратко] char(10), [название] varchar(100), [кафедра] char(20))
 exec sp_xml_removedocument @h;

 select * from SUBJECT;
 delete SUBJECT where SUBJECT like 'КЯР'
  delete SUBJECT where SUBJECT like 'КСиС'
   delete SUBJECT where SUBJECT like 'ТРПИ'

   --- 4 задание ----

insert into STUDENT(NAME, BDAY, IDGROUP, INFO)
	values('Тараканов Никита Сергеевич', '07-02-2004', 4,	'<паспорт><серия>MC</серия>
																<номер>71634823</номер>
																<личный_номер>787EWFJ73HD3D</личный_номер>
																<дата_выдачи>07022013</дата_выдачи>
																<адрес>г. Слуцк, 2 пер. Жуковского д. 4</адрес></паспорт>')

update STUDENT set INFO = '<паспорт><серия>PT</серия>
							<номер>9283744</номер>
							<личный_номер>94832J2H4F38D</личный_номер>
							<дата_выдачи>17042016</дата_выдачи>
							<адрес>г. Минск, ул. Революционная д. 23</адрес></паспорт>' where IDSTUDENT = 1059

update STUDENT set INFO = '<паспорт><серия>PT</серия>
							<номер>1234567</номер>
							<личный_номер>94832J2H4F38D</личный_номер>
							<дата_выдачи>17042016</дата_выдачи>
							<адрес>г. Минск, ул. Революционная д. 23</адрес></паспорт>' where INFO.value('(/паспорт/номер)[1]', 'varchar(15)') = 9283744

select s.NAME, s.INFO.value('(/паспорт/серия)[1]', 'varchar(3)')[Серия], s.INFO.value('(/паспорт/номер)[1]', 'varchar(15)')[Номер паспорта],
		s.INFO.value('(/паспорт/личный_номер)[1]', 'varchar(20)')[Личный номер], 
		s.INFO.value('(/паспорт/дата_выдачи)[1]', 'varchar(15)')[Дата выдачи],
		s.INFO.query('(/паспорт/адрес)')[Адрес]
	from STUDENT s
		where s.IDSTUDENT = 1059 or s.IDSTUDENT = 2082


--- 5 задание ---

create xml schema collection checkStudent as
N'<?xml version="1.0" encoding="utf-16" ?>
<xs:schema attributeFormDefault="unqualified" 
           elementFormDefault="qualified"
           xmlns:xs="http://www.w3.org/2001/XMLSchema">
       <xs:element name="студент">  
       <xs:complexType><xs:sequence>
       <xs:element name="паспорт" maxOccurs="1" minOccurs="1">
       <xs:complexType>
       <xs:attribute name="серия" type="xs:string" use="required" />
       <xs:attribute name="номер" type="xs:unsignedInt" use="required"/>
       <xs:attribute name="дата"  use="required" >  
       <xs:simpleType>  <xs:restriction base ="xs:string">
   <xs:pattern value="[0-9]{2}.[0-9]{2}.[0-9]{4}"/>
   </xs:restriction> 	</xs:simpleType>
   </xs:attribute> </xs:complexType> 
   </xs:element>
   <xs:element maxOccurs="3" name="телефон" type="xs:unsignedInt"/>
   <xs:element name="адрес">   <xs:complexType><xs:sequence>
   <xs:element name="страна" type="xs:string" />
   <xs:element name="город" type="xs:string" />
   <xs:element name="улица" type="xs:string" />
   <xs:element name="дом" type="xs:string" />
   <xs:element name="квартира" type="xs:string" />
   </xs:sequence></xs:complexType>  </xs:element>
   </xs:sequence></xs:complexType>
   </xs:element>
</xs:schema>'
alter table STUDENT drop column INFO;
alter table STUDENT add INFO xml(checkStudent);

update STUDENT set INFO = 
'<студент>
	<паспорт серия="МС" номер="123456" дата="07.02.2004" />
	<телефон>3752574118</телефон>
	<адрес>
		<страна>Беларусь</страна>
		<город>Слуцк</город>
		<улица>Жуковского</улица>
		<дом>4</дом>
		<квартира>2</квартира>
	</адрес>
</студент>' where IDSTUDENT = 1000

update STUDENT set INFO = 
'<студент>
	<паспорт серия="МС" номер="123456" дата="07.02.2004" />
	<телефон>3752574118</телефон>
	<адрес>
		<страна>Беларусь</страна>
		<город>Слуцк</город>
		<улица>Жуковского</улица>
		<дом>4</дом>
		<квартира>2</квартира>
		<подъезд>4</подъезд>
	</адрес>
</студент>' where IDSTUDENT = 1001

create xml schema collection checkStudent2 as
N'<?xml version="1.0" encoding="utf-16" ?>
<xs:schema attributeFormDefault="unqualified" 
           elementFormDefault="qualified"
           xmlns:xs="http://www.w3.org/2001/XMLSchema">
       <xs:element name="паспорт">  
		<xs:complexType>
			<xs:sequence>
				<xs:element name="серия" maxOccurs="1" minOccurs="1" type="xs:string" />
				<xs:element name="номер" maxOccurs="1" minOccurs="1" type="xs:unsignedInt" />
				<xs:element name="личный_номер" maxOccurs="1" minOccurs="1" type="xs:string" />
				<xs:element name="дата_выдачи" maxOccurs="1" minOccurs="1" type="xs:string" />
				<xs:element name="адрес" maxOccurs="1" minOccurs="1" type="xs:string" />
			</xs:sequence>
		</xs:complexType>
   </xs:element>
</xs:schema>'

alter table STUDENT drop column INFO;
alter table STUDENT add INFO xml(checkStudent2);


update STUDENT set INFO = '<паспорт><серия>PT</серия>
							<номер>9283744</номер>
							<личный_номер>94832J2H4F38D</личный_номер>
							<дата_выдачи>17042016</дата_выдачи>
							<адрес>г. Минск, ул. Революционная д. 23</адрес></паспорт>' where IDSTUDENT = 1059


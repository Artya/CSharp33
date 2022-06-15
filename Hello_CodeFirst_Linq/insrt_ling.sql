DELETE FROM Emails;
DELETE FROM CourseLecturers;
DELETE FROM courses;
DELETE FROM faculties;
DELETE FROM lecturers;
INSERT INTO lecturers VALUES('L_1','Antony','Bridge','123-496-4567','15 East 205 St','Bronx','NY','10468');
INSERT INTO lecturers VALUES('L_2','Mary','Crtist','409-456-7420','232 Green Rd','Boulder','CO','80303');
INSERT INTO lecturers VALUES('L_3','John','Wall','333-589-1238','7600 May Ave, #22A','San Francisco','CA','24123');
INSERT INTO lecturers VALUES('L_4','Andy','Holl','415-549-4278','7600 May Ave, #22A','San Francisco','CA','24123');
INSERT INTO lecturers VALUES('L_5','Christian','Anders','212-771-4680','234 Limon St','New York','NY','10014');
INSERT INTO lecturers VALUES('L_6','','Kelly','650-836-7128','456 Novie Mall','Palo Alto','CA','94305');
INSERT INTO lecturers VALUES('L_7','Ann','Nill','945-925-0752','1442 Main St','Hartford','CT','34236');
INSERT INTO faculties VALUES('SC1','Informatics','Columbia','NY','USA');
INSERT INTO faculties VALUES('SC2','Cybernetics','San Francisco','CA','USA');
INSERT INTO faculties VALUES('SC3','Mathematics','Hamburg',NULL,'Germany');
INSERT INTO faculties VALUES('SC4','Physics','Berkeley','CA','USA');
INSERT INTO courses VALUES('C01','Modern history','history','SC1',3000,21.99,10,'2015-08-01',1);
INSERT INTO courses VALUES('C02','C# basics','programming','SC3',5300,19.95,20,'2015-04-01',1);
INSERT INTO courses VALUES('C03','System Administration','computer','SC2',2000,39.95,40,'2014-09-01',1);
INSERT INTO courses VALUES('C04','Algebra','Mathematics','SC1',1500,12.99,30,'2014-05-31',1);
INSERT INTO courses VALUES('C05','Geometry','Mathematics','SC1',1600,6.95,20,'2013-01-01',1);
INSERT INTO courses VALUES('C06','Databases','computer','SC1',2000,19.95,20,'2015-07-31',1);
INSERT INTO courses VALUES('C07','Data structures','computer','SC3',1020,23.95,30,'2013-10-01',1);
INSERT INTO courses VALUES('C08','Visual Basic','programming','SC1',3000,10.00,27,'2015-06-01',1);
INSERT INTO courses VALUES('C09','Java','programming','SC1',5300,13.95,31,'2014-05-31',1);
INSERT INTO courses VALUES('C10','TDD','computer','SC1',4000,20.00,45,'2014-05-31',1);
INSERT INTO courses VALUES('C11','SQL server','computer','SC1',NULL,NULL,NULL,NULL,0);
INSERT INTO courses VALUES('C12','XML','computer','SC1',507,12.99,16,'2013-08-31',1);
INSERT INTO courses VALUES('C13','SOAP','computer','SC3',802,29.99,24,'2014-05-31',1);
INSERT INTO CourseLecturers VALUES('C01','L_1',1,1.0);
INSERT INTO CourseLecturers VALUES('C02','L_1',1,1.0);
INSERT INTO CourseLecturers VALUES('C03','L_5',1,1.0);
INSERT INTO CourseLecturers VALUES('C04','L_3',1,0.6);
INSERT INTO CourseLecturers VALUES('C04','L_4',2,0.4);
INSERT INTO CourseLecturers VALUES('C05','L_4',1,1.0);
INSERT INTO CourseLecturers VALUES('C06','L_2',1,1.0);
INSERT INTO CourseLecturers VALUES('C07','L_2',1,0.5);
INSERT INTO CourseLecturers VALUES('C07','L_4',2,0.5);
INSERT INTO CourseLecturers VALUES('C08','L_6',1,1.0);
INSERT INTO CourseLecturers VALUES('C09','L_6',1,1.0);
INSERT INTO CourseLecturers VALUES('C10','L_2',1,1.0);
INSERT INTO CourseLecturers VALUES('C11','L_3',2,0.3);
INSERT INTO CourseLecturers VALUES('C11','L_4',3,0.3);
INSERT INTO CourseLecturers VALUES('C11','L_6',1,0.4);
INSERT INTO CourseLecturers VALUES('C12','L_2',1,1.0);
INSERT INTO CourseLecturers VALUES('C13','L_1',1,1.0);
INSERT INTO Emails VALUES('user1@example.com','L_1');
INSERT INTO Emails VALUES('user2@example.com','L_2');
INSERT INTO Emails VALUES('user3@example.com','L_3');
INSERT INTO Emails VALUES('user2@example.com','L_4');
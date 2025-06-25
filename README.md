# ElectronicLibrary
Десктопное приложение для таварооборота электронных книг.

## Предметная область, проблемы в работе отделов 
Предприятие занимается предоставлением услуг по продаже электронных книг. 

• Без специализированной системы клиенты и продавцы испытывали трудности в поиске нужных книг, получении подробной информации.

• Отсутствие системы мониторинга продаж затрудняет принятие стратегических решений, связанных с ассортиментом книг, ценообразованием и маркетинговыми акциями.

• Без системы резервного копирования и защиты данных существуют риски потери важной информации о книгах, клиентах и финансах.

## Описание подсистем
• Покупатель: поиск и покупка книг, просмотр доступных для приобретения и купленных книг, ознакомление со следующей информацией: название книги, авторы, жанры, издатели, год издания, краткий сюжет книги, количество страниц, тип литературы, фото обложки,  возврат книг, управление учетной записью и способом оплаты покупки, получение обратной связи;

• Библиотекарь (продавец): выкладывание и учет доступных книг, мониторинг продаж, управление промокодами;

• Администратор базы данных: журнализация и транзакция действий пользователей системы, управление базой данных, включающее также импорт и экспорт данных;

## Диаграмма прецедентов
![image](https://github.com/user-attachments/assets/0981fae7-5e84-477c-8cd7-6e5dac3967a1)
При запуске десктопного приложения открывается главное окно с выбором авторизации или регистрации пользователя, после чего пользователю необходимо ввести данные в определенное окно. В приложении есть 3 роли: покупатель, продавец (библиотекарь) и администратор базы данных. 

При авторизации под ролью «Покупатель» пользователю доступен выбор следующих окон: каталог, приобретенные книги, корзина, личный аккаунт, обратная связь (помощь). В каталоге пользователь имеет возможность искать книги по поиску или фильтрации, сортировать их по стоимости, просматривать подробную информацию о книгах, покупать книги или добавлять их в корзину. В окне приобретенных книг пользователь имеет возможность просматривать подробную информацию о книгах, скачивать содержание книги в формате TXT или FB2. В корзине пользователь имеет возможность также купить набор добавленных в нее книг. В личном аккаунте пользователь имеет возможность управлять своими личными данными, изменять пароль. В окне обратной связи пользователь имеет возможность отправлять заявку на получение обратной связи.

При авторизации под ролью «Продавец» пользователю доступен выбор следующих окон: каталог, управление книгами, мониторинг продаж, возврат средств, обратная связь. В каталоге пользователь имеет возможность искать книги по поиску или фильтрации, сортировать их по стоимости, просматривать подробную информацию о книгах. В окне управления книгами пользователь имеет возможность добавлять, изменять, удалять информацию о книгах, а также о промокодах. В окне мониторинга продаж пользователь имеет возможность просматривать диаграмму продаж по определенному периоду, а также просматривать купленные товары по читательскому билету. В окне возврата средств пользователь имеет возможность принять заявку на возврат средств, либо отказать, при этом, написав причину отказа. В окне обратной связи пользователь имеет возможность ответить на заявку на получение обратной связи, после чего этот ответ отправляется пользователю, отправлявшему эту заявку на введенную им электронную почту.

При авторизации под ролью «Администратор» пользователю доступен выбор следующих окон: книги, пользователи, логирование и работа с данными. В окне книг пользователь имеет возможность просматривать книги, добавлять, изменять и удаляться данные об авторе, жанре, типе литературы и издателе. Также здесь доступно архивирование данных и восстановление их. В окне пользователей пользователь имеет возможность просматривать сотрудников, покупателей, роли, читательские билеты, а также добавлять, изменять, удалять продавцов, роли, и только изменять читательские билеты. Также здесь доступно архивирование данных и восстановление их. В окне логирования и работы с данными пользователь имеет возможность увидеть какие действия делали покупатели, а также импортировать и экспортировать базу данных.

## Диаграмма IDEF0 до внедрения информационной системы, контекстная диаграмма

<div align="center">
  <img src="https://github.com/user-attachments/assets/c5109fb1-74c8-45f0-bedd-024456b74828" width="100%">
</div>

## Диаграмма IDEF0 до внедрения информационной системы, уровень А0 «Оказание услуг по продаже электронных книг»

<div align="center">
  <img src="https://github.com/user-attachments/assets/a4bf6fcb-c53a-44e5-bbb2-b95624247ff0"  width="100%">
</div>

## Диаграмма IDEF0 после внедрения информационной системы, контекстная диаграмма

<div align="center">
  <img src="https://github.com/user-attachments/assets/13c17e14-d7ff-4131-ac82-167118074f78" width="100%">
</div>

## Диаграмма IDEF0 после внедрения информационной системы, уровень А0 «Оказание услуг по продаже электронных книг»

<div align="center">
  <img src="https://github.com/user-attachments/assets/a598cbbe-1487-4f43-b80b-eb8e526bb74a" width="100%">
</div>

## Диаграмма потока данных DFD

<div align="center">
  <img src="https://github.com/user-attachments/assets/e95e321e-1aee-4f28-afd5-9484ac064709" alt="image1" width="100%">
  <img src="https://github.com/user-attachments/assets/46ea0598-5ca8-44bb-adf5-146059893580" alt="image2" width="100%">
  <img src="https://github.com/user-attachments/assets/edad094b-1b55-4eef-91b2-daaec41bbf06" alt="image3" width="100%">
  <img src="https://github.com/user-attachments/assets/a89f250a-b4f1-4241-be19-752f3cbae308" alt="image4" width="100%">
</div>

## Логическая схема данных

<div align="center">
  <img src="https://github.com/user-attachments/assets/3b8e6ce7-d4ab-4128-89d4-f2fe845d30b8" width="100%">
</div>

## Физическая схема данных 

<div align="center">
  <img src="https://github.com/user-attachments/assets/b713b240-6f1e-4ca8-9808-82dcec898d56" width="100%">
</div>

## Проектирование архитектуры ИС

<div align="center">
  <img src="https://github.com/user-attachments/assets/d35b27fd-5036-4312-a13e-e77cbd53b480" width="100%">
</div>

## Примеры окон приложения

<div align="center">
  <img src="https://github.com/user-attachments/assets/32cb9477-a4e6-49c8-b018-57ad8d841c5f" alt="image1" width="70%">
  <img src="https://github.com/user-attachments/assets/6bfd2e50-c3ba-4909-9faa-8e36b10e6d77" alt="image2" width="70%">
  <img src="https://github.com/user-attachments/assets/1130a2b1-735a-44cc-ac76-78d70d127caa" alt="image3" width="70%">
  <img src="https://github.com/user-attachments/assets/38b5431f-4a0d-4491-9f78-b62fe6d33589" alt="image4" width="70%">
  <img src="https://github.com/user-attachments/assets/e8ff013c-da3b-4cd2-8361-54e29f09aeed" alt="image5" width="70%">
  <img src="https://github.com/user-attachments/assets/6a8ca1e9-4196-4105-9c78-cc88c9acc544" alt="image6" width="70%">
  <img src="https://github.com/user-attachments/assets/78475002-b1a8-484c-a14b-d3809485fb5f" alt="image7" width="70%">
  <img src="https://github.com/user-attachments/assets/96447b23-b175-459c-abc2-5f599b75388d" alt="image8" width="70%">
  <img src="https://github.com/user-attachments/assets/69b27da9-ab55-40d2-886b-4124b9f664b4" alt="image9" width="70%">
  <img src="https://github.com/user-attachments/assets/9bf912f1-5dc5-4276-b212-580d599fc47d" alt="image10" width="70%">
  <img src="https://github.com/user-attachments/assets/5e66da04-03f4-4c49-8068-816ef1d03237" alt="image11" width="70%">
  <img src="https://github.com/user-attachments/assets/807d0263-a172-4e65-ab7b-b2cff60d105e" alt="image12" width="70%">
  <img src="https://github.com/user-attachments/assets/cc2d605f-99f0-477d-8f87-2556db0ea1b8" alt="image13" width="70%">
  <img src="https://github.com/user-attachments/assets/1dc52727-4eaa-4e82-9485-81e48429cc7c" alt="image14" width="70%">
  <img src="https://github.com/user-attachments/assets/39a4015a-a12a-4fa0-8b0b-322ddd85347e" alt="image15" width="70%">
  <img src="https://github.com/user-attachments/assets/a7c58d83-8664-4fb5-af23-c3855f61671b" alt="image16" width="70%">
  <img src="https://github.com/user-attachments/assets/a756ab92-8df5-4900-81bc-6afd9b7e8d87" alt="image17" width="70%">
  <img src="https://github.com/user-attachments/assets/c52ec542-fe98-40de-9c26-34a016ec8c16" alt="image18" width="70%">
  <img src="https://github.com/user-attachments/assets/a56de204-f54c-4c94-b59c-5c4c1e89115e" alt="image19" width="70%">
  <img src="https://github.com/user-attachments/assets/3a20651b-973f-4d9c-bfae-a230cc1db399" alt="image20" width="70%">
  <img src="https://github.com/user-attachments/assets/afcf0eb8-e2d6-423c-9d2d-13b20db8963a" alt="image21" width="70%">
  <img src="https://github.com/user-attachments/assets/c64aa37f-5e70-4cc4-a22d-727d376ecb23" alt="image22" width="70%">
  <img src="https://github.com/user-attachments/assets/aa74780d-fca7-4655-ba5f-9529322b2868" alt="image23" width="70%">
  <img src="https://github.com/user-attachments/assets/e45c353c-379e-44be-a9d4-d57a54076de6" alt="image24" width="70%">
</div>

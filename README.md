# Etprf.QaTestPublic.Testing
Описание процесса выполнения ручного тестирования на основании которого был написан данный автотест:
1. Открыть репозиторий по адресу https://github.com/Etprf/Etprf.QaTestPublic/tree/master
2. Переключитесь на ветку add-page-with_form.
3. Открыть index.html и скопировать содержимое в новый пустой документ блокнот, сохранить, изменить формат файла на .html // Данный шаг можно выполнить скопировав репозиторий себе на ПК или скачав документ.
4. Открыть страницу из шага 3 в браузере, поддерживающем панель разработчика.
5. Открыть панель разработчика
6. Во вкладке Elements найти поиском (ctrl+F) //*[@id='important']
7. В найденной строке из шага 6 получить значение data-value. (в данном случае значение равно 3)
8. Значение data-value (3) ввести в поле ввода на странице из шага 3.
9. Нажать на стринце кнопку submit
10. В панели разработчика перейти во вкладку "Network".
11. Открыть строку, в данном случае она имеет название "stage1?important=3"
12. В заголовках найти значение хедера "Secret" (в данном случае значение равно 2022)
13. В панели разработчика перейти во вкладку Elements
14. Во вкладке Elements найти поиском (ctrl+F) //*[@id='special']
15. В найденной строке из шага 14 получить значение data-value. (в данном случае значение равно 55)
16. Открыть Postman.
17. Создать новый HTTP Request.
18. Применить метод запроса POST.
19. В строку ввести URL: http://qatest.etprf.ru/api/stage2
20. В параметрах создаем новый параметр с названием query string и значением хедера "Secret" из шага 12 (в данном случае значение равно 2022)// Данный шаг можно выполнить иначе: В строке URL после http://qatest.etprf.ru/api/stage2 ввести ?query string=2022
21. Перейти во вкладку Body
22. Выбираем raw
23. Выбираем JSON
24. В теле запроса JSON записываем поле special со значением data-value, найденным в шаге 15, в данном случае JSON будет: {"special": 55}
25. Отправить GET-запрос нажатием кнопки "Send"
26. В поле Response получаем ответ от сервера.
Ожидаемый результат ответа "Final result = 220"

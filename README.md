# Домашняя работа для третьего учебного семестра (2 год обучения, 1 семестр)

![.NET](https://github.com/backdorJ/HT-ITIS.2.1-student/actions/workflows/dotnet.yml/badge.svg)
[![codecov](https://codecov.io/gh/backdorJ/HT-ITIS.2.1-student/branch/HW7/graph/badge.svg?token=QOCT1JKZU6)](https://codecov.io/gh/backdorJ/HT-ITIS.2.1-student)


## Как устроены Actions
1. ***build***: *Проверка: собирается ли проект.*
2. ***test*** и ***test-report***: *Все тесты должны проходить*
4. ***codecov***: *Программа должна быть на 100% покрыта тестами.* 
Тесты, которые уже были написаны заранее, проверяют работоспособность вашей программы:  верно ли выполнено задание.
Однако при реализации вы можете добавлять свои классы/сервисы и вот для них вы должны написать свои собственные тесты.
5. После этого в рамках локального репозитория создаётся пулл реквест из ветки с решённым домашним заданием в master. Далее произойдёт автоматический запуск всех workflow:
- если все workflow успешно отработают , то просите ментора провести code-review. 
- иначе смотрите логи workflow, который не отработал и исправляете проблему.

## Как выполнить домашку
1. В файле Tests.RunLogic/TestConfig.cs проставить номер текущей выполняемой домашки. Т.е. если выполняете домашку 1, то ставите HomeworkProgress(Homeworks.HomeWork1) и т.д.
2. Открываете нужную папку с домашкой и знакомитесь с постановкой задачи. Выполняете поставленные требования.
3. Если пишете свои тесты, не забывайте поставить им кастомный xUnit-атрибут из Tests.RunLogic/Attributes по аналогии с уже написанными тестами. Если ваш тест - Theory - добавляете аттрибут [HomeworkTheory(Homeworks.HomeWork*)], если Fact - [Homework(Homeworks.HomeWork*)]. Подробнее о различиях можете почитать [здесь](https://codebots.com/docs/what-is-xunit)

- Если ваша ide говорит о проблемах с зависимостями во второй домашке, просто сбилдите проект - ошибки должны исчезнуть. Это из-за IL. Узнаете что это на соответствующей паре.
- [дополнительные инструкции по работе с репозиторием](https://docs.google.com/document/d/1DPAfO-v2acR-CmLviX3qCnTBwUYPyipARdPjUjTZKdo/edit?usp=sharing)
- Для чего все эти атрибуты? - Чтобы тесты с домашками, которые вы ещё не выполнили, не влияли на прохождение github workflow, т.е. чтобы тесты следующих домашек не валились, пока вы до них не дойдёте.

# VodovozTestApp
Строка подключения к базе данных указана в TestApp.UI/settings.json, для выполнения её инициализации достаточно выполнить следующую команду из папки с решением:
dotnet ef database update --project "TestApp.Model" --startup-project "TestApp.UI"

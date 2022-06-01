# Zadanie 

Stwórz dwie aplikacje, które będą komunikować się za pomocą wiadomości usługi Azure Service Bus. Jedna z aplikacji powinna umożliwiać: 
- Dodanie użytkownika do bazy danych (id, adres email, imię, nazwisko, wiek, flaga active = false) 
- Wysłanie powiadomienia na Service bus o tym, że użytkownik został dodany 
- Edycja użytkownika 
- Wysłanie powiadomienia na Service Bus o tym, że użytkownik został zaktualizowany 
- Zwracanie listy aktywnych użytkowników 

Druga aplikacja powinna: 
- Mieć konsumenta zdarzenia dodania użytkownika - konsument powinien walidować czy użytkownik jest prawidłowy - ma uzupełnione wszystkie pola oraz czy kolumna emailu jest emailem (regex). Jeśli jest – flaga is active w bazie danych powinna zostać ustawiona na true. 
- Mieć konsumenta reagującego na edycję użytkownika - zasady działania identyczne jak w poprzednim punkcie. 

Jako miejsce przechowywania danych wykorzystaj bazę danych SQL przy podejściu database first. Do obsługi bazy danych wykorzystaj Entity Framework. 
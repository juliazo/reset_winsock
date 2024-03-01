# reset_winsock  WSAStartup  WSACleanup
c# .NET class to reset winsock 

This small class is intended for developers who have problems with some thirdparty libraries that overuses the  WSAStartup and  WSACleanup class in windows specially i had this problem with 
Banorte's PINPAD.TPV
afer a couple of uses with the pinpad it was not posible anymore for my app to make the API Request calls.

The class and code snipets are just abstract examples that you must adapt to your production flow 

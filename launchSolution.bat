@echo off
REM Define paths for each project
set CardsPath=src\Services\Cards\Cards.API
set TransactionsPath=src\Services\Transactions\Transactions.API
set UsersPath=src\Services\Users\Users.API

REM Start Cards.API
echo Starting Cards.API...
start cmd /k "cd /d %CardsPath% && dotnet run"

REM Start Transactions.API
echo Starting Transactions.API...
start cmd /k "cd /d %TransactionsPath% && dotnet run"

REM Start Users.API
echo Starting Users.API...
start cmd /k "cd /d %UsersPath% && dotnet run"

echo All projects are starting...

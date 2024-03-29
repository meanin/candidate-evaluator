# candidate-evaluator

## Introduction
Purpose of this repository is to learn how to use [Blazor](https://blazor.net/docs/index.html) framework as a frontend SPA application. Backend is built on top of .net core framework, so the whole solution is able to be hosted on any machine. User authentication is based on Azure Active Directory tenant. 

## Aim
Functionallity of this application is to store categories and questions for technical interviews and in the future to make it possible to write down a evaluation notes during the process. Data will be stored on an Azure Storage Account - table storage. It's intended to not store any sensitive data. Username is provided with a token and stored only within a session. Backend services will store UUID identifier from AAD token.

## Authors
We intent to do this project on our owns, but any help will be appreciated. Feature requests are also very welcome. Following people are initial authors of this repository:
* [Paweł Ruciński](https://github.com/meanin)
* [Przemysław Ciszak](https://github.com/plaumen)
* [Grzegorz Jońca](https://github.com/devmonte)

## CI / CD
Continuous integration process is maintained on an Azure DevOps portal. Unit tests are in a roadmap
###### Build status: 
![build](https://dev.azure.com/meaninit-after-hours/candidate-evaluator/_apis/build/status/Candidate%20Evaluator%20build%20master)
###### Release status:
![release](https://vsrm.dev.azure.com/meaninit-after-hours/_apis/public/Release/badge/c2137fd5-c76d-4cb8-8c75-f2439324623d/1/1)

App avaliable [here](https://candidate-evaluator.azurewebsites.net/). If you want to have a demo access for a some time, contact [me](mailto:meanin@wp.pl).

## Roadmap
- [x] Create users categories and questions
- [x] Dashboard for latest edited entities
- [x] Create interview layout with given number of randomized questions
- [x] Store interview results
- [x] Create interview report (pdf support maybe?)
- [ ] Dashboard on which you will be able to select your favourite interview categories
- [ ] Enable / Disable questions
- [ ] Generate interview sheet based on selected categories (with support for favourite ones)
- [ ] Azure function for completed interview - send mail
- [ ] Automatic trial accounts after registration

and more
 

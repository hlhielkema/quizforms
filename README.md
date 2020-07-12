![banner](img/banner.svg)

## Introduction
QuizForms is a web application to host online pub quizzes. It was created and used during the first months of the COVID-19 crisis. It’s designed to be hosted temporary to save expenses. Using Dockers allows quick automated deployment.

## Features
Features of QuizForms include:

- Public forms to submit quiz answers
- Embedded (Twitch) live stream.
- A automated scoreboard.
- Semi-automated answer checking.
- Efficient answer checking.
- Form availability management.
- Open or multiple choice questions.
- Remembers the Team name.
- Warning before submitting an incomplete form.
- Full touchscreen support.
- Responsive GUI for public pages.
- No database dependency
- Quick automated deployment 

## Screenshots

| | |
|:---|:---|
| ![homepage](screenshots/homepage.png)  | ![form](screenshots/form.png) |
| ![form2](screenshots/form2.png) | ![manage-forms](screenshots/manage-forms.png) |

## Tech Stack
The primary components of the QuizForms Tech Stack are:

- .NET Core 3.1
- ASP.NET Core
- Sass (scss)
- HTML, CSS, JavaScript
- Docker
- PowerShell

## Requirements
Only `Docker` and `PowerShell` are required to build and run QuizForms. Other dependencies are included through the Docker container image.

## Docker
QuizForms uses Docker to compile and run.

<img src="img/docker.jpg" width="200">

### Build and run container
Run this *PowerShell* command to build and start the Docker container for QuizForms.

```
.\build.ps1; .\run.ps1
```

### Build container
Run this *PowerShell* command to build the Docker container for QuizForms.

```
.\build.ps1;
```

### Run container
Run this *PowerShell* command to start the Docker container for QuizForms.

```
.\run.ps1
```

### List active containers
Run this *PowerShell* command to list the active contains.

```
docker ps
```

### Stop container
Run this *PowerShell* command to stop the QuizForms Docker container.

```
docker stop quizformsapp
```
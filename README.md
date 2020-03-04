# das-findapprenticeship-rss

This project is an extraction of the RSS feed functionality from the [NAVMS](https://github.com/SkillsFundingAgency/navms). It provides a single RSS feed endpoint that returns active vacancies from the [FAA](https://github.com/SkillsFundingAgency/das-findapprenticeship) application(s)

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

You will need:

* Visual Studio
* A local copy of the AVMS+ database or access to a test environment with an AVMS+ database
* An RSS reader to verify the output

### Installing

1. Clone the [repo](https://github.com/SkillsFundingAgency/das-findapprenticeship-rss)
1. Open the das-findapprenticeship-rss solution
1. Set VacancyRssFeedService as the start-up project

Example call

```
http://localhost:56369/VacancyRss.aspx?feedType=1&dayRange=360&regionCode=WM
```

## Running the tests

TBD

## Deployment

Deployment is via Azure DevOps pipeline

## Authors

* **Dan Beavon** - *Initial work* - [dannygb](https://github.com/dannygb)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details
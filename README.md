# ASP-Dotnet-MicroServices-CICD
 Contains The ASP DotNet Web API -Micro Services With Unit tests and Integration tests, Deployed with github actions by setting up a CICD Pilepline that Test -> Build -> Test -> Deploys the Docker containers

 
# Ocelot Gateway Configuration

This document provides an overview of the Ocelot Gateway configuration for routing requests to the **Food API** and **Students API** with rate limiting options.
## Base URL

`https://demos.sreekeshkprabhu.me`

## Routes Configuration

### 1. Route to Food API

- **DownstreamPathTemplate**: `/api/foods/{everything}`
- **DownstreamScheme**: `http`
- **DownstreamHostAndPorts**:  
  - Host: `foodapi`  
  - Port: `8080`
- **UpstreamPathTemplate**: `/foods/{everything}`
- **UpstreamHttpMethod**: [ "GET", "POST", "PUT", "DELETE" ]

#### Rate Limit Options:

| Parameter        | Value       |
|------------------|-------------|
| EnableRateLimiting | true       |
| Period           | 1h          |
| PeriodTimespan   | 5 requests  |
| Limit            | 20 requests |

---

### 2. Route to Students API

- **DownstreamPathTemplate**: `/api/students/{everything}`
- **DownstreamScheme**: `http`
- **DownstreamHostAndPorts**:  
  - Host: `studentsapi`  
  - Port: `8080`
- **UpstreamPathTemplate**: `/students/{everything}`
- **UpstreamHttpMethod**: [ "GET", "POST", "PUT", "DELETE" ]

#### Rate Limit Options:

| Parameter        | Value       |
|------------------|-------------|
| EnableRateLimiting | true       |
| Period           | 1h          |
| PeriodTimespan   | 10 requests |
| Limit            | 20 requests |

---

### 3. Route to Students Search

- **DownstreamPathTemplate**: `/api/students/search?course={course}&age={age}&name={name}&email={email}`
- **DownstreamScheme**: `http`
- **DownstreamHostAndPorts**:  
  - Host: `studentapi`  
  - Port: `5002`
- **UpstreamPathTemplate**: `/students/search?course={course}&age={age}&name={name}&email={email}`
- **UpstreamHttpMethod**: [ "GET" ]

#### Rate Limit Options:

| Parameter        | Value       |
|------------------|-------------|
| EnableRateLimiting | true       |
| Period           | 1h          |
| PeriodTimespan   | 10 requests |
| Limit            | 20 requests |

---

## Global Configuration

The gateway is configured with global rate limiting and a base URL.

### Global Rate Limit Options:

| Parameter             | Value             |
|-----------------------|-------------------|
| DisableRateLimitHeaders | false           |
| QuotaExceededMessage  | "Too Many Requests" |
| HttpStatusCode        | 418               |
| ClientIdHeader        | "ClientId"        |



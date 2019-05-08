# Example of a ASP.NET Core Nexus adapter

This example has the following objectives:
* Serve as an example for the [adapter developer documentation](https://nexus.link/docs/development/nexus-adapter)
* Act as a pattern for simple Nexus adapters
* Tackle the most common challenges when writing an adapter

## Solution overview

The solution consists of the following projects:

### BusinessApi.Contracts

Contains the contracts that are provided by the Nexus integration team. The contracts are grouped into capabilities that are implemented by the different adapters. An adapter will implement a few of them, often only one. In the example we have two capability contracts; Integration and OnBoarding.

In a real situation the contracts would be provided as a NuGet package.

### BusinessApi.Mock

Code to simulate the business API that is provided by the Nexus integration team.

In a real situation this would be provided as an SDK 
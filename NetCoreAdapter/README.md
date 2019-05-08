# Example of a ASP.NET Core Nexus adapter

This example has the following objectives:
* Serve as an example for the [adapter developer documentation](https://nexus.link/docs/development/nexus-adapter)
* Act as a pattern for simple Nexus adapters
* Tackle the most common challenges when writing an adapter

If you are new to Nexus Link, then you might want to keep a browser tab open with the [glossary](https://nexus.link/docs/glossary) until you have become familiar with the Nexus Link vocabulary. 

## Scenario

The example is for an organization that is focused on its members and their needs. To become a member you will have to apply first and you will either be approved or rejected. You can also withdraw your application. 

### Entities

The example deals with two entities; *Applicant* and *Member*. A person that has applied to become a member is called an Applicant. If an application is approved, then the Applicant becomes a Member.

### Capabilities

There are two capabilities in this example; the *integration capability* that is provided by the *business API* and the *on-boarding capability* that should be provided by the CRM system.

The integration capability provides services for publishing events, etc. The on-boarding capability provides services for dealing with the application process.

### Challenges

The CRM system is a standard system that has another entity model and a business logic that doesn't fit one-to-one with how the business logic think of the on-boarding process:
* Applicant is called *Lead*
* Member is called *Contact*
* A Lead can be "qualified" or rejected, never withdrawn.
* A Lead is a complex object to handle situations that our organization does not need
* The list of Applicants is not the same as the list of Leads, because a Lead is not deleted after it has been qualified or rejected, it only changes its state.
* The CRM system has proprietary error codes that doesn't make any sense in the digital platform

## Solution overview

The solution consists of the following projects:

### Crm.NexusAdapter.Service

This is the only code that you really need to focus on. The rest of the projects are mock-ups of things that either would be provided to you as NuGet packages or code that would be applications of their own.

### BusinessApi.Contracts

Contains all contracts for the organization. The contracts are grouped into *capabilities* that are implemented by the different adapters. An adapter will implement a few of them, often only one. In the example we have two capability contracts; `Integration` and `OnBoarding`.

In reality the contracts would be provided as a NuGet package by the *Nexus integration team*.

### BusinessApi.Controllers

Contains the controller templates that makes it really easy to make the adapter controllers, because you simply inherit from these template controllers.

In reality the controller templates would be provided as a NuGet package by the *Nexus integration team*.

### BusinessApi.Sdk

If the adapter wants to access functionality from another capability, it has to go through the *business API*. 

In reality this would be provided as a NuGet package by the *Nexus integration team* and would call the services in the *business API* for you. The business API and its SDK is developed by the Nexus integration team.
In the example we provide a mock implementation instead of calling an API.

### Crm.NexusAdapter.Contract

This part is optional, but the back-end system must call the adapter somehow whenever it wants to reach other parts of the digital platform. In this example this is the contract for those calls.

### Crm.System

This is the "back-end system" that the adapter is for. In reality this would be an application of its own and the adapter would communicate with that system in a proprietary way.


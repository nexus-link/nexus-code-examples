# Example of a ASP.NET Core Nexus adapter

This example has the following objectives:
- Serve as an example for the [adapter developer documentation](https://nexus.link/docs/development/nexus-adapter)
- Act as a pattern for simple Nexus adapters
- Tackle the most common challenges when writing an adapter

If you are new to Nexus Link, then you might want to keep a browser tab open with the [glossary](https://nexus.link/docs/glossary) until you have become familiar with the Nexus Link vocabulary. 

## TODO to make the example complete

- The adapter must catch the CRM exceptions and convert them into FulcrumExceptions.

## Scenario

The example is for an organization that is focused on its members and their needs. To become a member you will have to apply first and you will either be approved or rejected. You can also withdraw your application. 

### Entities

The example deals with two entities; *Applicant- and *Member*. A person that has applied to become a member is called an Applicant. If an application is approved, then the Applicant becomes a Member.

### Capabilities

There are two capabilities in this example; the *integration capability- that is provided by the *business API- and the *on-boarding capability- that should be provided by the CRM system.

The integration capability provides services for publishing events, etc. The on-boarding capability provides services for dealing with the application process.

### Challenges

The CRM system is a standard system that has another entity model and a business logic that doesn't fit one-to-one with how the business logic think of the on-boarding process:
- Applicant is called *Lead* and Member is called *Contact*
- A Lead can be "qualified" or rejected, never withdrawn.
- A Lead is a complex object to handle situations that our organization does not need
- The list of Applicants is not the same as the list of Leads, because a Lead is not deleted after it has been qualified or rejected, it only changes its state.
- The CRM system has proprietary error messages that doesn't make any sense in the digital platform

## Solution overview

The solution consists of the following projects:

### Adapter Code

The code that actually is part of the adapter. All other projects represents code that normally is created outside the adapter by other developers than the adapter developer.

#### Crm.NexusAdapter.Contract

This part is optional, but the back-end system must call the adapter somehow whenever it wants to reach other parts of the digital platform. In this example this is the contract for those calls.

#### Crm.NexusAdapter.Service

This is where you put your main effort as a developer and the focus for this example.

### External applications

These projects represents code that normally would be their own separate applications.

#### Crm.System

This is the "back-end system" that the adapter is for. It represents the data persistence and the business logic for the capability. In reality the adapter would communicate with it in a way that is proprietary for that specific system.

### Imported NuGets

These projects represents code that normally would be provided as NuGet packages by the *Nexus integration team*.

#### BusinessApi.Contracts

Contains all contracts for the organization. The contracts are grouped into *capabilities- that are implemented by the different adapters. An adapter will implement a few of them, often only one. In the example we have two capability contracts; `Integration` and `OnBoarding`.

### BusinessApi.Controllers

Contains the controller templates that makes it really easy to make the adapter controllers, because you simply inherit from these template controllers.

#### BusinessApi.Sdk

If the adapter wants to access functionality from another capability, it has to go through the *business API*. The SDK make that simple and hides the integration details. In reality this SDK would call the services in the *business API- for you, but here that functionality is mocked.

## Try it

Run the application and try to do the following from the swagger web page.

1. Seed the application with initial data. (`POST /api/Administration/Seed`)
2. List the current members (`GET /api/Members`, expect to get a list of 2 members)
3. List the current applicants (`GET /api/Applicants`, expect to get a list of 3 applicants)
4. Copy the id for each of the applicants and do the following:
	1. Approve "Johnny B. Goode" (`POST /api/Applicants/{id}/Approve`)
	2. Reject "Bad Cousin" (`POST /api/Applicants/{id}/Reject`)
	3. Withdraw "Willy Nilly" (`POST /api/Applicants/{id}/Withdraw`)
5. List the current members (expect to get a list of 3 members)
6. List the current applicants (expect to get an empty list)

## A deeper look

Now it is time to have a close look at the code. We will do this by guiding you through some use cases where we follow the flow of the code.

Prepare by setting a debug breaking point in every method in the controller base classes in `BusinessApi.Controllers.Capabilities.OnBoarding`. Start the adapter in debug mode.

### Create an applicant

1. Reset the database (`POST /api/Administration/Reset`)
2. Create an applicant (`POST /api/Applicants`) with name "John Doe". You should end up in the debugger in the method `ApplicantsControllerBase.CreateAsync`. This is in the service layer that was provided by the Nexus integration team. The service layer provides the REST interface, verifies the parameters and calls the logic layer where the real job is done.
	1. The `ServiceContract` statements are guards that verifies the input parameters. A problem with an in parameter results in a response with status code 400. (Try for instance to send in a non-null `id`). Please note that if a class implements `IValidatable`, then there is a special guard method to validate on object of that class.
	2. Notice that the service layer has access to the entire on-boarding capability as one object. This makes the dependency injection part so much easier.
	3. Step into the call to the logic layer. This is the main code for the adapter.
		1. The `InternalContract` statement is a guard that protects the code from unexpected in parameters. In this case its make no sense to create an applicant with no data. This protects us from missing guards in the service layer. If an internal contract guard fails it will result in a response with status code 500, as we consider this as an internal server error.
		2. The in parameters are converted to the data model for the CRM system.
		3. We create a *Lead* in the CRM system.
		4. The result is converted to the capability contract data model (in this case from a Guid to a string).

### Read all applicants

1. Seed the application with initial data.

 

### Crm.System

## A deeper look

The core challenge is that the CRM system (`Crm.System`) is both modeled differently and behave differently from what the contract (`BusinessApi.Contracts`) says. The adapter (`Crm.NexusAdapter.Service`) is where we add code to translate between the contract and the actual system. The code for this is located in the `Capabilities.OnBoarding.Logic` part of that project.

### Capabilities.OnBoarding.Logic.TypeMappingExtensions.cs

This is an internal support class for mapping between the data models. The models doesn't differ too much, so this is mostly mapping of fields.

### ApplicantLogic.cs


### The data model differs

The data model for the OnBoarding capability in project `BusinessApi.Contracts` is different from the data model in project `Crm.System`. Mapping between these models is the main task of the adapter.  



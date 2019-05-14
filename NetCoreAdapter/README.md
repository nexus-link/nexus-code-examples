# Example of a ASP.NET Core Nexus adapter

This example has the following objectives:
- Serve as an example for the [adapter developer documentation](https://nexus.link/docs/development/nexus-adapter)
- Act as a pattern for simple Nexus adapters
- Tackle the most common challenges when writing an adapter

If you are new to Nexus Link, then you might want to keep a browser tab open with the [glossary](https://nexus.link/docs/glossary) until you have become familiar with the Nexus Link vocabulary. 

## Try it

The adapter implements the on-boarding capability for member-driven organization, so it provides functionality for applying for membership and for qualifying and rejecting applicants.

Build and run the application in debug mode. Try to do the following from the swagger web page.

1. Seed the application with initial data. (`POST /api/Administration/Seed`)
2. List the current members (`GET /api/Members`, expect to get a list of 2 members)
3. List the current applicants (`GET /api/Applicants`, expect to get a list of 3 applicants)
4. Copy the id for each of the applicants and do the following:
	1. Approve "Johnny B. Goode" (`POST /api/Applicants/{id}/Approve`)
	2. Reject "Bad Cousin" (`POST /api/Applicants/{id}/Reject`)
	3. Withdraw "Willy Nilly" (`POST /api/Applicants/{id}/Withdraw`)
5. List the current members (expect to get a list of 3 members)
6. List the current applicants (expect to get an empty list)

## Scenario

The example is for an organization that is focused on its members and their needs. To become a member you will have to apply first and you will either be approved or rejected. You can also withdraw your application. 

### Entities

The example deals with two entities; *Applicant* and *Member*. A person that has applied to become a member is called an Applicant. If an application is approved, then the Applicant becomes a Member.

### Capabilities

There are two capabilities in this example; the *integration capability* that is provided by the *business API* and the *on-boarding capability* that should be provided by the CRM system.

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

Contains all contracts for the organization. The contracts are grouped into *capabilities* that are implemented by the different adapters. An adapter will implement a few of them, often only one. In the example we have two capability contracts; `Integration` and `OnBoarding`.

### BusinessApi.Controllers

Contains the controller templates that makes it really easy to make the adapter controllers, because you simply inherit from these template controllers.

#### BusinessApi.Sdk

If the adapter wants to access functionality from another capability, it has to go through the *business API*. The SDK make that simple and hides the integration details. In reality this SDK would call the services in the *business API* for you, but here that functionality is mocked.

## A deeper look

Now it is time to have a close look at the code. We will do this by guiding you through some use cases where we follow the flow of the code.

Prepare by setting a debug breaking point in every method in `BusinessApi.Controllers.Capabilities.OnBoarding.ApplicantsControllerBase`. Start the adapter in debug mode.

### Create an applicant

This use case serves as introduction to
- Service layer and logic layer
- Guards (`ServiceContract` and `InternalContract`
- The interface `IValidatable`
- The basic structure for a method in the logic layer

1. Reset the database (`POST /api/Administration/Reset`)
2. Create an applicant (`POST /api/Applicants`) with name "John Doe". You should end up in the debugger in the method `ApplicantsControllerBase.CreateAsync`. This is in the service layer that was provided by the Nexus integration team. The service layer provides the REST interface, verifies the parameters and calls the logic layer where the real job is done.
	1. The `ServiceContract` statements are guards that verifies the input parameters. A problem with an in parameter results in a response with status code 400. (Try for instance to send in a non-null `id`). Please note that if a class implements `IValidatable`, then there is a special guard method to validate on object of that class.
	2. Notice that the service layer has access to the entire on-boarding capability as one object. This makes the dependency injection part so much easier.
	3. Step into the call to the logic layer. This is the main code for the adapter.
		1. The `InternalContract` statement is a guard that protects the code from unexpected in parameters. In this case its make no sense to create an applicant with no data. This protects us from missing guards in the service layer. If an internal contract guard fails it will result in a response with status code 500, as we consider this as an internal server error.
		2. The in parameters are converted to the data model for the CRM system.
		3. We create a lead in the CRM system and get the id for that new lead in return.
		4. The result is converted to the capability contract data model (in this case from a Guid to a string).
3. Done

### Withdraw an applicant

This use case shows:
- How to customize the functionality of the back-end system.
- How a proprietary error can be converted to a business API contract error.

1. Reset the database and seed with initial data.
2. List the current applicants without debugging the code. Copy the id for "Willy Nilly".
3. Withdraw "Willy Nilly" (`POST /api/Applicants/{id}/Withdraw`). You should end up in the method `ApplicantsControllerBase.WithdrawAsync`. Step into the the call to the logic layer.
	1. There are no parameters that need to be converted.
	2. The CRM system has no concept of "withdraw", so we will use its `RejectAsync` method to withdraw the application. So in practice, the lead will be marked as rejected with the comment "Application withdrawn".
	3. There is no result that needs to be converted.
4. Now try to accept "Willy Nilly", i.e. use the same id that was withdrawn.
	1. Step over the `RejectAsync` method. The method will throw an exception.
	2. The exception is proprietary to the CRM system, so we will convert it to a Nexus exception.
5. Note (in the swagger UI) that you will get status code 400 and a structured error description in JSON format. The conversion from a Nexus exception into an HTTP response was made in the middleware that is provided by a Nexus NuGet package. The adapter has activated it in the `Configure` method of `Startup.cs`. 
	
### Read applicants

This use case shows how to customize the visible behavior of the back-end system. The back-end system saves all historic applicants, but we only want to deal with the active applicants.

1. Reset the database and seed with initial data.
2. List the current applicants without debugging the code. Copy the id for "Willy Nilly".
3. Withdraw the applicant "Willy Nilly" without debugging the code.
4. Call `GET /api/Applicants`. You should end up in the method `ApplicantsControllerBase.ReadAllsync`. Step into the the call to the logic layer.
	1. There are no parameters that needs conversion.
	2. We get a list of all leads from the CRM system. Note that we get three leads where one has type `Rejected` and the other two has type `Active`.
	3. We select the leads that has type `Active` and converts them to the capability contract model.
5. Note that there are only two items in the list that is displayed in the swagger UI.
6. Done



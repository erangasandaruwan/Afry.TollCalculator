# **AFRI Toll Fee Calculator**

## **Introduction**
The AFRI toll calculator calculates the toll fee for a vehicle based on the type of vehiclea and the date/time ranges. 

## **Domain Problem**
There are few errors with the toll fee calculation logic and need to refactor the code to make it more readable and maintainable.

## **Logic Implementation**
The following code changes have been done on top of the intially defined logics, available with implementation at https://github.com/afry-recruitment/toll-calculator.
The goal of the implementations available with this coding assignment is to fixe logical errors and optimize the code in more descriptive way.

1. Error correction
   - Sort the data range to handle calculations correctly. **dates.OrderBy(d => d).ToList()**
   - Set the entry point for the date calculation correctly. **intervalStart = date**
2. Create extension methods for common functions.
3. Refactor the code for better readability and maintainability.
4. Add unit tests for testability.

## **API Implementation**
API Implementation is the real world application of the AFRI Toll Fee Calculator. It has implemented under micro service architecture following CQRS pattern and is presented with swagger. Further, the NUnit tests are added to the CQRS logics implementation including the core logics of the toll calculation. Also for the input validations at API endpoints, FluentValidation is used and OpenAPI is used to generate the API documentation with swagger.

Frameworks and Libraries used:

 - .NET Core 3.1
 - NUnit
 - Autofac
 - FluentValidation
 - OpenAPI Standard

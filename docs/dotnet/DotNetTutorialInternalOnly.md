# [Popcorn](../../README.md) > [Documentation](../Documentation.md) > [DotNet](DotNetDocumentation.md) > Tutorial: Internal Only Attribute

[Table Of Contents](../../docs/TableOfContents.md)
We assume you've already learned the basics of configuring Popcorn in this tutorial. 
If not, you should probably go back and complete [Getting Started](DotNetTutorialGettingStarted.md) first.


This tutorial will walk you through a few ways to protect undesired data from being passed to the client.

### Overview
We will achieve this using the [InternalOnly] attribute, which can be used on Classes, Properties and Methods.
```csharp
[InternalOnly(throwException)]
```
throwException is a bool parameter and the default value is true.
* When used on classes:
    * If true, an InternalOnlyViolationException will be thrown when you try to expand the class. 
    * If false, you will receive a null object in response.
 * When used on methods and properties:
    * If true an InternalOnlyViolationException will be thrown when you try to access the marked field or method. 
    * If false, you will receive a null object for the requested property or method in response.

### Example usage:
Let's say  we store employee Social Security Numbers in our database. Under no circumstance do we want their socials to 
be transmitted through our project using Popcorn. Enter the power of [InternalOnly]!

First, we add the SocialSecurityNumber property to our Employee class and its projection.
(Admittedly you could just not add the Social to the projection, but technically that is still a little vulnerable to 
blind mapping and the like)
```csharp
public class Employee
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [InternalOnly(true)]
    public int SocialSecurityNumber { get; set; }
	...
}

public class EmployeeProjection
{
    [IncludeByDefault]
    public string FirstName { get; set; }
    [IncludeByDefault]
    public string LastName { get; set; }

    public int SocialSecurityNumber { get; set; }
 }

```

Note that the SocialSecurityNumber property is marked as [InternalOnly] and set to throw an exception should it be called.
**A key difference to remember here when compared to some other attributes is the source object is marked with the [InternalOnly] attibute and not its projection  
- to make sure that the SocialSecurityNumber attribute isn't referenced accidentally in another place**

Let's try making a request now to a GET employees endpoint and see what comes back when we specifically try to include SocialSecurityNumbers.
```json
http://localhost:49699/api/example/employees?include=[SocialSecurityNumber]
{
    "Success": false,
    "ErrorCode": "Skyward.Popcorn.InternalOnlyViolationException",
    "ErrorMessage": "Expand: SocialSecurityNumber property inside Employee class is marked [InternalOnly]",
    "ErrorDetails": "Skyward.Popcorn.InternalOnlyViolationException: Expand: SocialSecurityNumber property inside Employee class is marked [InternalOnly]...
}
```

An exception was thrown as expected!
If we were to make a request to the GET employees method without a specific mention of SocialSecurityNumber, we would see a success response 
with all of the other relevant employee information - just not the Social.

It is up to you on how you will use the throwException parameter based on your needs.

**Don't Forget:** This attribute can be applied to Classes, Methods, and Properties so you have a lot of freedom here!

And that's it, you can now use the [InternalOnly] attribute.

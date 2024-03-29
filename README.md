## The CRM Web API consists of 3 projects
 - CRM_AuthAPI
 - CRM_WebApp
 - CRMWeb_API

### CRM_AuthAPI
The CRM_AuthAPI consists of the implementation of Authentication and Authorization using JWTBearerTokens and Microsoft Identity.
.Net Core 8 Web API project

### CRM_WebApp
The CRM_WebApp contains the implementation for the frontend MVC for the Tenants, Register and Login Entities
.Net Core 8 MVC Project

### CRMWeb_Api
This contains the WebAPI controller for the Tenant, Customer and the middleware fpr multitenant implementation
.Net Core 8 Web API project
   

# Azure B2C Setup for Blazor WebAssembly and Web API

This repository contains a Blazor WebAssembly application and a Web API that use Azure B2C for authentication. Follow these steps to configure Azure B2C.

## 1. Create Azure B2C Tenant
1.1 Search for **Azure Active Directory B2C** under **Create a resource**.  
1.2 Create a new **Azure AD B2C Tenant**.

## 2. Create User Flow (Sign-Up and Sign-In - SUSI)
- Select or add **Identity Providers**, otherwise use local email login.  
- Choose relevant **claims**.
- Add more User Flows if needed. (Edit profile etc..)

## 3. Create App Registration for Web API
3.1 **Expose API**.  
3.2 **Create API Scope**.

## 4. Create App Registration for Blazor WebAssembly
4.1 **Add Callback URL** for SPA:  
https://<your-url> (Example: https://localhost:7022)/authentication/login-callback
4.2 **Check both Access Token and ID Token** in the **Authorization** tab.  
4.3 **Add API Permission** for Web API scope and **grant admin consent**.

## 5. Configure Blazor WebAssembly and Web Api
- Fill appsettings.json in both projects with correct values from Azure B2C.

---

This guide provides a minimal yet complete setup for integrating Azure B2C authentication with the provided Blazor WebAssembly and Web API projects.

# What does this project do?

This is an open source password keeper developed in c# and usable on Android, iOS and Windows 10.

# Why is this project useful?

There are many password keeper out there (commercial and free), I have decided to develop my personal one because for this type of tool, availability of the cource code is a plus, and a guarantee. Even if you are not able to read the code, open discussions here can help you to understand if you can trust this code or not.

This tool also do not have his own cloud, but just save a file on your OneDrive (in future I hope also to support on other cloud storages)

# How do I get started?
Fork this repository, choose one of the open issues and start coding.

# Where can I get more help, if I need it?
just open an issue

# Build status
|Build server   | Operationg System  | Status  
|---|---|---
|  AppCenter |  Android | [![Build status](https://build.appcenter.ms/v0.1/apps/90672336-6ebb-43b7-9768-1b73806603a4/branches/master/badge)](https://appcenter.ms) |
|  AppCenter |  UWP | [![Build status](https://build.appcenter.ms/v0.1/apps/22192568-d4b0-4018-9b77-07361c3be646/branches/master/badge)](https://appcenter.ms)  |
|  AppCenter | iOs  |  never built  |

# Configure your ApplicationId for OneDrive access  
To access to OndDrive Personal Files, Open.Padlock uses Microsoft Graph. To allow access to this API, an [Azure AD application and service principal](https://docs.microsoft.com/en-us/azure/active-directory/develop/howto-create-service-principal-portal#register-an-application-with-azure-ad-and-create-a-service-principal) on an Azure Active Directory is required.

* go to https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/Overview to access to your Azure Active Directory
* go to: App Registrations > New Registration
  * Name: `Nicold.Padlock`
  * Supported account types: `Personal Microsoft accounts only`
  * Click **Register**
  
once service principal is created:

* go to Authentication > Add a platform > Mobile and desktop applications
  * select the row that begins with **msal** (MSAK only)
  * click **Configure**
* go to API Permission > Add a Permission > click on Microsoft Graph
  * Select Delegated Permission
  * type `Files.ReadWrite.All` in Search Permission field.
    * Select Files > Files.ReadWrite.All permission
  * Click **Add Permission**
  
once configured the Service principal you have to use its ApplicationID guid in:
    * `nicold.Padlock\nicold.Padlock.Models\Services\OneDrive.cs`: ClientID private string
    * `nicold.Padlock\nicold.Padlock.Android\Properties\AndroidManifest.xml`: intent-filter > data > android:scheme attribute
  
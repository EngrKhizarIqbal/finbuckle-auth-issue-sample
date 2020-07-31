# finbuckle-auth-issue-sample

Use the following commands to create databases. We are using LocalDb for development, you can update it to whatever server you are using.
```
Update-Database -Context ApplicationDbContext
Update-Database -Context QuodyMutiTenantDbContext
```

If you update data source, don't forgot to update the **ApplicationDbContextFactory**

A file named SeedData.sql is also included into the project which you can run to populate the sample data.

Email is hard coded in the system and the password for all users is **Admin@1234_**

To allow subdomains in the development mode, you need to update the applicationhost which you can find under **{solution folder}\.vs\HelpDesk.MultiTenant\config\applicationhost**. Open the file and find for entry `<binding protocol="https" bindingInformation="*:44343:localhost" />` below this line, add the following lines to use subdomains for this project.
```
<binding protocol="https" bindingInformation="*:44343:idsa.localhost" />
<binding protocol="https" bindingInformation="*:44343:contoso.localhost" />
<binding protocol="https" bindingInformation="*:44343:kiqbal.localhost" />
<binding protocol="https" bindingInformation="*:44343:abacus.localhost" />
<binding protocol="https" bindingInformation="*:44343:test.localhost" />
<binding protocol="https" bindingInformation="*:44343:helpdesk.localhost" />
```

Now, you can use any subdomain listed above to test the app. **Don't forget to open visual studio as admin**

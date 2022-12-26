# skinet_Push

Creating Online Store using .net core.

# PostMan 

postman is tool for testing API's. here are some endpoints which i build yet.

# some useful command:

dotnet build : for building project
dotnet watch run : check changes along side  with save

# Git

Git Status
Git add .
Git commit -m "Your Message"
Git Push origion master

# EF migrations

dotnet ef migrations add IdentityInitial -p Infrastructure -s API -c AppIdentityDbContext -o Identity/Migrations
- Drop database
 * dotnet ef database drop -p Infrastructure -s API -c StoreContext

# Angular Extension

install Angular language service and snippet for help.

# Angular Basics

For creating Components: 

ng g c component-name  ( Use Pascal case for naming convention)
if you want to skip any file e.g test ts or any. just use:

ng g c component-name --skip-tests
ng g m core (Create core module)
ng g c shop --flat --skip-tests
ng g s shop --flat --skip-tests (it will create service inside the shop folder. Flat will not allow him to add new folder)
ng generate module Component_Name-routing --flat
# CSS

- flexboxfroggy

# Bootstrap Theme

By using this website, we can change anytheme design. Just go to angular json file, where we define bootswatch change theme name and reload the application. everything is setup.
 https://bootswatch.com/

# SQL Query 

ALTER TABLE Orders
RENAME COLUMN OldName TO newName;
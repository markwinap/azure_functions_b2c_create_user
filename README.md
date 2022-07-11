# Azure function B2C create user and reactjs login and view user info
## Function init
```sh
func init . --dotnet
func new --name HttpExample --template "HTTP trigger" --authlevel "anonymous"
```

## Restore current project
```sh
dotnet restore
```

## APP Auth

https://docs.microsoft.com/en-us/graph/sdks/choose-authentication-providers?view=graph-rest-beta&tabs=CS


## User Create
https://docs.microsoft.com/en-us/graph/api/user-post-users?view=graph-rest-beta&tabs=http



## Request
```sh
curl --location --request POST 'http://localhost:7071/api/user' \
--header 'Content-Type: application/json' \
--data-raw '{
    "name": "test3 name",
    "last_name": "test3 lastname",
    "user_name": "test3"
}'
```
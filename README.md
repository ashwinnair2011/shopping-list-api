# Shopping List API

A .NET 8.0 Web API for managing shopping lists with user authentication and authorization.

## Features

- User authentication with JWT tokens
- Shopping list management (create, read, update, delete)
- Shopping list item management
- User permissions system
- Settings management
- Swagger/OpenAPI documentation

## Prerequisites

- .NET 8.0 SDK
- SQL Server (or compatible database)
- Visual Studio 2022 or compatible IDE

## Getting Started

1. Clone the repository
```bash
git clone [repository-url]
cd shopping-list-api
```

2. Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your-Connection-String"
  }
}
```

3. Update JWT settings in `appsettings.json`:
```json
{
  "Jwt": {
    "Key": "Your-Secret-Key",
    "Issuer": "Your-Issuer",
    "Audience": "Your-Audience"
  }
}
```

4. Run database migrations:
```bash
dotnet ef database update
```

5. Run the application:
```bash
dotnet run
```

The API will be available at `https://localhost:5001` and `http://localhost:5000`

## API Documentation

### Authentication

#### Login
- **POST** `/api/authentication`
- **Parameters:**
  - `username`: User's username
  - `password`: User's password
- **Returns:** JWT token for authenticated requests

#### Get Current User
- **GET** `/api/authentication/me`
- **Authorization:** Bearer token required
- **Returns:** Current user's details and permissions

#### Logout
- **GET** `/api/authentication/logout`
- **Authorization:** Bearer token required
- **Returns:** Logout confirmation

### Shopping Lists

#### Get All Shopping Lists
- **GET** `/api/shoppinglist`
- **Authorization:** Bearer token required
- **Parameters:**
  - `orderBy`: Field to order by
  - `order`: "asc" or "desc"
  - `search`: Search term (optional)
  - `skip`: Number of records to skip
  - `pageSize`: Number of records per page
- **Returns:** List of shopping lists and total count

#### Get Single Shopping List
- **GET** `/api/shoppinglist/{id}`
- **Authorization:** Bearer token required
- **Returns:** Shopping list details with items

#### Create Shopping List
- **POST** `/api/shoppinglist`
- **Authorization:** Bearer token required
- **Body:**
```json
{
  "name": "string",
  "description": "string",
  "statusId": 0
}
```

#### Update Shopping List
- **PUT** `/api/shoppinglist`
- **Authorization:** Bearer token required
- **Body:**
```json
{
  "shoppingListId": 0,
  "name": "string",
  "description": "string",
  "statusId": 0
}
```

#### Delete Shopping List
- **DELETE** `/api/shoppinglist/{id}`
- **Authorization:** Bearer token required

### Shopping List Items

#### Add Item
- **POST** `/api/shoppinglist/item`
- **Authorization:** Bearer token required
- **Body:**
```json
{
  "shoppingListId": 0,
  "itemName": "string",
  "quantity": 0,
  "notes": "string"
}
```

#### Update Item
- **PUT** `/api/shoppinglist/item`
- **Authorization:** Bearer token required
- **Body:**
```json
{
  "shoppingListItemId": 0,
  "shoppingListId": 0,
  "itemName": "string",
  "quantity": 0,
  "notes": "string",
  "isCompleted": false
}
```

#### Delete Item
- **DELETE** `/api/shoppinglist/item/{id}`
- **Authorization:** Bearer token required

#### Toggle Item Completion
- **PUT** `/api/shoppinglist/item/toggle/{id}`
- **Authorization:** Bearer token required

### Settings

#### Get Settings
- **GET** `/api/settings`
- **Authorization:** Bearer token required
- **Returns:** List of system settings

#### Save Setting
- **POST** `/api/settings`
- **Authorization:** Bearer token required
- **Parameters:**
  - `settingId`: Setting ID
  - `settingValue`: New setting value

## Security

- All endpoints (except login) require JWT authentication
- Passwords are hashed using BCrypt
- User permissions are checked for each operation
- HTTPS is enforced in production

## Error Handling

The API uses standard HTTP status codes:
- 200: Success
- 400: Bad Request
- 401: Unauthorized
- 403: Forbidden
- 404: Not Found
- 500: Internal Server Error

All error responses follow the format:
```json
{
  "success": false,
  "result": "Error message"
}
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

[Your License Here] 
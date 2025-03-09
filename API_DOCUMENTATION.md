# Shopping List API Documentation

## Base URL
```
https://localhost:5001
```

## Authentication

### Login
Authenticate a user and receive a JWT token.

**Endpoint:** `POST /api/authentication`

**Parameters:**
```json
{
  "username": "string",
  "password": "string"
}
```

**Success Response:**
```json
{
  "success": true,
  "token": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**Error Response:**
```json
{
  "success": false,
  "result": "Invalid user credentials"
}
```

### Get Current User
Get details of the currently authenticated user.

**Endpoint:** `GET /api/authentication/me`

**Headers:**
```
Authorization: Bearer {token}
```

**Success Response:**
```json
{
  "success": true,
  "result": {
    "userId": 1,
    "userName": "john.doe",
    "permissions": [
      "canLogInToApi",
      "canManageShoppingLists"
    ]
  }
}
```

### Logout
Invalidate the current user's token.

**Endpoint:** `GET /api/authentication/logout`

**Headers:**
```
Authorization: Bearer {token}
```

**Success Response:**
```json
{
  "success": true,
  "result": "Logged out"
}
```

## Shopping Lists

### Get All Shopping Lists
Retrieve a paginated list of shopping lists.

**Endpoint:** `GET /api/shoppinglist`

**Headers:**
```
Authorization: Bearer {token}
```

**Query Parameters:**
```json
{
  "orderBy": "name",
  "order": "asc",
  "search": "groceries",
  "skip": 0,
  "pageSize": 10
}
```

**Success Response:**
```json
{
  "success": true,
  "result": {
    "items": [
      {
        "shoppingListId": 1,
        "name": "Grocery List",
        "description": "Weekly groceries",
        "statusId": 1,
        "items": [
          {
            "shoppingListItemId": 1,
            "itemName": "Milk",
            "quantity": 1,
            "notes": "2% fat",
            "isCompleted": false
          }
        ]
      }
    ],
    "totalCount": 1
  }
}
```

### Get Single Shopping List
Retrieve a specific shopping list by ID.

**Endpoint:** `GET /api/shoppinglist/{id}`

**Headers:**
```
Authorization: Bearer {token}
```

**Success Response:**
```json
{
  "success": true,
  "result": {
    "shoppingListId": 1,
    "name": "Grocery List",
    "description": "Weekly groceries",
    "statusId": 1,
    "items": [
      {
        "shoppingListItemId": 1,
        "itemName": "Milk",
        "quantity": 1,
        "notes": "2% fat",
        "isCompleted": false
      }
    ]
  }
}
```

### Create Shopping List
Create a new shopping list.

**Endpoint:** `POST /api/shoppinglist`

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
```

**Request Body:**
```json
{
  "name": "Grocery List",
  "description": "Weekly groceries",
  "statusId": 1
}
```

**Success Response:**
```json
{
  "success": true,
  "result": {
    "shoppingListId": 1,
    "name": "Grocery List",
    "description": "Weekly groceries",
    "statusId": 1,
    "items": []
  }
}
```

### Update Shopping List
Update an existing shopping list.

**Endpoint:** `PUT /api/shoppinglist`

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
```

**Request Body:**
```json
{
  "shoppingListId": 1,
  "name": "Updated Grocery List",
  "description": "Monthly groceries",
  "statusId": 2
}
```

**Success Response:**
```json
{
  "success": true,
  "result": {
    "shoppingListId": 1,
    "name": "Updated Grocery List",
    "description": "Monthly groceries",
    "statusId": 2,
    "items": []
  }
}
```

### Delete Shopping List
Delete a shopping list.

**Endpoint:** `DELETE /api/shoppinglist/{id}`

**Headers:**
```
Authorization: Bearer {token}
```

**Success Response:**
```json
{
  "success": true,
  "result": true
}
```

## Shopping List Items

### Add Item
Add a new item to a shopping list.

**Endpoint:** `POST /api/shoppinglist/item`

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
```

**Request Body:**
```json
{
  "shoppingListId": 1,
  "itemName": "Milk",
  "quantity": 1,
  "notes": "2% fat"
}
```

**Success Response:**
```json
{
  "success": true,
  "result": {
    "shoppingListItemId": 1,
    "shoppingListId": 1,
    "itemName": "Milk",
    "quantity": 1,
    "notes": "2% fat",
    "isCompleted": false
  }
}
```

### Update Item
Update an existing shopping list item.

**Endpoint:** `PUT /api/shoppinglist/item`

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
```

**Request Body:**
```json
{
  "shoppingListItemId": 1,
  "shoppingListId": 1,
  "itemName": "Milk",
  "quantity": 2,
  "notes": "Whole milk",
  "isCompleted": true
}
```

**Success Response:**
```json
{
  "success": true,
  "result": {
    "shoppingListItemId": 1,
    "shoppingListId": 1,
    "itemName": "Milk",
    "quantity": 2,
    "notes": "Whole milk",
    "isCompleted": true
  }
}
```

### Delete Item
Delete a shopping list item.

**Endpoint:** `DELETE /api/shoppinglist/item/{id}`

**Headers:**
```
Authorization: Bearer {token}
```

**Success Response:**
```json
{
  "success": true,
  "result": true
}
```

### Toggle Item Completion
Toggle the completion status of a shopping list item.

**Endpoint:** `PUT /api/shoppinglist/item/toggle/{id}`

**Headers:**
```
Authorization: Bearer {token}
```

**Success Response:**
```json
{
  "success": true,
  "result": true
}
```

## Settings

### Get Settings
Retrieve all system settings.

**Endpoint:** `GET /api/settings`

**Headers:**
```
Authorization: Bearer {token}
```

**Success Response:**
```json
{
  "success": true,
  "result": [
    {
      "settingId": 1,
      "settingName": "DefaultPageSize",
      "settingValue": "10",
      "settingType": "Integer"
    }
  ]
}
```

### Save Setting
Update a system setting.

**Endpoint:** `POST /api/settings`

**Headers:**
```
Authorization: Bearer {token}
```

**Parameters:**
```json
{
  "settingId": 1,
  "settingValue": "20"
}
```

**Success Response:**
```json
{
  "success": true,
  "result": true
}
```

## Error Responses

### Unauthorized Access
```json
{
  "success": false,
  "result": "Unauthorized access"
}
```

### Not Found
```json
{
  "success": false,
  "result": "Resource not found"
}
```

### Bad Request
```json
{
  "success": false,
  "result": "Invalid request parameters"
}
```

### Server Error
```json
{
  "success": false,
  "result": "An error occurred while processing your request"
}
```

## Status Codes

- `200 OK`: Request successful
- `201 Created`: Resource created successfully
- `400 Bad Request`: Invalid request parameters
- `401 Unauthorized`: Authentication required or failed
- `403 Forbidden`: Permission denied
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

## Rate Limiting

The API implements rate limiting to prevent abuse. Clients are limited to:
- 100 requests per minute for authenticated users
- 20 requests per minute for unauthenticated users

## Data Types

### Shopping List
```json
{
  "shoppingListId": "integer",
  "name": "string",
  "description": "string?",
  "statusId": "integer",
  "userId": "integer",
  "items": "ShoppingListItem[]"
}
```

### Shopping List Item
```json
{
  "shoppingListItemId": "integer",
  "shoppingListId": "integer",
  "itemName": "string",
  "quantity": "integer",
  "notes": "string?",
  "isCompleted": "boolean"
}
```

### Setting
```json
{
  "settingId": "integer",
  "settingName": "string",
  "settingValue": "string",
  "settingType": "string"
}
```

### User Permission
```json
{
  "userId": "integer",
  "userName": "string",
  "permissions": "string[]"
}
``` 
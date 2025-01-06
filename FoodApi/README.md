# Food API 

## Base URL

`https://demos.sreekeshkprabhu.me/api/foods`

---

## Endpoints

### 1. Get All Foods

**GET** `/`

Fetch all foods, with optional filters.

#### Query Parameters:

| Parameter   | Type     | Description                           |
|-------------|----------|---------------------------------------|
| `name`      | `string` | Filter foods by name (optional).      |
| `quantity`  | `int`    | Filter foods by quantity (optional).  |

#### Responses:

- **200 OK**  
  Returns a list of foods.

- **404 Not Found**  
  No foods found matching the criteria.

---

### 2. Get Food by ID

**GET** `/{id}`

Fetch a specific food item by its ID.

#### Path Parameters:

| Parameter | Type  | Description           |
|-----------|-------|-----------------------|
| `id`      | `int` | The ID of the food item.|

#### Responses:

- **200 OK**  
  Returns the food item.

- **404 Not Found**  
  Food item with the specified ID was not found.

---

### 3. Create Food

**POST** `/`

Create a new food item.

#### Request Body:

| Field        | Type     | Description              |
|--------------|----------|--------------------------|
| `name`       | `string` | Name of the food item.    |
| `quantity`   | `int`    | Quantity of the food item.|
| `description`| `string` | Description of the food item.|

#### Responses:

- **201 Created**  
  Food item created successfully.

- **400 Bad Request**  
  Invalid food data.

---

### 4. Update Food

**PUT** `/{id}`

Update an existing food item.

#### Path Parameters:

| Parameter | Type  | Description           |
|-----------|-------|-----------------------|
| `id`      | `int` | The ID of the food item.|

#### Request Body:

| Field        | Type     | Description              |
|--------------|----------|--------------------------|
| `name`       | `string` | Updated name of the food item. |
| `quantity`   | `int`    | Updated quantity of the food item. |
| `description`| `string` | Updated description of the food item. |

#### Responses:

- **204 No Content**  
  Food item updated successfully.

- **400 Bad Request**  
  Invalid food data.

- **404 Not Found**  
  Food item with the specified ID was not found.

---

### 5. Delete Food

**DELETE** `/{id}`

Delete a specific food item.

#### Path Parameters:

| Parameter | Type  | Description           |
|-----------|-------|-----------------------|
| `id`      | `int` | The ID of the food item.|

#### Responses:

- **204 No Content**  
  Food item deleted successfully.

- **404 Not Found**  
  Food item with the specified ID was not found.


# Students API

## Base URL

`https://demos.sreekeshkprabhu.me/api/students`

---

## Endpoints

### 1. Get All Students

**GET** `/`

Fetch all students.

#### Responses:

- **200 OK**  
  Returns a list of students.

- **404 Not Found**  
  No students found.

- **500 Internal Server Error**  
  An error occurred while retrieving students.

---

### 2. Get Student by ID

**GET** `/{id}`

Fetch a specific student by their ID.

#### Path Parameters:

| Parameter | Type  | Description           |
|-----------|-------|-----------------------|
| `id`      | `int` | The ID of the student. |

#### Responses:

- **200 OK**  
  Returns the student data.

- **404 Not Found**  
  Student with the specified ID was not found.

- **500 Internal Server Error**  
  An error occurred while retrieving the student.

---

### 3. Create Student

**POST** `/`

Create a new student.

#### Request Body:

| Field        | Type     | Description              |
|--------------|----------|--------------------------|
| `studentName`| `string` | Name of the student.      |
| `course`     | `string` | Course the student is enrolled in. |
| `age`        | `int`    | Age of the student.       |
| `email`      | `string` | Email address of the student. |

#### Responses:

- **201 Created**  
  Student created successfully.

- **400 Bad Request**  
  Invalid student data.

- **500 Internal Server Error**  
  An error occurred while creating the student.

---

### 4. Update Student

**PUT** `/{id}`

Update an existing student.

#### Path Parameters:

| Parameter | Type  | Description           |
|-----------|-------|-----------------------|
| `id`      | `int` | The ID of the student. |

#### Request Body:

| Field        | Type     | Description              |
|--------------|----------|--------------------------|
| `studentName`| `string` | Updated name of the student. |
| `course`     | `string` | Updated course of the student. |
| `age`        | `int`    | Updated age of the student. |
| `email`      | `string` | Updated email of the student. |

#### Responses:

- **200 OK**  
  Student updated successfully.

- **400 Bad Request**  
  Invalid student data.

- **404 Not Found**  
  Student with the specified ID was not found.

- **500 Internal Server Error**  
  An error occurred while updating the student.

---

### 5. Delete Student

**DELETE** `/{id}`

Delete a specific student.

#### Path Parameters:

| Parameter | Type  | Description           |
|-----------|-------|-----------------------|
| `id`      | `int` | The ID of the student. |

#### Responses:

- **200 OK**  
  Student deleted successfully.

- **404 Not Found**  
  Student with the specified ID was not found.

- **500 Internal Server Error**  
  An error occurred while deleting the student.

---

### 6. Search Students

**GET** `/search`

Search students based on filters.

#### Query Parameters:

| Parameter | Type     | Description                    |
|-----------|----------|--------------------------------|
| `course`  | `string` | Filter by course (optional).    |
| `age`     | `int`    | Filter by age (optional).       |
| `name`    | `string` | Filter by name (optional).      |
| `email`   | `string` | Filter by email (optional).     |

#### Responses:

- **200 OK**  
  Returns a list of students matching the criteria.

- **404 Not Found**  
  No students found with the given criteria.

- **500 Internal Server Error**  
  An error occurred while searching for students.

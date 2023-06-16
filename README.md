# ITI-Graduation-Project ( Hospital Reservation System)
This system is made to make patient hospital reservation easier and more organized for patient and hospital staff, staff can limit number of daily reservations so there will not be any work load on them.
When patient make a reservation he receive a queue number and an estimated time to arrive at and that will decrease waste of time and infections

## Installation

### 1. Getting Files
Using [Git Bash](https://github.com/git-for-windows/git/releases/download/v2.41.0.windows.1/Git-2.41.0-64-bit.exe) to clone the repository
  ```bash
    git clone https://github.com/Collines/ITI-Graduation-Project.git FolderName
 ```
OR
Download it as ZIP from [Here](https://github.com/Collines/ITI-Graduation-Project/archive/refs/heads/main.zip)

### 2. Editing Database Configuration
Navigate to downloaded-folder/GraduationProject and open **appsettings.json**
Edit the following
```json
  "ConnectionStrings": {
    "DefaultConnectionString": "Data Source=SERVERNAME;Initial Catalog=DatabaseName;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True"
  }
```
SERVERNAME: usually .
DatabaseName: name it whatever you want
### 3. Installing Database Migrations
1. Open Package Console Manager
![image](https://github.com/Collines/ITI-Graduation-Project/assets/26139899/0bb088c5-d8e0-48a3-9b29-d4e7dca2308f)
2. Choose DAL project
![image](https://github.com/Collines/ITI-Graduation-Project/assets/26139899/7c81c63d-22a6-418e-9162-32d2cf79e872)
4. write
```bash
  update-database
```
Now your database is ready, Compile and build -> Your API server is now ready

### 4.1 Using our Frontend (Client Site)
1. First you need to install [nodeJS](https://nodejs.org/en/download)
2. Install angular CLI
```bash
npm install -g @angular/cli
```
3. Navigate to downloaded-file/Client and open it using Git Bash
![image](https://github.com/Collines/ITI-Graduation-Project/assets/26139899/d1c21b95-2616-43b1-bcaa-2873693e0ece)

4. Use the following command to download our used dependencies
```bash
npm install
```
5. Now you're ready to go, run the following command (**Make sure you're running API server**)
```bash
ng serve --port NeededPortNumber
```
### 4.2 Using our Frontend (Admin Dashboard Site)
1. Navigate to downloaded-file/Dashvoard and open it using Git Bash
![image](https://github.com/Collines/ITI-Graduation-Project/assets/26139899/d1c21b95-2616-43b1-bcaa-2873693e0ece)

2. Use the following command to download our used dependencies
```bash
npm install
```
3. Now you're ready to go, run the following command (**Make sure you're running API server**)
```bash
ng serve --port NeededPortNumber (should be different from the one you've used in client)
```
4. Create an admin account through Database, open your database > Admins table and insert a new account

Now you can manage the site through dashboard and use it through client

# Notice
This project is for learning purposes and not for production yet as there are many modules needed to be added

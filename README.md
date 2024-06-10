# My_Ctf

## Project Overview

My_Ctf is a full-stack school project developed as a Capture The Flag (CTF) challenge platform. This project utilizes ASP.NET for the backend and HTML, CSS, and JavaScript on ASPX for the frontend. The platform features secure user registration and challenge management, with robust security measures to ensure the safety and integrity of user data and challenge flags.

## Features

- **User Registration**: Secure user registration with encrypted passwords.
- **Security Measures**:
  - Password and flag hashing
  - SQL parameterization to prevent SQL injection
  - Strong password enforcement via regular expressions (regex)
  - ASP.NET built-in protections against Cross-Site Scripting (XSS) attacks
  - Whitelist against Arbitrary File Read and Directory Traversal Attack

## Future Features
- **Challenge Management**: Admin interface for managing CTF challenges.

## Frontend Template

The frontend of this project is based on the [CTF-Website-Template-2020](https://github.com/ashawe/CTF-Website-Template-2020) by ashawe. This template provides a structured and responsive design, which has been customized to fit the needs of this CTF platform.

## Technologies Used

### Backend
- **Language**: C#
- **Framework**: ASP.NET

### Frontend
- **Languages**: HTML, CSS, JavaScript
- **Framework**: ASPX

## Setup and Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/yourusername/My_Ctf.git
   cd My_Ctf
   ```

2. **Install Dependencies**:
   Make sure you have Visual Studio installed along with the ASP.NET.
   
3. **Build the Project**:
   Open the project in your Visual Studio and build it to restore the necessary packages and dependencies.

4. **Run the Project**:
   Start the project using your Visual Studio or your preferred IDE. This will launch the local development server and open the application in your default web browser.


## Acknowledgements

- Thanks to ashawe for the [CTF-Website-Template-2020](https://github.com/ashawe/CTF-Website-Template-2020), which served as the base for the frontend of this project.

---

Thank you for checking out My_Ctf! We hope this platform helps you in creating engaging and secure CTF challenges.

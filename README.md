# My_Ctf

## Project Overview

My_Ctf is a full-stack school project developed as a Capture The Flag (CTF) challenge platform. This project utilizes ASP.NET for the backend and HTML, CSS, and JavaScript on ASPX for the frontend. The platform features secure user registration and challenge management, with robust security measures to ensure the safety and integrity of user data and challenge flags.

## Features

- **User Registration**: Secure user registration with encrypted passwords.
- **Challenge Management**: Admin interface for managing CTF challenges.
- **Security Measures**:
  - Password and flag hashing
  - SQL parameterization to prevent SQL injection
  - Strong password enforcement via regular expressions (regex)
  - ASP.NET built-in protections against Cross-Site Scripting (XSS) attacks
  - Whitelist against Arbitrary File Read and Directory Traversal Attack

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
   Make sure you have Visual Studio or Visual Studio Code installed along with the .NET SDK.
   
3. **Build the Project**:
   Open the project in your IDE and build it to restore the necessary packages and dependencies.

4. **Run the Project**:
   Start the project using your IDE. This will launch the local development server and open the application in your default web browser.

## Configuration

Ensure that your database connection strings and other configurations are properly set in the `appsettings.json` file. You may need to create a local database or adjust the configuration to point to your database server.

## Contributing

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Make your changes.
4. Commit your changes (`git commit -am 'Add new feature'`).
5. Push to the branch (`git push origin feature-branch`).
6. Create a new Pull Request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Acknowledgements

- Thanks to ashawe for the [CTF-Website-Template-2020](https://github.com/ashawe/CTF-Website-Template-2020), which served as the base for the frontend of this project.

## Contact

For any questions or feedback, please open an issue on the repository or contact the project maintainer at your-email@example.com.

---

Thank you for checking out My_Ctf! We hope this platform helps you in creating engaging and secure CTF challenges.

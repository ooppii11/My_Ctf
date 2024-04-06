﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="WebApplicationSchoolProject.CTF_pages.Signup" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>My CTF</title>

    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.6.3/css/all.css" integrity="sha384-UHRtZLI+pbxtHCWp1t77Bi1L4ZtiqrqD80Kn4Z8NTSRyMA2Fd33n5dQ8lWUE00s/" crossorigin="anonymous">

    <link rel="stylesheet" href="css/bootstrap4-neon-glow.min.css">


    <link href="https://fonts.googleapis.com/css?family=Roboto" rel="stylesheet">
    <link rel='stylesheet' href='//cdn.jsdelivr.net/font-hack/2.020/css/hack.min.css'>
    <link rel="stylesheet" href="css/main.css">
</head>

<body class="imgloaded">
    <form id="signup" runat="server">
    <div class="glitch">
        <div class="glitch__img glitch__img_register"></div>
        <div class="glitch__img glitch__img_register"></div>
        <div class="glitch__img glitch__img_register"></div>
        <div class="glitch__img glitch__img_register"></div>
        <div class="glitch__img glitch__img_register"></div>
    </div>
    <div class="navbar-dark text-white">
        <div class="container">
            <nav class="navbar px-0 navbar-expand-lg navbar-dark">
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
                <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
                    <div class="navbar-nav">
                        <a href="Index.aspx" class="pl-md-0 p-3 text-decoration-none text-light">
                            <h3 class="bold"><span class="color_danger">MY</span><span class="color_white">CTF</span></h3>
                        </a>
                    </div>
                    <div class="navbar-nav ml-auto">
                        <a href="Index.aspx" class="p-3 text-decoration-none text-light bold">Home</a>
                        <a href="about.html" class="p-3 text-decoration-none text-light bold">About</a>
                        <a href="Login.aspx" class="p-3 text-decoration-none text-light bold">Login</a>
                        <a href="Signup.aspx" class="p-3 text-decoration-none text-white bold">Register</a>
                    </div>
                </div>
            </nav>

        </div>
    </div>

    <div class="jumbotron bg-transparent mb-0 pt-3 radius-0">
        <div class="container">
            <div class="row">
                <div class="col-xl-10">
                    <h1 class="display-1 bold color_white content__title">MY CTF<span class="vim-caret">&nbsp;</span></h1>
                    <p class="text-grey text-spacey hackerFont lead mb-5">
                        Join the community and be part of the future of the information security industry.
                    </p>
                    <div class="row  hackerFont">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:TextBox ID="txtNewUsername" runat="server" CssClass="form-control" placeholder="Username - Team name"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="New Password"></asp:TextBox>
                                <small id="passHelp" class="form-text text-muted">Make sure nobody's behind you</small>
                            </div>
                            <div class="custom-control custom-checkbox my-4">
                                <input type="checkbox" class="custom-control-input" id="solemnly-swear">
                                <label class="custom-control-label" for="solemnly-swear">I solemnly swear that I am up to no good.</label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xl-12">
                    <asp:Button ID="btnSignup" runat="server" Text="Signup" CssClass="btn btn-outline-danger btn-shadow px-3 my-2 ml-0 ml-sm-1 text-left typewriter" OnClientClick="return signup();" OnClick="btnSignup_Click" />
                    <small id="registerHelp" class="mt-2 form-text text-muted">Already Registered? <a href="Login.aspx">Login here</a></small>
                    <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red" CssClass="form-text"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <!-- Optional JavaScript -->
    <script>

        function signup() {
            let username = document.getElementById("<%= txtNewUsername.ClientID %>").value.trim();
            let password = document.getElementById("<%= txtNewPassword.ClientID %>").value.trim();
            let errorId = "<%= lblErrorMessage.ClientID %>";
            if (!checkUserAndPassInput(username, password, errorId) || !checkSolemnlySwear()) {
                return false;
            }
            return true;
        }

        function checkSolemnlySwear() {
            let checkBox = document.getElementById("solemnly-swear");
            if (checkBox.checked) {
                return true;
            } else {
                document.getElementById("<%= lblErrorMessage.ClientID %>").innerText = "You must solemnly swear to continue.";
                return false;
            }
        }
    </script>
    <script src="<%= ResolveClientUrl("js/script.js") %>"></script>
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->

    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
        </form>
</body>

</html>
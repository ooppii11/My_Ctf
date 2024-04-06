<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Quests.aspx.cs" Inherits="WebApplicationSchoolProject.CTF_pages.Quests" %>

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
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.3/Chart.min.js"></script>
    <link rel="stylesheet" href="css/main.css">
    <!-- <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"> -->
</head>

<body>
    <form id="quests" runat="server">
    <div class="navbar-dark text-white">
        <div class="container">
            <nav class="navbar px-0 py-0 navbar-expand-lg navbar-dark">
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
                <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
                    <div class="navbar-nav">
                            <h3 class="bold"><span class="color_danger">MY</span><span class="color_white">CTF</span></h3>
                    </div>
                    <div class="navbar-nav ml-auto">
                        <a href="Instructions.aspx" class="p-3 text-decoration-none text-light bold">Instructions</a>
                        <a href="Quests.aspx" class="p-3 text-decoration-none text-white bold">Challenges</a>
                        <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="p-3 btn-transparent text-decoration-none text-cyber-red bold" OnClick="btnLogout_Click" />
                    </div>
                </div>
            </nav>

        </div>
    </div>

    <div class="jumbotron bg-transparent mb-0 pt-0 radius-0">
        <div class="container">
            <div class="row">
                <div class="col-xl-12  text-center">
                    <h1 class="display-1 bold color_white content__title">QUESTS<span class="vim-caret">&nbsp;</span></h1>
                    <p class="text-grey text-spacey hackerFont lead mb-5">
                        Its time to show the world what you can do!
                    </p>
                </div>
            </div>
            <div class="row hackerFont" id="challengeCards">
                <div class="col-md-12">
                    <h4>Challenges</h4>
                </div>
            </div>
                <div class="row hackerFont">

                    <div class="row hackerFont justify-content-center mt-5">
                        <div class="col-md-12">
                            Chart Difficulties:
                            <span style="color:#17b06b">Very Easy,</span>
                            <span style="color:#17b06b">Easy,</span>
                            <span style="color:#ffce56">Medium,</span>
                            <span style="color:#ef121b">Hard,</span>
                            <span style="color:#ef121b">Very Hard,</span>
                            <br><br>Challenge Types:
                            <span class="p-1" style="background-color:#ef121b94">Web</span>
                            <span class="p-1" style="background-color:#17b06b94">Reversing</span>
                            <span class="p-1" style="background-color:#f9751594">Steganography</span>
                            <span class="p-1" style="background-color:#36a2eb94">Pwning</span>
                            <span class="p-1" style="background-color:#9966FF94">Cryptography</span>
                            <span class="p-1" style="background-color:#ffce5694">Other</span>
                        </div>
                    </div>
                   </div>
                </div>
        </div>
        <div class="modal fade" id="hint" tabindex="-1" role="dialog" aria-labelledby="hint label" style="display: none;" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-body" id="hintContent">
                        HINT GOES HERE
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hiddenField" runat="server" />
        <script>

            

            var dataset = [
                [41, 42, 43, 44, 45, 0], // keep the zero here
                [10, 9, 8, 7, 6, 0],
                [21, 16, 23, 1, 15, 0],
                [71, 12, 13, 17, 25, 0],
                [31, 5, 23, 24, 10, 0],
                [11, 2, 13, 41, 15, 0],
                [31, 5, 23, 24, 10, 0],
                [11, 2, 13, 41, 15, 0],
            ]

            function getBarChartData(i) {
                return barChartData = {
                    labels: ['Easy1', 'Easy2', 'Medium3', 'Hard4', 'Hard5'],
                    datasets: [{
                        label: 'Dataset 1',
                        backgroundColor: [
                            '#17b06b', '#17b06b', '#ffce56', '#ef121b', '#ef121b'
                        ],
                        borderColor: [
                            '#17b06b', '#17b06b', '#ffce56', '#ef121b', '#ef121b'
                        ],
                        borderWidth: 1,
                        data: dataset[i - 1]
                    }]

                };
            }

            
            function generateChallengeCards() {

                <% 
                 List<WebApplicationSchoolProject.CTF_utilities.Challenge> challenges = GetChallengesFromDatabase();
                %>
                const challenges = [
                <% foreach (var challenge in challenges) { %>
                   {
                        title: "<%= challenge.Title.Replace(" ", "_") %>",
                        category: "<%= challenge.Category %>",
                        status: "<%= challenge.Status %>",
                        points: <%= challenge.Points %>,
                        solvers: <%= challenge.Solvers %>,
                        instructions: "<%= challenge.Instructions %>",
                        description: "<%= challenge.Description %>",
                        hint: "<%= challenge.Hint %>",
                        file_path_hash: "<%= challenge.FilePath != "" ? WebApplicationSchoolProject.Cryptography_And_Security.Crypto.Hash(challenge.FilePath) : ""%>"
                },
                <% } %>
                ];
                const challengeCardsContainer = document.getElementById("challengeCards");
                challenges.forEach((challenge) => {
                    const cardHtml = `
                        <div class="col-md-4 mb-3">
                            <div class="card category_${challenge.category.toLowerCase()}">
                                <div id="card_problem_title_${challenge.title}" class="card-header ${challenge.status === 'solved' ? 'solved' : ''}" data-target="#problem_title_${challenge.title}" data-toggle="collapse" aria-expanded="true" aria-controls="problem_title_${challenge.title}">
                                    ${challenge.title.replace('_', ' ')} <span class="badge">${challenge.status === 'solved' ? 'solved' : ''}</span> <span class="badge">${challenge.points} points</span>
                                </div>
                                <div id="problem_title_${challenge.title}" class="collapse card-body">
                                    <blockquote class="card-blockquote">
                                        <div style="display: flex;">
                                            <h6 class="solvers">Solvers: <span class="solver_num">${challenge.solvers}</span><br><span class="color_danger">Difficulty: <span style="color:${challenge.points <= 250 ? "#17b06b" : challenge.points <= 500 ? "#ffce56" : "#ef121b"}"">${challenge.points <= 125 ? "Very Easy" : challenge.points <= 250 ? "Easy" : challenge.points <= 500 ? "Medium" : challenge.points <= 750 ? "Hard" : "Very Hard"}</span></span></h6>
                                            <div class="pl-2"><canvas style="width:80px;height:20px" id="problem_title_${challenge.title}_chart"></canvas></div>
                                        </div>
                                        <p>
                                            ${challenge.instructions}
                                        </p>
                                        <h4>The Challenge:</h4>
                                        <p>
                                            ${challenge.description}
                                        </p>
                                        ${challenge.file_path_hash != "" ? `<a href="Quests.aspx?file=${challenge.file_path_hash}" class="btn btn-outline-secondary btn-shadow" download><span class="fa fa-download mr-2"></span>Download</a>` : ""}
                                        <a href="#hint" class="btn btn-outline-secondary btn-shadow btn-hint" data-toggle="modal" data-target="#hint" data-hint="${challenge.hint}">
                                            <span class="far fa-lightbulb mr-2"></span>Hint
                                        </a>
                                        <div class="input-group mt-3">
                                            <input id="flag_${challenge.title}" name="flag_${challenge.title}" type="text" class="form-control" placeholder="Enter Flag" aria-label="Enter Flag" aria-describedby="basic-addon2">
                                            <div class="input-group-append">
                                                <asp:Button runat="server" CssClass="btn btn-outline-secondary" Text="Go!" OnClientClick="return checkFlag('${challenge.title}')" OnClick="btnSubmitFlag_Click" />
                                                <br>
                                            </div>
                                            <br>
                                            <div>
                                                <b><label id="feedback_${challenge.title}" style="color: red;"></label></b>
                                            </div>
                                        </div>
                                    </blockquote>
                                </div>
                            </div>
                        </div>`;
                    challengeCardsContainer.innerHTML += cardHtml;
                    
                    
                });
                const hintButtons = document.querySelectorAll('.btn-hint');
                hintButtons.forEach(function (button) {
                    button.addEventListener('click', function (event) {
                        event.preventDefault();
                        const hint = this.dataset.hint;
                        document.getElementById('hintContent').innerText = hint;
                    });
                });
            }
            
            function checkFlag(challengeTitle) {
                let flag = document.getElementById(`flag_${challengeTitle}`).value;
                let correctFlagRegex = /My_ctf\{[^}]+\}/i;
                if (!correctFlagRegex.test(flag)) {
                    let label = document.getElementById(`feedback_${challengeTitle}`);
                    label.innerText = "Please enter a valid flag. My_ctf{some_text}";
                    label.style.color = 'red';
                    return false;
                }

                document.getElementById('<%= hiddenField.ClientID %>').value = challengeTitle;
                return true;
            }

            function updateLabelAndCardStatus(text, color, challengeTitle) {
                let label = document.getElementById(`feedback_${challengeTitle}`);
                label.innerText = text;
                label.style.color = color;

                let card = document.getElementById(`card_problem_title_${challengeTitle}`);
                card.classList.remove('collapse');
                card.ariaExpanded = "true";



                card = document.getElementById(`problem_title_${challengeTitle}`);
                card.classList.add('show');
            }

            function createCharts()
            {
                var numcharts = 1;
                for (let i = 1; i <= numcharts; i++) {
                    var ctx = document.getElementById('problem_id_' + i + '_chart').getContext('2d');
                    window.myBar = new Chart(ctx, {
                        type: 'bar',
                        data: getBarChartData(i),
                        options: {
                            tooltips: {
                                enabled: false,
                            },
                            responsive: false,
                            legend: {
                                display: false,
                            },
                            scales: {
                                yAxes: [{
                                    display: false
                                }],
                                xAxes: [{
                                    display: false
                                }]
                            }
                        }
                    });
                    myBar.canvas.parentNode.style.width = '80px';
                    myBar.canvas.parentNode.style.height = '20px';
                }
            }

            generateChallengeCards();
        </script>

        <!-- Optional JavaScript -->
        <!-- jQuery first, then Popper.js, then Bootstrap JS -->

        <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
</form>
</body>

</html>
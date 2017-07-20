$(document).ready(function ()
{


    //-------------------------------------------------------BAR-------------------------------------------------------
    if ($("#AID").val() != null)
    {
            //animal specific stats
            //weight graph
            $("#Bar-example").html("");
            $("#BAR").html("Bull/Cow Weight");
            Morris.Bar
                ({
                    element: 'Bar-example',
                    //data: Graph(),
                    data: $.parseJSON(ShowGrBar()),
                    xkey: 'label',
                    ykeys: ['valueWeight'],
                    labels: ['value'],
                    hideHover: 'auto',
                    xLabelAngle: 35,
                    barColors: function (row, series, type) {
                        if (series.key == 'valueWeight') {
                            if (row.y < 700) {
                                return "red";
                            }
                            else {
                                return "blue";
                            }
                        }
                        else {
                            return "green";
                        }
                    },
                    resize: true
                });
        }
    else
    {
            //for general stats
            $("#Bar-example").html("");
            $("#BAR").html("Bull Weights");
            Morris.Bar
                ({
                    element: 'Bar-example',
                    //data: Graph(),
                    data: $.parseJSON(ShowGreneral()),
                    xkey: 'label',
                    ykeys: ['valueWeight'],
                    labels: ['Current Weight'],
                    hideHover: 'auto',
                    xLabelAngle: 35,
                    barColors: function (row, series, type) {
                        if (series.key == 'valueWeight') {
                            if (row.y < 700) {
                                return "orange";
                            }
                            else {
                                return "blue";
                            }
                        }
                        else {
                            return "green";
                        }
                    },
                    resize: true
                });

        }
        //////////////////////////////////////////////////////////////

        var r = ShowGr()
        //alert(r);


        /////////////////////////DOnut Donut Donut/////////////////////////////////////
        var color_array = ['Blue', 'Green', 'Red', 'Orange', '#7E6F6A', '#36AFB2', '#9c6db2', '#d24a67', '#89a958', '#00739a', '#BDBDBD'];


        if ($("#AID").val() != null) {
            $("#Donut").html("Offspring For:" + $("#AID").val())
            //existing animals 
            $("#morris-donut-chart").html("");
            $("#Donutlegend").html("");
            // Donut Chart
            var browsersChart = Morris.Donut({
                element: 'morris-donut-chart',
                data: $.parseJSON(ShowGr()),
                colors: color_array,
                formatter: function (y) { return y + "" },
                resize: false
            });

            browsersChart.options.data.forEach(function (label, i) {
                var legendItem = $('<span></span>').text(label['label']).prepend('<i>&nbsp;</i>');
                legendItem.find('i').css('backgroundColor', browsersChart.options.colors[i]);

                $('#Donutlegend').append(legendItem);
            })
        }
        else
        {

            $("#Donut").html("Current Animal Count:");
            //existing animals 
            $("#morris-donut-chart").html("");
            $("#Donutlegend").html("");
            // Donut Chart
            var browsersChart = Morris.Donut({
                element: 'morris-donut-chart',
                data: $.parseJSON(ShowGr()),
                colors: ['green', '#FF0000'],
                formatter: function (y) { return y + "" },
                resize: false
            });

            //Legend
            browsersChart.options.data.forEach(function (label, i) {
                var legendItem = $('<span></span>').text(label['label'] + " : " + label['value'] + " ").prepend('<span>&nbsp;</span>');
                legendItem.find('span')
                  .css('backgroundColor', browsersChart.options.colors[i])
                  .css('width', '20px')
                  .css('display', 'inline-block')
                  .css('margin', '5px');
                $('#Donutlegend').append(legendItem)
            });


        }
        //////////////////////////////////////////////////////////////

        ///////////////////////////Line Line Line ///////////////////////////////////

        //offspring performance by year on season
        $("#morris-line-chart").html("");
        Morris.Line
           ({
               element: 'morris-line-chart',
               //data: Graph(),
               data: $.parseJSON(ShowLineGr()),
               xkey: 'valuex',
               ykeys: ['valuey'],
               xLabels: 'day',
               labels: ['Breed'],
               hideHover: 'auto',
               behaveLikeLine: true,
               smooth: true,
               hover: true,
               xLabelAngle: 35,
               resize: true
           });

        //////////////////////////////////////////////////////////////


        $("#morris-area-chart").html("Hello World 3");
        Morris.Area
      ({
          element: 'morris-area-chart',
          //data: Graph(),
          data: $.parseJSON(ShowGr()),
          xkey: 'value',
          ykeys: ['value'],
          labels: ['label'],
          pointSize: 2,
          colors: ['#7BB661', '#72A0C1'],
          formatter: function (y) { return y + " liters" },
          hideHover: 'auto',
          resize: false
      });


  

});


//show general statistics
function ShowGr() {


    console.log($("#AID").val());
    if ($("#AID").val() == null) {
        //alert("No Loaded Animal");
        //return;
    }
    else {
        console.log($("#AID").val());
    }

    //function BarGraph() {
    var barls = "";
    if ($("#AID").val() != null) {

        $.ajax({
            type: 'GET',
            url: '@Url.Action("GetChartData", "Admin")',
            dataType: 'json',
            async: false,
            contentType: "application/json; charset=utf-8",
            data:
            {
                userID: 100,
                selectedValue: $("#AID").val()

            },
            success: function (data) {



                //  console.log(data);
                barls = JSON.stringify(data)
                // alert("We have Arrived:" + barls);

                var sumT = 0;
                for (var key in data) {
                    if (data.hasOwnProperty(key)) {
                        // here you have access to
                        var valueX = data[key].value;
                        sumT = sumT + valueX
                    }
                }

                $("#Donut").html("Current Animal Offspring Count :: " + sumT)
            },
            error: function (xhr, status, error) {
                alert(error);
            }
        });
    }
    else {
        $.ajax({
            type: 'GET',
            url: '@Url.Action("GetChartData", "Admin")',
            dataType: 'json',
            async: false,
            contentType: "application/json; charset=utf-8",
            data: {userID:100, selectedValue: $("#AID").val() },
            success: function (data) {



                //  console.log(data);
                barls = JSON.stringify(data)
                // alert("We have Arrived:" + barls);

                var sumT = 0;
                for (var key in data) {
                    if (data.hasOwnProperty(key)) {
                        // here you have access to
                        var valueX = data[key].value;
                        sumT = sumT + valueX
                    }
                }

                $("#Donut").html("Current Animal Count : " + sumT)
            },
            error: function (xhr, status, error) {
                alert(error);
            }
        });


    }
    //  alert(barls);
    return barls;
}

//show bar graph statistics
function ShowGrBar() {
    console.log($("#AID").val());
    if ($("#AID").val() == null) {
        //alert("No Loaded Animal");
        //return;
    }
    else {
        console.log($("#AID").val());
    }

    //function BarGraph() {
    var barls = "";
    if ($("#AID").val() != null) {
        $.ajax({
            type: 'GET',
            url: '@Url.Action("GetChartDataB", "Admin")',
            dataType: 'json',
            async: false,
            contentType: "application/json; charset=utf-8",
            data: { userID:100, selectedValue: $("#AID").val() },
            success: function (data) {
                barls = JSON.stringify(data)
                var sumT = 0;
                for (var key in data) {
                    if (data.hasOwnProperty(key)) {
                        // here you have access to
                        var valueX = data[key].value;
                        sumT = sumT + valueX
                    }
                }

                // alert("Current Bar 1 || " + barls);
                $("#Donut").html("Current Bar 1  Animal Count : " + sumT)
            },
            error: function (xhr, status, error) {
                alert(error);
            }
        });
    }
    else {
        $.ajax({
            type: 'GET',
            url: '@Url.Action("GetChartData", "Admin")',
            dataType: 'json',
            async: false,
            contentType: "application/json; charset=utf-8",
            data: { userID: 100, selectedValue: $("#AID").val() },
            success: function (data) {
                barls = JSON.stringify(data)
                var sumT = 0;
                for (var key in data) {
                    if (data.hasOwnProperty(key)) {
                        // here you have access to
                        var valueX = data[key].value;
                        sumT = sumT + valueX
                    }
                }
                // alert("Current Bar 2 || " + barls);
                $("#Donut").html("Current Bar 2 Animal Count : " + sumT)
            },
            error: function (xhr, status, error) {
                alert(error);
            }
        });


    } return barls;
}


//load when there is an ID involved
//shows genral statistics
function ShowGreneral() {
    if ($("#AID").val() == null) {
        //alert("No Loaded Animal");
        //return;
    }
    else {
        console.log($("#AID").val());
    }
    var barls = "";
    $.ajax({
        type: 'GET',
        url: '@Url.Action("GetChartDataG", "Admin")',
        dataType: 'json',
        async: false,
        contentType: "application/json; charset=utf-8",
        data: {userID:100, selectedValue: "POPPY" },
        success: function (data) {
            barls = JSON.stringify(data)
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
    // alert("General Stats:"+barls);
    return barls;
}


function ShowLineGr() {
    console.log($("#AID").val());
    if ($("#AID").val() == null) {
        //alert("No Line Loaded Animal");
        //return;
    }
    else {
        console.log("Line::" + $("#AID").val());
    }

    var barls = "";
    $.ajax({
        type: 'GET',
        url: '@Url.Action("GetXYChartData", "Admin")',
        dataType: 'json',
        async: false,
        contentType: "application/json; charset=utf-8",
        data: {userID:"100", selectedValue: $("#AID").val() },
        success: function (data) {
            barls = JSON.stringify(data)
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
    //  alert(barls);
    return barls;
}


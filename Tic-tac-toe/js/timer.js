var mm = 0;
var ss = 0;
var GameInProgress = 0;

var StartGameTimer = function () 
{
	GameInProgress = 1;
    SetTimer("00:00");
	GameTimer = setInterval(RunTimer, 1000);
};

var StopTimer = function ()
{
    GameInProgress = 0;
    clearInterval(GameTimer);
    return($("#GameTimer").text());
}

var SetTimer = function (StartTime)
{
	$("#GameTimer").text(StartTime);
	mm = 0;
	ss = 0;
}

var RunTimer = function ()
{
    if (ss < 59)
    {
        ss++;
    }
    else
    {
        ss = 0;
        mm++;
    }
    var Time = "";
    if (mm < 10)
    {
        Time = "0" + mm.toString();
    }
    else
    {
        Time = mm.toString();
    }

    Time = Time + ":";

    if (ss < 10)
    {
        Time = Time + "0" + ss.toString();
    }
    else
    {
        Time = Time + ss.toString();
    }
    $("#GameTimer").text(Time);
}

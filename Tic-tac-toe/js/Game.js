x = new Image(64,64);
x.src = "../res/x1.gif";
o = new Image(64,64);
o.src = "../res/o1.gif";
e = new Image(64,64);
e.src = "../res/z1.gif";

var size = 3; // Размер игрового поля

// Значения клеток игрового поля 0-свободно,1-крестик,2-нолик
var GameCells = new Array(size);
for (i=0; i<size; i++)
{
    GameCells[i] = new Array(size);
    for (j=0; j<size; j++)
        GameCells[i][j] = 0;
}
var Game;

var GameIsStarted = 0;

var Score;

var TableRecordsCount = 0;

var ScoreMessage;

var CurrentPlayerMessage;

var ResultScore = new Array(2);

var PlayersTurns = new Array(2);
for (i=0; i<2; i++)
    PlayersTurns[i] = 0;

var TurnOfPlayer = 1;

var NotFirstGame = 0;

var ElementsClick1 = function()
{
    x.src = "../res/x1.gif";
    o.src = "../res/o1.gif";
    e.src = "../res/z1.gif";
    if(GameIsStarted == 1)
    {
        for(i = 0; i < size; i++)
        {
            for(j = 0; j < size; j++)
            {
                var div = document.getElementById("GameMap");
                div.setAttribute("align","center");
                var td = document.getElementsByTagName('td');
                var table = document.getElementById("HighscoreTable") //получаем элемент таблицы
                var sum = (table.rows.length)*3-2; //количество строк и столбцов
                var t = td[j+(i*size)+sum];
                var img = t.getElementsByTagName("img");
                img = img[0];
                if(GameCells[i][j] == 1)
                    img.setAttribute("SRC",x.src);
                else if(GameCells[i][j] == 2)
                    img.setAttribute("SRC",o.src);
                else
                    img.setAttribute("SRC",e.src);
                div = document.getElementById("CurrentPlayer");
                var p = div.children;
                img = p[0].children;
                img = img[0];
                if(TurnOfPlayer == 1)
                    img.setAttribute("SRC",x.src);
                else
                    img.setAttribute("SRC",o.src);
            }
        }
    }
    $("#Elements_1").css(
    {
      "background-color": "#007800",
    });
    $("#Elements_2").css(
    {
      "background-color": "#000000",
    });
}

var ElementsClick2 = function()
{
    x.src = "../res/x2.gif";
    o.src = "../res/o2.gif";
    e.src = "../res/z1.gif";
    if(GameIsStarted == 1)
    {
        for(i = 0; i < size; i++)
        {
            for(j = 0; j < size; j++)
            {
                var div = document.getElementById("GameMap");
                div.setAttribute("align","center");
                var td = document.getElementsByTagName('td');
                var table = document.getElementById("HighscoreTable") //получаем элемент таблицы
                var sum = (table.rows.length)*3-2; //количество строк и столбцов
                var t = td[j+(i*size)+sum];
                var img = t.getElementsByTagName("img");
                img = img[0];
                if(GameCells[i][j] == 1)
                    img.setAttribute("SRC",x.src);
                else if(GameCells[i][j] == 2)
                    img.setAttribute("SRC",o.src);
                else
                    img.setAttribute("SRC",e.src);
                div = document.getElementById("CurrentPlayer");
                var p = div.children;
                img = p[0].children;
                img = img[0];
                if(TurnOfPlayer == 1)
                    img.setAttribute("SRC",x.src);
                else
                    img.setAttribute("SRC",o.src);
            }
        }
    }
    $("#Elements_2").css(
    {
      "background-color": "#0000aa",
    });
    $("#Elements_1").css(
    {
      "background-color": "#000000",
    });
}

var Level1Click = function()
{
    if(GameIsStarted != 0)
    {
        alert("Игра уже начата!");
        return;
    }
    else
    {
        // Удаляем старые значения
        //for (i=0; i<size*size; i++) delete.cell[i];
        size = 3;
        cell = new Array(size*size); // Значения клеток игрового поля 0-свободно,1-крестик,2-нолик
        for (i=0; i<size*size; i++) cell[i] = 0;
        $("#Level_1").css(
        {
          "background-color": "#780000",
        });
        $("#Level_2").css(
        {
          "background-color": "#000000",
        });
        $("#Level_3").css(
        {
          "background-color": "#000000",
        });
    }
}

var Level2Click = function()
{
    if(GameIsStarted != 0)
    {
        alert("Игра уже начата!");
        return;
    }
    else
    {
        // Удаляем старые значения
        //for (i=0; i<size*size; i++) delete.cell[i];
        size = 4;
        cell = new Array(size*size); // Значения клеток игрового поля 0-свободно,1-крестик,2-нолик
        for (i=0; i<size*size; i++) cell[i] = 0;
        $("#Level_1").css(
        {
          "background-color": "#000000",
        });
        $("#Level_2").css(
        {
          "background-color": "#007800",
        });
        $("#Level_3").css(
        {
          "background-color": "#000000",
        });
    }
}

var Level3Click = function()
{
    if(GameIsStarted != 0)
    {
        alert("Игра уже начата!");
        return;
    }
    else
    {
        // Удаляем старые значения
        size = 5;
        cell = new Array(size*size); // Значения клеток игрового поля 0-свободно,1-крестик,2-нолик
        for (i=0; i<size*size; i++) cell[i] = 0;
        $("#Level_1").css(
        {
          "background-color": "#000000",
        });
        $("#Level_2").css(
        {
          "background-color": "#000000",
        });
        $("#Level_3").css(
        {
          "background-color": "#000078",
        });
    }
}

var StartGame = function()
{
    if(GameIsStarted != 0)
    {
        alert("Игра уже начата!");
        return;
    }
    else
    {
        if(NotFirstGame == 1)
        {
            Game.parentNode.removeChild(Game);
            ScoreMessage.parentNode.removeChild(ScoreMessage);
            CurrentPlayerMessage.parentNode.removeChild(CurrentPlayerMessage);
        }
        else
        {
            var div = document.getElementById("GameTimer");
            $(div).css(
            {
              "visibility": "visible",
            });
            div = document.getElementById("TimeText");
            $(div).css(
            {
              "visibility": "visible",
            });
        }
        Score = 6000;
        PlayersTurns[0] = 0;
        PlayersTurns[1] = 0;
        GameIsStarted = 1;
        TurnOfPlayer = 1;
        GameCells = new Array(size);
        for (i=0; i<size; i++)
        {
            GameCells[i] = new Array(size);
            for (j=0; j<size; j++)
                GameCells[i][j] = 0;
        }
        var div = document.getElementById("CurrentPlayer");
        div.setAttribute("align","center");
        var p = document.createElement('p');
        CurrentPlayerMessage = p;
        var img = document.createElement('img'); 
        p.innerHTML = "Ход игрока " + TurnOfPlayer + "   ";
        img.setAttribute("SRC",x.src);
        p.appendChild(img);
        div.appendChild(p);
        CurrentPlayerMessage = p;
        for(i = 0; i < 2; i++)
        {
            if(PlayersTurns[i] == 0)
                ResultScore[i] = Score;
            else
                ResultScore[i] = Score/PlayersTurns[i];
        }
        div = document.getElementById("CurrentScore");
        div.setAttribute("align","center");
        p = document.createElement('p');
        p.setAttribute("id","CurrentScore");
        ScoreMessage = p;
        var br = document.createElement('br');
        p.innerHTML = "Игрок " + 1 + ". Счет: " + ResultScore[0];
        p.appendChild(br);
        p.innerHTML = p.innerHTML + "Игрок " + 2 + ". Счет: " + ResultScore[1];
        div.appendChild(p);
        // Отрисовка игрового поля
        DrawGameMap();
        StartGameTimer();
    }
}

var DrawGameMap = function()
{
    var i = 0;
    var j = 0;
    var div = document.createElement('div');
    div.setAttribute("id","GameMap");
    div.setAttribute("align","center");
    Game = div;
    document.body.appendChild(div);
    var table = document.createElement('table');
    for (i = 0; i < size; i++)
    {
        var row  = table.insertRow(0);
        //row.setAttribute("class","row"+i);
        for (j = 0; j < size; j++)
        {
            var cell = row.insertCell(0);
            cell.setAttribute("align","center");
            cell.setAttribute("WIDTH","64");
            cell.setAttribute("HEIGHT","64");
            cell.setAttribute("onClick","Place("+((size-1)-i)+","+((size-1)-j)+")");
            var img = document.createElement('img');
            img.setAttribute("SRC",e.src);
            img.setAttribute("style","border:3px solid #006400");
            cell.appendChild(img);
        }
    }
    document.body.appendChild(div);
    div.appendChild(table);
}

var Place = function(Row,Cell)
{
    if(GameCells[Row][Cell] == 0 && GameIsStarted != 0) // Если ячейка пустая
    {
        // Берем необходимую таблицу
        var div = document.getElementById("GameMap");
        var td = document.getElementsByTagName('td');
        var count = 0;
        var table = document.getElementById("HighscoreTable") //получаем элемент таблицы
        var sum = (table.rows.length)*3-2; //количество строк и столбцов
        
        var t = td[Cell+(Row*size)+sum];
        var img = t.getElementsByTagName("img");
        img = img[0];
        if(TurnOfPlayer == 1)
            img.setAttribute("SRC",x.src);
        else
            img.setAttribute("SRC",o.src);
        GameCells[Row][Cell] = TurnOfPlayer;
        // После заполнения новой ячейки нужно проверить условия победы
        var ret = CheckWin(Row,Cell);
        if(ret) // В случае победы оповещаем пользователя
        {
            if(ret == 3)
                alert("Ничья!"); 
            else
                alert("Игрок "+TurnOfPlayer+" победил со счетом " + ResultScore[TurnOfPlayer-1] + "!");
            var GameTime = StopTimer();
            SetCookie(GameTime);
            GameIsStarted = 0;
            TurnOfPlayer = 1;
            NotFirstGame = 1;
        }
        else
        {
            CurrentPlayerMessage.parentNode.removeChild(CurrentPlayerMessage);
            ScoreMessage.parentNode.removeChild(ScoreMessage);
            PlayersTurns[TurnOfPlayer-1]++;
            for(i = 0; i < 2; i++)
            {
                if(PlayersTurns[i] == 0)
                    ResultScore[i] = Score;
                else
                    ResultScore[i] = Score/PlayersTurns[i];
            }
            div = document.getElementById("CurrentScore");
            div.setAttribute("align","center");
            var p = document.createElement('p');
            p.setAttribute("id","CurrentScore");
            ScoreMessage = p;
            var br = document.createElement('br');
            p.innerHTML = "Игрок " + 1 + ". Счет: " + ResultScore[0];
            p.appendChild(br);
            p.innerHTML = p.innerHTML + "Игрок " + 2 + ". Счет: " + ResultScore[1];
            div.appendChild(p);
            if(TurnOfPlayer == 1)
                TurnOfPlayer = 2;
            else
                TurnOfPlayer = 1;
            
            div = document.getElementById("CurrentPlayer")
            div.setAttribute("align","center");
            var p = document.createElement('p');
            var img = document.createElement('img'); 
            p.innerHTML = "Ход игрока " + TurnOfPlayer + "   ";
            if(TurnOfPlayer == 1)
                img.setAttribute("SRC",x.src);
            else
                img.setAttribute("SRC",o.src);
            p.appendChild(img);
            div.appendChild(p);
            CurrentPlayerMessage = p;
        }
    }
}

var CheckWin = function(Row,Cell)
{
    // Проверяем все возможные варианты выйгрыша относительно установленной точки
    // По вертикали
    if(VerticalCheck(Row,Cell))
        return true;
    // По горизонтали
    else if(HorizontalCheck(Row,Cell))
        return true;
    // По диагонали
    else if(DiagonalCheck(Row,Cell))
        return true;
    else if(Draw())
        return 3;
}

var VerticalCheck = function(Row,Cell)
{
    // По вертикали
    for(CurRow = Row; CurRow >= 0; CurRow--) // Идем вверх
    {
        if(CurRow == Row) continue;
        if(GameCells[CurRow][Cell] != TurnOfPlayer)
            return false;
    }
    for(CurRow = Row; CurRow < size; CurRow++) // Идем вниз
    {
        if(CurRow == Row) continue;
        if(GameCells[CurRow][Cell] != TurnOfPlayer)
            return false;
    }
    return true;
}

var HorizontalCheck = function(Row,Cell)
{
    // По горизонтали
    VerticalCheck();
    for(CurСell = Cell; CurСell >= 0; CurСell--) // Идем влево
    {
        if(CurСell == Cell) continue;
        if(GameCells[Row][CurСell] != TurnOfPlayer)
            return false;
    }
    for(CurСell = Cell; CurСell < size; CurСell++) // Идем вниз
    {
        if(CurСell == Cell) continue;
        if(GameCells[Row][CurСell] != TurnOfPlayer)
            return false;
    }
    return true;
}

var DiagonalCheck = function(Row,Cell)
{
    // По диангонали
    // Проверяем, является ли объект элементом главной или побочной диагонали матрицы
    var MainDiagonal = false;
    var SecondaryDiagonal = false;
    if(Row == Cell)
        MainDiagonal = true;
    if((Row + Cell == size-1) && (Row == (size-1)-Cell) && (Cell == (size-1)-Row))
        SecondaryDiagonal = true;
    // Продолжаем проверку только если объект является элементом главной или побочной диагонали матрицы
    if(MainDiagonal || SecondaryDiagonal)
    {
        if(MainDiagonal) // Проверяем главную диагональ
        {
            var CurCell = Cell;
            var CurRow = Row;
            // Идем вверх по главной диагонали
            while((CurCell < size) && (CurRow < size))
            {
                if(GameCells[CurRow][CurCell] != TurnOfPlayer)
                    return false;
                else
                {
                    CurCell++;
                    CurRow++;    
                }
            }
            CurCell = Cell;
            CurRow = Row;
            // Идем вниз по главной диагонали
            while((CurCell >= 0) && (CurRow >= 0))
            {
                if(GameCells[CurRow][CurCell] != TurnOfPlayer)
                    return false;
                else
                {
                    CurCell--;
                    CurRow--;    
                }
            }
        }
        if(SecondaryDiagonal) // Проверяем побочную диагональ
        {
            var CurCell = Cell;
            var CurRow = Row;
            // Идем вверх по главной диагонали
            while((CurCell < size) && (CurRow >= 0))
            {
                if(GameCells[CurRow][CurCell] != TurnOfPlayer)
                    return false;
                else
                {
                    CurCell++;
                    CurRow--;    
                }
            }
            CurCell = Cell;
            CurRow = Row;
            // Идем вниз по главной диагонали
            while((CurCell >= 0) && (CurRow < size))
            {
                if(GameCells[CurRow][CurCell] != TurnOfPlayer)
                    return false;
                else
                {
                    CurCell--;
                    CurRow++;    
                }
            }    
        }
    }
    else
        return false;
    return true;
}

var Draw = function()
{
    for(i = 0; i < size; i++)
    {
        for(j = 0; j < size; j++)
            if(GameCells[i][j] == 0)
                return false;
    }
    return true;
}

var SetCookie = function(Time) 
{
	var HaveCookies = $.cookie('Result');
    var TotalStr = "";
    var Str = "Игрок " + TurnOfPlayer + " " + ResultScore[TurnOfPlayer-1] + " " + Time + " ";
    if(HaveCookies != null)
        TotalStr += HaveCookies + Str;
    else
        TotalStr += Str;
    Str = Str.split(' ');
    var table = document.getElementById("HighscoreTable");
    var tr = document.createElement('tr');
    tr.setAttribute("id","Record"+TableRecordsCount);
    table.appendChild(tr);
    var td = document.createElement('td');
    td.setAttribute("class","HighscoreTableElement");
    td.innerHTML = Str[0] + Str[1];
    tr.appendChild(td);
    td = document.createElement('td');
    td.setAttribute("class","HighscoreTableElement");
    td.innerHTML = Str[2];
    tr.appendChild(td);
    td = document.createElement('td');
    td.setAttribute("class","HighscoreTableElement");
    td.innerHTML = Str[3];
    tr.appendChild(td);
    TableRecordsCount++;
	$.cookie('Result', TotalStr);
}

var LoadCookie = function()
{
	var Str = $.cookie('Result');
	if (Str != null) 
    {
		Str = Str.split(' ');
        for(i = 0; i < (Str.length/4)-1; i++) // По 4 элемента для каждого деления
        {
            var table = document.getElementById("HighscoreTable");
            var tr = document.createElement('tr');
            tr.setAttribute("id","Record"+i);
            table.appendChild(tr);
            var td = document.createElement('td');
            td.setAttribute("class","HighscoreTableElement");
            td.innerHTML = Str[0+(4*i)] + Str[1+(4*i)];
            tr.appendChild(td);
            td = document.createElement('td');
            td.setAttribute("class","HighscoreTableElement");
            td.innerHTML = Str[2+(4*i)];
            tr.appendChild(td);
            td = document.createElement('td');
            td.setAttribute("class","HighscoreTableElement");
            td.innerHTML = Str[3+(4*i)];
            tr.appendChild(td);
            TableRecordsCount++;
        }
	}
}

var DeleteCookies = function()
{
    for(i = 0; i < TableRecordsCount; i++)
    {
        var tr = document.getElementById("Record"+i);
        tr.parentNode.removeChild(tr);
    }
    TableRecordsCount = 0;
    $.cookie('Result',null); // Очищаем cookie
}

var main = function()
{
    LoadCookie();
    
    $('#Elements_1').click(ElementsClick1);
    $('#Elements_2').click(ElementsClick2);
    $('#Level_1').click(Level1Click);
    $('#Level_2').click(Level2Click);
    $('#Level_3').click(Level3Click);
    $('#NewGame').click(StartGame);
    $('#DeleteCookies').click(DeleteCookies);
    
    $("#Level_1").css(
    {
      "background-color": "#780000",
    });
    $("#Elements_1").css(
    {
      "background-color": "#007800",
    });
}

$(document).ready(main);
var board = new Array();
var score = 0;
var hasConficted = new Array();
var row = 4, clo = 4;

$(document).ready(function() {
    newgame();
});

function newgame() {

    $(function () {
        $("#init_page").hide();
        $("#alert_message_success").hide();
        $("#alert_message_failure").hide();        
    });

    // 初始化棋盘格
    init();
    // 随机两个格子生成数字
    generateOneNumber();
    generateOneNumber();
}

function init() {
    for (var i = 0; i < row; i++) {
        board[i] = new Array();
        hasConficted[i] = new Array();
        for (var j = 0; j < clo; j++) {
            board[i][j] = 0;//初值设置为0
            hasConficted[i][j] = false;
            var gridCell = $("#grid-cell-" + i + "-" + j);
            gridCell.css('top', getPosTop(i, j));
            gridCell.css('left', getPosLeft(i, j));
        }

    }
    updateBoardView();
    score = 0;
}

function updateBoardView() {
    $(".number-cell").remove();
    for (var i = 0; i < row; i++)
        for (var j = 0; j < clo; j++) {
            $("#grid-container").append('<div class="number-cell" id="number-cell-' + i + '-' + j + '"></div>')
            var theNumberCell = $('#number-cell-' + i + '-' + j);
            if (board[i][j] == 0) {
                theNumberCell.css('width', '0px');
                theNumberCell.css('height', '0px');
                theNumberCell.css('top', getPosTop(i, j) + 50);
                theNumberCell.css('left', getPosLeft(i, j) + 50);
            } else {
                theNumberCell.css('width', '100px');
                theNumberCell.css('height', '100px');
                theNumberCell.css('top', getPosTop(i, j));
                theNumberCell.css('left', getPosLeft(i, j));
                theNumberCell.css('background-color', getNumberBackgroundColor(board[i][j]));
                theNumberCell.css('color', getNumberColor(board[i][j]));
                theNumberCell.css('font-size', getNumberFont(board[i][j]));
                theNumberCell.text(board[i][j]);
            }

            hasConficted[i][j] = false;
        }
}


function generateOneNumber() {
    if (nospace(board))
        return false;
    // 随机一个位
    var randx = getRand44Num();
    var randy = getRand44Num();

    var times = 0;
    while (times < 60) {//随机放置次数设置上限为60次
        if (board[randx][randy] == 0)
            break;

        var randx = getRand44Num();
        var randy = getRand44Num();
        times++;
    }

    if (times == 60) {
        for (var i = 0; i < row; i++) {
            for (var j = 0; j < clo; j++) {
                if (board[i][j] == 0) {
                    randx = i;
                    randy = j;
                }

            }
        }
    }

    // 随机一个数字
    var randNumber = getRand24Num();
    board[randx][randy] = randNumber;
    showNumberWithAnimation(randx, randy, randNumber);
    return true;
}

$(document).keydown(function (event) {
    if (isgameover()) {
        AutoSubmit();
        return false;
    }
    else if (parseFloat($("#score").text()) >= parseFloat($("#score_submit").val())) {
        return false;
    }
    else {
        switch (event.keyCode) {
            case 65:// A
            case 37:// left
                if (moveLeft(board)) {
                    generateOneNumber();
                    isgameover();
                }
                break;
            case 87:// W
            case 38:// up
                if (moveUp(board)) {
                    generateOneNumber();
                    isgameover();
                }
                break;
            case 68:// D
            case 39:// right
                if (moveRight(board)) {
                    generateOneNumber();
                    isgameover();
                }
                break;
            case 83:// S
            case 40:// down
                if (moveDown(board)) {
                    generateOneNumber();
                    isgameover();
                }
                break;
            default:
                break;
        }
    }
});

function moveLeft(board) {
    if (!canMoveLeft(board))
        return false;

    for (var i = 0; i < row; i++)
        for (var j = 1; j < clo; j++) {
            if (board[i][j] != 0) {
                for (var k = 0; k < j; k++) {
                    if (board[i][k] == 0 && noBlockHorizontal(i, k, j, board)) {
                        // move
                        showMoveAnimation(i, j, i, k);//从[i,j]到[i,k]显示移动动画
                        board[i][k] = board[i][j];
                        board[i][j] = 0;
                        continue;
                    } else if (board[i][k] == board[i][j] && noBlockHorizontal(i, k, j, board) && !hasConficted[i][k]) {
                        // move  add
                        showMoveAnimation(i, j, i, k);
                        board[i][k] += board[i][j];
                        board[i][j] = 0;
                        score += board[i][k];
                        updateScore(score);
                        AutoSubmit();
                        hasConficted[i][k] = true;
                        continue;
                    }
                }
            }
        }
    setTimeout("updateBoardView()", 200);
    return true;
}

function moveRight(board) {
    if (!canMoveRight(board))
        return false;

    for (var i = 0; i < row; i++)
        for (var j = clo-2; j >= 0; j--) {
            if (board[i][j] != 0) {
                for (var k = clo-1; k > j; k--) {
                    if (board[i][k] == 0 && noBlockHorizontal(i, k, j, board)) {
                        // move
                        showMoveAnimation(i, j, i, k);//从[i,j]到[i,k]显示移动动画
                        board[i][k] = board[i][j];
                        board[i][j] = 0;
                        continue;
                    } else if (board[i][k] == board[i][j] && noBlockHorizontal(i, k, j, board) && !hasConficted[i][k]) {
                        // move  add
                        showMoveAnimation(i, j, i, k);
                        board[i][k] += board[i][j];
                        board[i][j] = 0;
                        score += board[i][k];
                        updateScore(score);
                        AutoSubmit();
                        hasConficted[i][k] = true;
                        continue;
                    }
                }
            }
        }
    setTimeout("updateBoardView()", 200);
    return true;
}

function moveUp(board) {
    if (!canMoveUp(board))
        return false;

    for (var j = 0; j < clo; j++)
        for (var i = 1; i < row; i++) {
            if (board[i][j] != 0) {
                for (var k = 0; k < i; k++) {
                    if (board[k][j] == 0 && noBlockVertical(j, i, k, board)) {
                        // move
                        showMoveAnimation(i, j, k, j);//从[i,j]到[k,j]显示移动动画
                        board[k][j] = board[i][j];
                        board[i][j] = 0;
                        continue;
                    } else if (board[k][j] == board[i][j] && noBlockVertical(j, i, k, board) && !hasConficted[k][j]) {
                        // move  add
                        showMoveAnimation(i, j, k, j);
                        board[k][j] += board[i][j];
                        board[i][j] = 0;
                        score += board[k][j];
                        updateScore(score);
                        AutoSubmit();
                        hasConficted[k][j] = true;
                        continue;
                    }
                }
            }
        }
    setTimeout("updateBoardView()", 200);
    return true;
}

function moveDown(board) {
    if (!canMoveDown(board))
        return false;

    for (var j = 0; j < clo; j++)
        for (var i = row-2; i >= 0; i--) {
            if (board[i][j] != 0) {
                for (var k = row-1; k > i; k--) {
                    if (board[k][j] == 0 && noBlockHorizontal(j, i, k, board)) {
                        // move
                        showMoveAnimation(i, j, k, j);//从[i,j]到[k,j]显示移动动画
                        board[k][j] = board[i][j];
                        board[i][j] = 0;
                        continue;
                    } else if (board[k][j] == board[i][j] && noBlockHorizontal(j, i, k, board) && !hasConficted[k][j]) {
                        // move  add
                        showMoveAnimation(i, j, k, j);
                        board[k][j] += board[i][j];
                        board[i][j] = 0;
                        score += board[k][j];
                        updateScore(score);
                        AutoSubmit();
                        hasConficted[k][j] = true;
                        continue;
                    }
                }
            }
        }
    setTimeout("updateBoardView()", 200);
    return true;
}

function isgameover() {
    if (nospace(board) && nomove(board)) return true;
    else return false;
}
